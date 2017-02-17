using System;
using System.Threading;
using w = System.Windows;
using f = System.Windows.Forms;
using System.Windows.Input;
using System.Windows.Media.Animation;
using System.Windows.Threading;
using URLManager.Input.Keyboard.HotKey;
using URLManager.Input.Keyboard.HotKey.Base;
using URLManager.Windows;
using static URLManager.Global.Globals;
using System.Windows.Media;
using URLManager.Stoargy;
using URLManager.Stoargy.Data;
using URLManager.Core.Executor.Base;
using URLManager.Core.Executor;
using System.Collections.Generic;
using URLManager.Controls;
using URLManager.Core.Collection;
using System.Windows.Controls;
using System.Drawing;
using System.Net;
using System.IO;
using URLManager.Serialization;
using URLManager.Data;

namespace URLManager
{
    /// <summary>
    /// MainWindow.xaml에 대한 상호 작용 논리
    /// </summary>
    public partial class MainWindow : w.Window
    {
        
        f.NotifyIcon notify;


        public MainWindow()
        {
            InitializeComponent();

            //Serializer sr = new Serializer(new CategoryData()
            //{
            //    URLs = new List<URLExecutor>() { new URLExecutor("www.naver.com","NAVER") ,
            //                                     new URLExecutor("www.daum.net","DAUM")
            //                                     }
            //});



            //Deserializer dsr = new Deserializer(sr.SaveToFile(@"C:\Users\uutak\Desktop\asdf.asd"));



            //Deserializer dsr = new Deserializer(sr.Base64StringOutput());

            //w.MessageBox.Show(((CategoryData)dsr.OutputData()).URLs.Count.ToString());

            //w.MessageBox.Show(sr.Base64StringOutput());

        
            
            this.Topmost = true;

            GeneralHotKey hk = new GeneralHotKey(Key.LeftCtrl, new Key[] { Key.K });
            GeneralHotKey EscapeHk = new GeneralHotKey(Key.Escape);
            DoubleHotKey dk = new DoubleHotKey(Key.LeftShift, Key.LeftShift);
            
            HotKeyManager.Add(dk);
            HotKeyManager.Add(hk);
            
            dk.Pressed += doubleShiftPressed;
            this.Deactivated += This_Deactivated;
            this.KeyDown += This_KeyDown;

            CategoryManager.Category.ListChanged += ItemChange;

            var itm = new CategoryItem() { Content = "클라우드 Start", Source = GetResourcesIcon("FindIcon.png") };

            itm.Click += CategoryClick;


            CategoryManager.Category.Add(new CategoryData()
            {
                URLs = new List<URLExecutor>() { new URLExecutor("www.naver.com","NAVER") ,
                                                 new URLExecutor("www.daum.net","DAUM")},
                ProgramFiles = new List<ProgramExecutor>() { new ProgramExecutor(@"C:\Users\uutak\Desktop\asdf.asd","asdf.asd") }
                ,
                CategoryName = "기본 포털 사이트",
                Icon = GetResourcesIcon("CloudIcon.png"),
                IsEnabled = true
            });


            tmr = new DispatcherTimer();

            tmr.Interval = TimeSpan.FromMilliseconds(100);

            tmr.Tick += tmrtick;

            tmr.Start();

            LocalStoargy ls = new LocalStoargy();

            //ls.Save(, false);

            StoargyData sd;

            ls.Load(out sd);

            this.Opacity = 0.0;
            //MessageBox.Show(ls.GetValue("A", "A", "A", "A").Value);



            f.ContextMenu menu = new f.ContextMenu();

            f.MenuItem itm1 = new f.MenuItem();
            f.MenuItem itm2 = new f.MenuItem();
            menu.MenuItems.Add(itm1);
            menu.MenuItems.Add(itm2);

            itm1.Index = 0;
            itm1.Text = "프로그램 종료";
            itm1.Click += delegate (object click, EventArgs e)
            {
                this.Close();
            };

            itm2.Index = 0;
            itm2.Text = "내부 설정";
            itm2.Click += delegate (object click, EventArgs e)
            {
                this.Close();
            };


            notify = new f.NotifyIcon();
            notify.ContextMenu = menu;
            notify.Text = "URL Manager 동작 중";
            notify.Visible = true;
            notify.Icon = Properties.Resources.ShortCutIcon2;

            notify.BalloonTipTitle = "URL Manager 실행 알림";
            notify.BalloonTipText = "URL Manager가 실행되었습니다.";
            notify.ShowBalloonTip(1000);
        }

        private void CategoryClick(object sender, w.RoutedEventArgs e)
        {
            var itm = (CategoryItem)sender;
            //w.MessageBox.Show(itm.Content.ToString());

            var ct = (CategoryData)itm.Tag;

            CloseAllWindows();
            this.Hide();

            LinkSelectWindow lsw = new LinkSelectWindow(itm.Content.ToString(),ct);
            lsw.Show();

            lsWdws.Push(lsw);


        }

        private void ItemChange(object sender, NotifyListEventArgs ev)
        {
            if (CategoryManager.Category.Count <= 5)
            {
                LeftBtn.Visibility = w.Visibility.Hidden;
                RightBtn.Visibility = w.Visibility.Hidden;
            }
            else
            {
                LeftBtn.Visibility = w.Visibility.Visible;
                RightBtn.Visibility = w.Visibility.Visible;
            }
            ItemsGrid.Children.Clear();
            int index = 0;
            foreach (var ct in CategoryManager.Category)
            {
                var itm = new CategoryItem();
                itm.Content = ct.CategoryName;
                itm.Source = ct.Icon;
                itm.Click += CategoryClick;
                itm.Tag = ct;

                Grid.SetColumn(itm,index);
                ItemsGrid.Children.Add(itm);

                index += 2;
            }
        }

        private void This_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Escape)
            {
                s = false;

                WindowFadeInOut(false);
                Thread thr = new Thread(() =>
                {
                    Thread.Sleep(500);
                    Dispatcher.Invoke(new Action(() =>
                    {
                        this.Hide();
                    }));
                });

                thr.Start();
            }
        }

        private void tmrtick(object sender, EventArgs e)
        {
            //if (this.ShowActivated)
        }

        DispatcherTimer tmr;
        bool s = false;


        private void doubleShiftPressed(BaseHotKey sender, HotKeyEventArgs e)
        {
            if (s)
            {
                s = false;

                WindowFadeInOut(false);
                Thread thr = new Thread(() =>
                {
                    Thread.Sleep(500);
                    Dispatcher.Invoke(new Action(() =>
                    {
                        this.Hide();
                    }));
                });

                thr.Start();
                

            }
            else
            {
                if (CloseAllWindows()) return;
                s = true;
                this.Opacity = 0.0;
                WindowFadeInOut(true);

                SetLocation();
                this.Show();
                this.Activate();
            }
        }

        public void WindowFadeInOut(bool FadeIn)
        {
            DoubleAnimation da;
            if (FadeIn)
            {
                da = new DoubleAnimation(0, 1.0, new w.Duration(TimeSpan.FromMilliseconds(500)));
            }
            else
            {
                da = new DoubleAnimation(1.0, 0, new w.Duration(TimeSpan.FromMilliseconds(500)));
            }

            this.BeginAnimation(OpacityProperty, da);
        }


        private void This_Deactivated(object sender, EventArgs e)
        {
            this.Hide();
            s = false;
        }
        

        private bool CloseAllWindows()
        {
            if (lsWdws.Count != 0)
            {
                while (lsWdws.Count != 0)
                {
                    var wdw = lsWdws.Pop();

                    wdw.Close();
                }
                return true;
            }

            return false;
        }


        public void SetLocation()
        {
            var transform = w.PresentationSource.FromVisual(this).CompositionTarget.TransformFromDevice;
            var mouse = transform.Transform(GetMousePosition());
            foreach (f.Screen scr in f.Screen.AllScreens)
            {
                //mouse.X < scr.Bounds.Location.X && mouse.X > scr.Bounds.Location.X + scr.Bounds.Width
                if (mouse.X > scr.Bounds.Left && mouse.X < scr.Bounds.Left + scr.Bounds.Width)
                {
                    if (mouse.Y > scr.Bounds.Top && mouse.Y < scr.Bounds.Top + scr.Bounds.Height)
                    {
                        this.Top = scr.Bounds.Height - this.Height - 200;
                        this.Left = scr.Bounds.Left + (scr.Bounds.Width / 2) - this.Width / 2;
                    }

                    break;
                }
            }
        }
        public w.Point GetMousePosition()
        {
            Point point = f.Control.MousePosition;
            return new w.Point(point.X, point.Y);
        }

    }
}
