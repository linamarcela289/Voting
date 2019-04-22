

namespace Voting.IUFroms.ViewModels
{
    using Common.Model;
    using Common.Service;
    using GalaSoft.MvvmLight.Command;
    using System.Collections.Generic;
    using System.Collections.ObjectModel;
    using System.Linq;
    using System.Windows.Input;
    using Xamarin.Forms;


    public class EventsViewModel : BaseViewModel
    {
        private readonly ApiService apiService;

        // private ObservableCollection<EventItemViewModel> events;
        private ObservableCollection<Events> events;
        private bool isRefreshing;


        //  public ObservableCollection<EventItemViewModel> Events
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
            var url = Application.Current.Resources["UrlAPI"].ToString();
            var response = await this.apiService.GetListAsync<Events>(
                    url,
                   "/api",
                   "/Events",
                   "bearer",
                   MainViewModel.GetInstance().Token.Token);

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
