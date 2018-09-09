using DotaHelper.Models.JsonModels;
using System;
using System.Collections.Generic;
using System.Text;

namespace DotaHelper.Models.Dto
{
    public class PlayerDetailsDto
    {
        public PlayerDetailsJsonModel Details { get; set; }

        public ICollection<PlayerHeroesJsonModel> Heroes { get; set; }

        public ICollection<PlayerRecentMatchesJsonModel> RecentMatchHistory { get; set; }

        public PlayerWinsLossesJsonModel WinsAndLosses { get; set; }
    }
}
