using System;
using System.Collections.Generic;
using System.Text;

namespace DotaHelper.Data.Models
{
    public class DotaHelperUserGuide
    {
        public string DotaHelperUserId { get; set; }

        public DotaHelperUser User { get; set; }

        public Guid GuideId { get; set; }

        public Guide Guide { get; set; }
    }
}
