using DotaHelper.Models.Dto;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace DotaHelper.Services.Interfaces
{
    public interface IPlayerService
    {
        Task<IEnumerable<PlayerSearchDto>> SearchPlayersAsync(string name);

        Task<PlayerDetailsDto> GetPlayerDetailsAsync(string accountId);

        Task<MatchDetailsDto> GetMatchDetailsAsync(string id);
    }
}
