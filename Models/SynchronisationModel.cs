using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AvilaNaturalle.Shared.Models;

public class SynchronisationModel
{
    public Category[] Categories { get; set; } = default!;
    public Customer[] Customers { get; set; } = default!;
    public Item[] Items { get; set; } = default!;
    public Item Item { get; set; } = default!;
    public Order[] Orders { get; set; } = default!;
    public OrderItem[] OrderItems { get; set; } = default!;
    public OrderPayment[] PaymentDetails { get; set; } = default!;
    public Stock[] Stocks { get; set; } = default!;
    public int TotalChanges
    {
        get
        {
            return  Categories.Count() + Customers.Count() + Items.Count() + Orders.Count() + OrderItems.Count() + PaymentDetails.Count() + Stocks.Count();
        }
    }

    public string LastDateUpdated => "Last Updated:" + ' ' + DateTime.FromBinary(LastUpdatedTicks);
    public string LastDurationUpdated => "Duration:" + ' ' + string.Format("{0} Days", Duration.Days);
    public long LastUpdatedTicks { get; set; }
    public DateTime LastUpdated => DateTime.FromBinary(LastUpdatedTicks);
    public TimeSpan Duration => DateTime.Now.Subtract(LastUpdated);

    public static DbParameter AddNamedParameter(DbCommand command, string name)
    {
        var parameter = command.CreateParameter();
        parameter.ParameterName = name;
        command.Parameters.Add(parameter);
        return parameter;
    }
}
