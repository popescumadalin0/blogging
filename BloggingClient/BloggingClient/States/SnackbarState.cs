using System;
using System.Threading.Tasks;
using Blazorise.Snackbar;

namespace BloggingClient.States;

public class SnackbarState
{
    public SnackbarStack Snackbar { get; set; }

    public event Action OnStateChange;

    public async Task PushAsync(string message, bool isError = false)
    {
        await Snackbar.PushAsync(
            message,
            isError ? SnackbarColor.Danger : SnackbarColor.Info,
            options => { options.IntervalBeforeClose = 3000; });

        NotifyStateChanged();
    }

    private void NotifyStateChanged() => OnStateChange?.Invoke();
}