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
using static URLManager.Global.Globals;

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

        private void BtnSetFolder_Click(object sender, RoutedEventArgs e)
        {
            FolderBrowserDialog fbd = new FolderBrowserDialog();

            if (fbd.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                TBFolderLocation.Text = fbd.SelectedPath;
            }
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

        private void TBFolderLocation_TextChanged(object sender, TextChangedEventArgs e)
        {
            bool Flag = false;

            Flag = string.IsNullOrEmpty(TBFolderLocation.Text) || !Directory.Exists(TBFolderLocation.Text);

            TBFolderName.Text = null;

            if (Flag)
            {
                TBFolderExists.Visibility = Visibility.Visible;
                CBUseFolderName.IsEnabled = false;
                TBFolderName.IsEnabled = false;
            }
            else
            {
                TBFolderExists.Visibility = Visibility.Hidden;
                CBUseFolderName.IsEnabled = true;
                TBFolderName.IsEnabled = true;
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
        private void CBUseFolderName_Checked(object sender, RoutedEventArgs e)
        {
            if ((bool)CBUseFolderName.IsChecked)
            {
                TBFolderName.IsEnabled = false;
                TBFolderName.Text = new FileInfo(TBFolderLocation.Text).Name;
            }
            else
            {
                TBFolderName.IsEnabled = true;
                TBFolderName.Text = null;
            }
        }

        private void BtnCancel_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void BtnOK1_Click(object sender, RoutedEventArgs e)
        {
            var fi = new FileInfo(TBFileLocation.Text);
            if ((FileExtensions)Enum.Parse(typeof(FileExtensions), fi.Extension.Substring(1).ToUpper()) == FileExtensions.EXE)
            {
                cd.LocalFiles.Add(new Core.Executor.LocalFileExecutor(TBFileLocation.Text, TBFileName.Text));
            }
            else
            {
                cd.ProgramFiles.Add(new Core.Executor.ProgramExecutor(TBFileLocation.Text, TBFileName.Text));
            }
            this.Close();
        }
        private void BtnOK2_Click(object sender, RoutedEventArgs e)
        {
            cd.FolderFiles.Add(new Core.Executor.FolderExecutor(TBFolderLocation.Text, TBFolderName.Text));

            this.Close();
        }

        private void BtnOK3_Click(object sender, RoutedEventArgs e)
        {
            string urllink = GetURLLink(TBURLLoc.Text);


            Uri uriResult;
            bool result = Uri.TryCreate(urllink, UriKind.Absolute, out uriResult) && uriResult.Scheme == Uri.UriSchemeHttp;
            
            
            if (result)
            {
                cd.URLs.Add(new Core.Executor.URLExecutor(TBURLLoc.Text, TBURLName.Text));

                this.Close();
            }
            else
            {
                System.Windows.MessageBox.Show("해당 URL로 부터 정보를 받아오지 못했습니다.\n올바르지 않은 URL이거나 컴퓨터가 인터넷에 연결이 되어 있지 않을 수 있습니다.");
            }
            
        }


        private void TBFileName_TextChanged(object sender, TextChangedEventArgs e)
        {
            CheckEnable();
        }

        public void CheckEnable()
        {
            BtnOK1.IsEnabled = TBFileName.IsEnabled || File.Exists(TBFileLocation.Text);
            BtnOK1.IsEnabled = !string.IsNullOrEmpty(TBFileName.Text);

            BtnOK2.IsEnabled = TBFolderName.IsEnabled || Directory.Exists(TBFolderLocation.Text);
            BtnOK2.IsEnabled = !string.IsNullOrEmpty(TBFolderName.Text);
        }
        

        private void TBURLLoc_TextChanged(object sender, TextChangedEventArgs e)
        {
            if ((bool)CBUseURLName.IsChecked)
            {
                TBURLName.Text = TBURLLoc.Text;
            }

            if (string.IsNullOrEmpty(TBURLLoc.Text) || string.IsNullOrEmpty(TBURLName.Text))
                BtnOK3.IsEnabled = false;
            else
                BtnOK3.IsEnabled = true;
        }

        private void CBUseURLName_Checked(object sender, RoutedEventArgs e)
        {
            TBURLName.IsEnabled = !(bool)CBUseURLName.IsChecked;
        }

        private void TBURLName_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (string.IsNullOrEmpty(TBURLLoc.Text) || string.IsNullOrEmpty(TBURLName.Text))
                BtnOK3.IsEnabled = false;
            else
                BtnOK3.IsEnabled = true;
        }

        
    }
}
