using DotaHelper.Models.Dto;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace DotaHelper.Services.Interfaces
{
    public interface IItemsProvider
    {
        Task<IEnumerable<ItemDto>> GetAllItemsAsync();

        Task<ItemDto> GetItemAsync(string id);
    }
}
