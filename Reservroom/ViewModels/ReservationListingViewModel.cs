using Reservroom.Commands;
using Reservroom.Models;
using Reservroom.Services;
using Reservroom.Stores;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Reservroom.ViewModels
{
    public class ReservationListingViewModel : ViewModelBase
    {
        private readonly ObservableCollection<ReservationViewModel> _reservations;
        public IEnumerable<ReservationViewModel> Reservations => _reservations;
        
        public ICommand MakeReservationCommand { get; }
        public ICommand LoadReservationCommand { get; }

        public ReservationListingViewModel(Hotel hotel, NavigationService makeReservationNavigationService)
        {
            _reservations = new ObservableCollection<ReservationViewModel>();

            MakeReservationCommand = new NavigationCommand(makeReservationNavigationService);
            LoadReservationCommand = new LoadReservationsCommand(this, hotel);
        }

        public static ReservationListingViewModel LoadViewModel(Hotel hotel, NavigationService makeReservationNavigationService)
        {
            ReservationListingViewModel viewModel = new ReservationListingViewModel(hotel, makeReservationNavigationService);

            viewModel.LoadReservationCommand.Execute(null);

            return viewModel;
        }

        public void UpdateReservations(IEnumerable<Reservation> reservations)
        {
            _reservations.Clear();

            foreach(Reservation reservation in reservations)
            {
                ReservationViewModel reservationViewModel=new ReservationViewModel(reservation);
                _reservations.Add(reservationViewModel);
            }
        }
    }
}
