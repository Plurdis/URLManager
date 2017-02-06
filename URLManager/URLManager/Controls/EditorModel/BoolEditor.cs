using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using URLManager.Controls.SetterEditor.Base;
using static WPFExtension.DependencyHelper;

namespace URLManager.Controls.SetterEditor
{
    class BoolEditor : BaseEditor
    {
        public BoolEditor()
        {
            this.Style = FindResource("BoolEditorStyle") as Style;
        }

        public static DependencyProperty LabelProperty = Register();
        public static DependencyProperty IsCheckedProperty = Register();

        public string Label
        {
            get { return (string)GetValue(LabelProperty); }
            set { SetValue(LabelProperty, value); }
        }

        public bool IsChecked
        {
            get { return (bool)GetValue(IsCheckedProperty); }
            set { SetValue(IsCheckedProperty, value); }
        }
    }
}
