using System;
using System.Collections.Generic;
using System.Text;

namespace DotaHelper.Models.Dto
{
    public class HeroesPlayersDto
    {
        public string AccountId { get; set; }

        public PlayerProfileDetailsDto PlayerProfile { get; set; }

        public double GamesPlayed { get; set; }

        public double WinRate { get; set; }
    }
}
