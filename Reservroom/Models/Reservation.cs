using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Reservroom.Models
{
    public class Reservation
    {
        public string Username { get; }
        public RoomId RoomId { get; }
        public DateTime StartTime { get; }
        public DateTime EndTime { get; }

        public TimeSpan Length => EndTime.Subtract(StartTime);

        public Reservation(string username, RoomId roomId, DateTime startTime, DateTime endTime)
        {
            Username = username;
            this.RoomId = roomId;
            StartTime = startTime;
            EndTime = endTime;
        }

        public bool Conflicts(Reservation reservation)
        {
            if (this.RoomId != reservation.RoomId)
            {
                return false;
            }

            return reservation.StartTime < this.EndTime && reservation.EndTime > this.StartTime;
        }

    }
}
