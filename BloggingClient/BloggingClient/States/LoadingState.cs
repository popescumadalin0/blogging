using System;
using System.Threading.Tasks;
using Blazorise.LoadingIndicator;

namespace BloggingClient.States;

public class LoadingState
{
    public bool IsLoading { get; private set; }

    public event Action OnStateChange;

    public async Task ShowAsync()
    {
        await Task.Run(() => IsLoading = true);

        NotifyStateChanged();
    }

    public async Task HideAsync()
    {
        await Task.Run(() => IsLoading = false);

        NotifyStateChanged();
    }

    private void NotifyStateChanged() => OnStateChange?.Invoke();
}