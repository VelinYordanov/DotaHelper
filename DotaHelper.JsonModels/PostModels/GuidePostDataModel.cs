using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace DotaHelper.Models.PostModels
{
    public class GuidePostDataModel
    {
        [MinLength(10, ErrorMessage = "Must be at least 10 characters long")]
        [Required(ErrorMessage = "Guide title is required")]
        [MaxLength(50, ErrorMessage = "Title too long")]
        public string Title { get; set; }

        [MinLength(50, ErrorMessage = "Must be at least 50 characters long")]
        [Required(ErrorMessage = "Guide description is required")]
        [MaxLength(30000, ErrorMessage = "Guide too long")]
        public string Text { get; set; }

        public string HeroId { get; set; }

        public string Item1 { get; set; }

        public string Item2 { get; set; }

        public string Item3 { get; set; }

        public string Item4 { get; set; }

        public string Item5 { get; set; }

        public string Item6 { get; set; }

        public IDictionary<string, string> HeroIdsToNames { get; set; }

        public IDictionary<string, string> ItemIdsToNames { get; set; }
    }
}
