using DotaHelper.Models.Dto;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace DotaHelper.Services.Interfaces
{
    public interface IHeroesProvider
    {
        Task<IEnumerable<HeroDto>> GetAllHeroesAsync();

        Task<HeroDto> GetHeroAsync(string id);
    }
}
