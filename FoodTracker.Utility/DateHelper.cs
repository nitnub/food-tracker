namespace FoodTracker.Utility
{
    public class DateHelper
    {
        private readonly int CALENDAR_BUFFER = 7;
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
            if (dayIndex < 0 || dayIndex > DaysInMonth - 1)
            {
                return FirstDayOfMonth.AddDays(dayIndex);
            }
            return new DateTime(_dateTime.Year, _dateTime.Month, dayIndex + 1);
        }

        public DateTime GetLastMonthPad()
        {
            return FirstDayOfMonth.AddDays(-CALENDAR_BUFFER);
        }

        public DateTime GetNextMonthPad()
        {
            return FirstDayOfMonth.AddDays(DaysInMonth + CALENDAR_BUFFER);
        }

        public int GetDayLabel(int index)
        {
            return FirstDayOfMonth.AddDays(index).Day;
        }
    }
}
