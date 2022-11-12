using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Autopark.DAL.Entities
{
    public class Vehicle
    {
        public int VehicleId { get; set; }
        public int VehicleTypeId { get; set; }
        public VehicleType Type { get; set; }
        public string Model { get; set; }
        public string RegistrationNumber { get; set; }
        public double Weight { get; set; }
        public int Year { get; set; }
        public int MileAge { get; set; }
        public Colors Color { get; set; }
        public double Volume { get; set; }
        public double FuelConsumption { get; set; }


        public double GetCalcTaxPerMonth()
        {
            return (this.Weight * 0.00013) + (Type.TaxCoefficient * 30) + 5;
        }
        public double GetMaxKilometers()
        {
            return Volume / FuelConsumption * 100;
        }

    }
}
