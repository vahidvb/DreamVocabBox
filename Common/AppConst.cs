using System;

namespace Common
{
    public class AppConst
    {
        public const string Role_User = "User";
        public const string Role_Admin = "Admin";
        public const string Role_Personnel = "Personnel";
        public const string Role_User_Personnel = "User,Personnel";
        public const string AppExceptionTypeName = "AppException";
        public const string GuestPrefix = "مهمان ";
        public const string TokenContentPrefix = "Bearer ";
        private static string recoveryPassText;
        public static string RecoveryPassText
        {
            get
            {
                return recoveryPassText ??= System.IO.File.ReadAllText("RecoveryPass.txt");
            }
        }

        public static int RedisServer { get; set; }
        public static string RedisPassword { get; set; }
        public static string rightToLeft = ((Char)0x200F).ToString();
        public const int scanCount = 9999;
    }
}