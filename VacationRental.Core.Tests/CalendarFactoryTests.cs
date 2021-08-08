using System;
using System.Collections.Generic;
using System.Linq;
using VacationRental.Core.Factories;
using VacationRental.Core.Models;
using Xunit;

namespace VacationRental.Core.Tests
{
    public class CalendarFactoryTests
    {
        [Fact]
        public void CreateCalendar_5NightsAndRentalWith1Unit2NightBooking2PreparationTimeDaysSameStartDate_CalendarWith1FreeDay2BookingDates2PreparationTimes()
        {
            var now = DateTime.Now;
            var mockedBookings = new List<Booking>
            {
                new Booking(1, now, 2, 1)
            };
            var mockedRental = new Rental(1, 1, 2, new List<Booking>(), new List<PreparationTime>());

            foreach (var mockedBooking in mockedBookings)
            {
                mockedBooking.Rental = mockedRental;
                mockedRental.AddBooking(mockedBooking);
            }

            var calendar = CalendarFactory.CreateCalendar(5, mockedRental, now);

            var freeDaysCount = calendar.Dates.Where(d => (d.PreparationTimes == null || !d.PreparationTimes.Any()) && (d.Bookings == null || !d.Bookings.Any())).ToList().Count;
            Assert.Equal(1, freeDaysCount);

            var bookingCount = calendar.Dates.Where(d => d.Bookings != null && d.Bookings.Any()).ToList().Count;
            Assert.Equal(2, bookingCount);

            var preparationTimeCount = calendar.Dates.Where(d => d.PreparationTimes != null && d.PreparationTimes.Any()).ToList().Count;
            Assert.Equal(2, preparationTimeCount);
        }
    }
}
