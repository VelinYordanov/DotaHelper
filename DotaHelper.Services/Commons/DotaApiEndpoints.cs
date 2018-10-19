using System;
using System.Collections.Generic;
using System.Text;

namespace DotaHelper.Services.Commons
{
    public static class DotaApiEndpoints
    {
        public const string SearchUrlTemplate = "https://api.opendota.com/api/search/?q={0}";

        public const string PlayerDetailsUrlTemplate = "https://api.opendota.com/api/players/{0}";

        public const string PlayerWinLossUrlTemplate = "https://api.opendota.com/api/players/{0}/wl";

        public const string PlayerRecentMatchesUrlTemplate = "https://api.opendota.com/api/players/{0}/recentMatches";

        public const string PlayerHeroesUrlTemplate = "https://api.opendota.com/api/players/94296097/heroes?sort=win";

        public const string HeroImageUrlTemplate = "https://api.opendota.com/apps/dota2/images/heroes/{0}_sb.png";

        public const string AllHeroesUrl = "https://api.opendota.com/api/heroes";

        public const string MatchDetailsUrl = "https://api.opendota.com/api/matches/{0}";

        public const string AllItemsUrl = "http://www.dota2.com/jsfeed/itemdata";

        public const string ItemImageUrlTemplate = "http://cdn.dota2.com/apps/dota2/images/items/{0}";

        public const string HeroesListUrl = "https://api.opendota.com/api/heroStats";

        public const string HeroesRankingsUrlTemplate = "https://api.opendota.com/api/rankings?hero_id={0}";

        public const string HeroesPlayersUrlTemplate = "https://api.opendota.com/api/heroes/{0}/players";

        public const string HeroesMatchupsUrlTemplate = "https://api.opendota.com/api/heroes/{0}/matchups";
    }
}
