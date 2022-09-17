using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Reservroom.Exceptions;

namespace Reservroom.Models
{
    // Data structure hold a lot of reservation
    public class ReservationBook
    {
        private readonly List<Reservation> _reservations;

        public ReservationBook()
        {
            _reservations = new List<Reservation>();
        }

        public IEnumerable<Reservation> GetReservationsForUser(string username)
        {
            return _reservations.Where(x => x.Username == username);
        }

        public void AddReservation(Reservation reservation)
        {
            foreach(var existingReservation in _reservations)
            {
                if (existingReservation.Conflict(reservation))
                {
                    throw new ReservationConflictException(existingReservation, reservation);
                }
            }
            _reservations.Add(reservation);
        }


    }
}
