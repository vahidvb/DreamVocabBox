using System.ComponentModel.DataAnnotations;

namespace Common
{
    public enum ApiResultStatusCode
    {
        /// <summary>
        /// عملیات با موفقیت انجام شد
        /// </summary>
        [Display(Name = "عملیات با موفقیت انجام شد")]
        Success = 0,

        /// <summary>
        /// <para>علت: یک خطا ناشناخته و هندل نشده رخ داده است</para>
        /// <para>راه حل : باید اطلاعات لاگ بررسی شود</para>
        /// </summary>
        [Display(Name = "خطایی رخ داده است")]
        UnHandledError = 1,

        /// <summary>
        /// پارامتر های ارسالی معتبر نیستند
        /// </summary>
        [Display(Name = "پارامتر های ارسالی معتبر نیستند")]
        BadRequest = 2,

        /// <summary>
        /// <para> علت: برای تبدیل استتوس پیدا نشدن به پیدا نشدن در پروژه ما </para>
        /// </summary>
        NotFound = 3,

        /// <summary>
        /// <para>علت: تاریخ انقضا و یا عدم دسترسی این توکن و یا مشکلی در توکن ارسال شده دارد</para>
        /// <para>راه حل : باید دوباره لاگین شد و یک توکن جدید گرفته شود</para>
        /// </summary>
        [Display(Name = "خطای احراز هویت")]
        UnAuthorized = 4,

        /// <summary>
        /// <para>علت: آیتمی در پیدا کردن چندین موجودیت پیدا نشد</para>
        /// <para>راه حل : باید مدلهایی را در ورودی درخواست کرد که در دیتابیس وجود دارد</para>
        /// </summary>
        NotFoundAtFindRange = 5,

        /// <summary>
        /// <para>علت: در هنگام ثبت یک موجودی قبلا این موجودی وجو داشته و امکان ثبت مجدد آن نیست </para>
        /// <para>راه حل : نباید موجودی که وجود دارد را دوباره ثبت کرد</para>
        /// </summary>
        [Display(Name = "این مورد قبلا ثبت شده است")]
        EntityExists = 6,

        /// <summary>
        /// <para>علت: در هنگام ثبت یک کاربر قبلا این کاربر وجو داشته و امکان ثبت مجدد آن نیست </para>
        /// <para>راه حل : نباید کاربری که وجود دارد را دوباره ثبت کرد</para>
        /// </summary>
        [Display(Name = "این کاربر قبلا ثبت شده است")]
        ExistUser = 7,

        /// <summary>
        /// <para>علت: در هنگام لاگین برای این کاربر رل یوزر غیرفعال شده و یا ثبت نشده </para>
        /// <para>راه حل : فقط افرادی که رل یوزر دارند میتواند لاگین با رمز یکبار مصرف کنند</para>
        /// </summary>
        RoleNotQulifiedForOtpLogin = 8,

        /// <summary>
        /// <para>علت: در هنگام گرفتن رل برای ساختن توکن لاگین ، رلی برای این کاربر ثبت نشده بود</para>
        /// <para>راه حل : ساخت این کاربر اشتباه بوده و باشد چک کرد چرا رلی ندارد چون هر کاربر حداقل یک رل دارد</para>
        /// </summary>
        UserHasNoRole = 9,

        /// <summary>
        /// <para>علت: در هنگام ثبت رل برای کاربر رل مورد نظر در دیتابیس وجود ندارد</para>
        /// <para>راه حل : باید یک رل مجاز که در دیتابیس وجود دارد را برای این کاربر وارد کرد</para>
        /// </summary>
        RoleNotExists = 10,

        /// <summary>
        /// <para>علت: در هنگام ثبت کاربر رلی برای آن وارد نشده</para>
        /// <para>راه حل : برای ثبت یک کاربر حتما حداقل یک رل باید وارد شود</para>
        /// </summary>
        RoleParameterIsEmpty = 11,

        /// <summary>
        /// <para>علت: در هنگام ثبت رل در ثبت کاربر خطا در اعطا رل رخ داد</para>
        /// <para>راه حل : باید لاگ بررسی شود</para>
        /// </summary>
        CantSetRoleToUser = 12,

        /// <summary>
        /// <para>علت: در هنگام درخواست اسمس فیلد شماره موبایل خالی میباشد</para>
        /// <para>راه حل : باید برای درخواست اسمس شماره موبایل را وارد کرد</para>
        /// </summary>
        MobileNoIsEmpty = 13,

        /// <summary>
        /// <para>علت: کاربر مورد نظر وجود ندارد</para>
        /// <para>راه حل : باید اطلاعاتی را وارد کرد که کاربری قبلا با آن اطلاعات ثبت شده باشد</para>
        /// </summary>
        [Display(Name = "کاربر مورد نظر وجود ندارد")]
        UserNotExist = 14,

        /// <summary>
        /// <para>علت: در هنگام لاگین رمز یکبار مصرف وارد شده منقضی شده است</para>
        /// <para>راه حل : باید یکبار دیگر رمز یکبار مصرف درخواست کرد و وارد کرد</para>
        /// </summary>
        [Display(Name = "رمز منقضی شده است")]
        OtpExpired = 15,

        /// <summary>
        /// <para>علت: در هنگام لاگین کلمه عبور وارد شده اشتباه میباشد</para>
        /// <para>راه حل : باید کلمه عبور صحیح را وارد کرد</para>
        /// </summary>
        [Display(Name = "کلمه عبور اشتباه میباشد")]
        wrongPassword = 16,

        /// <summary>
        /// <para>علت:در هنگام حذف موجودیت یافت نشد</para>
        /// <para>راه حل : باید یک موجودیت که در دیتابیس وجود دارد را وارد کرد</para>
        /// </summary>
        [Display(Name = "یافت نشد")]
        NotFoundAtDelete = 17,

        /// <summary>
        /// <para>علت:در هنگام آپدیت موجودیت یافت نشد</para>
        /// <para>راه حل : باید موجودیتهای وارده برای اپدیت را بررسی و موحودیتهایی که وجود دارد را وارد کرد</para>
        /// </summary>
        NotFoundAtUpdate = 18,

        /// <summary>
        /// <para>علت:در هنگام حذف رل کاربر مورد نظر پیدا نشد</para>
        /// <para>راه حل : باید کاربر صحیح را وارد کرد</para>
        /// </summary>
        [Display(Name = "کاربر مورد نظر پیدا نشد")]
        CantFindUserForRemoveRole = 19,

        /// <summary>
        /// <para>علت:کلمه عبور بیش از حد مجاز اشتباه وارد شده است و به این علت کد منقضی شد.</para>
        /// <para>راه حل : باید مجددا کد دریافت نمایید</para>
        /// </summary>
        [Display(Name = "بعلت وارد کردن کد اشتباه کد منقضی شد. لطفا مجددا کد دریافت نمایید")]
        WrongOtpTime = 20,

        /// <summary>
        /// <para>علت:موجودیت در هنگام پیدا کردن یافت نشد</para>
        /// <para>راه حل : باید یک موجودیت که در دیتابیس وجود دارد را وارد کرد</para>
        /// </summary>
        [Display(Name = "پیدا نشد")]
        NotFoundAtFind = 21,

        /// <summary>
        /// <para>علت:موجودیت در هنگام پیدا کردن بوسیله آیدی یافت نشد</para>
        /// <para>راه حل : باید یک موجودیت که در دیتابیس وجود دارد را وارد کرد</para>
        /// </summary>
        [Display(Name = "پیدا نشد")]
        NotFoundAtGetById = 22,

        /// <summary>
        /// <para>علت:یک خطا ناشناخته در هنگام آپدیت رخ داده</para>
        /// <para>راه حل : باید لاگ بررسی شود</para>
        /// </summary>
        [Display(Name = "خطا در بروز رسانی")]
        ErrorAtServerUpdating = 23,

        /// <summary>
        /// <para>علت:موجودیت پیدا نشد</para>
        /// <para>راه حل : باید یک موجودیت معتبر را وارد کرد</para>
        /// </summary>
        [Display(Name = "پیدا نشد")]
        NotFoundAtFindAll = 24,

        /// <summary>
        /// <para>علت:در پارامترهای ورودی اکشن یک یا چند پارامتر مورد نیاز وارد نشده است</para>
        /// <para>راه حل : باید یک موجودیت معتبر را وارد کرد</para>
        /// </summary>
        OneRequairedParameterIsNull = 25,

        /// <summary>
        /// <para>علت:در هنگام حذف دسته ای یکی از آیتمها پیدا نشد</para>
        /// <para>راه حل : باید موجودیت هایی که در دیتابیس موجود هست را وارد کرد</para>
        /// </summary>
        NotFoundAtDeleteRange = 26,

        /// <summary>
        /// <para>علت:در هنگام آپدیت دسته ای یکی از آیتمها پیدا نشد</para>
        /// <para>راه حل : باید موجودیت هایی که در دیتابیس موجود هست را وارد کرد</para>
        /// </summary>
        NotFoundAtUpdateRange = 27,

        /// <summary>
        /// <para>علت:در هنگام آپدیت یک یا چند پارامتر ارسال شده صحیح نمیباشد</para>
        /// <para>راه حل : باید پارامترهای ارسالی را چک کرد و پامتر نامعتبر را حذف کزد </para>
        /// </summary>
        [Display(Name = "پارامتر های ارسالی معتبر نیستند")]
        ParameterIsNotExist = 28,

        /// <summary>
        /// <para>علت:در هنگام آپدیت و چک کردن اینکه آیا پارامتر مقدار دیفالت آن پراپرتی رو دارد یک یا چند پارامتر با نوعهای دیفالتی که چک کردیم متفاوت بودند  </para>
        /// <para>راه حل : باید چک کردن این نوع پارامتر که وچود نداشت را به کد اضافه کنیم </para>
        /// </summary>
        CantDetectIfParameterIsDefultAtUpdateDTO = 29,

        /// <summary>
        /// <para>علت: مشکلی در توکن ارسال شده وجود دارد</para>
        /// <para>راه حل : باید یک توکن جدید گرفته شود</para>
        /// </summary>
        [Display(Name = "خطای احراز هویت")]
        AuthenticateFailure = 30,

        /// <summary>
        /// <para>علت:کاربر رل مورد نیاز برای فراخوانی این سرویس را ندارد </para>
        /// <para>راه حل : باید توکنی را وارد کرد که رل مورد نظر را دارد</para>
        /// </summary>
        [Display(Name = "عدم دسترسی")]
        DontAllowAccessThisResource = 31,

        /// <summary>
        /// <para>علت: در هنگام آپدیت کردن اطلاعات ورود کاربر در زمان چک کردن توکن کاربر پیدا نشد - کاربری که این توکن برایش صادر شده حذف شده است</para>
        /// <para>راه حل : باید ریکوئست را با توکن جدیدی که از لاگین گرفته میشود ارسال کرد</para>
        /// </summary>
        UserNotExistAtAuthorizatin = 32,

        /// <summary>
        /// <para>علت: در هنگام آپدیت کردن دسته ای آیدی یک یا چند پارامتر نامعتبر میباشد و یا آن پارامتر غیر فعال میباشد</para>
        /// <para>راه حل : یک آیدی معتبر باید وارد کرد</para>
        /// </summary>
        InvalidIdAtUpdateRange = 33,

        /// <summary>
        /// <para>توکن ریکوئست فعال  نیست و یا وجود ندارد</para>
        /// <para>راه حل : باید یک توکن معتبر وارد کرد</para>
        /// </summary>
        InvalidToken = 34,

        /// <summary>
        /// <para>علت: در آپدیت یکی از آیدی های فرستاده شده معتبر نمیباشد</para>
        /// <para>راه حل : باید آیدی معتبر وارد کرد</para>
        /// </summary>
        IdIsInvalidAtUpdate = 35,

        /// <summary>
        /// <para>علت: در هنگام گرفتن جدول از توکن جدول با نوع مورد نظر یافت نشد </para>
        /// <para>راه حل : باید علت اینکه جدول با نوع تعیین شده وجود ندارد را بررسی کرد</para>
        /// </summary>
        CantFindUserTable = 36,

        /// <summary>
        /// <para>علت:توکن ارسال شده فاقد کلِیم میباشد </para>
        /// <para>راه حل : باید بررسی کرد چرا توکن کلیم ندارد </para>
        /// </summary>
        UserHasNoClaims = 37,

        /// <summary>
        /// <para>علت:در هنگام ثبت توکن قبلا توکن  </para>
        /// <para>راه حل : باید بررسی کرد چرا توکن کلیم ندارد </para>
        /// </summary>
        TokenExist = 38,

        /// <summary>
        /// <para>علت:در هنگام حذف رول یکی از رول ها وجود ندارد   </para>
        /// <para>راه حل : باید رول های معتبر وارد کرد </para>
        /// </summary>
        NotFoundAtRemoveRole = 39,

        /// <summary>
        /// <para>علت :قبل از منقضی شدن کد درخواست کد مجدد داده شد   </para>
        /// <para>راه حل : باید تا اتمام زمان منقضی شدن کد صبر کنید و بعد درخواست کد کنسد و یا از کد موجود استفاده نمایید </para>
        /// </summary>
        ActiveOtpExist = 40,

        /// <summary>
        /// <para> علت : کد یکبار مصرف اشتباه است</para>
        /// </summary>
        [Display(Name = "کد عبور اشتباه میباشد")]
        WrongOtp = 41,

        /// <summary>
        /// <para>علت: در هنگام آپدیت پراپرتی وارد شده در پراپرتی های مجاز برای آپدیت وجود ندارد </para>
        /// <para>راه حل : باید یا آن پراپرتی را حذف کرد و یا یک پراپرتی مجاز وارد کرد</para>
        /// </summary>
        PropertyNotFound = 42,

        /// <summary>
        /// <para>در هنگام بررسی وجود آیتم آن یافت نشد</para>
        /// </summary>
        NotFoundAtAny = 43,

        /// <summary>
        /// <para>در هنگام بررسی وجود آیتم آن وجود داشت</para>
        /// </summary>
        ItemFoundAtAny = 44,

        /// <para>علت: فایل با موفقیت بارگذاری شد</para>
        /// </summary>
        [Display(Name = "فایل با موفقیت بارگذاری شد")]
        UploadFile = 45,

        /// <summary>
        /// <para>علت:چون این فایل در سرور موجود هست دوباره نمیتوان این فایل را آپلود کرد</para>
        /// <para>راه حل : باید یک فایل جدید آپلود و یا فایل مورد نظر را آپدیت کرد</para>
        /// </summary>
        [Display(Name = "این فایل موجود میباشد")]
        FileExits = 46,

        /// <summary>
        /// <para>علت: مدل جدیدی برای آپدیت کردن ارسال نشده</para>
        /// <para>راه حل :باید آبجکتی شامل پراپرتی های تغییر کرده به ورودی این متد ارسال شود </para>
        /// </summary>
        UpdateModelIsEmpty = 47,

        [Display(Name = "فایل مورد نظر یافت نشد")]
        /// <summary>
        /// <para>علت: در هنگام حدف فایل فایلی یافت نشد</para>
        /// </summary>
        FileNotFoundAtDelete = 48,

        /// <summary>
        /// <para>علت : کلیم مورد نظر قبلا به صورت دیفالت  به کلیم ها اضافه شده و امکان اضافه کردن مجدد آن نیست  </para>
        /// </summary>
        InvalidClaim = 49,

        /// <summary>
        /// <para> علت : این سرویس یکی از سرویسهاس کلاس کراد کنترلر میباشد که برای این کنترلر تعریف نشده و مسدود شده است</para>
        /// <para> راه حل : چون این سرویس جزو سرویس های در اختیار فرونت قرار داده شده نیست باید بررسی کرد از کجا کال میشود که احتمال اتک وجود دارد</para>
        /// </summary>
        ServiceNotFound = 50,

        [Display(Name = "آیتم مورد نظر یافت نشد")]
        /// <summary>
        /// <para> علت: آیتم مورد نظر وجود ندارد</para>
        /// </summary>
        EntityNotFound = 51,

        [Display(Name = "ایمیل وارد شده با ایمیل ثبت شده تطبیق ندارد")]
        /// <summary>
        /// <para> علت: ایمیل وارد شده با ایمیلی که قبلا برای این یوزر ثبت شده تطبیق ندارد</para>
        /// </summary>
        EmailIsNotCorrect = 52,

        /// <summary>
        /// <para>علت : یکی از پارامترهای ورودی در لیست پارامترهای معتبر برای آپدیت نمیباشد</para>
        /// </summary>
        InvalidParameterAtUpdate = 53,

        [Display(Name = "پیامک با موفقیت ارسال شد")]
        SMSSent = 54,

        [Display(Name = "ایمیل با موفقیت فعالسازی گردید")]
        /// <summary>
        /// <para>علت : کد وریفای ایمیل صحیح بوده و ایمیل وریفای شد</para>
        /// </summary>
        EmailVerified = 55,

        [Display(Name = "کدی ثبت نشده است")]
        /// <summary>
        /// <para>علت : کد یکبار مصرف  مورد نظر برای این کاربر درخواست نشده</para>
        /// </summary>
        CodeNotExist = 56,

        /// <summary>
        /// <para> علت : فیلدی که برای آپدیت کردن فرستاده شده است شامل کلمه آیدی میباشد</para>
        /// <para>فیلدی که برای آپدیت فرستاده میشود نیابد دارای کلمه آپدیت باشد</para>
        /// </summary>
        FieldContainsId = 57,

        /// <summary>
        /// <para>علت: در کنترلر کراد این متد اجازه داده نشده تا از بیرون بهش دسترسی داشته باشیم</para>
        /// <para>راه حل : برای اینکه بتوان از این متد استفاده کرد باید نام متد مورد نظر را در متد اکتیو متد قرار داد</para>
        /// </summary>
        ThisMethodNotAllowed = 58,

        /// <summary>
        /// <para>علت: این سرویس به صورت غیر عادی کال شده و نباید اینگونه کال شده باشد</para>
        /// <para>راه حل : چک شود چرا در این شرایط کال شده است</para>
        /// </summary>
        LuckyWheelNotQualified = 59,




        ///////////////////////////////////////////////////////////////////////////////////////////////




        /// <summary>
        /// <para>علت:در هنگام ساخت یک سوال دسته بندی وارد شده معتبر نیست</para>
        /// <para>راه حل:باید یک دسته بندی معتبر را وارد کرد</para>
        /// </summary>
        [Display(Name = "دسته بندی مورد نظر یافت نشد")]
        NotFoundCategory = 200,

        /// <summary>
        /// <para>علت: در هنگام آپدیت سوال دسته بندی وارد شده معتبر نیست</para>
        /// <para>راه حل : باید یک دسته بندی که در دیتابیس وجود دارد را وارد کرد</para>
        /// </summary>
        [Display(Name = "دسته بندی مورد نظر یافت نشد")]
        CategoryIsnotExist = 202,

        /// <summary>
        /// <para>علت: در هنگام گرفتن سوال ، سوال مورد نظر پیدا نشد</para>
        /// <para>راه حل : باید یک آیدی صحیح برای پیدا کردن سوال وارد شود</para>
        /// </summary>
        NotFoundQuestion = 203,

        /// <para>علت: در ایجاد دسته ای یکی از دسته ها پیدا نشد</para>
        /// <para>راه حل : باید یک دسته بندی معتبر وارد شود</para>
        /// </summary>
        [Display(Name = "یکی از دسته بندی ها درست نمیباشد")]
        CategoryRangeIsNotExit = 204,

        /// <para>علت: در آپدیت یک یا چند دسته بندی های وارد شده معتبر نمیباشد</para>
        /// <para>راه حل : باید یک دسته بندی معتبر وارد شود</para>
        /// </summary>
        [Display(Name = "یک یا چند دسته بندی وارد شده معتبر نمیباشد")]
        CategoryIsNotValidAtUpdateRange = 205,

        /// <para>علت: در هنگام ملحق شدن به اتاق بازی،اتاق مورد نظر پیدا نشد</para>
        /// <para>راه حل : باید یک ورودی معتبر وارد شود</para>
        /// </summary>
        RoomIsNotExist = 206,

        /// <summary>
        /// <para>علت: برای کانکشن استرینگ این هاب این پلیر قبلا ثبت شده</para>
        /// </summary>
        PlayerExist = 207,


        [Display(Name = "بازیی یافت نشد.")]
        /// <summary>
        /// <para>علت :بازیی ثبت نشده >
        /// </summary>
        NotFoundAnyGame = 208,

        [Display(Name = "سکه شما کافی نیست")]
        /// <summary>
        /// <para>علت : مقدار سکه برای انجام این سرویس کافی نیست</para>
        /// </summary>
        CoinsAreNotEnough = 209,

        [Display(Name = "این نام کاربری قبلا انتخاب شده است")]
        /// <summary>
        /// <para>علت : نام کاربریی که یوزر فرستاده قبلا گرفته شده است</para>
        /// </summary>
        UserNameExist = 210,

        [Display(Name = "این ایمیل قبلا انتخاب شده است")]
        /// <summary>
        /// <para>علت : ایمیلی که یوزر فرستاده قبلا گرفته شده است</para>
        /// </summary>
        EmailExist = 211,

        [Display(Name = "این شماره موبایل قبلا انتخاب شده است")]
        /// <summary>
        /// <para>علت : شماره موبایلی که یوزر فرستاده قبلا گرفته شده است</para>
        /// </summary>
        MobileNoExist = 212,

        [Display(Name = "کد فعالسازی ایمیل با موفقیت ارسال گردید")]
        EmailVerifyCodeSent = 213,

        [Display(Name = "کاربر مورد نظر ایمیلی برای خود ثبت نکرده است")]
        EmailNotExist = 214,

        [Display(Name = "نماد انتخابی شما باید در لیست برترین های تسلاگراف باشد")]
        /// <summary>
        ///<para> علت : آیدی سیمبلی که برای ست کردن فرستاده شده جزو کتگوری های تاپ که قابلیت ست کردن دارد را ندارد</para> 
        /// </summary>
        UserCantPickThisSymbol = 215,

        [Display(Name = "نام در بازی باید کمتر از 12 کاراکتر باشد")]
        /// <summary>
        /// <para>علت : طول نام مستعاری که برای ادیت کردن انتخاب شده بیشتر از 12 کاراکتر میباشد</para>
        /// <para>راه حل : باید نام مستعار کمتر از 12 حرف باشد</para>
        /// </summary>
        NickNameIsLong = 216,

        [Display(Name = "نام کاربری باید وارد شود")]
        UserNameIsEmpty = 217,

        [Display(Name = "نام در بازی باید وارد شود")]
        NickNameIsEmpty = 218,

        [Display(Name = "گذرواژه باید وارد شود")]
        PasswordIsEmpty = 219,

        [Display(Name = "نام کاربری نباید شامل فضای خالی باشد")]
        UserNameHasSpace = 220,

        [Display(Name = "این درخواست قبلا ارسال شده است")]
        /// <summary>
        /// <para>علت : این درخواست قبلا ارسال شده و امکان درخواست مجدد وجود ندارد</para>
        /// </summary>
        RequestSent = 221,

        [Display(Name = "شما دوست هستید")]
        YouAreFriends = 222,

        /// <summary>
        /// <para>علت:چون درخواست دوستیی بین این دو کاربر ثبت نشده نمیتوان درخواست را اکسپت و یا رد کرد</para>
        /// </summary>
        RequestNotExist = 223,

        [Display(Name = "درخواست دوستی رد شد")]
        RequestRejected = 224,

        [Display(Name = "کد دعوت وارد شده اشتباه میباشد")]
        InviteCodeIsWrong = 225,
        /// <summary>
        /// <para>علت : یکی از کوئسشن استیسها پیدا نشد</para>
        /// </summary>
        QSNotFound = 226,

        /// <summary>
        /// <para>علت : یکی از بازیکنان در جدول پلیرز پیدا نشد</para>
        /// </summary>
        PlayerNotFound = 227,

        /// <summary>
        /// <para>علت: سوال مورد نظر یافت نشد</para>
        /// </summary>
        QuestionNotFound = 228,

        /// <summary>
        /// <para>علت : یکی از کوئسشن استیسهای پاسخ داده نشده  پیدا نشد</para>
        /// </summary>
        QSNANotFound = 229,

        /// <summary>
        /// <para>علت: نوتیفیکیشنی که میخواهد ارسال شود داخل دیتابیس وجود ندارد</para>
        /// </summary>
        NotificationNotFound = 230,

        /// <summary>
        /// <para> علت در هنگام ثبت نوتیفیکیشن چون فرمت نوتیفیکشن در دیتابیس تغییر کرده دچار ارور شده</para>
        /// <para>راه حل: یا باید نوتیفیکیشن در دیتابیس به حالت قبل برگردوند یا در کد دستور ارسال را مطابق نوتیفیکیشن جدید اصلاح کرد</para>
        /// </summary>
        NotidicationIsChanged = 231,

        /// <summary>
        /// <para> علت: در هنگام فرستادن پیام چت بوسیله ربات پیام چت پیدا نشد</para>
        /// </summary>
        ChatNotFound = 232,

        /// <summary>
        /// <para>علت: قبلا شخصی که میخواهید به آن درخواست دوستی بفرستید به شما درخواست داده</para>
        /// </summary>
        CheckRelation = 233,

        /// <summary>
        /// <para>علت: برای پر کردن بلیتها سکه کم داریم</para>
        /// </summary>
        HaveNotMonyForFullTicket = 234,
    }
}