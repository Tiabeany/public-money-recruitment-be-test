using AutoMapper;
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
        private readonly IMapper _mapper;

        public RentalsController(IRentalService rentalService, IMapper mapper)
        {
            _rentalService = rentalService;
            _mapper = mapper;
        }

        [HttpGet]
        [Route("{rentalId:int}")]
        public RentalViewModel Get(int rentalId)
        {
            var rental = _rentalService.Get(rentalId);
            return _mapper.Map<RentalViewModel>(rental);
        }

        [HttpPost]
        public ResourceIdViewModel Post(RentalBindingModel model)
        {
            var id = _rentalService.Add(new Rental(0, model.Units, model.PreparationTimeInDays, new List<Booking>(), new List<PreparationTime>()));

            return new ResourceIdViewModel
            {
                Id = id
            };
        }

        [HttpPut]
        [Route("{rentalId:int}")]
        public ResourceIdViewModel Put(int rentalId, RentalBindingModel model)
        {
            var id = _rentalService.Update(new Rental(rentalId, model.Units, model.PreparationTimeInDays, new List<Booking>(), new List<PreparationTime>()));

            return new ResourceIdViewModel
            {
                Id = id
            };
        }
    }
}
