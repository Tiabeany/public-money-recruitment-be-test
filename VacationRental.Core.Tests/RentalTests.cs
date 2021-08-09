using System;
using System.Collections.Generic;
using System.Linq;
using VacationRental.Core.Models;
using Xunit;

namespace VacationRental.Core.Tests
{
    public class RentalTests
    {
        [Fact]
        public void UpdatePreparationTimeInDays_From1To2Days_PreparationTimesMustBeAdded1Day()
        {
            // First booking 01/01/2000
            var firstBooking = new Booking(1, 1, new DateTime(2000, 01, 01), 1, 1);
            // Second booking 01/03/2000 - 2 days after first prepararation time so first preparation time can be extended 1 day
            var secondBooking = new Booking(2, 1, new DateTime(2000, 01, 04), 1, 1);
            var mockedBookings = new List<Booking>
            {
                firstBooking,
                secondBooking
            };

            // First preparation time 01/02/2000 after first booking
            var firstPreparationTime = new PreparationTime(1, 1, new DateTime(2000, 01, 02), 1, 1);
            // Second preparation time 01/04/2000 after second booking
            var secondPreparationTime = new PreparationTime(2, 1, new DateTime(2000, 01, 05), 1, 1);
            var mockedPreparationTimes = new List<PreparationTime>
            {
                firstPreparationTime,
                secondPreparationTime
            };
            
            var mockedRental = new Rental(1, 1, 1, mockedBookings, mockedPreparationTimes);
            mockedRental.UpdatePreparationTimeInDays(2);

            // Updated preparation times
            var firstRentalPreparationTime = mockedRental.PreparationTimes.First(p => p.Id == 1);
            var secondRentalPreparationTime = mockedRental.PreparationTimes.First(p => p.Id == 2);

            Assert.Equal(2, firstRentalPreparationTime.Nights);
            Assert.Equal(2, secondRentalPreparationTime.Nights);
        }

        [Fact]
        public void UpdatePreparationTimeInDays_From1To2DaysWithBookingConflict_ApplicationExceptionMustBeThrown()
        {
            // First booking 01/01/2000
            var firstBooking = new Booking(1, 1, new DateTime(2000, 01, 01), 1, 1);
            // Second booking 01/03/2000 - 1 day after first prepararation time so first preparation time cannot be extended 1 day
            var secondBooking = new Booking(2, 1, new DateTime(2000, 01, 03), 1, 1);
            var mockedBookings = new List<Booking>
            {
                firstBooking,
                secondBooking
            };

            // First preparation time 01/02/2000 after first booking
            var firstPreparationTime = new PreparationTime(1, 1, new DateTime(2000, 01, 02), 1, 1);
            // Second preparation time 01/04/2000 after second booking
            var secondPreparationTime = new PreparationTime(2, 1, new DateTime(2000, 01, 04), 1, 1);
            var mockedPreparationTimes = new List<PreparationTime>
            {
                firstPreparationTime,
                secondPreparationTime
            };

            var mockedRental = new Rental(1, 1, 1, mockedBookings, mockedPreparationTimes);

            Action action = () => mockedRental.UpdatePreparationTimeInDays(2);
            
            var exception = Assert.Throws<ApplicationException>(action);
            
            Assert.Equal("Not available", exception.Message);

            // Updated preparation times
            var firstRentalPreparationTime = mockedRental.PreparationTimes.First(p => p.Id == 1);
            var secondRentalPreparationTime = mockedRental.PreparationTimes.First(p => p.Id == 2);

            // Nights should not have been updated
            Assert.Equal(1, firstRentalPreparationTime.Nights);
            Assert.Equal(1, secondRentalPreparationTime.Nights);
        }

        [Fact]
        public void UpdatePreparationTimeInDays_From2To1Days_PreparationTimesMustBeSubtracted1Day()
        {
            // First booking 01/01/2000
            var firstBooking = new Booking(1, 1, new DateTime(2000, 01, 01), 1, 1);
            // Second booking 01/03/2000 - 2 days after first prepararation time so first preparation time can be exist for 2 nights
            var secondBooking = new Booking(2, 1, new DateTime(2000, 01, 04), 1, 1);
            var mockedBookings = new List<Booking>
            {
                firstBooking,
                secondBooking
            };

            // First preparation time 01/02/2000 after first booking
            var firstPreparationTime = new PreparationTime(1, 1, new DateTime(2000, 01, 02), 2, 1);
            // Second preparation time 01/04/2000 after second booking
            var secondPreparationTime = new PreparationTime(2, 1, new DateTime(2000, 01, 05), 2, 1);
            var mockedPreparationTimes = new List<PreparationTime>
            {
                firstPreparationTime,
                secondPreparationTime
            };

            var mockedRental = new Rental(1, 1, 2, mockedBookings, mockedPreparationTimes);
            mockedRental.UpdatePreparationTimeInDays(1);

            // Updated preparation times
            var firstRentalPreparationTime = mockedRental.PreparationTimes.First(p => p.Id == 1);
            var secondRentalPreparationTime = mockedRental.PreparationTimes.First(p => p.Id == 2);

            Assert.Equal(1, firstRentalPreparationTime.Nights);
            Assert.Equal(1, secondRentalPreparationTime.Nights);
        }
    }
}
