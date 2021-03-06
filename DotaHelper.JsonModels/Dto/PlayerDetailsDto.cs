﻿using DotaHelper.Models.JsonModels;
using System;
using System.Collections.Generic;
using System.Text;

namespace DotaHelper.Models.Dto
{
    public class PlayerDetailsDto
    {
        public PlayerProfileDetailsDto Details { get; set; }

        public ICollection<PlayerHeroesDto> Heroes { get; set; }

        public ICollection<PlayerRecentMatchesDto> RecentMatchHistory { get; set; }

        public PlayerWinsLossesDto WinsAndLosses { get; set; }
    }
}
