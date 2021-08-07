using System;

namespace VacationRental.Api.Models
{
    public class PreparationTimeBindingModel
    {
        public int Id { get; set; }
        public DateTime Start { get; set; }
        public int Nights { get; set; }
        public int Unit { get; set; }
    }
}
