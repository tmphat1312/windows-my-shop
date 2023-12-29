using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MyShop.Core.Models;

namespace MyShop.Core.Contracts.Repository;
public interface IStatisticRepository
{
    public Task<(int, string, int)> CountSellingBooksAsync(string query);
    public Task<(int, string, int)> CountBooksAsync();

    public Task<(int, string, int)> CountNewOrdersAsync(string query);

    public Task<(IEnumerable<Revenue_Profit>,string,int)> GetRevenue_ProfitAsync(string query);
    public Task<(IEnumerable<BookSaleStat>,string,int)> GetBookSaleStatAsync(string query);
}
