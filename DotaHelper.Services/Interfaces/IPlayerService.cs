﻿using DotaHelper.Models.Dto;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace DotaHelper.Services.Interfaces
{
    public interface IPlayerService
    {
        Task<IEnumerable<PlayerSearchDto>> SearchPlayers(string name);
    }
}