using DotaHelper.Models.Dto;
using System;
using System.Collections.Generic;
using System.Text;

namespace DotaHelper.Web.ViewModels
{
    public class PlayerDetailsViewModel
    {
        public PlayerProfileDetailsDto Details { get; set; }

        public ICollection<PlayerHeroesDto> Heroes { get; set; }

        public ICollection<PlayerRecentMatchesDto> RecentMatchHistory { get; set; }

        public PlayerWinsLossesDto WinsAndLosses { get; set; }
    }
}
