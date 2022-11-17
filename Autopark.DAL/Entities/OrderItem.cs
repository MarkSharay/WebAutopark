namespace Autopark.DAL.Entities
{
    public class OrderItem
    {
        public int OrderItemId { get; set; }
        public int OrderId { get; set; }
        public Order Order { get; set; }
        public int ComponentId { get; set; }
        public Component Component { get; set; }
        public int Quantity { get; set; }

        public OrderItem(int orderId, int componentId, int quantity) // Entities should not have constructors
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
