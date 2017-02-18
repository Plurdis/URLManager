using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using URLManager.Controls;
using URLManager.Core.Executor;
using URLManager.Core.Executor.Base;
using c = System.Windows.Controls;
using static URLManager.Global.Globals;
using URLManager.Data;

namespace URLManager.Windows
{
    /// <summary>
    /// LinkSelectWindow.xaml에 대한 상호 작용 논리
    /// </summary>
    public partial class LinkSelectWindow : Window
    {

        ContextMenu cm = new ContextMenu();
        bool IgnoreClose = false;
        private string CategoryName;
        private CategoryData cd;


        public LinkSelectWindow(string CategoryName, CategoryData data)
        {
            InitializeComponent();

            cd = data;

            UpdateItem();

            MenuItem itm1, itm2, itm3;

            itm1 = new MenuItem() { Header = "아이템 삭제" };
            itm2 = new MenuItem() { Header = "속성 변경" };
            itm3 = new MenuItem() { Header = "이름 변경" };

            cm.Items.Add(itm1);
            cm.Items.Add(itm2);
            cm.Items.Add(itm3);

            itm1.Click += RemoveItem;
            itm2.Click += ChangeProperty;
            itm3.Click += ChangeName;


            this.Title = "링크 목록 - " + CategoryName;
            this.CategoryName = CategoryName;

            this.Deactivated += this_Deactivated;
            this.KeyDown += CloseEvent;

            this.Activate();
            this.Topmost = true;
        }


        private void ChangeName(object sender, RoutedEventArgs e)
        {
            
        }

        private void ChangeProperty(object sender, RoutedEventArgs e)
        {
            
        }

        private void RemoveItem(object sender, RoutedEventArgs e)
        {
            IgnoreClose = true;
            if (MessageBox.Show($"'{((Button)cm.Tag).Content}' 아이템을 삭제합니다.\n정말 계속하시겠습니까?", "삭제 여부 확인", MessageBoxButton.YesNo) == MessageBoxResult.Yes)
            {
                cd.Remove((BaseExecutor)((Button)cm.Tag).Tag);
                UpdateItem();
            }


            IgnoreClose = false;
        }

        public void UpdateItem()
        {
            ItemPanel.Children.Clear();

            
            foreach (var itm in cd.LocalFiles)
            {
                ContentItem ci = new ContentItem();

                ci.Click += ItemClick;
                ci.MouseRightButtonDown += ItemRightDown;

                ci.Content = itm.Name;
                ci.Tag = itm;
                ci.Source = itm.Icon;
                ItemPanel.Children.Add(ci);
            }
            foreach (var itm in cd.ProgramFiles)
            {
                ContentItem ci = new ContentItem();

                ci.Click += ItemClick;
                ci.MouseRightButtonDown += ItemRightDown;

                ci.Content = itm.Name;
                ci.Tag = itm;
                ci.Source = itm.Icon;
                ItemPanel.Children.Add(ci);
            }
            foreach (var itm in cd.URLs)
            {
                ContentItem ci = new ContentItem();

                ci.Click += ItemClick;
                ci.MouseRightButtonDown += ItemRightDown;

                ci.Content = itm.Name;
                ci.Tag = itm;
                ci.Source = itm.Icon;
                ItemPanel.Children.Add(ci);
            }
            foreach (var itm in cd.FolderFiles)
            {
                ContentItem ci = new ContentItem();
                ci.Click += ItemClick;
                ci.MouseRightButtonDown += ItemRightDown;

                ci.Content = itm.Name;
                ci.Tag = itm;
                ci.Source = itm.Icon;
                ItemPanel.Children.Add(ci);
            }
            

                
            if (ItemPanel.Children.Count == 0)
                TBAddInfo.Visibility = Visibility.Visible;
            else
                TBAddInfo.Visibility = Visibility.Hidden;


        }


        private void ItemRightDown(object sender, MouseButtonEventArgs e)
        {
            cm.Tag = sender;

            ((MenuItem)cm.Items[2]).Header = "'" + ((Button)sender).Content + "' 이름 변경";

            cm.IsOpen = true;
        }

        private void ItemClick(object sender, RoutedEventArgs e)
        {
            var itm = (ContentItem)sender;
            var Executor = (BaseExecutor)itm.Tag;

            if (Executor.CanExecute) Executor.Execute();
            else MessageBox.Show("실행 할 수 없는 상태입니다.");

            try
            {
                lsWdws.Pop();
            }
            catch (Exception)
            { }

            this.Hide();
        }

        private void this_Deactivated(object sender, EventArgs e)
        {
            if (IgnoreClose) return;
            try
            {
                lsWdws.Pop();
            }
            catch (Exception)
            { }
            
            this.Hide();
        }
        
        private void DragRect_MouseDown(object sender, MouseButtonEventArgs e)
        {
            this.DragMove();
        }

        private void btnItmAdd_Click(object sender, RoutedEventArgs e)
        {
            IgnoreClose = true;

            AddItem ai = new AddItem(cd);

            ai.ShowDialog();
            UpdateItem();

            IgnoreClose = false;
        }

        bool DeleteMode = false;

        private void btnItmRemove_Click(object sender, RoutedEventArgs e)
        {
            DeleteMode = !DeleteMode;

            if (DeleteMode)
            {
                sv.Foreground = Brushes.Red;
            }
        }

        private void CloseEvent(object sender, RoutedEventArgs e)
        {
            if (e.GetType() == typeof(KeyEventArgs))
            {
                if (((KeyEventArgs)e).Key == Key.Escape) this.Close();
            }
            else this.Close();
        }
    }
}
