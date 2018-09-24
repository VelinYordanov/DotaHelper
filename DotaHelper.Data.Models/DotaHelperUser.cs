using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace DotaHelper.Data.Models
{
    public class DotaHelperUser : IdentityUser
    {
        public DotaHelperUser()
        {
            this.PostedGuides = new HashSet<Guide>();
            this.FavoritedGuides = new HashSet<DotaHelperUserGuide>();
        }

        [InverseProperty("Creator")]
        public virtual ICollection<Guide> PostedGuides { get; set; }

        public virtual ICollection<DotaHelperUserGuide> FavoritedGuides { get; set; }
    }
}
