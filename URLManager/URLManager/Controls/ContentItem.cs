using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using URLManager.Core.Executor.Base;

namespace URLManager.Controls
{
    class ContentItem : Button
    {
        public ContentItem()
        {
            this.Style = FindResource("ContentItemStyle") as Style;
        }

        public static DependencyProperty SourceProperty =
            DependencyProperty.Register(nameof(Source), typeof(ImageSource), typeof(ContentItem)); //DependencyHelper.Register();

        public ImageSource Source
        {
            get { return (ImageSource)GetValue(SourceProperty); }
            set { SetValue(SourceProperty, value); }
        }
        
    }
}
