using Blazored.LocalStorage;
using QuestPDF.Drawing;
using QuestPDF.Fluent;
using QuestPDF.Helpers;
using QuestPDF.Infrastructure;
using QuestPDF.Previewer;
using System.Linq;
using AvilaNaturalle.Shared.Models;

namespace AvilaNaturalle.Reports;
public class Receipt
{
    public Order? Order { get; set; }
    public byte[] QR { get; set; }
    public Receipt(Order? order, byte[] qR)
    {
        Order = order;
        QR = qR;
    }

    public byte[] Create()
    {
        var document = Document.Create(container =>
        {
            container.Page(page =>
            {
                page.Size(3.1486f, 5.5f, Unit.Inch);
                page.MarginTop(0, Unit.Inch);                
                                
                page.Header().Element(ComposeHeader);
                page.Content().Element(ComposeContent);
            
            });    
        });
        return document.GeneratePdf();
    }
    void ComposeHeader(IContainer container)
    {
        container.AlignCenter().Column(column => 
        {
            column.Spacing(5);
            //column.Item().AlignCenter().Width(30, Unit.Millimetre).Height(30, Unit.Millimetre).Image(Logo, ImageScaling.FitArea);
            column.Item().AlignCenter().Text("Avila").Bold().FontSize(20);
            column.Item().AlignCenter().Text("Order Receipt").FontSize(13);
            column.Item().AlignCenter().Text($"{Order!.ReceiptNo}").FontSize(10);
        });
        
    }

    void ComposeContent(IContainer container)
    {
        container.PaddingVertical(20).Padding(1.5f).Column(column => 
        {      
            column.Item().Row(row => 
            {
                row.RelativeItem().AlignLeft().Text($"{Order!.Customer!.CustomerName}").FontSize(9);
                row.RelativeItem().AlignRight().Text($"{GetPaymentDate()}").FontSize(9);
            });            
            column.Spacing(10);
            column.Item().Element(ComposeTable);
        });
    }    

    void ComposeTable(IContainer container)
    {        

        container.PaddingVertical(1.2f).Padding(1.2f).Table(table =>
        {
            table.ColumnsDefinition(column => 
            {
                column.RelativeColumn(0.5f);
                column.RelativeColumn(3f);
                //column.RelativeColumn(1.5f);
                column.RelativeColumn(1.5f);
            });

            table.Header(header =>
            {
                header.Cell().AlignCenter().Text("Qty").FontSize(9);
                header.Cell().Text("Item").FontSize(9);
                //header.Cell().AlignRight().Text("Rate").FontSize(9);
                header.Cell().AlignRight().Text("Cost").FontSize(9);
                // header.Cell().ColumnSpan(5)
                //     .PaddingVertical(5).BorderBottom(1).BorderColor(Colors.Black);
            });


            var Model = Order!.OrderItems;
            // step 3
            foreach (var item in Model!)
            {
                table.Cell().AlignCenter().Text(item.Quantity).FontSize(9);
                table.Cell().Text(item.Item!.ItemName).FontSize(9);
                //table.Cell().AlignRight().Text($"{item.Price:N2}").FontSize(9);
                table.Cell().AlignRight().Text($"{(item.Price * item.Quantity):N2}").FontSize(9);                                
            }

            table.Footer(footer => 
            {                
                footer.Cell().RowSpan(3).ColumnSpan(3).Row(row => 
                {
                    row.RelativeItem(7).AlignRight().Text("Total").FontSize(9);
                    row.RelativeItem(2).AlignRight().Text(GetTotal()).FontSize(9);
                });
                footer.Cell().RowSpan(3).ColumnSpan(3).Row(row => 
                {
                    row.RelativeItem(7).AlignRight().Text("Sub Total").FontSize(9);
                    row.RelativeItem(2).AlignRight().Text(GetSubTotal()).FontSize(9);
                });
                footer.Cell().RowSpan(3).ColumnSpan(3).Row(row =>
                {
                    row.RelativeItem(7).AlignRight().Text("Discount").FontSize(9);
                    row.RelativeItem(2).AlignRight().Text(GetDiscount()).FontSize(9);
                });
                footer.Cell().RowSpan(3).ColumnSpan(3).Row(row =>
                {
                    row.RelativeItem(7).AlignRight().Text("Sub Total").FontSize(9);
                    row.RelativeItem(2).AlignRight().Text(GetSubTotal()).FontSize(9);
                });
                footer.Cell().RowSpan(3).ColumnSpan(3).Row(row =>
                {
                    row.RelativeItem(7).AlignRight().Text("Previous Payment").FontSize(9);
                    row.RelativeItem(2).AlignRight().Text(GetPreviousPayment()).FontSize(9);
                });
                footer.Cell().RowSpan(3).ColumnSpan(3).Row(row =>
                {
                    row.RelativeItem(7).AlignRight().Text("Amount Paid").FontSize(9);
                    row.RelativeItem(2).AlignRight().Text(GetPayment()).FontSize(9);
                });
                footer.Cell().RowSpan(3).ColumnSpan(3).Row(row =>
                {
                    row.RelativeItem(7).AlignRight().Text("Balance").FontSize(9);
                    row.RelativeItem(2).AlignRight().Text(GetBalance()).FontSize(9);
                });
                footer.Cell().RowSpan(3).ColumnSpan(3).AlignCenter().Width(30, Unit.Millimetre).Height(30, Unit.Millimetre).Image(QR, ImageScaling.FitArea);
            });
        });
    }
    private string GetTotal() =>        
        Order!.TotalAmount.ToString("N2");

    private string GetVAT() => "0.00";

    private string GetDiscount() =>
        Order!.Discount.ToString("N2");

    private string GetSubTotal() =>
        Order!.SubTotal.ToString("N2");

    private string GetPayment()
    {        
        return Order!.PaymentDetails!.OrderByDescending(d => d.ModifiedTicks)
                                           .Take(1)
                                           .Select(x => x.Amount)
                                           .FirstOrDefault(0)
                                           .ToString("N2");
    }

    private DateTime? GetPaymentDate()
    {        
        return Order!.PaymentDetails!.OrderByDescending(d => d.ModifiedTicks)
                                           .Take(1)
                                           .Select(x => x.PaymentDate)
                                           .FirstOrDefault();
    }


    private string GetPreviousPayment()
    {
        return Order!.PaymentDetails!.OrderByDescending(d => d.ModifiedTicks)
                                           .Skip(1)
                                           .Take(1)
                                           .Select(x => x.Amount)
                                           .FirstOrDefault(0)
                                           .ToString("N2");
        
    }

    private string GetBalance()
    {
        return (Order!.SubTotal - Order!.PaymentDetails!.Sum(x => x.Amount)).ToString("N2");        
    }
}