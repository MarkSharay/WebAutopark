namespace Autopark.DAL.Entities
{
    public class Order
    {
        public int OrderId { get; set; }
        public Vehicle Vehicle { get; set; }
        public int VehicleId { get; set; }
        public DateTime Date { get; set; }
    }
}
