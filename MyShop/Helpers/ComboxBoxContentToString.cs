using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Data;

namespace MyShop.Helpers;
internal class ComboxBoxContentToString : IValueConverter
{
    public object Convert(object value, Type targetType, object parameter, string language)
    {
        return null;    
    }
    public object ConvertBack(object value, Type targetType, object parameter, string language)
    {
        if (value is ComboBoxItem item)
        {
            return item.Content.ToString();
        }
        return "customer";
    }
}
