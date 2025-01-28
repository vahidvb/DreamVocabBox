using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common.Extensions
{
    public static class DateTimeExtensions
    {
        /// <summary>
        /// Converts DateTime to a human-readable string based on the specified pattern (y: years, M: months, w: weeks, d: days, h: hours, m: minutes, s: seconds).
        /// </summary>
        /// <param name="targetTime">The target DateTime to be converted.</param>
        /// <param name="pattern">The pattern specifying which time units to include in the output.</param>
        /// <returns>A human-readable string representation of the time difference.</returns>
        public static string ToHumanReadableTime(this DateTime targetTime, string pattern = "yMwdhms") => FormatTime(targetTime - DateTime.Now, pattern);
        /// <summary>
        /// Converts DateTime to a human-readable string based on the specified pattern (y: years, M: months, w: weeks, d: days, h: hours, m: minutes, s: seconds).
        /// </summary>
        /// <param name="targetTime">The target DateTime to be converted.</param>
        /// <param name="fromTime">The starting DateTime to calculate the difference from.</param>
        /// <param name="pattern">The pattern specifying which time units to include in the output.</param>
        /// <returns>A human-readable string representation of the time difference.</returns>
        public static string ToHumanReadableTime(this DateTime targetTime, DateTime fromTime, string pattern = "yMwdhms") => FormatTime(targetTime - fromTime, pattern);
        private static string FormatTime(TimeSpan delta, string pattern)
        {
            int totalSeconds = Math.Abs((int)delta.TotalSeconds);
            int years = totalSeconds / 31536000; // 365 * 24 * 60 * 60
            totalSeconds %= 31536000;
            int months = totalSeconds / 2592000; // 30 * 24 * 60 * 60
            totalSeconds %= 2592000;
            int days = totalSeconds / 86400; // 24 * 60 * 60
            totalSeconds %= 86400;
            int hours = totalSeconds / 3600; // 60 * 60
            totalSeconds %= 3600;
            int minutes = totalSeconds / 60;
            int seconds = totalSeconds % 60;

            // Convert remaining months and years into days if not included in the pattern
            if (!pattern.Contains("M"))
            {
                days += months * 30;
                months = 0;
            }
            if (!pattern.Contains("y"))
            {
                days += years * 365;
                years = 0;
            }

            // Convert weeks if included in the pattern
            int weeks = 0;
            if (pattern.Contains("w"))
            {
                weeks = days / 7;
                days %= 7;
            }

            // Convert all remaining time units into the highest possible unit if not included in the pattern
            if (!pattern.Contains("d"))
            {
                hours += days * 24;
                days = 0;
            }
            if (!pattern.Contains("h"))
            {
                minutes += hours * 60;
                hours = 0;
            }
            if (!pattern.Contains("m"))
            {
                seconds += minutes * 60;
                minutes = 0;
            }

            var parts = new List<string>();
            if (pattern.Contains("y") && years > 0) parts.Add($"{years} year{(years > 1 ? "s" : "")}");
            if (pattern.Contains("M") && months > 0) parts.Add($"{months} month{(months > 1 ? "s" : "")}");
            if (pattern.Contains("w") && weeks > 0) parts.Add($"{weeks} week{(weeks > 1 ? "s" : "")}");
            if (pattern.Contains("d") && days > 0) parts.Add($"{days} day{(days > 1 ? "s" : "")}");
            if (pattern.Contains("h") && hours > 0) parts.Add($"{hours} hour{(hours > 1 ? "s" : "")}");
            if (pattern.Contains("m") && minutes > 0) parts.Add($"{minutes} minute{(minutes > 1 ? "s" : "")}");
            if (pattern.Contains("s") && seconds > 0) parts.Add($"{seconds} second{(seconds > 1 ? "s" : "")}");

            return string.Join(" and ", parts);
        }
        public static string ToAutoHumanReadableTime(this DateTime targetTime, DateTime? fromTime = null)
        {
            if (fromTime == null)
                fromTime = DateTime.Now;

            TimeSpan delta = targetTime - fromTime.Value;
            int totalSeconds = Math.Abs((int)delta.TotalSeconds);

            if (totalSeconds < 60)
                return "Just now";

            int minutes = totalSeconds / 60;
            if (minutes < 60)
                return $"{minutes} minute{(minutes > 1 ? "s" : "")}";

            int hours = minutes / 60;
            if (hours < 24)
                return $"{hours} hour{(hours > 1 ? "s" : "")}";

            int days = hours / 24;
            if (days < 7)
                return $"{days} day{(days > 1 ? "s" : "")}";

            int weeks = days / 7;
            if (weeks < 4)
                return $"{weeks} week{(weeks > 1 ? "s" : "")}";

            int months = weeks / 4;
            if (months < 12)
                return $"{months} month{(months > 1 ? "s" : "")}";

            int years = months / 12;
            return $"{years} year{(years > 1 ? "s" : "")}";
        }

        public static DateTime ToNotNullable(this DateTime? value) => (DateTime)value;
    }
}
