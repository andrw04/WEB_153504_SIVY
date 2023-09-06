using Microsoft.AspNetCore.Mvc;
using WEB_153504_SIVY.Services.CarBrandService;
using WEB_153504_SIVY.Services.CarModelService;

namespace WEB_153504_SIVY.Controllers
{
    public class CarModel : Controller
    {
        ICarModelService carModelService;
        ICarBrandService carBrandService;
        public CarModel(ICarModelService carModelService, ICarBrandService carBrandService)
        {
            this.carModelService = carModelService;
            this.carBrandService = carBrandService;
        }

        public async Task<IActionResult> Index(string category)
        {
            var carModelResponse = await carModelService.GetCarModelListAsync("bmw");

            if (!carModelResponse.Success)
                return NotFound(carModelResponse.ErrorMessage);

            return View(carModelResponse.Data);
        }

    }
}
