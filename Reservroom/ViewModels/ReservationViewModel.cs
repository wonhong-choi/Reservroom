using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Reservroom.Models;

namespace Reservroom.ViewModels
{
    public class ReservationViewModel : ViewModelBase
    {
         // glue - model
        private readonly Reservation _reservation;

        // glue - view
        public string RoomID => _reservation.RoomId.ToString();
        public string Username => _reservation.Username;

        public string StartDate => _reservation.StartTime.ToString("d");
        public string EndDate => _reservation.EndTime.ToString("d");


        public ReservationViewModel(Reservation reservation)
        {
            _reservation = reservation;
        }
    }
}
