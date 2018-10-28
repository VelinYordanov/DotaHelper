using DotaHelper.Models.Dto;
using System;
using System.Collections.Generic;
using System.Text;

namespace DotaHelper.Web.ViewModels
{
    public class GuideDetailsViewModel
    {
        public string Id { get; set; }

        public string Title { get; set; }

        public string Text { get; set; }

        public IEnumerable<string> ItemIds { get; set; }

        public IEnumerable<ItemDto> Items { get; set; }

        public string HeroId { get; set; }

        public HeroDto Hero { get; set; }

        public bool IsFavorited { get; set; }
    }
}
