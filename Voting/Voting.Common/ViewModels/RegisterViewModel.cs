﻿namespace Voting.Common.ViewModels
{
    using System;
    using System.Collections.Generic;
    using System.Windows.Input;
    using Interfaces;
    using Models;
    using MvvmCross.Commands;
    using MvvmCross.Navigation;
    using MvvmCross.ViewModels;
    using Voting.Common.Service;

    public class RegisterViewModel : MvxViewModel
    {
        private readonly IApiService apiService;
        private readonly IMvxNavigationService navigationService;
        private readonly IDialogService dialogService;
        private List<Country> countries;
        private List<City> cities;
        private Country selectedCountry;
        private City selectedCity;
        private MvxCommand registerCommand;
        private string firstName;
        private string lastName;
        private string email;
        private string ocupation;
        private int stratum;
        private int gender;
        private string password;        
     //   private DateTime birthdate;
        private string confirmPassword;

        public RegisterViewModel(
            IMvxNavigationService navigationService,
            IApiService apiService,
            IDialogService dialogService)
        {
            this.apiService = apiService;
            this.navigationService = navigationService;
            this.dialogService = dialogService;
            this.LoadCountries();
        }

        public ICommand RegisterCommand
        {
            get
            {
                this.registerCommand = this.registerCommand ?? new MvxCommand(this.RegisterUser);
                return this.registerCommand;
            }
        }



        public string FirstName
        {
            get => this.firstName;
            set => this.SetProperty(ref this.firstName, value);
        }

        public string LastName
        {
            get => this.lastName;
            set => this.SetProperty(ref this.lastName, value);
        }

        public string Email
        {
            get => this.email;
            set => this.SetProperty(ref this.email, value);
        }

        public string Ocupation
        {
            get => this.ocupation;
            set => this.SetProperty(ref this.ocupation, value);
        }

        public int Stratum
        {
            get => this.stratum;
            set => this.SetProperty(ref this.stratum, value);
        }

        public int Gender
        {
            get => this.gender;
            set => this.SetProperty(ref this.gender, value);
        }

        
        public string Password
        {
            get => this.password;
            set => this.SetProperty(ref this.password, value);
        }

        public string ConfirmPassword
        {
            get => this.confirmPassword;
            set => this.SetProperty(ref this.confirmPassword, value);
        }

        public List<Country> Countries
        {
            get => this.countries;
            set => this.SetProperty(ref this.countries, value);
        }

        public List<City> Cities
        {
            get => this.cities;
            set => this.SetProperty(ref this.cities, value);
        }

        public Country SelectedCountry
        {
            get => selectedCountry;
            set
            {
                this.selectedCountry = value;
                this.RaisePropertyChanged(() => SelectedCountry);
                this.Cities = SelectedCountry.Cities;
            }
        }

        public City SelectedCity
        {
            get => selectedCity;
            set
            {
                selectedCity = value;
                RaisePropertyChanged(() => SelectedCity);
            }
        }

        private async void LoadCountries()
        {
            var response = await this.apiService.GetListAsync<Country>(
                "https://systemvotingitm.azurewebsites.net",
                "/api",
                "/Countries");

            if (!response.IsSuccess)
            {
                this.dialogService.Alert("Error", response.Message, "Accept");
                return;
            }

            this.Countries = (List<Country>)response.Result;
        }

        private async void RegisterUser()
        {

            if (string.IsNullOrEmpty(this.LastName))
            {
                this.dialogService.Alert("Error", "You must enter a lastName.", "Accept");
                return;
            }
            if (string.IsNullOrEmpty(this.FirstName))
            {
                this.dialogService.Alert("Error", "You must enter a firstName.", "Accept");
                return;
            }

            if (string.IsNullOrEmpty(this.Ocupation))
            {
                this.dialogService.Alert("Error", "You must enter a ocupation.", "Accept");
                return;
            }

            // TODO: Make the local validations
            var request = new NewUserRequest
            {
                Ocupation = this.ocupation,
                Stratum = this.stratum,
              //  Birthdate = this.birthdate,
                CityId = this.SelectedCity.Id,
                Email = this.Email,
                FirstName = this.FirstName,
                LastName = this.LastName,
                Password = this.Password
            };

            var response = await this.apiService.RegisterUserAsync(
                "https://systemvotingitm.azurewebsites.net",
                "/api",
                "/Account",
                request);

            if (response == null)
            {
                this.dialogService.Alert("Error", "The user was not created succesfully", "Accept");
              //  await this.navigationService.Close(this);
               
            }
            else {
            this.dialogService.Alert("Ok", "The user was created succesfully, you must " +
                "confirm your user by the email sent to you and then you could login with " +
                "the email and password entered.", "Accept");

            await this.navigationService.Close(this);

            }
        }
    }

}
