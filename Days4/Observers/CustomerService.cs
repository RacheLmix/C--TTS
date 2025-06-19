using System;
using Models;

namespace Observers
{
    public class CustomerService
    {
        public void Subscribe(Order order)
        {
            order.StatusChanged += (sender, e) =>
            {
                if (e.NewStatus == "Hủy" || e.NewStatus == "Giao thất bại")
                {
                    Console.WriteLine($"☎️ CSKH: Gọi khách hàng về đơn #{e.Order.Id} bị {e.NewStatus.ToLower()}");
                }
            };
        }
    }
}
