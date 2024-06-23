using System.ComponentModel.DataAnnotations;

namespace Common
{
    public enum Helps
    {
        [Display(Name = "نمایش درصد جوابهای مردم")]
        AnsweredPercent,
        [Display(Name = "حذف دو گزینه غلط")]
        RemoveTwoAnswer,
        [Display(Name = "حذف یک گزینه غلط")]
        RemoveOneAnswer
    }
    public enum Enum_TimerType { Question, Result }

    public enum Enum_GameType { Speed, Classic }

    public enum Enum_DeviceType { Android }

    public enum Enum_ChatType { Welcome, GoodBye, ArgumentPositive, ArgumentNegative, Confess, NoType, AnswerHello, ReplayAnswer }

    public enum Enum_CupType { Weekly,Monthly,Custom }

    public enum Enum_Redis
    {
        ListOnlinePlayers, ListGamesSpeed, ListGamesClassic
    }

    public enum Enum_RewardType { Coin, Coin2, Coin3, Coin4, Coin5, Coin6, Coin7, Coin8 }
}
