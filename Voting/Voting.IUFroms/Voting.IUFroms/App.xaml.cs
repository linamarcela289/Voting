﻿
namespace Voting.IUFroms
{
    using Views;
    using Voting.IUFroms.ViewModels;
    using Xamarin.Forms;

    public partial class App : Application
    {
        internal static NavigationPage Navigator;

        public App()
        {
            InitializeComponent();
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
