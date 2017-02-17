using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Threading;
using URLManager.Input.Keyboard.HotKey.Base;
using input = System.Windows.Input;

namespace URLManager.Input.Keyboard.HotKey
{
    public delegate void HotKeyEvent(BaseHotKey sender, HotKeyEventArgs e);
    public static class HotKeyManager
    {
        public static event HotKeyEvent HotKeyPressed;
        private static List<BaseHotKey> hkList;
        private static Dictionary<string, string> holdDict;

        private static void OnHotKeyPressed(BaseHotKey sender, HotKeyEventArgs e)
        {
            if (HotKeyPressed != null) HotKeyPressed(sender, e);
        }

        static HotKeyManager()
        {
            Init();
        }

        public static void Add(BaseHotKey hotkey)
        {
            hkList.Add(hotkey);
        }

        public static void Remove(BaseHotKey hotkey)
        {
            hkList.Remove(hotkey);
        }

        static void Init()
        {
            hkList = new List<BaseHotKey>();
            holdDict = new Dictionary<string, string>();

            dt = new DispatcherTimer();

            dt.Interval = TimeSpan.FromMilliseconds(10);
            dt.Tick += TimerTick;

            dt.Start();
        }

        static int TimeCount = 0;

        private static input.Key LastKey = input.Key.None;
        private static bool Ignoring = false;

        private static void TimerTick(object sender, EventArgs e)
        {
            foreach (BaseHotKey hk in hkList.AvailableKeys())
            {
                switch (hk.GetType().Name)
                {
                    case "GeneralHotKey":
                        GeneralHotKey ghk = (GeneralHotKey)hk;

                        if (ghk.HasModifierKey && !ghk.IncludeKeyNone)
                        {
                            if (!input.Keyboard.IsKeyDown(ghk.ModifierKey)) continue;

                            bool Flag = true;

                            foreach(input.Key k in ghk.SubKeys)
                            {
                                if (!input.Keyboard.IsKeyDown(k)) Flag = false;
                            }

                            if (!Flag) continue;

                            OnHotKeyPressed(hk, new HotKeyEventArgs() { HotKeyType = typeof(GeneralHotKey) });
                            hk.OnPressed(hk, new HotKeyEventArgs() { HotKeyType = typeof(GeneralHotKey) });
                        }
                        break;
                    case "DoubleHotKey":
                        DoubleHotKey dhk = (DoubleHotKey)hk;

                        if (dhk.FirstKey != input.Key.None && dhk.SecondKey != input.Key.None)
                        {
                            

                            if (LastKey == dhk.FirstKey && input.Keyboard.IsKeyDown(dhk.SecondKey) && LastKey != input.Key.None && !dhk.Pressing)
                            {
                                LastKey = input.Key.None;

                                OnHotKeyPressed(hk, new HotKeyEventArgs() { HotKeyType = typeof(DoubleHotKey) });
                                hk.OnPressed(hk, new HotKeyEventArgs() { HotKeyType = typeof(DoubleHotKey) });

                                Ignoring = true;
                            }
                            else if (input.Keyboard.IsKeyUp(dhk.FirstKey))
                            {
                                dhk.Pressing = false;
                            }
                            else if (input.Keyboard.IsKeyDown(dhk.FirstKey) && !dhk.Pressing)
                            {
                                dhk.Pressing = true;
                                if (!Ignoring) LastKey = dhk.FirstKey;
                                else if (Ignoring) Ignoring = false;
                                TimeCount = 0;   
                            }
                        }
                        break;
                }
            }


            TimeCount += dt.Interval.Milliseconds;

            if (TimeCount >= 500)
            {
                LastKey = input.Key.None;
            }

        }

        private static DispatcherTimer dt;


        public static BaseHotKey[] AvailableKeys(this BaseHotKey[] hkArray)
        {
            List<BaseHotKey> hkList = new List<BaseHotKey>();

            foreach (BaseHotKey k in hkArray)
            {
                if (k.Enabled) hkList.Add(k);
            }

            return hkList.ToArray();
        }
        public static List<BaseHotKey> AvailableKeys(this List<BaseHotKey> hkArray)
        {
            List<BaseHotKey> hkList = new List<BaseHotKey>();

            foreach (BaseHotKey k in hkArray)
            {
                if (k.Enabled) hkList.Add(k);
            }

            return hkList;
        }
    }
}
