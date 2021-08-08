using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using VacationRental.Core.Models;

namespace VacationRental.Core.Specifications
{
    public class ScheduleAvailableSpecification : Specification<Schedule>
    {
        private List<Schedule> _existingSchedules;

        public ScheduleAvailableSpecification(List<Schedule> existingSchedules)
        {
            _existingSchedules = existingSchedules;
        }

        public override Expression<Func<Schedule, bool>> ToExpression()
        {
            return schedule => !_existingSchedules.Any(existingBooking =>
                        existingBooking.Unit == schedule.Unit
                        && (existingBooking.Start <= schedule.Start.Date && existingBooking.Start.AddDays(existingBooking.Nights) > schedule.Start.Date)
                        || (existingBooking.Start < schedule.Start.AddDays(schedule.Nights) && existingBooking.Start.AddDays(existingBooking.Nights) >= schedule.Start.AddDays(schedule.Nights))
                        || (existingBooking.Start > schedule.Start && existingBooking.Start.AddDays(existingBooking.Nights) < schedule.Start.AddDays(schedule.Nights)));
        }
    }
}
