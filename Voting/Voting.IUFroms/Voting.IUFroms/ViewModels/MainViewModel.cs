﻿

namespace Voting.IUFroms.ViewModels
{
    using GalaSoft.MvvmLight.Command;
    using System;
    using System.Collections.Generic;
    using System.Collections.ObjectModel;
    using System.Linq;
    using System.Windows.Input;
    using Voting.Common.Models;
    using Voting.IUFroms.Views;

    public class MainViewModel
    {
        private static MainViewModel instance;
        public LoginViewModel Login { get; set; }

        public EventsViewModel Events { get; set; }

        public TokenResponse Token { get; set; }

        public RegisterViewModel Register { get; set; }

        public CandidateViewModel Candidate { get; set; }

        public string UserEmail { get; set; }

        public string UserPassword { get; set; }


        public ObservableCollection<MenuItemViewModel> Menus { get; set; }

  
        public static MainViewModel GetInstance()
        {
            if (instance == null)
            {
                return new MainViewModel();
            }

            return instance;
        }
        public MainViewModel()
        {
            instance = this;
            this.LoadMenus();

        }
        private void LoadMenus()
        {
            var menus = new List<Menu>
    {
        new Menu
        {
            Icon = "ic_info",
            PageName = "AboutPage",
            Title = "About"
        },

        new Menu
        {
            Icon = "ic_phonelink_setup",
            PageName = "SetupPage",
            Title = "Setup"
        },

        new Menu
        {
            Icon = "ic_exit_to_app",
            PageName = "LoginPage",
            Title = "Close session"
        }
    };

            this.Menus = new ObservableCollection<MenuItemViewModel>(menus.Select(m => new MenuItemViewModel
            {
                Icon = m.Icon,
                PageName = m.PageName,
                Title = m.Title
            }).ToList());
        }

    }
}
