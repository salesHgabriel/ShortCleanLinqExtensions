namespace ShortCleanLinqExtensions.src.Extensions
{
    public static class DatetimeExtension
    {
        public static DateTime Now(Support.DateTime.Type type = Support.DateTime.Type.Utc)
        {
            return type == Support.DateTime.Type.Utc ? DateTime.UtcNow : DateTime.Now;
        }
    }
}