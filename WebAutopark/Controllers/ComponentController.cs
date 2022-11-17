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

        public async Task<IActionResult> Index()
        {
            var components = await componentRepository.GetList();
            return View(components);
        }

        public ActionResult Create() //rename
        {
            return View();
        }

        [HttpPost]
        public async Task<ActionResult> Create(Component component)
        {
            await componentRepository.Create(component);
            return RedirectToAction("Index");
        }

        [HttpPost] //we usually use HttpDelete attribute for delete method
        public async Task<ActionResult> Delete(int id)
        {
            await componentRepository.Delete(id);
            return RedirectToAction("Index");
        }

        public async Task<ActionResult> Edit(int id) //rename
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
