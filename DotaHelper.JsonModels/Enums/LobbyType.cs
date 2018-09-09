using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

namespace DotaHelper.Models.Enums
{
    public enum LobbyType
    {
        Normal = 0,
        Practice = 1,
        Tournament = 2,
        Tutorial = 3,

        [Description("Co-Op with Bots")]
        CoOpWithBots = 4,

        [Description("Team match")]
        TeamMatch = 5,
        Ranked = 7
    }
}
