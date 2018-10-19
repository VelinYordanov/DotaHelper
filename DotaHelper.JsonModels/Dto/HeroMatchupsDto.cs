using System;
using System.Collections.Generic;
using System.Text;

namespace DotaHelper.Models.Dto
{
    public class HeroMatchupsDto
    {
        public string HeroId { get; set; }

        public HeroDto HeroDto { get; set; }

        public double GamesPlayed { get; set; }

        public double WinRate { get; set; }
    }
}
