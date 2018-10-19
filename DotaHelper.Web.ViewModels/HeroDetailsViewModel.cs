using DotaHelper.Models.Dto;
using System;
using System.Collections.Generic;
using System.Text;

namespace DotaHelper.Web.ViewModels
{
    public class HeroDetailsViewModel
    {
        public HeroDto Hero { get; set; }

        public IEnumerable<HeroesPlayersDto> Players { get; set; }

        public IEnumerable<RankingDto> Rankings { get; set; }

        public IEnumerable<HeroMatchupsDto> Matchups { get; set; }
    }
}
