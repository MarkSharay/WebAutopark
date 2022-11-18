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

        public ActionResult GetCreateView()
        {
            return View("Create");
        }

        [HttpDelete]
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

        public async Task<ActionResult> GetEditView(int id)
        {
            Component component = await componentRepository.Get(id);
            return View("Edit", component);
        }

        public async Task<ActionResult> ComfirmEdit(Component component)
        {
            await componentRepository.Update(component);
            return RedirectToAction("Index");
        }
    }
}
