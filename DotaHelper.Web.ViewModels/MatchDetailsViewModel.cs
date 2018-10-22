using DotaHelper.Models.Dto;
using DotaHelper.Models.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace DotaHelper.Web.ViewModels
{
    public class MatchDetailsViewModel
    {
        public GameMode GameMode { get; set; }

        public LobbyType LobbyType { get; set; }

        public int RadiantScore { get; set; }

        public int DireScore { get; set; }

        public bool RadiantWin { get; set; }

        public string Duration { get; set; }

        public ICollection<PickOrBanDto> PicksAndBans { get; set; }

        public ICollection<MatchPlayerDto> Players { get; set; }
    }
}
