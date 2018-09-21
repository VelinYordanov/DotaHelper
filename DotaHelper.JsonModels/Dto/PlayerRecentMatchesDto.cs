using DotaHelper.Models.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace DotaHelper.Models.Dto
{
    public class PlayerRecentMatchesDto
    {
        public string MatchId { get; set; }

        public string Duration { get; set; }

        public string HeroId { get; set; }

        public HeroDto Hero { get; set; }

        public string Kills { get; set; }

        public string Deaths { get; set; }

        public string Assists { get; set; }

        public string XpPerMin { get; set; }

        public string GoldPerMin { get; set; }

        public string HeroDamageDone { get; set; }

        public string TowerDamageDone { get; set; }

        public int PlayerSlot { get; set; }

        public bool RadiantWin { get; set; }

        public int Lobby { get; set; }

        public int Game { get; set; }

        public GameMode GameMode { get; set; }

        public LobbyType LobbyType { get; set; }

        public string Team { get; set; }

        public bool WonGame { get; set; }
    }
}
