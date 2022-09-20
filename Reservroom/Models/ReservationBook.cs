using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Reservroom.Exceptions;
using Reservroom.Services.ReservationConflictValidator;
using Reservroom.Services.ReservationCreators;
using Reservroom.Services.ReservationProviders;

namespace Reservroom.Models
{
    // Data structure hold a lot of reservation
    public class ReservationBook
    {
        private readonly IReservationProvider _reservationProvider;
        private readonly IReservationCreator _reservationCreator;
        private readonly IReservationConflictValidator _reservationConflictValidator;

        public ReservationBook(IReservationProvider reservationProvider,
            IReservationCreator reservationCreator,
            IReservationConflictValidator reservationConflictValidator)
        {
            _reservationProvider = reservationProvider;
            _reservationCreator = reservationCreator;
            _reservationConflictValidator = reservationConflictValidator;
        }

        public async Task<IEnumerable<Reservation>> GetAllReservations()
        {
            return await _reservationProvider.GetAllReservations();
        }

        public async Task AddReservation(Reservation reservation)
        {
            Reservation conflictingReservation = await _reservationConflictValidator.GetConflictingReservation(reservation);
            if(conflictingReservation!= null)
            {
                throw new ReservationConflictException(conflictingReservation, reservation);
            }
            
            await _reservationCreator.CreateReservation(reservation);
        }


    }
}
