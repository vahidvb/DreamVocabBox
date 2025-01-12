using System;

namespace Common.EnumTools
{
    [AttributeUsage(AttributeTargets.Field, Inherited = false, AllowMultiple = false)]
    public sealed class TitleDescriptionAttribute : Attribute
    {
        public string Title { get; }
        public string Description { get; }

        public TitleDescriptionAttribute(string title, string description)
        {
            Title = title;
            Description = description;
        }
    }

}
