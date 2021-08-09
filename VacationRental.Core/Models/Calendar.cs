using System;
using System.Collections.Generic;

namespace VacationRental.Core.Models
{
    public class Calendar
    {
        public List<CalendarDate> Dates { get; private set; }
        public int RentalId { get; private set; }

        public Calendar(List<CalendarDate> dates, int rentalId)
        { 
            Dates = dates;
            RentalId = rentalId;
        }

        public void AddCalendarDate(CalendarDate calendarDate)
        {
            Dates.Add(calendarDate);
        }
    }
}
