using DotaHelper.Services.Commons.Interfaces;
using DotaHelper.Services.Interfaces;
using DotaHelper.Web.ViewModels;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DotaHelper.Web.Controllers
{
    public class PlayersController : Controller
    {
        private readonly IPlayerService playerService;
        private readonly IMapper mapper;

        public PlayersController(IPlayerService playerService, IMapper mapper)
        {
            this.playerService = playerService ?? throw new ArgumentException(nameof(playerService));
            this.mapper = mapper ?? throw new ArgumentException(nameof(mapper));
        }

        [HttpPost]
        public async Task<IActionResult> Search(string name)
        {
            var players = await this.playerService.SearchPlayersAsync(name);
            var result = new PlayerSearchViewModel { Players = players };

            return this.View(result);
        }

        public async Task<IActionResult> Details(string id)
        {
            var playerDetails = await this.playerService.GetPlayerDetailsAsync(id);
            var playerDetailsViewModel = this.mapper.Map<PlayerDetailsViewModel>(playerDetails);
            return this.View(playerDetailsViewModel);
        }

        public async Task<IActionResult> Matches(string id)
        {
            var matchDetails = await this.playerService.GetMatchDetailsAsync(id);
            var matchDetailsViewModel = this.mapper.Map<MatchDetailsViewModel>(matchDetails);
            return this.View(matchDetailsViewModel);
        }

        public async Task<IActionResult> Heroes()
        {
            var heroes = await this.playerService.GetHeroesListDataAsync();
            var heroesListViewModel = new HeroesListViewModel { Heroes = heroes };
            return this.View(heroesListViewModel);
        }

        public async Task<IActionResult> HeroDetails(string id)
        {
            var heroDetails = await this.playerService.GetHeroDetailsAsync(id);
            var heroDetailsViewModel = this.mapper.Map<HeroDetailsViewModel>(heroDetails);
            return this.View(heroDetailsViewModel);
        }
    }
}
