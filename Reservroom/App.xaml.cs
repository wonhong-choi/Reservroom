using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;

using Reservroom.Models;
using Reservroom.Exceptions;
using Reservroom.ViewModels;
using Reservroom.Stores;
using Reservroom.Services;
using Microsoft.EntityFrameworkCore;
using Reservroom.DBContexts;
using Reservroom.Services.ReservationProviders;
using Reservroom.Services.ReservationCreators;
using Reservroom.Services.ReservationConflictValidator;

namespace Reservroom
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        private readonly Hotel _hotel;
        private readonly NavigationStore _navigationStore;
        private const string CONNECTION_STRING = "Data Source=reservoom.db";
        private readonly ReservoomDbContextFactory _dbContextFactory;

        public App()
        {
            _dbContextFactory = new ReservoomDbContextFactory(CONNECTION_STRING);

            IReservationProvider reservationProvider = new DatabaseReservationProvider(_dbContextFactory);
            IReservationCreator reservationCreator = new DatabaseReservationCreator(_dbContextFactory);
            IReservationConflictValidator reservationConflictValidator = new DatabaseReservationConflictValidator(_dbContextFactory);

            ReservationBook reservationBook = new ReservationBook(reservationProvider, reservationCreator, reservationConflictValidator);
            _hotel = new Hotel("Wonhong Suites", reservationBook);
            _navigationStore = new NavigationStore();
        }

        protected override void OnStartup(StartupEventArgs e)
        {
            using (ReservoomDbContext dbContext = _dbContextFactory.CreateDbContext())
            {
                dbContext.Database.Migrate();
            }

            _navigationStore.CurrentViewModel = CreateReservationListingViewModel();

            MainWindow = new MainWindow()
            {
                DataContext = new MainViewModel(_navigationStore)
            };

            MainWindow.Show();

            base.OnStartup(e);
        }

        private MakeReservationViewModel CreateMakeReservationViewModel()
        {
            return new MakeReservationViewModel(_hotel, new NavigationService(_navigationStore, CreateReservationListingViewModel));
        }

        private ReservationListingViewModel CreateReservationListingViewModel()
        {
            return ReservationListingViewModel.LoadViewModel(_hotel, new NavigationService(_navigationStore, CreateMakeReservationViewModel));
        }
    }
}
