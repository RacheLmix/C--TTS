using System;

namespace Models
{
    public class OrderEventArgs : EventArgs
    {
        public Order Order { get; }
        public string NewStatus { get; }

        public OrderEventArgs(Order order, string newStatus)
        {
            Order = order;
            NewStatus = newStatus;
        }
    }
}
