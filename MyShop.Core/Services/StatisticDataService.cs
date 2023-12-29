using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MyShop.Core.Contracts.Repository;
using MyShop.Core.Contracts.Services;
using MyShop.Core.Models;
using MyShop.Core.Repository;

namespace MyShop.Core.Services;
public class StatisticDataService : IStatisticDataService
{
    private readonly IStatisticRepository _statisticRepository;

    private (int, string, int) _countSellingBooks;
    private (int, string, int) _countBooks;
    private (int, string, int) _countNewOrders;

    public StatisticDataService(IStatisticRepository statisticRepository)
    {
        _statisticRepository = statisticRepository;
    }

    public async Task<(int,string,int)> LoadDataAsync(string query)
    {
           _countSellingBooks = await _statisticRepository.CountSellingBooksAsync(query);
           _countBooks = await _statisticRepository.CountBooksAsync();
           _countNewOrders = await _statisticRepository.CountNewOrdersAsync(query);

        return _countSellingBooks;
    }

    public (int, string, int) CountBooksAsync()
    {
        return _countBooks;
    }
    public (int, string, int) CountNewOrdersAsync(string query)
    {
        return _countNewOrders;

    }
    public (int, string, int) CountSellingBooksAsync(string query)
    {
        return _countSellingBooks;

    }

    public async Task<(IEnumerable<Revenue_Profit>, string, int)> GetRevenue_ProfitAsync(string query)
    {

        return await _statisticRepository.GetRevenue_ProfitAsync(query);

    }

    public async Task<(IEnumerable<BookSaleStat>, string, int)> GetBookSaleStatAsync(string query)
    {
        return await _statisticRepository.GetBookSaleStatAsync(query);
    }
}
