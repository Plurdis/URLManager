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
using URLManager.Data;
using static URLManager.Global.Globals;

namespace URLManager.Windows
{
    /// <summary>
    /// AddCategory.xaml에 대한 상호 작용 논리
    /// </summary>
    public partial class AddCategory : Window
    {
        public AddCategory()
        {
            InitializeComponent();

            foreach(string path in GetResourceNames("resources/CategoryIcon"))
            {
                BitmapImage bmpimg = new BitmapImage(new Uri(path, UriKind.RelativeOrAbsolute));

                Image img = new Image();

                img.Width = 40;
                img.Source = bmpimg;

                IconsListView.Items.Add(img);
            }
        }
        
        private void BtnAdd_Click(object sender, RoutedEventArgs e)
        {
            CategoryManager.Add(new CategoryData() { CategoryName = TBCategoryName.Text, Icon = ((Image)IconsListView.SelectedItem).Source });
            this.Close();
        }

        private void BtnCancel_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void IconsListView_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            CheckEnable();
        }

        private void TBCategoryName_TextChanged(object sender, TextChangedEventArgs e)
        {
            CheckEnable();
        }

        public void CheckEnable()
        {
            if (string.IsNullOrEmpty(TBCategoryName.Text) || IconsListView.SelectedIndex == -1)
            {
                BtnAdd.IsEnabled = false;
            }
            else
            {
                BtnAdd.IsEnabled = true;
            }
        }
    }
}
