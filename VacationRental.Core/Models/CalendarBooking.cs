namespace VacationRental.Core.Models
{
    public class CalendarBooking
    {
        public int Id { get; private set; }
        public int Unit { get; private set; }

        public CalendarBooking(int id, int unit)
        {
            Id = id;
            Unit = unit;
        }
    }
}
