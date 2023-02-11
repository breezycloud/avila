using Microsoft.AspNetCore.Components;
using AvilaNaturalle.AppTheme;

namespace AvilaNaturalle.Shared;

public partial class MainLayout : LayoutComponentBase, IDisposable
{        
    protected override void OnInitialized()
    {        
        LayoutService?.SetBaseTheme(Theme.DocsTheme());
        LayoutService!.MajorUpdateOccured += LayoutServiceOnMajorUpdateOccured!;
        base.OnInitialized();
    }

    protected override async Task OnAfterRenderAsync(bool firstRender)
    {
        await base.OnAfterRenderAsync(firstRender);
        if (firstRender)
        {
            await LayoutService!.ApplyUserPreferences();
            LayoutService.OnMajorUpdateOccured();
            StateHasChanged();
        }
    }
   
    public void Dispose()
    {
        LayoutService!.MajorUpdateOccured -= LayoutServiceOnMajorUpdateOccured!;
    }

    private void LayoutServiceOnMajorUpdateOccured(object sender, EventArgs e) => StateHasChanged();
}
