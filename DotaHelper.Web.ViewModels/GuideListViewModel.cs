using DotaHelper.Models.Dto;
using System;
using System.Collections.Generic;
using System.Text;

namespace DotaHelper.Web.ViewModels
{
    public class GuideListViewModel
    {
        public IEnumerable<GuideListDto> Guides { get; set; }

        public int MaxPage { get; set; }
    }
}
