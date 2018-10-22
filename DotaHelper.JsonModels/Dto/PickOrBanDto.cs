using System;
using System.Collections.Generic;
using System.Text;

namespace DotaHelper.Models.Dto
{
    public class PickOrBanDto
    {
        public string HeroId { get; set; }

        public bool IsPicked { get; set; }

        public bool IsRadiant { get; set; }

        public int Order { get; set; }

        public HeroDto Hero { get; set; }
    }
}
