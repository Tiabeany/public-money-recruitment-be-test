using System;

namespace VacationRental.Core.Models
{
    public class Booking : Schedule
    {
        public Booking(int rentalId, DateTime start, int nights, int unit) : base(rentalId, start, nights, unit)
        {
        }
    }
}
