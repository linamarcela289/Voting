
namespace Voting.IUFroms
{
    using Newtonsoft.Json;
    using System;
    using Views;
    using Voting.Common.Helpers;
    using Voting.Common.Models;
    using Voting.IUFroms.ViewModels;
    using Xamarin.Forms;

    public partial class App : Application
    {
        internal static NavigationPage Navigator;

        public App()
        {
            InitializeComponent();
            if (Settings.IsRemember)
            {
                var token = JsonConvert.DeserializeObject<TokenResponse>(Settings.Token);
                if (token.Expiration > DateTime.Now)
                {
                    var mainViewModel = MainViewModel.GetInstance();
                    mainViewModel.Token = token;
                    mainViewModel.UserEmail = Settings.UserEmail;
                    mainViewModel.UserPassword = Settings.UserPassword;
                    mainViewModel.Events = new EventsViewModel();
                    this.MainPage = new MasterPage();
                    return;
                }
            }


            MainViewModel.GetInstance().Login = new LoginViewModel();
            MainPage = new NavigationPage(new LoginPage());
          
        }

        protected override void OnStart()
        {
            // Handle when your app starts
        }

        protected override void OnSleep()
        {
            // Handle when your app sleeps
        }

        protected override void OnResume()
        {
            // Handle when your app resumes
        }
    }
}
