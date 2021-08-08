using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using VacationRental.Api.Models;
using VacationRental.Application.Services.Interfaces;

namespace VacationRental.Api.Controllers
{
    [Route("api/v1/calendar")]
    [ApiController]
    public class CalendarController : ControllerBase
    {
        private readonly ICalendarService _calendarService;

        public CalendarController(ICalendarService calendarService)
        {
            _calendarService = calendarService;
        }

        [HttpGet]
        public CalendarViewModel Get(int rentalId, DateTime start, int nights)
        {
            var calendar = _calendarService.Get(rentalId, start, nights);
            return new CalendarViewModel()
            {
                Dates = calendar.Dates.Select(d => 
                    new CalendarDateViewModel
                    {
                        Date = d.Date,
                        Bookings = d.Bookings.Select(b => new CalendarBookingViewModel() { Id = b.Id }).ToList()
                    }
                ).ToList(),
                RentalId = calendar.RentalId
            };
        }
    }
}
