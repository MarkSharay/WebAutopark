using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Autopark.DAL.Interfaces;
using Autopark.DAL.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Autopark.DAL.Repositories;

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
                    vehicles = vehicles.OrderBy(vehicles => vehicles.RegistrationNumber);
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
        public async Task<IActionResult> Create()
        {
            var types = await vehicleTypeRepository.GetList();
            ViewBag.types = types.Select(type => new SelectListItem(type.TypeName, type.VehicleTypeId.ToString()));
            return View();
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
            var types = await vehicleTypeRepository.GetList();
            ViewBag.types = types.Select(type => new SelectListItem(type.TypeName, type.VehicleTypeId.ToString()));
            Vehicle vehicle = await vehicleRepository.Get(id);
            return View(vehicle);
        }
        [HttpPost()]
        public async Task<ActionResult> ConfirmEdit(Vehicle vehicle)
        {
            await vehicleRepository.Update(vehicle);
            return RedirectToAction("Index");
        }
        [HttpGet]
        public async Task<ActionResult> GetInfo(int id)
        {
            var vehicle = await vehicleRepository.Get(id);
            vehicle.Type = await vehicleTypeRepository.Get(vehicle.VehicleTypeId);
            return View(vehicle);
        }
    }
}
