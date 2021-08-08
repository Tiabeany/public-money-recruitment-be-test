using System;
using System.Collections.Generic;
using System.Linq;

namespace VacationRental.Core.Models
{
    public class Rental : BaseEntity
    {
        public int Units { get; private set; }
        public int PreparationTimeInDays { get; private set; }
        public List<Booking> Bookings { get; private set; }

        public Rental(int units, int preparationTimeInDays, List<Booking> bookings)
        {
            Units = units;
            PreparationTimeInDays = preparationTimeInDays;
            Bookings = bookings;
        }

        /// <summary>
        /// Add a book but first verifies if it doesn't conflict with existing bookings
        /// </summary>
        /// <param name="booking">Booking to be added</param>
        /// <exception cref="ApplicationException">If booking conflicts with existing bookings</exception>
        public void AddBooking(Booking booking)
        {
            if (Bookings.Any(existingBooking =>
                existingBooking.Unit == booking.Unit 
                && (existingBooking.Start <= booking.Start.Date && existingBooking.Start.AddDays(existingBooking.Nights) > booking.Start.Date)
                || (existingBooking.Start < booking.Start.AddDays(booking.Nights) && existingBooking.Start.AddDays(existingBooking.Nights) >= booking.Start.AddDays(booking.Nights))
                || (existingBooking.Start > booking.Start && existingBooking.Start.AddDays(existingBooking.Nights) < booking.Start.AddDays(booking.Nights))))
            {
                throw new ApplicationException("Not available");
            }

            Bookings.Add(booking);
        }
    }
}
