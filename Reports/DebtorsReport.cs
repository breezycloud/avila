using Blazored.LocalStorage;
using QuestPDF.Drawing;
using QuestPDF.Fluent;
using QuestPDF.Helpers;
using QuestPDF.Infrastructure;
using QuestPDF.Previewer;
using System.Linq;
using AvilaNaturalle.Shared.Models;
using static MudBlazor.CategoryTypes;
using Microsoft.AspNetCore.Components.Routing;

namespace AvilaNaturalle.Reports;

public class DebtorsReport
{
    public DebtorsReport(List<DebtorDataModel>? model, string? selectedOption)
    {
        Model = model;
        SelectedOption = selectedOption;
    }
    private List<DebtorDataModel>? Model { get; set; }
    private string? SelectedOption { get; set; }
    public byte[] Create()
    {
        var document = Document.Create(container =>
        {
            container.Page(page =>
            {
                page.Size(PageSizes.A4.Portrait());
                page.MarginTop(0, Unit.Inch);

                page.Header().Element(ComposeHeader);
                page.Content().Element(ComposeContent);

            });
        });
        return document.GeneratePdf();
    }
    void ComposeHeader(IContainer container)
    {
        container.AlignLeft().Column(column =>
        {
            column.Spacing(5);
            //column.Item().AlignCenter().Width(30, Unit.Millimetre).Height(30, Unit.Millimetre).Image(Logo, ImageScaling.FitArea);
            column.Item().AlignLeft().Text("Avila").Bold().FontSize(20);
            column.Item().AlignLeft().Text("Debtors Report").FontSize(15);            
        });
    }
    void ComposeContent(IContainer container)
    {
        container.PaddingVertical(20).Padding(1.5f).Column(column =>
        {
            column.Spacing(10);
            column.Item().Element(ComposeTable);
        });
    }

    void ComposeTable(IContainer container)
    {

        container.PaddingVertical(1.2f).Padding(1.2f).Border(1f).BorderColor(Colors.Grey.Medium).Table(table =>
        {
            table.ColumnsDefinition(column =>
            {
                column.RelativeColumn(0.7f);
                column.RelativeColumn(3f);
                if (SelectedOption == "Customer")
                {
                    column.RelativeColumn(1.5f);
                    column.RelativeColumn(1.5f);
                }
                column.RelativeColumn(2f);
                column.RelativeColumn(2f);
                column.RelativeColumn(2f);
                column.RelativeColumn(2f);
                column.RelativeColumn(2f);
            });

            table.Header(header =>
            {
                header.Cell().Border(1f).BorderColor(Colors.Grey.Medium).AlignCenter().Text("S/No").FontSize(10);
                header.Cell().Border(1f).BorderColor(Colors.Grey.Medium).Text("Customer").FontSize(10);
                if (SelectedOption == "Customer")
                {
                    header.Cell().Border(1f).BorderColor(Colors.Grey.Medium).Text("Receipt #").FontSize(10);
                    header.Cell().Border(1f).BorderColor(Colors.Grey.Medium).Text("Date").FontSize(10);
                }
                header.Cell().Border(1f).BorderColor(Colors.Grey.Medium).AlignRight().Text("SubTotal").FontSize(10);
                header.Cell().Border(1f).BorderColor(Colors.Grey.Medium).AlignRight().Text("Total").FontSize(10);
                header.Cell().Border(1f).BorderColor(Colors.Grey.Medium).AlignCenter().Text("Discount").FontSize(10);
                header.Cell().Border(1f).BorderColor(Colors.Grey.Medium).AlignRight().Text("Paid").FontSize(10);
                header.Cell().Border(1f).BorderColor(Colors.Grey.Medium).AlignRight().Text("Balance").FontSize(10);
                // header.Cell().ColumnSpan(5)
                //     .PaddingVertical(5).BorderBottom(1).BorderColor(Colors.Black);
            });


            // step 3
            foreach (var item in Model!)
            {
                table.Cell().Border(1f).BorderColor(Colors.Grey.Medium).AlignCenter().Text(Model.IndexOf(item) + 1).FontSize(10);
                table.Cell().Border(1f).BorderColor(Colors.Grey.Medium).Text(item.CustomerName).FontSize(10);
                if (SelectedOption == "Customer")
                {
                    table.Cell().Border(1f).BorderColor(Colors.Grey.Medium).Text($"{item.ReceiptNo}").FontSize(10);
                    table.Cell().Border(1f).BorderColor(Colors.Grey.Medium).Text(item.TransactionDate.ToString("dd/MM/yyyy")).FontSize(10);
                }                
                table.Cell().Border(1f).BorderColor(Colors.Grey.Medium).AlignRight().Text($"{item.SubTotal:N2}").FontSize(10);
                table.Cell().Border(1f).BorderColor(Colors.Grey.Medium).AlignRight().Text($"{item.TotalAmount:N2}").FontSize(10);
                table.Cell().Border(1f).BorderColor(Colors.Grey.Medium).AlignCenter().Text($"{item.Discount}").FontSize(10);
                table.Cell().Border(1f).BorderColor(Colors.Grey.Medium).AlignRight().Text($"{item.Paid:N2}").FontSize(10);
                table.Cell().Border(1f).BorderColor(Colors.Grey.Medium).AlignRight().Text($"{item.Balance:N2}").FontSize(10);
            }
            table.Cell();
            table.Cell();
            if (SelectedOption == "Customer")
            {
                table.Cell();
                table.Cell();
            }            
            table.Cell();
            table.Cell();
            table.Cell();
            table.Cell().Border(1f).BorderColor(Colors.Grey.Medium).AlignRight().Text($"{Model.Sum(x => x.Paid):N2}");
            table.Cell().Border(1f).BorderColor(Colors.Grey.Medium).AlignRight().Text($"{Model.Sum(x => x.Balance):N2}").FontColor(Colors.Red.Darken4);

            //table.Footer(footer =>
            //{
            //    footer.Cell().RowSpan(8).ColumnSpan(8).Border(1f).BorderColor(Colors.Grey.Medium).AlignRight().Text($"{Model.Sum(x => x.TotalSellPrice):N2}");
            //    footer.Cell().RowSpan(8).ColumnSpan(8).Border(1f).BorderColor(Colors.Grey.Medium).AlignRight().Text($"{Model.Sum(x => x.Profit):N2}");
            //});
        });
    }
}