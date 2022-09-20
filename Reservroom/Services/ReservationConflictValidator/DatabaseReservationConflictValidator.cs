using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Reservroom.DBContexts;
using Reservroom.DTOs;
using Reservroom.Models;

namespace Reservroom.Services.ReservationConflictValidator
{
    public class DatabaseReservationConflictValidator : IReservationConflictValidator
    {

        private readonly ReservoomDbContextFactory _reservoomDbContextFactory;

        public DatabaseReservationConflictValidator(ReservoomDbContextFactory reservoomDbContextFactory)
        {
            _reservoomDbContextFactory = reservoomDbContextFactory;
        }
        public async Task<Reservation> GetConflictingReservation(Reservation reservation)
        {
            using(ReservoomDbContext context = _reservoomDbContextFactory.CreateDbContext())
            {
                ReservationDTO reservationDTO = await context.Reservations
                    .Where(r => r.FloorNumber == reservation.RoomId.FloorNumber)
                    .Where(r => r.RoomNumber == reservation.RoomId.RoomNumber)
                    .Where(r => r.EndTime > reservation.StartTime)
                    .Where(r => r.StartTime < reservation.EndTime)
                    .FirstOrDefaultAsync();

                if (reservationDTO == null)
                {
                    return null;
                }
                return ToReservation(reservationDTO);
            }
        }

        private static Reservation ToReservation(ReservationDTO dto)
        {
            return new Reservation(dto.Username, new RoomId(dto.FloorNumber, dto.RoomNumber), dto.StartTime, dto.EndTime);
        }

 
    }
}
