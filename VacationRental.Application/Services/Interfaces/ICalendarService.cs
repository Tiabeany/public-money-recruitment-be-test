using System;
using VacationRental.Core.Models;

namespace VacationRental.Application.Services.Interfaces
{
    public interface ICalendarService
    {
        Calendar Get(int rentalId, DateTime start, int nights);
    }
}
