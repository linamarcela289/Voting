

namespace Voting.IUFroms.ViewModels
{
    using System.Collections.Generic;
    using System.Collections.ObjectModel;
    using System.Windows.Input;
    using Common.Model;
    using Common.Services;
    using GalaSoft.MvvmLight.Command;
    using Xamarin.Forms;


    public class EventsViewModel : BaseViewModel
    {
        private readonly ApiService apiService;
        private ObservableCollection<Events> events;
        private bool isRefreshing;



        public ObservableCollection<Events> Events
        {
            get => this.events;
            set => this.SetValue(ref this.events, value);
        }

        public bool IsRefreshing
        {
            get => this.isRefreshing;
            set => this.SetValue(ref this.isRefreshing, value);
        }



        public ICommand RefreshCommand => new RelayCommand(this.LoadEvents);

        public EventsViewModel()
        {
            this.apiService = new ApiService();
            this.LoadEvents();
        }

        private async void LoadEvents()
        {
            this.IsRefreshing = true;

            var response = await this.apiService.GetListAsync<Events>(
                   "https://votingevents.azurewebsites.net",
                   "/api",
                   "/Events");
           

            if (!response.IsSuccess)
            {
                await Application.Current.MainPage.DisplayAlert(
                    "Error",
                    response.Message,
                    "Accept");
                this.IsRefreshing = false;
                return;
            }

            var myevents = (List<Events>)response.Result;
            this.Events = new ObservableCollection<Events>(myevents);
            this.IsRefreshing = false;

        }
    }


}
