using System;
using BloggingClient.States;
using Microsoft.AspNetCore.Components;

namespace BloggingClient.Shared
{
    public partial class MainLayout : IDisposable
    {
        [Inject]
        private LoadingState LoadingState { get; set; }

        public void Dispose()
        {
            LoadingState.OnStateChange -= StateHasChanged;
        }

        protected override void OnInitialized()
        {
            LoadingState.OnStateChange += StateHasChanged;
        }
    }
}
