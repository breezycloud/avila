using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Json;
using System.Text;
using System.Threading.Tasks;
using AvilaNaturalle.Shared.Models;
using Microsoft.JSInterop;
using Nosthy.Blazor.DexieWrapper.JsModule;
namespace AvilaNaturalle.Data;

public interface IDashboardService
{
    ValueTask<DashboardModel> GetDashboard();
}
public class DashboardService : IDashboardService
{
    private readonly IJSRuntime _js;
    private EsModuleFactory moduleFactory;
    public DashboardService(IJSRuntime js)
    {
        _js = js;
        moduleFactory = new EsModuleFactory(js);

    }

    public async ValueTask<DashboardModel> GetDashboard()
    {
        DashboardModel model = new();
        var db = new MyDb(moduleFactory);
        model.AvailableProducts = await db.Items.Where(nameof(Item.Quantity)).AboveOrEqual(1).Count();
        var orders = await db.Orders.ToArray();
        var orderItems = orders.SelectMany(x => x.OrderItems!).ToArray();
        model.SoldProducts = orderItems.Sum(x => x.Quantity);
        model.Categories = await db.Categories.Count();
        //model.TotalEmployees = await _context.Employees.Where(x => x.PhoneNo != "07068716813").CountAsync();
        model.TotalCustomers = await db.Customers.Count();
        //model.TotalBranches = await _context.Branches.CountAsync();
        model.Items = orderItems.GroupBy(x => x.ItemId).Select(y => new ItemChartModel
                                        {
                                            ServiceName = y.Select(i => i.Item!.ItemName)
                                                            .FirstOrDefault(),
                                            SalesCount = y.Select(i => i.ItemId).Count()
                                        }).OrderByDescending(x => x.SalesCount).Take(10).ToArray();
        model.ItemSales = orders.GroupBy(x => new { x.TransactionDate.Year, x.TransactionDate.Month }).Select(y => new ItemSalesLine
        {
            Year = y.Key.Year,
            Month = y.Key.Month,
            Sales = y.Count(x => x.TransactionDate.Month == y.Key.Month && x.TransactionDate.Year == y.Key.Year)
        }).ToArray();

        return model;
    }
}
