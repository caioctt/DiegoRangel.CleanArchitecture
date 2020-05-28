using System;

namespace DiegoRangel.DotNet.Framework.CQRS.Infra.CrossCutting.Extensions
{
    public static class DatetimeExtensions
    {
        public static int ToAge(this DateTime value)
        {
            var age = DateTime.Now.Year - value.Year;
            if (DateTime.Now.DayOfYear < value.DayOfYear)
                age = age - 1;
            return age;
        }

        public static int MonthsBetweenUntil(this DateTime from, DateTime to)
        {
            var monthDiff = Math.Abs((to.Year * 12 + (to.Month - 1)) - (from.Year * 12 + (from.Month - 1)));

            if (from.AddMonths(monthDiff) > to || to.Day < from.Day)
                return monthDiff - 1;
            return monthDiff;
        }
    }
}