using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AvilaNaturalle.Shared.Models
{
    public class AppState
    {
        public event Action? OnUpdate;
        public Category Category { get; set; } = default!;
        public Category[] Categories { get; set; } = default!;
        public Customer[] Customers { get; set; } = default!;
        public Customer Customer { get; set; } = new();
        public Item[] Items { get; set; } = default!;        
        public Item Item { get; set; } = default!;        
        public Order Order { get; set; } = new();
        public Order[] Orders { get; set; } = default!;
        public OrderItem[] OrderItems { get; set; } = default!;
        public OrderPayment[] PaymentDetails { get; set; } = default!;
        public OrderPayment PaymentDetail { get; set; } = new();
        public bool IsRows { get; set; } = false;
        public string? searchString = "";
        public bool IsProcessing { get; set; }
        public bool IsBusy { get; set; }
        public bool IsConnected { get; set; }
        public bool IsUpload { get; set; }
        public bool IsDownload { get; set; }
        public string? Token { get; set; }


        public List<string> CheckOutValidation()
        {
            List<string> errors = new();
            if (Customer.Id == Guid.Empty)
            {
                errors.Add("Select Customer");
            }               
            if (Order!.OrderItems!.Count == 0)
            {
                errors.Add("Car is empty");
            }
            return errors;
        }
        public Task OnNotify()
        {
            OnUpdate?.Invoke();
            return Task.CompletedTask;
        }

        public Exception? syncException { get; set; }
        public void HandleSyncError(Exception ex)
        {
            syncException = ex;
        }
    }
}
