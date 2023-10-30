using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WEB_153504_SIVY.Domain.Entities
{
    public class Cart
    {
        public Dictionary<int, CartItem> CartItems { get; set; } = new();

        public virtual void AddToCart(CarModel carModel)
        {
            if (!CartItems.ContainsKey(carModel.Id))
            {
                CartItems.Add(carModel.Id, new CartItem() { CarModel = carModel, Quantity = 1 });
            }
            else
            {
                CartItems[carModel.Id].Quantity++;
            }
        }

        public virtual void RemoveItems(int id)
        {
            if (CartItems.ContainsKey(id))
            {
                if (CartItems[id].Quantity == 1)
                    CartItems.Remove(id);
                else
                    CartItems[id].Quantity--;
            }
        }

        public virtual void ClearAll() 
        {

        }

        public int Count { get => CartItems.Sum(item => item.Value.Quantity); }

        public double TotalPrice { get => CartItems.Sum(item => item.Value.CarModel.Price * item.Value.Quantity); }
    }
}
