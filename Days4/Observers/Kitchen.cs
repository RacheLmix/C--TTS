using System;
using Models;

namespace Observers
{
    public class Kitchen
    {
        public void Subscribe(Order order)
        {
            order.StatusChanged += (sender, e) =>
            {
                if (e.NewStatus == "Mới tạo")
                {
                    Console.WriteLine($"👨‍🍳 Kitchen: Chuẩn bị món cho đơn #{e.Order.Id}");
                }
            };
        }
    }
}
