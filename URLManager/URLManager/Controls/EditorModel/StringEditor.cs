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
    class StringEditor : BaseEditor
    {
        public StringEditor()
        {
            this.Style = FindResource("StringEditorStyle") as Style;
        }
        public static DependencyProperty TextProperty = Register();
        public static DependencyProperty LabelProperty = Register();

        public string Text
        {
            get { return (string)GetValue(TextProperty); }
            set { SetValue(TextProperty, value); }
        }
        public string Label
        {
            get { return (string)GetValue(LabelProperty); }
            set { SetValue(LabelProperty, value); }
        }
    }
}
