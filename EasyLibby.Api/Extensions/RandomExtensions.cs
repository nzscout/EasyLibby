namespace EasyLibby.Api.Extensions
{
    public static class RandomExtensions
    {
        public static DateTime GetRandomDate(this Random random, int minYear = 1900, int maxYear = 2024)
        {
            var year = random.Next(minYear, maxYear);
            var month = random.Next(1, 12);
            var noOfDaysInMonth = DateTime.DaysInMonth(year, month);
            var day = random.Next(1, noOfDaysInMonth);

            return new DateTime(year, month, day);
        }
    }
}
