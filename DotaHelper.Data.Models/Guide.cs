using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace DotaHelper.Data.Models
{
    public class Guide
    {
        public Guide()
        {
            this.Favorites = new HashSet<DotaHelperUserGuide>();
        }

        public string Id { get; set; }

        public virtual DotaHelperUser Creator { get; set; }

        public virtual ICollection<DotaHelperUserGuide> Favorites { get; set; }

        [Required]
        public string Title { get; set; }

        [Required]
        public string Text { get; set; }

        [Required]
        public string HeroId { get; set; }

        [Required]
        public string Item1Id { get; set; }

        [Required]
        public string Item2Id { get; set; }

        [Required]
        public string Item3Id { get; set; }

        [Required]
        public string Item4Id { get; set; }

        [Required]
        public string Item5Id { get; set; }

        [Required]
        public string Item6Id { get; set; }
    }
}
