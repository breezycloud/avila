using AvilaNaturalle.Shared.Models;
using Nosthy.Blazor.DexieWrapper.Database;
using Nosthy.Blazor.DexieWrapper.JsModule;
using static MudBlazor.CategoryTypes;
namespace AvilaNaturalle.Data
{
    public class MyDb : Db
    {
        public Store<Customer, Guid> Customers { get; set; } = new(nameof(Customer.Id), nameof(Customer.CustomerName), nameof(Customer.MobileNo1));
        public Store<Category, Guid> Categories { get; set; } = new(nameof(Category.Id), nameof(Category.CategoryName), nameof(Category.ModifiedDate));
        public Store<Shared.Models.Item, Guid> Items { get; set; } = new(nameof(Shared.Models.Item.Id), nameof(Shared.Models.Item.ItemName), nameof(Shared.Models.Item.Quantity), nameof(Shared.Models.Item.ModifiedTicks));
        public Store<Order, Guid> Orders { get; set; } = new(nameof(Order.Id), nameof(Order.TransactionDate), nameof(Order.CustomerId));
        public MyDb(IModuleFactory moduleFactory)
            : base("MyDb", 1, new DbVersion[] { }, moduleFactory)
        {
        }
    }
}
