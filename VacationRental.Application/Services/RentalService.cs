using VacationRental.Application.Services.Interfaces;
using VacationRental.Core.Models;
using VacationRental.Core.Repositories;

namespace VacationRental.Application.Services
{
    public class RentalService : IRentalService
    {
        private readonly IRentalRepository _rentalRepository;

        public RentalService(IRentalRepository rentalRepository)
        {
            _rentalRepository = rentalRepository;
        }

        public int Add(Rental rental)
        {
            return _rentalRepository.Insert(rental);
        }

        public Rental Get(int id)
        {
            return _rentalRepository.Get(id);
        }

        public int Update(Rental rental)
        {
            var existingRental = _rentalRepository.Get(rental.Id);
            existingRental.UpdatePreparationTimeInDays(rental.PreparationTimeInDays);
            existingRental.UpdateUnits(rental.Units);
            return _rentalRepository.Update(existingRental);
        }
    }
}
