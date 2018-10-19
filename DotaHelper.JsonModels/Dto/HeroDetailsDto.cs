using System;
using System.Collections.Generic;
using System.Text;

namespace DotaHelper.Models.Dto
{
    public class HeroDetailsDto
    {
        public HeroDto Hero { get; set; }

        public IEnumerable<HeroesPlayersDto> Players { get; set; }

        public IEnumerable<RankingDto> Rankings { get; set; }

        public IEnumerable<HeroMatchupsDto> Matchups { get; set; }
    }
}
