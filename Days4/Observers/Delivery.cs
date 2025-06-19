using System;
using Models;

namespace Observers
{
    public class Delivery
    {
        public void Subscribe(Order order)
        {
            order.StatusChanged += (sender, e) =>
            {
                if (e.NewStatus == "Đang giao")
                {
                    Console.WriteLine($"🚚 Delivery: Shipper nhận đơn #{e.Order.Id}");
                }
            };
        }
    }
}
