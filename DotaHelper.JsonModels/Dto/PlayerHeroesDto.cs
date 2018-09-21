using System;
using System.Collections.Generic;
using System.Text;

namespace DotaHelper.Models.Dto
{
    public class PlayerHeroesDto
    {
        public string HeroId { get; set; }

        public HeroDto Hero { get; set; }

        public double GamesPlayed { get; set; }

        public double GamesWon { get; set; }

        public double GamesLost { get; set; }

        public double WinRate { get; set; }
    }
}
