using AutoMapper;
using DotaHelper.Data.Models;
using DotaHelper.Models;
using DotaHelper.Models.Dto;
using DotaHelper.Models.Enums;
using DotaHelper.Models.JsonModels;
using DotaHelper.Services.Commons;
using DotaHelper.Web.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DotaHelper.Web.Commons
{
    public class MappingProfile : AutoMapper.Profile
    {
        private const string FullHeroNameStart = "npc_dota_hero_";
        private const string DireTeam = "Dire";
        private const string RadiantTeam = "Radiant";

        public MappingProfile()
        {
            CreateMap<PlayerSearchJsonModel, PlayerSearchDto>();

            CreateMap<HeroJsonModel, HeroDto>()
                .ForMember(dest => dest.ImageUrl, opts => opts.MapFrom(x => string.Format(DotaApiEndpoints.HeroImageUrlTemplate, x.FullName.Remove(0, FullHeroNameStart.Length))))
                .ForMember(dest => dest.BigImageUrl, opts => opts.MapFrom(x => string.Format(DotaApiEndpoints.HerBigImageUrlTemplate, x.FullName.Remove(0, FullHeroNameStart.Length))));

            CreateMap<PlayerDetailsJsonModel, PlayerProfileDetailsDto>()
                .ForMember(x => x.AvatarUrl, x => x.MapFrom(y => y.Profile.AvatarUrl))
                .ForMember(x => x.Name, x => x.MapFrom(y => y.Profile.Name))
                .ForMember(x => x.SteamProfile, x => x.MapFrom(y => y.Profile.SteamProfile))
                .ForMember(x => x.AccountId, x => x.MapFrom(y => y.Profile.AccountId))
                .ForMember(x => x.AvatarFullUrl, x => x.MapFrom(y => y.Profile.AvatarFullUrl))
                .ForMember(x => x.RankingImageUrl, x => x.MapFrom(y => string.Format(DotaApiEndpoints.PlayersRankingImageUrlTemplate, y.RankTier.ToCharArray()[0])))
                .ForMember(x => x.RankingImageStarsUrl, x => x.MapFrom(y => string.Format(DotaApiEndpoints.PlayersRankingStarImageUrlTemplate, y.RankTier.ToCharArray()[1])));


            CreateMap<PlayerHeroesJsonModel, PlayerHeroesDto>()
                .ForMember(x => x.GamesLost, x => x.MapFrom(y => y.GamesPlayed - y.GamesWon))
                .ForMember(x => x.WinRate, x => x.MapFrom(y => Math.Round((y.GamesWon / y.GamesPlayed) * 100)));

            CreateMap<PlayerRecentMatchesJsonModel, PlayerRecentMatchesDto>()
                .ForMember(x => x.GameMode, x => x.MapFrom(y => (GameMode)y.Game))
                .ForMember(x => x.LobbyType, x => x.MapFrom(y => (LobbyType)y.Lobby))
                .ForMember(x => x.Duration, x => x.MapFrom(y => (int)TimeSpan.FromSeconds(y.Duration).TotalMinutes + ":" + TimeSpan.FromSeconds(y.Duration).Seconds))


                //https://wiki.teamfortress.com/wiki/WebAPI/GetMatchHistory#Player_Slot
                .ForMember(x => x.Team, x => x.MapFrom(y => y.PlayerSlot >= 128 ? DireTeam : RadiantTeam))
                .ForMember(x => x.WonGame, x => x.MapFrom(y => y.RadiantWin ? y.PlayerSlot < 128 : y.PlayerSlot >= 128));

            CreateMap<PlayerDetailsDto, PlayerDetailsViewModel>();

            CreateMap<PlayerWinsLossesJsonModel, PlayerWinsLossesDto>()
                .ForMember(x => x.WinRate, x => x.MapFrom(y => Math.Round(((y.Wins / (y.Wins + y.Losses)) * 100), 2)));

            CreateMap<MatchPlayerJsonModel, MatchPlayerDto>()
                .ForMember(x => x.Items, x => x.MapFrom(y => new List<ItemDto>
                {
                    new ItemDto{ ItemId = y.Item1Id},
                    new ItemDto{ ItemId = y.Item2Id},
                    new ItemDto{ ItemId = y.Item3Id},
                    new ItemDto{ ItemId = y.Item4Id},
                    new ItemDto{ ItemId = y.Item5Id},
                    new ItemDto{ ItemId = y.Item6Id},
                }))
                .ForMember(x => x.PlayerRanking, x => x.MapFrom(y => (Rank)(int)Char.GetNumericValue(y.PlayerRanking.ToCharArray()[0]) + "[" + y.PlayerRanking.ToCharArray()[1] + "]"));

            CreateMap<ItemJsonModel, ItemDto>();

            CreateMap<MatchDetailsJsonModel, MatchDetailsDto>()
                .ForMember(x => x.GameMode, x => x.MapFrom(y => (GameMode)y.Game))
                .ForMember(x => x.LobbyType, x => x.MapFrom(y => (LobbyType)y.Lobby))
                .ForMember(x => x.Duration, x => x.MapFrom(y => (int)TimeSpan.FromSeconds(y.Duration).TotalMinutes + ":" + TimeSpan.FromSeconds(y.Duration).Seconds));

            CreateMap<PickOrBanJsonModel, PickOrBanDto>()
                 .ForMember(x => x.IsRadiant, x => x.MapFrom(y => y.Team == 0));

            CreateMap<MatchDetailsDto, MatchDetailsViewModel>();

            CreateMap<Guide, GuideListDto>()
                .ForMember(x => x.ItemIds, x => x.MapFrom(y => new List<string> { y.Item1Id, y.Item2Id, y.Item3Id, y.Item4Id, y.Item5Id, y.Item6Id }));

            CreateMap<Guide, GuideDetailsDto>()
                .ForMember(x => x.ItemIds, x => x.MapFrom(y => new List<string> { y.Item1Id, y.Item2Id, y.Item3Id, y.Item4Id, y.Item5Id, y.Item6Id }));

            CreateMap<HeroesListJsonModel, HeroesListDto>()
                .ForMember(x => x.ProWinRate, x => x.PreCondition(y => y.ProPick > 0))
                .ForMember(x => x.ProWinRate, x => x.MapFrom(y => Math.Round((y.ProWins / y.ProPick), 2) * 100))
                .ForMember(x => x.WinRate, x => x.MapFrom(y => Math.Round(((y.Win1 + y.Win2 + y.Win3 + y.Win4 + y.Win5 + y.Win6 + y.Win7 + y.Win8) /
                     (y.Pick1 + y.Pick2 + y.Pick3 + y.Pick4 + y.Pick5 + y.Pick6 + y.Pick7 + y.Pick8)), 2) * 100));

            CreateMap<HeroDetailsDto, HeroDetailsViewModel>();

            CreateMap<HeroesPlayersJsonModel, HeroesPlayersDto>()
                .ForMember(x => x.WinRate, x => x.PreCondition(y => y.GamesPlayed > 0))
                .ForMember(x => x.WinRate, x => x.MapFrom(y => Math.Round((y.Wins / y.GamesPlayed), 2) * 100));

            CreateMap<HeroMatchupsJsonModel, HeroMatchupsDto>()
                .ForMember(x => x.WinRate, x => x.PreCondition(y => y.GamesPlayed > 0))
                .ForMember(x => x.WinRate, x => x.MapFrom(y => Math.Round((y.Wins / y.GamesPlayed), 2) * 100));

            CreateMap<HeroRankingsJsonModel, IEnumerable<RankingDto>>()
                .ConstructUsing(x => x.Rankings.Select(y => new RankingDto { AccountId = y.AccountId, Avatar = y.Avatar, Name = y.Name, Score = Math.Round(y.Score) }).ToList());
        }
    }
}

