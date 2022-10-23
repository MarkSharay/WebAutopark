namespace WebAutopark.Models
{
    public class Order
    {
        public int OrderId { get; set; }
        public int VehicleId { get; set; }
        public string Date { get; private set; }

        public Order()
        {

        }

        public Order(int vehicleId, string date)
        {
            VehicleId = vehicleId;
            Date = date;
        }
    }
}
