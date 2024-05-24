using System;
using BloggingClient.States;
using Microsoft.AspNetCore.Components;

namespace BloggingClient;

public partial class App : ComponentBase, IDisposable
{
    [Inject]
    private SnackbarState SnackbarState { get; set; }

    protected override void OnInitialized()
    {
        SnackbarState.OnStateChange += StateHasChanged;
    }

    public void Dispose()
    {
        SnackbarState.OnStateChange -= StateHasChanged;
    }
}