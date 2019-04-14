using System.Globalization;

namespace PeriodicTable.Model.Support
{
    public static class Common
    {
        public static string ToTitleCase(string title)
        {
            return CultureInfo.CurrentCulture.TextInfo.ToTitleCase(title.ToLower());
        }
    }
}
