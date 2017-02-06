using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using static WPFExtension.DependencyHelper;
using URLManager.Controls.SetterEditor.Base;

namespace URLManager.Controls
{
    class SettingEditor : Control
    {
        public SettingEditor()
        {
            this.Style = FindResource("SettingEditorStyle") as Style;
        }

        public static DependencyProperty EditorsProperty = Register();
        public static DependencyProperty SettingNameProperty = Register();

        public List<BaseEditor> Editors
        {
            get { return (List<BaseEditor>)GetValue(EditorsProperty); }
            set { SetValue(EditorsProperty, value); }
        }

        public string SettingName
        {
            get { return (string)GetValue(SettingNameProperty); }
            set { SetValue(SettingNameProperty, value); }
        }
    }
}
