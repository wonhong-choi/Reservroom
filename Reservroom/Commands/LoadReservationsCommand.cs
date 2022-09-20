using Reservroom.Models;
using Reservroom.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace Reservroom.Commands
{
    internal class LoadReservationsCommand : AsyncCommandBase
    {
        private readonly ReservationListingViewModel _reservationListingViewModel;
        private readonly Hotel _hotel;

        public LoadReservationsCommand(ReservationListingViewModel reservationListingViewModel, Hotel hotel)
        {
            _reservationListingViewModel = reservationListingViewModel;
            _hotel = hotel;
        }

        public override async Task ExecuteAsync(object parameter)
        {
            try
            {
                IEnumerable<Reservation> reservations = await _hotel.GetAllReservation();
                _reservationListingViewModel.UpdateReservations(reservations);
            }
            catch(Exception)
            {
                MessageBox.Show("Falied to load Reservations.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
    }
}
