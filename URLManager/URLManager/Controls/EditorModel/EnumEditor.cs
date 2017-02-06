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
    class EnumEditor : BaseEditor
    {
        public EnumEditor()
        {
            this.Style = FindResource("EnumStringEditorStyle") as Style;
        }
        public static DependencyProperty LabelProperty = Register();
        public static DependencyProperty ListProperty = Register();

        public string Label
        {
            get { return (string)GetValue(LabelProperty); }
            set { SetValue(LabelProperty, value); }
        }

        public List<Enum> List
        {
            get { return (List<Enum>)GetValue(ListProperty); }
            set { SetValue(ListProperty, value); }
        }
    }
}
