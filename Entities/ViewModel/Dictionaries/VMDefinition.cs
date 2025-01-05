using Entities.Model.Dictionaries;

namespace Entities.ViewModel.Dictionaries
{
    
    public class VMDefinition
    {
        public string G { get; set; } // نوع کلمه (صفت، اسم، فعل و...)
        public List<SubMeaning> Ss { get; set; } // توضیحات معنی کلمه
        public string P { get; set; } // تلفظ کلمه
        public List<Idioms> Is { get; set; } // عبارات و اصطلاحات
    }

    public class SubMeaning
    {
        public List<Translation> Es { get; set; } // ترجمه‌ها
        public string S { get; set; } // توضیحات مربوط به معنی
    }

    public class Translation
    {
        public string E { get; set; } // معنی انگلیسی
        public string T { get; set; } // معنی فارسی
    }

    public class Idioms
    {
        public List<SubMeaning> Ss { get; set; } // توضیحات عبارات
        public string I { get; set; } // عبارت یا اصطلاح
    }
}
