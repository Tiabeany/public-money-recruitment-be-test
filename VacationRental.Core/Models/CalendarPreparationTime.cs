namespace VacationRental.Core.Models
{
    public class CalendarPreparationTime
    {
        public int Unit { get; private set; }

        public CalendarPreparationTime(int unit)
        {
            Unit = unit;
        }
    }
}
