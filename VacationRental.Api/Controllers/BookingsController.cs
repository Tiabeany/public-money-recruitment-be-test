using System;
using System.Collections.Generic;
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

        public BookingsController(IBookingService bookingService)
        {
            _bookingService = bookingService;
        }

        [HttpGet]
        [Route("{bookingId:int}")]
        public BookingViewModel Get(int bookingId)
        {
            var booking = _bookingService.Get(bookingId);
            return new BookingViewModel
            {
                Id = booking.Id,
                Nights = booking.Nights,
                RentalId = booking.RentalId,
                Start = booking.Start
            };
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
