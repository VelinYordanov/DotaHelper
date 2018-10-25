using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text.Encodings.Web;
using System.Threading.Tasks;
using AutoMapper;
using DotaHelper.Data;
using DotaHelper.Data.Interfaces;
using DotaHelper.Data.Models;
using DotaHelper.Models.Dto;
using DotaHelper.Services.Interfaces;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace DotaHelper.Web.Areas.Identity.Pages.Account.Manage
{
    public partial class IndexModel : PageModel
    {
        private readonly UserManager<DotaHelperUser> userManager;
        private readonly SignInManager<DotaHelperUser> signInManager;
        private readonly IDotaHelperData dotaHelperData;
        private readonly IMapper mapper;
        private readonly IItemsProvider itemsProvider;

        public IndexModel(UserManager<DotaHelperUser> userManager, SignInManager<DotaHelperUser> signInManager, IDotaHelperData dotaHelperData, IMapper mapper, IItemsProvider itemsProvider)
        {
            this.userManager = userManager;
            this.signInManager = signInManager;
            this.dotaHelperData = dotaHelperData ?? throw new ArgumentException(nameof(dotaHelperData));
            this.mapper = mapper ?? throw new ArgumentException(nameof(mapper));
            this.itemsProvider = itemsProvider ?? throw new ArgumentException(nameof(itemsProvider));
        }

        public string Username { get; set; }

        public bool IsEmailConfirmed { get; set; }

        public IEnumerable<GuideListDto> PostedGuides { get; set; }

        public IEnumerable<GuideListDto> FavoritedGuides { get; set; }

        [TempData]
        public string StatusMessage { get; set; }

        [BindProperty]
        public InputModel Input { get; set; }

        public class InputModel
        {
            [Required]
            [EmailAddress]
            public string Email { get; set; }

            [Phone]
            [Display(Name = "Phone number")]
            public string PhoneNumber { get; set; }
        }

        public async Task<IActionResult> OnGetAsync()
        {
            var currentUserTask = this.userManager.GetUserAsync(HttpContext.User);
            var itemsTask = this.itemsProvider.GetAllItemsAsync();
            var currentUser = await currentUserTask;
            var items = await itemsTask;

            var postedGuides = this.dotaHelperData.Guides.All.Where(x => x.Creator == currentUser).ToList();
            var favorittedGuideIds = this.dotaHelperData.UserGuides.All.Where(x => x.User == currentUser).Select(x => x.GuideId).ToList();
            var favoritedGuides = this.dotaHelperData.Guides.All.Where(x => favorittedGuideIds.Contains(x.Id)).ToList();
            this.FavoritedGuides = this.mapper.Map<IEnumerable<GuideListDto>>(favoritedGuides);
            this.PostedGuides = this.mapper.Map<IEnumerable<GuideListDto>>(postedGuides);
            foreach (var guide in this.PostedGuides)
            {
                guide.Items = guide.ItemIds.Select(x => items.SingleOrDefault((y => y.ItemId == x))).ToList();
            }

            foreach (var guide in this.FavoritedGuides)
            {
                guide.Items = guide.ItemIds.Select(x => items.SingleOrDefault((y => y.ItemId == x))).ToList();
            }


            return this.Page();
        }
    }
}
