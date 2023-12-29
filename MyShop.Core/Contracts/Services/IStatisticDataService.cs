using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MyShop.Core.Models;

namespace MyShop.Core.Contracts.Services;
public interface IStatisticDataService
{
    public Task<(int,string,int)> LoadDataAsync(string query);
    public (int, string, int) CountSellingBooksAsync(string query);
    public (int, string, int) CountBooksAsync();

    public (int, string, int) CountNewOrdersAsync(string query);

    public Task<(IEnumerable<Revenue_Profit>,string,int)> GetRevenue_ProfitAsync(string query);
    public Task<(IEnumerable<BookSaleStat>,string,int)> GetBookSaleStatAsync(string query);
}
