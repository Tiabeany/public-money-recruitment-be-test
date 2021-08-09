using System;
using System.Collections.Generic;
using System.Text;

namespace VacationRental.Core.Models
{
    public class PreparationTime : Schedule
    {
        public PreparationTime(int id, int rentalId, DateTime start, int nights, int unit) : base(id, rentalId, start, nights, unit)
        {
        }
    }
}
