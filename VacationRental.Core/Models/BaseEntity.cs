using VacationRental.Core.Models.Interfaces;

namespace VacationRental.Core.Models
{
    public class BaseEntity : IBaseEntity
    {
        public int Id { get; set; }
    }
}
