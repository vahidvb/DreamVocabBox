namespace Common.Utilities
{
    public static class ConvertNumberToPersianAlphabet
    {
        private static string[] yakan = new string[10] { "صفر", "یک", "دو", "سه", "چهار", "پنج", "شش", "هفت", "هشت", "نه" };
        private static string[] dahgan = new string[10] { "", "", "بیست", "سی", "چهل", "پنجاه", "شصت", "هفتاد", "هشتاد", "نود" };
        private static string[] dahyek = new string[10] { "ده", "یازده", "دوازده", "سیزده", "چهارده", "پانزده", "شانزده", "هفده", "هجده", "نوزده" };
        private static string[] sadgan = new string[10] { "", "یکصد", "دویست", "سیصد", "چهارصد", "پانصد", "ششصد", "هفتصد", "هشتصد", "نهصد" };
        private static string[] basex = new string[5] { "", "هزار", "میلیون", "میلیارد", "تریلیون" };
        private static string getnum3(int num3)
        {
            string s = "";
            int d3, d12;
            d12 = num3 % 100;
            d3 = num3 / 100;
            if (d3 != 0)
                s = sadgan[d3] + " و ";
            if ((d12 >= 10) && (d12 <= 19))
            {
                s = s + dahyek[d12 - 10];
            }
            else
            {
                int d2 = d12 / 10;
                if (d2 != 0)
                    s = s + dahgan[d2] + " و ";
                int d1 = d12 % 10;
                if (d1 != 0)
                    s = s + yakan[d1] + " و ";
                s = s.Substring(0, s.Length - 3);
            };
            return s;
        }
        /// <summary>
        /// تبدیل اعداد به حروف فارسی
        /// </summary>
        /// <param name="number">
        /// ورود عدد با نوع متنی
        /// </param>        
        /// <param name="haveExtension">
        /// خروجی به صورت رتبه بندی (اول،دوم،سوم و...) باشد
        /// </param>
        public static string NumberToAlphabet(string number, bool haveExtension = false)
        {
            string stotal = "";
            if (number == "") return "صفر";
            if (haveExtension)
            {
                if (number == "1") return "اول";
                if (number == "2") return "دوم";
                if (number == "3") return "سوم";
            }
            if (number == "0")
            {
                return yakan[0];
            }
            else
            {
                number = number.PadLeft(((number.Length - 1) / 3 + 1) * 3, '0');
                int L = number.Length / 3 - 1;
                for (int i = 0; i <= L; i++)
                {
                    int b = int.Parse(number.Substring(i * 3, 3));
                    if (b != 0)
                        stotal = stotal + getnum3(b) + " " + basex[L - i] + " و ";
                }
                stotal = stotal.Substring(0, stotal.Length - 3);
            }
            if (haveExtension)
                return stotal.Trim() + "م";
            else
                return stotal.Trim();
        }
        /// <summary>
        /// تبدیل اعداد به حروف فارسی
        /// </summary>
        /// <param name="number">
        /// ورود عدد با نوع عدد صحیح
        /// </param>
        /// <param name="haveExtension">
        /// خروجی به صورت رتبه بندی (اول،دوم،سوم و...) باشد
        /// </param>
        public static string NumberToAlphabet(int number, bool haveExtension = false)
        {
            return NumberToAlphabet(number.ToString(), haveExtension);
        }

    }
}
