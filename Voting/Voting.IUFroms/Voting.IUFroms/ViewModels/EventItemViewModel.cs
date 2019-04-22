namespace Voting.IUFroms.ViewModels
{

    using GalaSoft.MvvmLight.Command;
    using System;
    using System.Collections.Generic;
    using System.Text;
    using System.Windows.Input;
    using Voting.Common.Model;
    using Voting.IUFroms.Views;

    public class EventItemViewModel:Events
    {
        public ICommand SelectEventsCommand => new RelayCommand(this.SelectCandidate);

        private async void SelectCandidate()
        {
           // MainViewModel.GetInstance(). = new CandidateViewModel((Events)this);
            await App.Navigator.PushAsync(new EventsPage());
        }
    }
}
