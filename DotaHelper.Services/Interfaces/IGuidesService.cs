﻿using DotaHelper.Models.Dto;
using DotaHelper.Models.PostModels;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace DotaHelper.Services.Interfaces
{
    public interface IGuidesService
    {
        Task AddGuide(string userId, GuidePostDataModel data);

        Task<GuidePostDataModel> GetCreateModel();

        Task FavoriteGuide(string userId, string guideId);

        Task<IEnumerable<GuideListDto>> GetGuidesAsync(int page);

        Task<int> GetGuidesMaxPageAsync();

        Task<GuideDetailsDto> GetGuideDetailsAsync(string id, string userId);
    }
}
