using System;

namespace VacationRental.Core.Models
{
    public class Booking : Schedule
    {
        public Booking(int id, int rentalId, DateTime start, int nights, int unit) : base(id, rentalId, start, nights, unit)
        {
        }
    }
}
