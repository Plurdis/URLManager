using System;
using System.Collections.Generic;
using System.Diagnostics;
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
using System.Windows.Navigation;
using System.Windows.Shapes;
using URLManager.Core.Executor;
using URLManager.Core.Extension;
using URLManager.Input.Keyboard.HotKey;
using URLManager.Input.Keyboard.HotKey.Base;
using URLManager.Stoargy;

namespace URLManager
{
    /// <summary>
    /// MainWindow.xaml에 대한 상호 작용 논리
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();

            GeneralHotKey hk = new GeneralHotKey(Key.LeftCtrl, new Key[] { Key.K });
            DoubleHotKey dk = new DoubleHotKey(Key.LeftShift, Key.LeftShift);

            HotKeyManager.Add(dk);
            HotKeyManager.Add(hk);
            
            HotKeyManager.HotKeyPressed += KeyPressed;

            LocalStoargy ls = new LocalStoargy();

            ls.SetValue("A", "A", "A", "A", "E");
            ls.SetValue("A", "A", "A", "A", "D");
            ls.SetValue("A", "A", "A", "A", "F");

            

            //MessageBox.Show(ls.GetValue("A", "A", "A", "A").Value);


        }

        private void KeyPressed(BaseHotKey sender, HotKeyEventArgs e)
        {
            MessageBox.Show(e.HotKeyType.ToString());
        }
    }
}
