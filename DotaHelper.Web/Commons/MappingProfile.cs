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
                .ForMember(dest => dest.ImageUrl, opts => opts.MapFrom(x => string.Format(DotaApiEndpoints.HeroImageUrlTemplate, x.FullName.Remove(0, FullHeroNameStart.Length))));

            CreateMap<PlayerDetailsJsonModel, PlayerProfileDetailsDto>()
                .ForMember(x => x.AvatarUrl, x => x.MapFrom(y => y.Profile.AvatarUrl))
                .ForMember(x => x.Name, x => x.MapFrom(y => y.Profile.Name))
                .ForMember(x => x.SteamProfile, x => x.MapFrom(y => y.Profile.SteamProfile));

            CreateMap<PlayerWinsLossesJsonModel, PlayerWinsLossesDto>();

            CreateMap<PlayerHeroesJsonModel, PlayerHeroesDto>()
                .ForMember(x => x.GamesLost, x => x.MapFrom(y => y.GamesPlayed - y.GamesWon))
                .ForMember(x => x.WinRate, x => x.MapFrom(y => Math.Round((y.GamesWon / y.GamesPlayed) * 100)));

            CreateMap<PlayerRecentMatchesJsonModel, PlayerRecentMatchesDto>()
                .ForMember(x => x.GameMode, x => x.MapFrom(y => (GameMode)y.Game))
                .ForMember(x => x.LobbyType, x => x.MapFrom(y => (LobbyType)y.Lobby))

                //https://wiki.teamfortress.com/wiki/WebAPI/GetMatchHistory#Player_Slot
                .ForMember(x => x.Team, x => x.MapFrom(y => y.PlayerSlot > 128 ? DireTeam : RadiantTeam))
                .ForMember(x => x.WonGame, x => x.MapFrom(y => y.RadiantWin ? y.PlayerSlot <= 128 : y.PlayerSlot > 128));

            CreateMap<PlayerDetailsDto, PlayerDetailsViewModel>();

            CreateMap<PlayerWinsLossesJsonModel, PlayerWinsLossesDto>();

            CreateMap<MatchPlayerJsonModel, MatchPlayerDto>()
                .ForMember(x => x.Items, x => x.MapFrom(y => new List<ItemDto>
                {
                    new ItemDto{ ItemId = y.Item1Id},
                    new ItemDto{ ItemId = y.Item2Id},
                    new ItemDto{ ItemId = y.Item3Id},
                    new ItemDto{ ItemId = y.Item4Id},
                    new ItemDto{ ItemId = y.Item5Id},
                    new ItemDto{ ItemId = y.Item6Id},
                }));

            CreateMap<ItemJsonModel, ItemDto>();

            CreateMap<MatchDetailsJsonModel, MatchDetailsDto>()
                .ForMember(x => x.GameMode, x => x.MapFrom(y => (GameMode)y.Game))
                .ForMember(x => x.LobbyType, x => x.MapFrom(y => (LobbyType)y.Lobby));

            CreateMap<PickOrBanJsonModel, PickOrBanDto>();

            CreateMap<MatchDetailsDto, MatchDetailsViewModel>();

            CreateMap<Guide, GuideListDto>();
        }
    }
}
