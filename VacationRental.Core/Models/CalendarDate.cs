using System;
using System.Collections.Generic;

namespace VacationRental.Core.Models
{
    public class CalendarDate
    {        
        public DateTime Date { get; private set; }
        public List<CalendarBooking> Bookings { get; private set; }
        public List<CalendarPreparationTime> PreparationTimes { get; private set; }

        public CalendarDate(DateTime date, List<CalendarBooking> bookings, List<CalendarPreparationTime> preparationTimes)
        {
            Date = date;
            Bookings = bookings;
            PreparationTimes = preparationTimes;
        }

        public void AddCalendarBooking(CalendarBooking calendarBooking)
        {
            Bookings.Add(calendarBooking);
        }

        public void AddCalendarPreparationTime(CalendarPreparationTime calendarPreparationTime)
        {
            PreparationTimes.Add(calendarPreparationTime);
        }
    }
}
