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

        public Rental(int id, int units, int preparationTimeInDays, List<Booking> bookings, List<PreparationTime> preparationTimes) : base(id)
        {
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
            bool isScheduleAvailable = false;

            for (int i = 0; i < Units; i++)
            {
                // Incrementing the booking.Units will make ScheduleAvailableSpecification test for each possible unit
                booking.Unit++;
                if (scheduleAvailable.IsSatisfiedBy(booking))
                {
                    isScheduleAvailable = true;
                    break;
                }
            }

            // If no schedule is available on any unit
            if (!isScheduleAvailable)
            {
                var ex = new ApplicationException("Not available");
                ex.Data.Add("Booking with Invalid Schedule", "The selected dates are not available.");
                throw ex;
            }

            // preparationTimeStart should be the last day of the booking
            var preparationTimeStart = booking.Start.AddDays(booking.Nights);
            // preparationTimeAfterBooking will have the same Unit of the booking
            var preparationTimeAfterBooking = new PreparationTime(0, booking.RentalId, preparationTimeStart, 
                booking.Rental.PreparationTimeInDays, booking.Unit);

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

        private void UpdatePreparationTimes(int daysToAdd)
        {
            foreach (var preparationTime in PreparationTimes)
            {
                preparationTime.Nights += daysToAdd;

                // Since we are updating PreparationTime we should only check if there is no Booking Schedule conflict
                var scheduleAvailable = new ScheduleAvailableSpecification(Bookings.Select(b => (Schedule)b).ToList());
                if (!scheduleAvailable.IsSatisfiedBy(preparationTime))
                {
                    // reverting update
                    preparationTime.Nights -= daysToAdd;
                    throw new ApplicationException("Not available");
                }
            }
        }

        public void UpdatePreparationTimeInDays(int newPreparationTimeInDays)
        {
            if (PreparationTimeInDays != newPreparationTimeInDays)
            {
                var daysDifference = newPreparationTimeInDays - PreparationTimeInDays;
                UpdatePreparationTimes(daysDifference);
            }
        }

        public void UpdateUnits(int newUnits)
        {
            Units = newUnits;
        }
    }
}
