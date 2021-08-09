using System;
using System.Collections.Generic;
using System.Linq;
using AutoMapper;
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
        private readonly IMapper _mapper;

        public CalendarController(ICalendarService calendarService, IMapper mapper)
        {
            _calendarService = calendarService;
            _mapper = mapper;
        }

        [HttpGet]
        public CalendarViewModel Get(int rentalId, DateTime start, int nights)
        {
            var calendar = _calendarService.Get(rentalId, start, nights);
            return _mapper.Map<CalendarViewModel>(calendar);
        }
    }
}
