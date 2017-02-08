using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace URLManager.Core.Extension
{
    static class EnumEx
    {
        public static string GetDescription(this Enum enumData)
        {
            DescriptionAttribute attribute = enumData.GetType()
                        .GetField(enumData.ToString())
                        .GetCustomAttributes(typeof(DescriptionAttribute), false)
                        .SingleOrDefault() as DescriptionAttribute;
            return attribute == null ? enumData.ToString() : attribute.Description;
        }


        
    }
}
