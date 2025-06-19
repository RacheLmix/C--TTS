using System;
using System.Collections.Generic;
using System.Linq;
using Models;
using Observers;

class Program
{
    static void Main()
    {
        var kitchen = new Kitchen();
        var delivery = new Delivery();
        var cskh = new CustomerService();

        var orders = new List<Order>
        {
            new Order(1, "An"),
            new Order(2, "Bình"),
            new Order(3, "Chi")
        };

        // Delegate - Func, Predicate, Action
        Predicate<Order> isDelivering = o => o.Status == "Đang giao";
        Func<Order, string> describe = o => $"Đơn #{o.Id} của {o.CustomerName} - Trạng thái: {o.Status}";
        Action<string> log = msg => Console.WriteLine($"[Log] {msg}");

        foreach (var order in orders)
        {
            kitchen.Subscribe(order);
            delivery.Subscribe(order);
            cskh.Subscribe(order);

            // Log sự kiện mỗi lần thay đổi
            order.StatusChanged += (sender, e) =>
            {
                var info = describe(e.Order);
                log(info);
            };
        }

        // Mô phỏng thay đổi trạng thái
        orders[0].UpdateStatus("Mới tạo");
        orders[0].UpdateStatus("Đang giao");
        orders[0].UpdateStatus("Hoàn tất");

        orders[1].UpdateStatus("Mới tạo");
        orders[1].UpdateStatus("Hủy");

        orders[2].UpdateStatus("Mới tạo");
        orders[2].UpdateStatus("Đang giao");
        orders[2].UpdateStatus("Giao thất bại");

        Console.WriteLine("\n📊 Thống kê:");
        var deliveredCount = orders.Count(o => o.Status == "Hoàn tất");
        var canceledCount = orders.Count(o => o.Status == "Hủy" || o.Status == "Giao thất bại");

        Console.WriteLine($"✅ Đơn giao thành công: {deliveredCount}");
        Console.WriteLine($"❌ Đơn bị hủy/thất bại: {canceledCount}");
    }
}
