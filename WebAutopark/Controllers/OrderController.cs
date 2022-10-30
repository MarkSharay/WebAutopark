using Autopark.DAL.Entities;
using Autopark.DAL.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace WebAutopark.Controllers
{
    public class OrderController : Controller
    {
        private readonly IRepository<Vehicle> vehicleRepository;
        private readonly IRepository<Order> orderRepository;
        public OrderController(IRepository<Vehicle> _vehicleRepository, IRepository<Order> _orderRepository)
        {
            vehicleRepository = _vehicleRepository;
            orderRepository = _orderRepository;
        }

        public async Task<IActionResult> Index(string sortOption)
        {
            var vehicles = await vehicleRepository.GetList();

            var orders = await orderRepository.GetList();

            foreach (var item in orders)
            {
                item.Vehicle = await vehicleRepository.Get(item.VehicleId);
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

            return View(vehicles);
        }

        [HttpPost]
        public async Task<ActionResult> Create(Order order)
        {
            await orderRepository.Create(order);
            return RedirectToAction("Index");
        }

        [HttpPost]
        public async Task<ActionResult> Delete(int id)
        {
            await orderRepository.Delete(id);
            return RedirectToAction("Index");
        }

        public async Task<ActionResult> Edit(int id)
        {
            Order order = await orderRepository.Get(id);
            return View(order);
        }

        public async Task<ActionResult> ComfirmEdit(Order order)
        {
            await orderRepository.Update(order);
            return RedirectToAction("Index");
        }
    }
}
