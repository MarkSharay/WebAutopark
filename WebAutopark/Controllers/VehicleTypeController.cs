using Autopark.DAL.Entities;
using Autopark.DAL.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace WebAutopark.Controllers
{
    public class VehicleTypeController : Controller
    {

        private readonly IRepository<VehicleType> repository;

        public VehicleTypeController(IRepository<VehicleType> repository)
        {
            this.repository = repository;
        }

        public async Task<ActionResult> Index()
        {
            var types = await repository.GetList();
            return View(types);
        }

        [HttpPost]
        public async Task<ActionResult> Create(VehicleType vehicleType)
        {
            await repository.Create(vehicleType);
            return RedirectToAction("Index");
        }

        [HttpPost]
        public async Task<ActionResult> Delete(int id)
        {
            await repository.Delete(id);
            return RedirectToAction("Index");
        }

        public async Task<ActionResult> Edit(int id)
        {
            VehicleType type = await repository.Get(id);
            return View(type);
        }

        [HttpPost]
        public async Task<ActionResult> EditConfirm(VehicleType type)
        {
            await repository.Update(type);
            return RedirectToAction("Index");
        }
    }
}
