namespace LibraryBookManagement.Utils
{
    public static class Extensions
    {
        public static string FormatTitle(this string input)
        {
            return System.Globalization.CultureInfo.CurrentCulture.TextInfo.ToTitleCase(input.ToLower());
        }
    }
}
