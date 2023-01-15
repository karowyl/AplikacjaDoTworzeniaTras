using System;

namespace RunnerApp.Extensions
{
    public static class DataTimeExtensions
    {
        public static int CalculateAge(this DateTime data )
        {
            var today = DateTime.Today;
            var age = today.Year - data.Year;

            if(data.Date > today.AddYears(-age))
            {
                age--;
            }
            return age;
        }
    }
}
