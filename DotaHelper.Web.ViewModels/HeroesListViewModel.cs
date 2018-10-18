using DotaHelper.Models.Dto;
using System;
using System.Collections.Generic;
using System.Text;

namespace DotaHelper.Web.ViewModels
{
    public class HeroesListViewModel
    {
        public IEnumerable<HeroesListDto> Heroes { get; set; }
    }
}
