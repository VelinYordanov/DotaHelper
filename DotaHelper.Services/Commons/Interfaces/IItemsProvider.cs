using DotaHelper.Models.Dto;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace DotaHelper.Services.Commons.Interfaces
{
    public interface IItemsProvider
    {
        Task<IEnumerable<ItemDto>> GetAll();

        Task<ItemDto> GetItemById(string id);
    }
}
