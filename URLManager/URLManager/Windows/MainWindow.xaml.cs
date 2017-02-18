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
using System.Reflection;
using System.Linq;
using System.Collections;
using System.Text.RegularExpressions;

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


            ////////
            //w.MessageBox.Show(string.Join("\r\n", GetResourceNames("resources/CategoryIcon")));
            ////////

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


            ItemChange(null, null);


            //CategoryManager.Category.Add(new CategoryData()
            //{
            //    URLs = new List<URLExecutor>() { new URLExecutor("www.ticketmonster.co.kr","티몬") ,
            //                                     new URLExecutor("http://www.wemakeprice.com/","위메프"),
            //                                     new URLExecutor("http://www.coupang.com/", "쿠팡"),
            //                                     new URLExecutor("http://www.interpark.com/","인터파크"),
            //                                     new URLExecutor("http://www.enuri.com/","에누리")},
            //    //ProgramFiles = new List<ProgramExecutor>() { new ProgramExecutor(@"C:\Users\uutak\Desktop\asdf.asd","asdf.asd") },
            //    //FolderFiles = new List<FolderExecutor>() { new FolderExecutor(@"C:\Users\uutak\Desktop\", "바탕화면")}
            //    //,
            //    CategoryName = "기본 포털 사이트",
            //    Icon = GetResourcesIcon("CloudIcon.png"),
            //    IsEnabled = true
            //});

            CategoryManager.Category.Add(new CategoryData()
            {
                FolderFiles = new List<FolderExecutor>() { new FolderExecutor(@"F:\영화\[Leopard-Raws] Sword Art Online II (TVS 1280x720 x264 AAC)","소드 아트 온라인 2기") ,
                                                    new FolderExecutor(@"F:\영화\노 게임 노 라이프","노 게임 노 라이프"),
                                                    new FolderExecutor(@"F:\영화\재와 환상의 그림갈", "재와 환상의 그림갈")},
                CategoryName = "애니메이션들",
                Icon = GetResourcesIcon("slow_motion_video.png"),
                IsEnabled = true
            });


            tmr = new DispatcherTimer();

            tmr.Interval = TimeSpan.FromMilliseconds(100);

            tmr.Tick += tmrtick;

            tmr.Start();

            LocalStoargy ls = new LocalStoargy();
            
            CategoryData[] sd;

            ls.Load(out sd);

            this.Opacity = 0.0;
            //MessageBox.Show(ls.GetValue("A", "A", "A", "A").Value);



            f.ContextMenu menu = new f.ContextMenu();

            f.MenuItem itm1 = new f.MenuItem();
            f.MenuItem itm2 = new f.MenuItem();
            f.MenuItem itm3 = new f.MenuItem();
            menu.MenuItems.Add(itm1);
            menu.MenuItems.Add(itm2);
            menu.MenuItems.Add(itm3);

            itm1.Index = 0;
            itm1.Text = "프로그램 종료";
            itm1.Click += delegate (object s, EventArgs e)
            {
                this.Close();
            };

            itm2.Index = 0;
            itm2.Text = "설정";
            itm2.Click += delegate (object s, EventArgs e)
            {
                this.Close();
            };

            itm3.Index = 0;
            itm3.Text = "카테고리 추가";
            itm3.Click += delegate (object s, EventArgs e)
            {

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

        private void BtnAdd_Click(object sender, w.RoutedEventArgs e)
        {
            AddCategory ac = new AddCategory();

            ac.ShowDialog();
        }

        private void BtnEnd_Click(object sender, w.RoutedEventArgs e)
        {
            Environment.Exit(0);
        }

        private void BtnSetting_Click(object sender, w.RoutedEventArgs e)
        {

        }
    }
}
