using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using VacationRental.Api.Models;
using VacationRental.Application.Services.Interfaces;
using VacationRental.Core.Models;

namespace VacationRental.Api.Controllers
{
    [Route("api/v1/rentals")]
    [ApiController]
    public class RentalsController : ControllerBase
    {
        private readonly IRentalService _rentalService;

        public RentalsController(IRentalService rentalService)
        {
            _rentalService = rentalService;
        }

        [HttpGet]
        [Route("{rentalId:int}")]
        public RentalViewModel Get(int rentalId)
        {
            var rental = _rentalService.Get(rentalId);

            return new RentalViewModel()
            {
                Id = rental.Id,
                PreparationTimeInDays = rental.PreparationTimeInDays,
                Units = rental.Units
            };
        }

        [HttpPost]
        public ResourceIdViewModel Post(RentalBindingModel model)
        {
            var id = _rentalService.Add(new Rental(model.PreparationTimeInDays, model.Units, new List<Booking>(), new List<PreparationTime>()));

            return new ResourceIdViewModel
            {
                Id = id
            };
        }
    }
}
