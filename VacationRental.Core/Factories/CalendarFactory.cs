﻿using System;
using System.Collections.Generic;
using VacationRental.Core.Models;

namespace VacationRental.Core.Factories
{
    public static class CalendarFactory
    {
        public static Calendar CreateCalendar(int nights, Rental rental, DateTime start)
        {
            if (nights < 0)
                throw new ApplicationException("Nights must be positive");

            var calendar = new Calendar(new List<CalendarDate>(), rental.Id);

            for (var i = 0; i < nights; i++)
            {
                var date = start.Date.AddDays(i);
                var calendarDate = new CalendarDate(date, new List<CalendarBooking>(), new List<CalendarPreparationTime>());
                foreach (var booking in rental.Bookings)
                {
                    if (booking.Start <= date.Date && booking.Start.AddDays(booking.Nights) > date.Date)
                    {
                        calendarDate.AddCalendarBooking(new CalendarBooking(booking.Id, booking.Unit));
                    }
                }
                foreach (var preparationTime in rental.PreparationTimes)
                {
                    if (preparationTime.Start <= date.Date && preparationTime.Start.AddDays(preparationTime.Nights) > date.Date)
                    {
                        calendarDate.AddCalendarPreparationTime(new CalendarPreparationTime(preparationTime.Unit));
                    }
                }
                calendar.AddCalendarDate(calendarDate);
            }

            return calendar;
        }

        public static Calendar CreateCalendar(object p, int nights, int rentalId, DateTime start)
        {
            throw new NotImplementedException();
        }
    }
}
