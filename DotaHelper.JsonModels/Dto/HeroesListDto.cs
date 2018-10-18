using System;
using System.Collections.Generic;
using System.Text;

namespace DotaHelper.Models.Dto
{
    public class HeroesListDto
    {
        public string Id { get; set; }

        public HeroDto Hero { get; set; }

        public double ProWinRate { get; set; }

        public double WinRate { get; set; }
    }
}
