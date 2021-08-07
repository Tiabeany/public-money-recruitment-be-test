using System;

namespace VacationRental.Core.Models
{
    public class Booking : BaseEntity
    {
        public int RentalId { get; set; }
        public DateTime Start { get; set; }
        public int Nights { get; set; }
        public int Unit { get; set; }

        public Rental Rental { get; set; }
    }
}
