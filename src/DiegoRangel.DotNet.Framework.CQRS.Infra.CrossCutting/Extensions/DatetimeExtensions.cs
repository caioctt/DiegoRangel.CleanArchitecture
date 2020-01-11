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
    }
}