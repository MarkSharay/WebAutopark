﻿namespace Autopark.DAL.Entities
{
    public class OrderItem
    {
        public int OrderItemId { get; set; }
        public int OrderId { get; set; }
        public int ComponentId { get; set; }
        public int Quantity { get; set; }

        public OrderItem(int orderId, int componentId, int quantity)
        {
            OrderId = orderId;
            ComponentId = componentId;
            Quantity = quantity;
        }

        public OrderItem()
        {

        }
    }
}