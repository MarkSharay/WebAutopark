using Autopark.DAL.Interfaces;
using Autopark.DAL.Entities;
using Microsoft.AspNetCore.Mvc;

namespace WebAutopark.Controllers
{
    public class VehicleController : Controller
    {
        private readonly IRepository<Vehicle> vehicleRepository;
        private readonly IRepository<VehicleType> vehicleTypeRepository;
        public VehicleController(IRepository<Vehicle> _vehicleRepository, IRepository<VehicleType> _vehicleTypeRepository)
        {
            vehicleRepository = _vehicleRepository;
            vehicleTypeRepository = _vehicleTypeRepository;
        }

        public async Task<IActionResult> Index(string sortOption)
        {
            var vehicles = await vehicleRepository.GetList();

            var vehicleTypes = await vehicleTypeRepository.GetList();

            foreach(var item in vehicles)
            {
                item.Type = await vehicleTypeRepository.Get(item.VehicleTypeId);
            }
            switch (sortOption){
                case "model":
                    vehicles = vehicles.OrderBy(vehicles => vehicles.Model);
                    break;
                case "number":
                    vehicles = vehicles.OrderBy(vehicles => vehicles.Number);
                    break;
                case "type":
                    vehicles = vehicles.OrderBy(vehicles => vehicles.Type.TypeName);
                    break;
                case "id":
                    vehicles = vehicles.OrderBy(vehicles => vehicles.VehicleId);
                    break;
                default:
                    vehicles = vehicles.OrderBy(vehicles => vehicles.VehicleId);
                    break;
            }
            
            return View(vehicles);
        }

        [HttpPost]
        public async Task<ActionResult> Create(Vehicle vehicle)
        {
            await vehicleRepository.Create(vehicle);
            return RedirectToAction("Index");
        }

        [HttpPost]
        public async Task<ActionResult> Delete(int id)
        {
            await vehicleRepository.Delete(id);
            return RedirectToAction("Index");
        }

        public async Task<ActionResult> Edit(int id)
        {
            Vehicle vehicle = await vehicleRepository.Get(id);
            return View(vehicle);
        }

        public async Task<ActionResult> ComfirmEdit(Vehicle vehicle)
        {
            await vehicleRepository.Update(vehicle);
            return RedirectToAction("Index");
        }
    }
}
