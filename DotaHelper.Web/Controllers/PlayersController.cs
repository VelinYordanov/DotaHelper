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

        public PlayersController(IPlayerService playerService)
        {
            this.playerService = playerService ?? throw new ArgumentException(nameof(playerService));
        }

        [HttpPost]
        public async Task<ActionResult> Search(string name)
        {
            var players = await this.playerService.SearchPlayers(name);
            var result = new PlayerSearchViewModel { Players = players };

            return this.View(result);
        }
    }
}
