using System.Globalization;

namespace MyShop.Core.Helpers;

public class VietnamesePriceConverter
{
    public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
    {
        var price = (int)value;
        CultureInfo cul = CultureInfo.GetCultureInfo("vi-VN");

        return price.ToString("#,### đ", cul.NumberFormat);
    }

    public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
    {
        throw new NotImplementedException();
    }
}
