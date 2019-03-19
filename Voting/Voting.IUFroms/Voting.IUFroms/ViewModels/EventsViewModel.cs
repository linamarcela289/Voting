﻿

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


        public ObservableCollection<Events> Events
        {
            get => this.events;
            set => this.SetValue(ref this.events, value);
        }



        public ICommand RefreshCommand => new RelayCommand(this.LoadEvents);

        public EventsViewModel()
        {
            this.apiService = new ApiService();
            this.LoadEvents();
        }

        private async void LoadEvents()
        {

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

                return;
            }

            var myevents = (List<Events>)response.Result;
            this.Events = new ObservableCollection<Events>(myevents);

        }
    }


}