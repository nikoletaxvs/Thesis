namespace ThesisOct2023.Helpers
{
    public static class DayPoints
    {
        public static string GetTimeOfDay()
        {
            string timeOfDay = "Closed";
            if (DateTime.Now.Hour > 8 && DateTime.Now.Hour <= 12)
            {
                timeOfDay = "Breakfast";
            }
            else if (DateTime.Now.Hour > 12 && DateTime.Now.Hour <= 17)
            {
                timeOfDay = "Launch";
            }
            else if (DateTime.Now.Hour > 17 && DateTime.Now.Hour <= 20)
            {
                timeOfDay = "Dinner";
            }
            return timeOfDay;
        }

        public static int GetCurrentWeekNumber()
        {
            return Iso8601WeekOfYear.GetIso8601WeekOfYear(DateTime.Now);
        }
        //Assuming Monday is 0
        public static int GetCurrentDayNumber() {
            return (int)(DateTime.Now.DayOfWeek + 6) % 7;
        }

    }
}
