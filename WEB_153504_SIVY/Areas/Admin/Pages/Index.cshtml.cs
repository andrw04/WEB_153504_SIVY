using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Azure;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using WEB_153504_SIVY.API.Data;
using WEB_153504_SIVY.API.Services.CarModelService;
using WEB_153504_SIVY.Domain.Entities;
using WEB_153504_SIVY.Services.CarModelService;

namespace WEB_153504_SIVY.Areas.Admin.Pages
{
    public class IndexModel : PageModel
    {
        private readonly WEB_153504_SIVY.Services.CarModelService.ICarModelService _carModelService;

        public IndexModel(WEB_153504_SIVY.Services.CarModelService.ICarModelService carModelService)
        {
            _carModelService = carModelService;
        }

        public IList<CarModel> CarModel { get;set; } = default!;

        public async Task OnGetAsync(int pageno = 1)
        {
            if (_carModelService != null)
            {
                var carmodels = await _carModelService.GetCarModelListAsync(null, pageno);

                if (carmodels.Success)
                {
                    CarModel = carmodels.Data.Items;
                    ViewData["TotalPages"] = carmodels.Data.TotalPages;
                    ViewData["CurrentPage"] = carmodels.Data.CurrentPage;
                }
            }
        }
    }
}
