using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Forms;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using URLManager.Data;
using URLManager.FileHelper;
using static URLManager.Core.Extension.EnumEx;

namespace URLManager.Windows
{
    /// <summary>
    /// AddItem.xaml에 대한 상호 작용 논리
    /// </summary>
    public partial class AddItem : Window
    {

        CategoryData cd;

        public AddItem(CategoryData data)
        {
            InitializeComponent();

            cd = data;
            this.Activate();
            this.Topmost = true;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog ofd = new OpenFileDialog();
            ofd.Filter = "모든 파일 (*.*)|*.*";

            if (ofd.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                TBFileLocation.Text = ofd.FileName;
            }
            CheckEnable();
        }

        private void TBFileLocation_TextChanged(object sender, TextChangedEventArgs e)
        {
            bool Flag = false;
            
            Flag = string.IsNullOrEmpty(TBFileLocation.Text) || !File.Exists(TBFileLocation.Text);

            TBFileName.Text = null;

            if (Flag)
            {
                TBFileName.IsEnabled = false;
                CBUseOriginName.IsEnabled = false;
                TBFileType.Visibility = Visibility.Hidden;
            }
            else
            {
                TBFileName.IsEnabled = true;
                CBUseOriginName.IsEnabled = true;
                TBFileType.Visibility = Visibility.Visible;

                var fi = new FileInfo(TBFileLocation.Text);
                try
                { RunFileType.Text = ((Enum)Enum.Parse(typeof(FileExtensions), fi.Extension.Substring(1).ToUpper())).GetDescription(); }
                catch (Exception)
                { RunFileType.Text = "기타"; }
                RunExtensionType.Text = fi.Extension;

                if ((bool)CBUseOriginName.IsChecked) TBFileName.Text = fi.Name;
            }

            CheckEnable();
        }

        private void CBUseOriginName_Checked(object sender, RoutedEventArgs e)
        {
            if ((bool)CBUseOriginName.IsChecked)
            {
                TBFileName.IsEnabled = false;
                TBFileName.Text = new FileInfo(TBFileLocation.Text).Name;
            }
            else
            {
                TBFileName.IsEnabled = true;
                TBFileName.Text = null;
            }
        }

        private void BtnCancel_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void BtnOK1_Click(object sender, RoutedEventArgs e)
        {
            cd.LocalFiles.Add(new Core.Executor.LocalFileExecutor(TBFileLocation.Text, TBFileName.Text));
            this.Close();
        }

        private void TBFileName_TextChanged(object sender, TextChangedEventArgs e)
        {
            CheckEnable();
        }

        public void CheckEnable()
        {
            BtnOK1.IsEnabled = TBFileName.IsEnabled || File.Exists(TBFileLocation.Text);
            BtnOK1.IsEnabled = !string.IsNullOrEmpty(TBFileName.Text);
        }
    }
}
