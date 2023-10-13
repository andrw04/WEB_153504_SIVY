using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using WEB_153504_SIVY.API.Data;
using WEB_153504_SIVY.Domain.Entities;

namespace WEB_153504_SIVY.Areas.Admin.Pages
{
    public class CreateModel : PageModel
    {
        private readonly WEB_153504_SIVY.Services.CarModelService.ICarModelService _carModelService;
        private readonly WEB_153504_SIVY.Services.CarBodyService.ICarBodyTypeService _carBodyTypeService;

        [BindProperty]
        public CarModel CarModel { get; set; } = default!;
        [BindProperty]
        public IFormFile? Image { get; set; } = default!;

        public CreateModel(WEB_153504_SIVY.Services.CarModelService.ICarModelService carModelService,
            WEB_153504_SIVY.Services.CarBodyService.ICarBodyTypeService carBodyTypeService)
        {
            _carModelService = carModelService;
            _carBodyTypeService = carBodyTypeService;
        }

        public async Task<IActionResult> OnGet()
        {
            var carBodyTypes = await _carBodyTypeService.GetCarBodyTypeListAsync();
            ViewData["CarBodyId"] = new SelectList(carBodyTypes.Data, "Id", "Name");
                return Page();
        }
        

        // To protect from overposting attacks, see https://aka.ms/RazorPagesCRUD
        public async Task<IActionResult> OnPostAsync()
        {
            var carModels = await _carModelService.GetCarModelListAsync(null);
            if (!ModelState.IsValid || carModels == null || CarModel == null)
            {
                return Page();
            }
            await _carModelService.CreateCarModelAsync(CarModel, Image);

            return RedirectToPage("./Index");
        }
    }
}
