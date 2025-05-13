namespace ReuseSystem.Helper.Extensions
{
    public static class StringExtension
    {
        public static string PaddingStringLeft(this string s, string pattern, int length)
        {
            string result = s;
            while (result.Length < length)
            {
                result = pattern + result;
            }

            return result;
        }
    }
}