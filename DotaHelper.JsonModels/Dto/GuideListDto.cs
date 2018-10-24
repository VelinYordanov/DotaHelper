using System;
using System.Collections.Generic;
using System.Text;

namespace DotaHelper.Models.Dto
{
    public class GuideListDto
    {
        public string Id { get; set; }

        public string Title { get; set; }

        public string HeroId { get; set; }

        public string HeroImageUrl { get; set; }

        public IEnumerable<ItemDto> Items { get; set; }

        public IEnumerable<string> ItemIds { get; set; }
    }
}
