using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataAccess.CartModel;

namespace BusinessAccess.Business
{
    public class ShoppingCart
    {
        public List<CartItem> Items { get; set; } = new List<CartItem>();

        public void AddToCart(CartItem item)
        {
            var existingItem = Items.FirstOrDefault(i => i.CourseId == item.CourseId);
            if (existingItem != null)
            {
                existingItem.Quantity += item.Quantity;
            }
            else
            {
                Items.Add(item);
            }
        }

        public void RemoveFromCart(int courseId)
        {
            var item = Items.FirstOrDefault(i => i.CourseId == courseId);
            if (item != null)
            {
                Items.Remove(item);
            }
        }
        public decimal Price()
        {
            return Items.Sum(i => (i.Price));
        }
        public decimal GetDiscount()
        {
            return Items.Sum(i => (i.DiscountPrice));
        }
        public decimal GetTotal()
        {
            return Items.Sum(i => (i.Price - i.DiscountPrice));
        }
    }
}

