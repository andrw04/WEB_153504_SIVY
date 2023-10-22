using Microsoft.AspNetCore.Mvc;
using WEB_153504_SIVY.Extensions;
using WEB_153504_SIVY.Services.CarBodyService;
using WEB_153504_SIVY.Services.CarModelService;

namespace WEB_153504_SIVY.Controllers
{
    public class CarModelController : Controller
    {
        ICarModelService carModelService;
        ICarBodyTypeService carBodyTypeService;
        public CarModelController(ICarModelService carModelService, ICarBodyTypeService carBodyTypeService)
        {
            this.carModelService = carModelService;
            this.carBodyTypeService = carBodyTypeService;
        }
        public async Task<IActionResult> Index(string? category, int pageno = 1)
        {
            ViewBag.CurrentCategory = category;

            var carModelResponse = await carModelService.GetCarModelListAsync(category, pageno);

            if (!carModelResponse.Success)
                return NotFound(carModelResponse.ErrorMessage);

            var categories = await carBodyTypeService.GetCarBodyTypeListAsync();
            ViewBag.Categories = categories.Data;

            string s = Request.Headers["x-requested-with"].ToString();

            if (Request.IsAjaxRequest())
                return PartialView(carModelResponse);
            else
                return View(carModelResponse);
        }

    }
}
