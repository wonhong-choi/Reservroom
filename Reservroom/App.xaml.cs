using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;

using Reservroom.Models;
using Reservroom.Exceptions;

namespace Reservroom
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        protected override void OnStartup(StartupEventArgs e)
        {
            Hotel hotel = new Hotel("Wonhong Suites");

            try
            {
                hotel.MakeReservation(new Reservation(
                    "wonhong",
                    new RoomId(1, 3),
                    new DateTime(2000, 1, 1),
                    new DateTime(2000, 1, 4)));

                hotel.MakeReservation(new Reservation(
                    "wonhong",
                    new RoomId(1, 3),
                    new DateTime(2000, 1, 1),
                    new DateTime(2000, 1, 4)));
            }
            catch(ReservationConflictException ex)
            {

            }

            IEnumerable<Reservation> reservations = hotel.GetReservationForUser("wonhong");

            base.OnStartup(e);
        }
    }
}
