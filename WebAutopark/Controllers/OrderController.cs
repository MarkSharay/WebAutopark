
using Autopark.DAL.Entities;
using Autopark.DAL.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace WebAutopark.Controllers
{
    public class OrderController : Controller
    {
        private readonly IRepository<Vehicle> vehicleRepository;
        private readonly IRepository<Order> orderRepository;
        private readonly IRepository<OrderItem> orderItemRepository;
        private readonly IRepository<Component> componentRepository;

        public OrderController(IRepository<Vehicle> _vehicleRepository, IRepository<Order> _orderRepository, IRepository<OrderItem> _orderItemRepository, IRepository<Component> _componentRepository)
        {
            vehicleRepository = _vehicleRepository;
            orderRepository = _orderRepository;
            orderItemRepository = _orderItemRepository;
            componentRepository = _componentRepository;
        }

        public async Task<IActionResult> Index(string sortOption)
        {
            var vehicles = await vehicleRepository.GetList();

            var orders = await orderRepository.GetList();

            foreach (var item in orders)
            {
                item.Vehicle = vehicles.FirstOrDefault(vehicle => vehicle.VehicleId == item.VehicleId);
            }
            switch (sortOption)
            {
                case "date":
                    orders = orders.OrderBy(orders => orders.Date);
                    break;
                case "id":
                    orders = orders.OrderBy(orders => orders.OrderId);
                    break;
                default:
                    orders = orders.OrderBy(orders => orders.OrderId);
                    break;
            }

            return View(orders);
        }

        public async Task<IActionResult> GetCreateView()
        {
            var vehs = await vehicleRepository.GetList();
            ViewBag.vehicles = vehs.Select(vehicle => new SelectListItem(vehicle.Model, vehicle.VehicleId.ToString()));
            return View("Create");
        }
       
        [HttpPost]
        public async Task<IActionResult> Create(Order order)
        {
            await orderRepository.Create(order);
            return RedirectToAction("Index");
        }

        [HttpDelete]
        public async Task<ActionResult> Delete(int id)
        {
            await orderRepository.Delete(id);
            return RedirectToAction("Index");
        }

        public async Task<ActionResult> ConfirmEdit(Order order)
        {
            await orderRepository.Update(order);
            return RedirectToAction("Index");
        }
    }
}
