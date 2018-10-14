using System;
using System.Collections.Generic;
using System.Text;

namespace DotaHelper.Models.Dto
{
    public class GuideDetailsDto
    {
        public string Id { get; set; }

        public string Title { get; set; }

        public string Text { get; set; }

        public IEnumerable<string> ItemIds { get; set; }

        public IEnumerable<ItemDto> Items { get; set; }

        public string HeroId { get; set; }

        public HeroDto Hero { get; set; }
    }
}
