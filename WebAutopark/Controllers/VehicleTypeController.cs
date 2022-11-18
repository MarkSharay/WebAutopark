using Autopark.DAL.Entities;
using Autopark.DAL.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace WebAutopark.Controllers
{
    public class VehicleTypeController : Controller
    {

        private readonly IRepository<VehicleType> repository;

        public VehicleTypeController(IRepository<VehicleType> _repository)
        {
            repository = _repository;
        }

        public async Task<IActionResult> Index()
        {
            var types = await repository.GetList();
            return View(types);
        }

        public IActionResult GetCreateView()
        {
            return View("Create");
        }

        [HttpPost]
        public async Task<IActionResult> Create(VehicleType vehicleType)
        {
            await repository.Create(vehicleType);
            return RedirectToAction("Index");
        }

        [HttpPost]
        public async Task<IActionResult> Delete(int id)
        {
            await repository.Delete(id);
            return RedirectToAction("Index");
        }

        public async Task<IActionResult> GetEditView(int id)
        {
            VehicleType type = await repository.Get(id);
            return View("Edit", type);
        }

        [HttpPost()]
        public async Task<IActionResult> EditConfirm(VehicleType type)
        {
            await repository.Update(type);
            return RedirectToAction("Index");
        }
    }
}
