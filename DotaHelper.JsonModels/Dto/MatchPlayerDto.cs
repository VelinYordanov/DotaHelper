using System;
using System.Collections.Generic;
using System.Text;

namespace DotaHelper.Models.Dto
{
    public class MatchPlayerDto
    {
        public string PlayerId { get; set; }

        public string PlayerName { get; set; }

        public string PlayerRanking { get; set; }

        public string HeroId { get; set; }

        public HeroDto Hero { get; set; }

        public int Level { get; set; }

        public int HeroDamageDone { get; set; }

        public int HealingDone { get; set; }

        public int GoldPerMin { get; set; }

        public ICollection<ItemDto> Items { get; set; }

        public int Kills { get; set; }

        public int Deaths { get; set; }

        public int Assists { get; set; }

        public int LastHits { get; set; }

        public int Denies { get; set; }

        public bool IsRadiant { get; set; }
    }
}
