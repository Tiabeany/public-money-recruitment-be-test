using System;
using System.Collections.Generic;
using System.Linq;
using VacationRental.Core.Specifications;

namespace VacationRental.Core.Models
{
    public class Rental : BaseEntity
    {
        public int Units { get; private set; }
        public int PreparationTimeInDays { get; private set; }
        public List<Booking> Bookings { get; private set; }
        public List<PreparationTime> PreparationTimes { get; private set; }

        // Here we concatenate Bookings and PreparationTimes but make sure they are casted as IEnumerable<Schedule> so they can be used on the ScheduleSpecification
        private List<Schedule> _allExistingSchedules => Bookings.Select(b => (Schedule)b).Concat(PreparationTimes.Select(p => (Schedule)p)).ToList();

        public Rental(int id, int units, int preparationTimeInDays, List<Booking> bookings, List<PreparationTime> preparationTimes)
        {
            Id = id;
            Bookings = bookings;
            PreparationTimes = preparationTimes;
            PreparationTimeInDays = preparationTimeInDays;
            Units = units;
        }

        /// <summary>
        /// Add a book but first verifies if it doesn't conflict with existing bookings and preparation times
        /// </summary>
        /// <param name="booking">Booking to be added</param>
        /// <exception cref="ApplicationException">If booking conflicts with existing bookings</exception>
        public void AddBooking(Booking booking)
        {
            var scheduleAvailable = new ScheduleAvailableSpecification(_allExistingSchedules);
            if (!scheduleAvailable.IsSatisfiedBy(booking))
            {
                var ex = new ApplicationException("Not available");
                ex.Data.Add("Booking with Invalid Schedule", "The selected dates are not available.");
                throw ex;
            }

            var preparationTimeAfterBooking = new PreparationTime(booking.RentalId, booking.Start.AddDays(booking.Nights), booking.Rental.PreparationTimeInDays, booking.Unit);

            try
            {
                AddPreparationTime(preparationTimeAfterBooking);
            }
            catch (ApplicationException ex)
            {
                ex.Data.Add("Booking with Invalid Schedule", "The selected dates are not available due to lack of preparation time for the next booking.");
                throw;
            }

            Bookings.Add(booking);
        }

        /// <summary>
        /// Add a preparation time but first verifies if it doesn't conflict with existing bookings and preparation times
        /// </summary>
        /// <param name="preparationTime">PreparationTime to be added</param>
        /// <exception cref="ApplicationException">If preparation time conflicts with existing bookings</exception>
        private void AddPreparationTime(PreparationTime preparationTime)
        {
            var scheduleAvailable = new ScheduleAvailableSpecification(_allExistingSchedules);
            if (!scheduleAvailable.IsSatisfiedBy(preparationTime))
            {
                throw new ApplicationException("Not available");
            }

            PreparationTimes.Add(preparationTime);
        }
    }
}
