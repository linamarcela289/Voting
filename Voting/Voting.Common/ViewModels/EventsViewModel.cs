namespace Voting.Common.ViewModels
{
    using System.Collections.Generic;
    using Helpers;
    using Interfaces;
    using Models;
    using MvvmCross.ViewModels;
    using Newtonsoft.Json;
    using Model;
    using Service;

    public class EventsViewModel : MvxViewModel
    {
        private List<Events> events;
        private readonly IApiService apiService;
        private readonly IDialogService dialogService;

        public List<Events> Events
        {
            get => this.events;
            set => this.SetProperty(ref this.events, value);
        }

        public EventsViewModel(
            IApiService apiService,
            IDialogService dialogService)
        {
            this.apiService = apiService;
            this.dialogService = dialogService;
            this.LoadEvents();
        }

        private async void LoadEvents()
        {
            var token = JsonConvert.DeserializeObject<TokenResponse>(Settings.Token);
            var response = await this.apiService.GetListAsync<Events>(
                "https://systemvotingitm.azurewebsites.net",
                "/api",
                "/Events",
                "bearer",
                token.Token);

            if (!response.IsSuccess)
            {
                this.dialogService.Alert("Error", response.Message, "Accept");
                return;
            }

            this.Events = (List<Events>)response.Result;
        }
    }
}
