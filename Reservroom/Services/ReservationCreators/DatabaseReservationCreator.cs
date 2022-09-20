using Reservroom.DBContexts;
using Reservroom.DTOs;
using Reservroom.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Reservroom.Services.ReservationCreators
{
    public class DatabaseReservationCreator : IReservationCreator
    {
        private readonly ReservoomDbContextFactory _reservoomDbContextFactory;

        public DatabaseReservationCreator(ReservoomDbContextFactory reservoomDbContextFactory)
        {
            _reservoomDbContextFactory = reservoomDbContextFactory;
        }

        public async Task CreateReservation(Reservation reservation)
        {
            using(ReservoomDbContext context = _reservoomDbContextFactory.CreateDbContext())
            {
                ReservationDTO reservationDTO = ToReservationDTO(reservation);

                context.Reservations.Add(reservationDTO);
                await context.SaveChangesAsync();
            }
        }

        private ReservationDTO ToReservationDTO(Reservation reservation)
        {
            return new ReservationDTO()
            {
                FloorNumber = reservation.RoomId?.FloorNumber ?? 0,
                RoomNumber = reservation.RoomId?.RoomNumber ?? 0,
                Username = reservation.Username,
                StartTime = reservation.StartTime,
                EndTime = reservation.EndTime,
            };
        }
    }
}
