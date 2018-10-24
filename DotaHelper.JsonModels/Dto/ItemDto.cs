using System;
using System.Collections.Generic;
using System.Text;

namespace DotaHelper.Models.Dto
{
    public class ItemDto
    {
        public string ItemId { get; set; }

        public string Name { get; set; }

        public string Cost { get; set; }

        public string Lore { get; set; }

        public string Description { get; set; }

        public string Attributes { get; set; }

        public int ManaCost { get; set; }

        public int Cooldown { get; set; }

        public string Image { get; set; }

        public string ImageUrl { get; set; }

        public string Quality { get; set; }

        public string Notes { get; set; }
    }
}
