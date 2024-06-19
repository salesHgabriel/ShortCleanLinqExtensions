namespace ShortCleanLinqExtensions.src.Extensions
{
    public static class DatetimeExtension
    {
        public static DateTime Now(Support.DateTime.Type type = Support.DateTime.Type.Utc)
        {
            return type == Support.DateTime.Type.Utc ? DateTime.UtcNow : DateTime.Now;
        }
        /// <summary>
        /// Create diffences two datetimes and return timespan
        /// You can get diffence.Days
        /// You can get diffence.Hours
        /// You can get diffence.Minutes
        /// You can get diffence.Seconds
        /// You can get diffence.Milliseconds
        /// You can get diffence.Ticks
        /// </summary>
        /// <param name="datetime"></param>
        /// <param name="anotherDateTime"></param>
        /// <returns></returns>
        public static TimeSpan Diff(this DateTime datetime, DateTime anotherDateTime) => datetime - anotherDateTime;

    }
}