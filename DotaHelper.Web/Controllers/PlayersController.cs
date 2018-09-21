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
        public async Task<ActionResult> Search(string name)
        {
            var players = await this.playerService.SearchPlayersAsync(name);
            var result = new PlayerSearchViewModel { Players = players };

            return this.View(result);
        }

        public async Task<ActionResult> Details(string id)
        {
            var playerDetails = await this.playerService.GetPlayerDetailsAsync(id);
            var playerDetailsViewModel = this.mapper.Map<PlayerDetailsViewModel>(playerDetails);
            return this.View(playerDetailsViewModel);
        }
    }
}
