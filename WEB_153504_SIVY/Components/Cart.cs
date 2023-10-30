using Microsoft.AspNetCore.Mvc;
using WEB_153504_SIVY.Extensions;
using System.Text.Json;
using NuGet.Protocol;

namespace WEB_153504_SIVY.Components
{
    [ViewComponent]
    public class Cart
    {
        private readonly HttpContext _context;
        private readonly WEB_153504_SIVY.Domain.Entities.Cart _cart;

        public Cart(IHttpContextAccessor httpContextAccessor, WEB_153504_SIVY.Domain.Entities.Cart cart)
        {
            _context = httpContextAccessor.HttpContext;
            _cart = cart;
        }
        public async Task<string> InvokeAsync()
        {
            if (_cart != null)
                return string.Format("{0:f2}$ ({1})", _cart.TotalPrice, _cart.Count);
            else
                return "0,00 (0)";
        }
    }
}
