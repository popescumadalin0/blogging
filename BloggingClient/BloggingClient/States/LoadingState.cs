using System;
using System.Threading.Tasks;
using Blazorise.LoadingIndicator;

namespace BloggingClient.States;

public class LoadingState
{
    public LoadingIndicator Loading { get; set; }

    public event Action OnStateChange;

    public async Task ShowAsync()
    {
        await Loading.Show();

        NotifyStateChanged();
    }

    public async Task HideAsync()
    {
        await Loading.Hide();

        NotifyStateChanged();
    }

    private void NotifyStateChanged() => OnStateChange?.Invoke();
}