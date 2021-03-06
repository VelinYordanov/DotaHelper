﻿using DotaHelper.Data.Interfaces;
using DotaHelper.Data.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace DotaHelper.Data
{
    public class DotaHelperData : IDotaHelperData
    {
        private readonly IUnitOfWork unitOfWork;

        public DotaHelperData(IDotaHelperRepository<DotaHelperUser> users, IGuideData guides, IDotaHelperRepository<DotaHelperUserGuide> userguides, IUnitOfWork unitOfWork)
        {
            this.Users = users ?? throw new ArgumentException(nameof(users));
            this.Guides = guides ?? throw new ArgumentException(nameof(guides));
            this.unitOfWork = unitOfWork ?? throw new ArgumentException(nameof(unitOfWork));
            this.UserGuides = userguides;
        }

        public IDotaHelperRepository<DotaHelperUser> Users { get; }

        public IDotaHelperRepository<DotaHelperUserGuide> UserGuides { get; }

        public IGuideData Guides { get; }

        public async Task SaveChangesAsync()
        {
            await this.unitOfWork.SaveChangesAsync();
        }
    }
}
