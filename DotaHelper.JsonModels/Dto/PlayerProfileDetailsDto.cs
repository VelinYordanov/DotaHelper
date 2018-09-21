using System;
using System.Collections.Generic;
using System.Text;

namespace DotaHelper.Models.Dto
{
    public class PlayerProfileDetailsDto
    {
        public string RankTier { get; set; }

        public string AvatarUrl { get; set; }

        public string Name { get; set; }

        public string SteamProfile { get; set; }
    }
}
