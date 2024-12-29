using System;
using System.Collections.Generic;

public static class GuestNicknames
{
    public static readonly List<string> Nicknames = new List<string>
    {
        "BraveSoul", "FreeSpirit", "BrightFuture", "GoldenHeart", "WiseOwl", "ShiningStar",
        "SilverLining", "TrueDreamer", "PeaceSeeker", "KindnessGiver", "JoyfulJourney",
        "HappyExplorer", "CoolVibes", "EpicMind", "DreamCatcher", "LuckyCharm", "GentleWave",
        "SwiftHawk", "SilentWind", "CalmOcean", "WildFlower", "OpenMind", "OldButGold",
        "SunnySide", "FreshStart", "EverGreen", "NightOwl", "RainbowChaser", "CloudWalker",
        "WonderSeeker", "MagicMaker", "SkyDancer", "EternalHope", "MoonWalker", "FreeBird",
        "StarGazer", "BoldMove", "QuickSilver", "BrightMind", "SilentRiver", "KindSoul",
        "EpicPath", "LightBearer", "SunshineGlow", "RainLover", "BlueHorizon", "TrueHeart",
        "LifeLearner", "GoldenWave", "HopefulSoul", "PeaceBringer", "HumbleHero", "DaringDreamer",
        "MindExplorer", "SpiritWalker", "OceanBreeze", "MysticGlow", "CoolWanderer", "BrightLight",
        "SoftBreeze", "DynamicSoul", "FearlessMind", "GreenSpirit", "SparkMaker", "GentleSoul",
        "HarmonySeeker", "DreamLover", "SkyWatcher", "MagicMind", "OpenHorizon", "GoldenGlow",
        "KindlyHeart", "FreshBreeze", "LuckyWanderer", "SweetDream", "HeartLifter", "ClearMind",
        "MindfulWave", "TrueCompass", "BlissfulSoul", "NobleHeart", "StarHunter", "SilverDreamer",
        "JoyBringer", "InnerGlow", "CosmicPath", "BoldExplorer", "LoneWanderer", "SilentEcho",
        "BrightSky", "InnerPeace", "FutureFinder", "SoulExplorer", "DreamWeaver", "PeacefulWave",
        "CalmSpirit", "MindMover", "EpicVision", "TrueFocus", "LifeDreamer", "GoldenRay",
        "ShiningPath", "SereneMind", "PureSpirit", "NatureLover", "HarmonyMaker", "LightSeeker",
        "InfiniteSoul", "SweetLight", "GentleGlow", "PureDream", "CalmStream", "BrightWanderer",
        "HopeChaser", "DreamPath", "SkyWalker", "GreenDreamer", "SoftWave", "SilentDream",
        "HeartExplorer", "BlissSeeker", "SilverLight", "EpicGlow", "NobleMind", "KindDreamer",
        "PeacefulLight", "BlueDream", "OpenWave", "GentleBreeze", "LightLover", "BrightHorizon",
        "PureVision", "InfiniteHope", "GoldenPath", "StarSeeker", "TrueGuide", "HappySoul",
        "BoldHeart", "OceanLover", "ClearWave", "DynamicDreamer", "CoolDream", "FreshWave",
        "HumbleWave", "GreenGlow", "LuckySoul", "SilentStar", "ShinySoul", "InnerDreamer",
        "MindfulSeeker", "GoldenHearted", "SilverWanderer", "PureHope", "LoneDreamer", "PeacefulPath",
        "DynamicGlow", "InfiniteDream", "NatureDreamer", "BrightDreamer", "FutureDreamer",
        "OpenSoul", "CosmicGlow", "FreshExplorer", "CalmLight", "LightExplorer", "InfinitePath",
        "TrueSoul", "LuckyDreamer", "KindWave", "RainDreamer", "BoldDreamer", "EpicLight",
        "InnerWave", "PureLight", "HarmonyPath", "BlissExplorer", "GoldenExplorer", "DynamicPath",
        "GentleDreamer", "SerenePath", "ClearDream", "SilentGlow", "OceanExplorer", "SilverSoul"
    };
    public static string GetRandomNickname()
    {
        Random random = new Random();
        int index = random.Next(Nicknames.Count); // Get a random index
        return Nicknames[index];
    }
}
