using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using WPFExtension;

namespace URLManager.Controls
{
    class CategoryItem : Button
    {
        public CategoryItem()
        {
            this.Style = FindResource("CategoryItemStyle") as Style;
        }
        
        public static DependencyProperty SourceProperty =
            DependencyProperty.Register(nameof(Source), typeof(ImageSource), typeof(CategoryItem)); //DependencyHelper.Register();

        public ImageSource Source
        {
            get { return (ImageSource)GetValue(SourceProperty); }
            set { SetValue(SourceProperty, value); }
        }
        

    }
}
