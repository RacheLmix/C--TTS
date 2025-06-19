using System;

namespace Models
{
    public class Order
    {
        public int Id { get; }
        public string CustomerName { get; }
        public string Status { get; private set; }

        public event EventHandler<OrderEventArgs>? StatusChanged;

        public Order(int id, string customerName)
        {
            Id = id;
            CustomerName = customerName;
            Status = "Mới tạo";
        }

        public void UpdateStatus(string newStatus)
        {
            Status = newStatus;
            OnStatusChanged(new OrderEventArgs(this, newStatus));
        }

        protected virtual void OnStatusChanged(OrderEventArgs e)
        {
            StatusChanged?.Invoke(this, e);
        }
    }
}
