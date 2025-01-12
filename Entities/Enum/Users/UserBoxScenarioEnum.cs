using Common.EnumTools;

namespace Entities.Enum.Users
{
    public enum UserBoxScenarioEnum
    {
        [TitleDescription("Half Day Box", "Review the box every half day (e.g., morning and evening).")]
        HalfDayBox,

        [TitleDescription("Daily Box", "Review the box once every day.")]
        DailyBox,

        [TitleDescription("Box Number Days", "Review the box at intervals based on the box number (e.g., Box 3 every 3 days).")]
        BoxNumberDays
    }
}
