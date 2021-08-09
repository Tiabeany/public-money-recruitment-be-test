using System;
using System.Collections.Generic;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using VacationRental.Api.Models;
using VacationRental.Application.Services.Interfaces;
using VacationRental.Core.Models;

namespace VacationRental.Api.Controllers
{
    [Route("api/v1/bookings")]
    [ApiController]
    public class BookingsController : ControllerBase
    {
        private readonly IBookingService _bookingService;
        private readonly IMapper _mapper;

        public BookingsController(IBookingService bookingService, IMapper mapper)
        {
            _bookingService = bookingService;
            _mapper = mapper;
        }

        [HttpGet]
        [Route("{bookingId:int}")]
        public BookingViewModel Get(int bookingId)
        {
            var booking = _bookingService.Get(bookingId);
            return _mapper.Map<BookingViewModel>(booking);
        }

        [HttpPost]
        public ResourceIdViewModel Post(BookingBindingModel model)
        {
            var id = _bookingService.Add(new Booking(0, model.RentalId, model.Start, model.Nights, 0));

            return new ResourceIdViewModel
            {
                Id = id
            };
        }
    }
}
