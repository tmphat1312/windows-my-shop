using ExcelDataReader;
using MyShop.Core.Models;
using Windows.Storage.Pickers;
using WinRT.Interop;

namespace MyShop.Core.Helpers;
public class ExcelDataModelReader
{
    public static async Task<List<Category>> ReadCategoryFileAsync()
    {
        #region Open File Picker
        var picker = new FileOpenPicker
        {
            ViewMode = PickerViewMode.List,
            SuggestedStartLocation = PickerLocationId.DocumentsLibrary
        };
        picker.FileTypeFilter.Add(".xlsx");
        picker.FileTypeFilter.Add(".xls");
        picker.FileTypeFilter.Add(".csv");

        var hwnd = WindowNative.GetWindowHandle(App.MainWindow);
        InitializeWithWindow.Initialize(picker, hwnd);
        #endregion

        #region Read File
        var file = await picker.PickSingleFileAsync();

        var categories = new List<Category>();

        if (file != null)
        {
            System.Text.Encoding.RegisterProvider(System.Text.CodePagesEncodingProvider.Instance);
            using var stream = File.Open(file.Path, FileMode.Open, FileAccess.Read);
            using var reader = ExcelReaderFactory.CreateReader(stream);

            /*
            * first row: empty
            * second row: header row (name: text, description: text)
            * after that: data
            */

            reader.Read(); // empty row
            reader.Read(); // header row

            // Data
            do
            {
                while (reader.Read())
                {
                    var category = new Category
                    {
                        Name = reader.GetString(1),
                        Description = reader.GetString(2)
                    };
                    categories.Add(category);
                }
            }
            while (reader.NextResult());
        }
        #endregion

        return categories;
    }

    public static async Task<List<Book>> ReadBookFileAsync()
    {
        #region Open File Picker
        var picker = new FileOpenPicker
        {
            ViewMode = PickerViewMode.List,
            SuggestedStartLocation = PickerLocationId.DocumentsLibrary
        };
        picker.FileTypeFilter.Add(".xlsx");
        picker.FileTypeFilter.Add(".xls");
        picker.FileTypeFilter.Add(".csv");

        var hwnd = WindowNative.GetWindowHandle(App.MainWindow);
        InitializeWithWindow.Initialize(picker, hwnd);
        #endregion

        #region Read File
        var file = await picker.PickSingleFileAsync();

        var books = new List<Book>();

        if (file != null)
        {
            System.Text.Encoding.RegisterProvider(System.Text.CodePagesEncodingProvider.Instance);
            using var stream = File.Open(file.Path, FileMode.Open, FileAccess.Read);
            using var reader = ExcelReaderFactory.CreateReader(stream);

            /*
            * first row: empty
            * second row: header row (name: text, description: text, purchasePrice: double, sellingPrice: double, author: text, publishedYear: int, ratingsAverage: double, quantity: int)
            * after that: data
            */

            reader.Read(); // empty row
            reader.Read(); // header row

            // Data
            do
            {
                while (reader.Read())
                {
                    var book = new Book
                    {
                        Name = reader.GetString(1),
                        Description = reader.GetString(2),
                        PurchasePrice = reader.GetDouble(3),
                        SellingPrice = reader.GetDouble(4),
                        Author = reader.GetString(5),
                        PublishedYear = (int)reader.GetDouble(6),
                        RatingsAverage = reader.GetDouble(7),
                        Quantity = (int)reader.GetDouble(8)
                    };

                    books.Add(book);
                }
            }
            while (reader.NextResult());
        }
        #endregion

        return books;
    }
}
