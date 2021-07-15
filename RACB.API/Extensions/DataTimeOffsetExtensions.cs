﻿using System;

namespace RACB.API.Extensions
{
    public static class DataTimeOffsetExtensions
    {
        public static int GetCurrentAge(this DateTimeOffset dob)
        {
            var age = DateTime.UtcNow.Year - dob.Year;

            if (DateTime.UtcNow.DayOfYear < dob.DayOfYear)
            {
                age--;
            }

            return age;
        }
    }
}
