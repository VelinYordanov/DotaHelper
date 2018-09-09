using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

namespace DotaHelper.Models.Enums
{
    public enum GameMode
    {
        Unknown = 0,

        [Description("All Pick")]
        AllPick = 1,

        [Description("Captain's Mode")]
        CaptainsMode = 2,

        [Description("Random Draft")]
        RandomDraft = 3,

        [Description("Single Draft")]
        SingleDraft = 4,

        [Description("All Random")]
        AllRandom = 5,
        Intro = 6,
        Diretide = 7,

        [Description("Reverse Captain's Mode")]
        ReverseCaptainsMode = 8,

        [Description("The Greeviling")]
        TheGreeviling = 9,
        Tutorial = 10,

        [Description("Mid Only")]
        MidOnly = 11,

        [Description("Least Played")]
        LeastPlayed = 12,

        [Description("New Player Pool")]
        NewPlayerPool = 13,

        [Description("Compendium Matchmaking")]
        CompendiumMatchmaking = 14,
        Custom = 15,

        [Description("Captain's Draft")]
        CaptainsDraft = 16,

        [Description("Balanced Draft")]
        BalancedDraft = 17,

        [Description("Ability Draft")]
        AbilityDraft = 18,
        Event = 19,

        [Description("All Random Death Match")]
        AllRandomDeathMatch = 20,

        [Description("Solo Mid 1 vs 1")]
        SoloMid1vs1 = 21,

        [Description("All Draft")]
        AllDraft = 22,
        Turbo = 23,
        Mutation = 24
    }
}
