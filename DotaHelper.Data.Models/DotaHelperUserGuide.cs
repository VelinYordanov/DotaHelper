using System;
using System.Collections.Generic;
using System.Text;

namespace DotaHelper.Data.Models
{
    public class DotaHelperUserGuide
    {
        public string DotaHelperUserId { get; set; }

        public virtual DotaHelperUser User { get; set; }

        public string GuideId { get; set; }

        public virtual Guide Guide { get; set; }
    }
}
