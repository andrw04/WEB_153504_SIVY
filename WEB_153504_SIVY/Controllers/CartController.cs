using Microsoft.AspNetCore.Mvc;
using WEB_153504_SIVY.Services.CarModelService;
using WEB_153504_SIVY.Domain.Entities;
using Microsoft.AspNetCore.Authorization;

namespace WEB_153504_SIVY.Controllers
{
    [Route("[controller]")]
    public class CartController : Controller
    {
        private readonly ICarModelService _carModelService;
        private readonly Cart _cart;
        public CartController(ICarModelService carModelService, Cart cart)
        {
            _carModelService = carModelService;
            _cart = cart;
        }

        [Authorize]
        [Route("add/{id:int}")]
        public async Task<ActionResult> Add(int id, string returnUrl)
        {
            var data = await _carModelService.GetCarModelByIdAsync(id);

            if (data.Success)
            {
                _cart.AddToCart(data.Data);
            }

            return Redirect(returnUrl);
        }

        [Authorize]
        [Route("remove/{id:int}")]
        public IActionResult Remove(int id)
        {
            _cart.RemoveItems(id);

            return RedirectToAction("Index");
        }

        [Authorize]
        public IActionResult Index()
        {
            return View(_cart);
        }
    }
}
