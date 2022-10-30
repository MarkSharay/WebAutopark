using Autopark.DAL.Entities;
using Autopark.DAL.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace WebAutopark.Controllers
{
    public class ComponentController : Controller
    {
        private readonly IRepository<Component> componentRepository;
        public ComponentController(IRepository<Component> _componentRepository)
        {
            componentRepository = _componentRepository;
        }

        public async Task<IActionResult> Index(string sortOption)
        {
            var components = await componentRepository.GetList();

            switch (sortOption)
            {
                case "name":
                    components = components.OrderBy(components=>components.Name);
                    break;
                case "id":
                    components = components.OrderBy(vehicles => vehicles.ComponentId);
                    break;
                default:
                    components = components.OrderBy(vehicles => vehicles.ComponentId);
                    break;
            }

            return View(components);
        }

        [HttpPost]
        public async Task<ActionResult> Create(Component component)
        {
            await componentRepository.Create(component);
            return RedirectToAction("Index");
        }

        [HttpPost]
        public async Task<ActionResult> Delete(int id)
        {
            await componentRepository.Delete(id);
            return RedirectToAction("Index");
        }

        public async Task<ActionResult> Edit(int id)
        {
            Component component = await componentRepository.Get(id);
            return View(component);
        }

        public async Task<ActionResult> ComfirmEdit(Component component)
        {
            await componentRepository.Update(component);
            return RedirectToAction("Index");
        }
    }
}
