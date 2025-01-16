using Common.Utilities;
using System;
using System.Globalization;

namespace Common.Extensions
{
    public static class StringExtensions
    {
        public static string ToLowerTrim(this string input) => input.ToLower().Trim();
        public static string ToUppercaseFirst(this string input)
        {
            if (string.IsNullOrWhiteSpace(input))
                return input;
            if (input.Length == 1)
                return input.ToUpper();
            return char.ToUpper(input[0]) + input.Substring(1);
        }
        public static string ToPascalCase(this string input)
        {
            if (string.IsNullOrWhiteSpace(input))
                return input;
            var words = input.Split(new char[] { ' ', '-', '_', '.' }, StringSplitOptions.RemoveEmptyEntries);
            for (int i = 0; i < words.Length; i++)
                words[i] = CultureInfo.CurrentCulture.TextInfo.ToTitleCase(words[i].ToLower());
            return string.Join(" ", words);
        }
        public static bool IsEmpty(this string value) => value == null || value.Trim() == "";
        public static Guid ToGuid(this string value) => new Guid(value);
        public static bool HasValue(this string value, bool ignoreWhiteSpace = true)
        {
            return ignoreWhiteSpace ? !string.IsNullOrWhiteSpace(value) : !string.IsNullOrEmpty(value);
        }

        public static int ToInt(this string value)
        {
            return Convert.ToInt32(value);
        }

        public static decimal ToDecimal(this string value)
        {
            return Convert.ToDecimal(value);
        }

        public static string ToNumeric(this int value)
        {
            return value.ToString("N0"); //"123,456"
        }

        public static string ToNumeric(this decimal value)
        {
            return value.ToString("N0");
        }

        public static string ToCurrency(this int value)
        {
            return value.ToString("C0");
        }

        public static string ToCurrency(this decimal value)
        {
            return value.ToString("C0");
        }

        public static string Fa2En(this string str)
        {
            return str.Replace("۰", "0")
                .Replace("۱", "1")
                .Replace("۲", "2")
                .Replace("۳", "3")
                .Replace("۴", "4")
                .Replace("۵", "5")
                .Replace("۶", "6")
                .Replace("۷", "7")
                .Replace("۸", "8")
                .Replace("۹", "9")
                //iphone numeric
                .Replace("٠", "0")
                .Replace("١", "1")
                .Replace("٢", "2")
                .Replace("٣", "3")
                .Replace("٤", "4")
                .Replace("٥", "5")
                .Replace("٦", "6")
                .Replace("٧", "7")
                .Replace("٨", "8")
                .Replace("٩", "9");
        }

        public static string FixPersianChars(this string str)
        {
            return str.Replace("ﮎ", "ک")
                .Replace("ﮏ", "ک")
                .Replace("ﮐ", "ک")
                .Replace("ﮑ", "ک")
                .Replace("ك", "ک")
                .Replace("ي", "ی")
                .Replace(" ", " ")
                .Replace("‌", " ")
                .Replace("ھ", "ه");//.Replace("ئ", "ی");
        }
    }
}