using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using WEB_153504_SIVY.Domain.Entities;

namespace WEB_153504_SIVY.Areas.Admin.Pages
{
    public class EditModel : PageModel
    {
        private readonly WEB_153504_SIVY.Services.CarModelService.ICarModelService _carModelService;
        private readonly WEB_153504_SIVY.Services.CarBodyService.ICarBodyTypeService _carBodyTypeService;

        public EditModel(WEB_153504_SIVY.Services.CarModelService.ICarModelService carModelService,
            WEB_153504_SIVY.Services.CarBodyService.ICarBodyTypeService carBodyTypeService)
        {
            _carModelService = carModelService;
            _carBodyTypeService = carBodyTypeService;
        }

        [BindProperty]
        public CarModel CarModel { get; set; } = default!;

        [BindProperty]
        public IFormFile? Image { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(int? id, int pageno = 1)
        {
            var carModels = await _carModelService.GetCarModelListAsync(null, pageno);
            if (id == null || carModels == null)
            {
                return NotFound();
            }
            var carmodel = carModels.Data.Items.FirstOrDefault(x => x.Id == id);
            if (carmodel == null)
            {
                return NotFound();
            }
            CarModel = carmodel;

            var carBodyTypes = await _carBodyTypeService.GetCarBodyTypeListAsync();

            ViewData["CarBodyId"] = new SelectList(carBodyTypes.Data, "Id", "Name");
            return Page();
        }

        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see https://aka.ms/RazorPagesCRUD.
        public async Task<IActionResult> OnPostAsync()
        {
            var carModels = await _carModelService.GetCarModelListAsync(null);
            if (!ModelState.IsValid || carModels == null || CarModel == null)
            {
                return Page();
            }

            var oldCarModel = carModels.Data.Items.FirstOrDefault(c => c.Id == CarModel.Id);

            if (oldCarModel != null && Image == null)
                CarModel.Image = oldCarModel.Image;

            await _carModelService.UpdateCarModelAsync(CarModel.Id, CarModel, Image);

            return RedirectToPage("./Index");
        }
    }
}
