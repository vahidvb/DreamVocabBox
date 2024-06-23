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

        
    }
}