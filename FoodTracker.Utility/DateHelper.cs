namespace FoodTracker.Utility
{
    public class DateHelper
    {
        private readonly DateTime _dateTime;
        public DateTime FirstDayOfMonth { get; }
        public int FirstDayOfMonthIndex { get; }
        public int DayIndex { get; }
        public int DaysInMonth { get; }
        public int WeeksInMonth { get; }
        public DateHelper(DateTime dateTime)
        {
            _dateTime = dateTime;

            var daysInMonth = DateTime.DaysInMonth(dateTime.Year, dateTime.Month);
            var firstDayOfMonth = new DateTime(dateTime.Year, dateTime.Month, 1);
            var firstDayOfMonthIndex = (int)firstDayOfMonth.DayOfWeek;

            DayIndex = 0 - firstDayOfMonthIndex;
            DaysInMonth = daysInMonth;
            WeeksInMonth = (firstDayOfMonthIndex + daysInMonth) / 7 + 1;
            FirstDayOfMonth = firstDayOfMonth;
            FirstDayOfMonthIndex = firstDayOfMonthIndex;
        }


        public DateTime GetTodayFromDayIndex(int dayIndex)
        {
            return new DateTime(_dateTime.Year, _dateTime.Month, dayIndex + 1);
        }
    }

}
