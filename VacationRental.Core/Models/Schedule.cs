using System;
using System.Collections.Generic;
using System.Text;

namespace VacationRental.Core.Models
{
    public class Schedule : BaseEntity
    {
        public int RentalId { get; set; }
        public DateTime Start { get; set; }
        public int Nights { get; set; }
        public int Unit { get; set; }

        public Rental Rental { get; set; }

        public Schedule(int id, int rentalId, DateTime start, int nights, int unit) : base(id)
        {
            RentalId = rentalId;
            Start = start;
            Nights = nights;
            Unit = unit;
        }
    }
}
