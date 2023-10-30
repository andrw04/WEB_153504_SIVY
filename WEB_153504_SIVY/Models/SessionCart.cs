using System.Text.Json.Serialization;
using WEB_153504_SIVY.Domain.Entities;
using WEB_153504_SIVY.Extensions;

namespace WEB_153504_SIVY.Models
{
    public class SessionCart : Cart
    {
        [JsonIgnore]
        public ISession? _session;

        public static Cart GetCart(IServiceProvider services)
        {
            ISession? session = services.GetRequiredService<IHttpContextAccessor>()
                .HttpContext?.Session;
            SessionCart cart = session?.Get<SessionCart>("cart") ?? new SessionCart();
            cart._session = session;

            return cart;
        }

        public override void AddToCart(CarModel carModel)
        {
            base.AddToCart(carModel);
            _session.Set<Cart>("cart", this);
        }
        public override void RemoveItems(int id)
        {
            base.RemoveItems(id);
            _session.Set<Cart>("cart", this);
        }
    }
}
