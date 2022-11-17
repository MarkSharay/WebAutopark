using Autopark.DAL.Entities;
using Autopark.DAL.Interfaces;
using Autopark.DAL.Repositories;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace WebAutopark.Controllers
{
    public class OrderItemController : Controller
    {
        private readonly IRepository<Component> componentRepository;
        private readonly IRepository<OrderItem> orderItemRepository;
        private readonly IRepository<Order> orderRepository;
        private readonly IRepository<Vehicle> vehicleRepository; 
        public OrderItemController(IRepository<Component> _componentRepository, IRepository<Order> _orderRepository, IRepository<OrderItem> _orderItemRepository, IRepository<Vehicle> _vehicleRepository)
        {
            orderRepository = _orderRepository;
            componentRepository = _componentRepository;
            orderItemRepository = _orderItemRepository;
            vehicleRepository = _vehicleRepository;
        }

        [HttpGet]
        public async Task<IActionResult> Create(int id) //rename
        {
            var components = await componentRepository.GetList();
            var order = await orderRepository.Get(id);
            var vehicle = await vehicleRepository.Get(order.VehicleId);
            ViewBag.Vehicle = vehicle;
            ViewBag.Components = components.Select(component => new SelectListItem(component.Name, component.ComponentId.ToString()));
            return View(new OrderItem() { OrderId = id});
        }

        [HttpPost]
        public async Task<IActionResult> Create(OrderItem orderItem)
        {
            await orderItemRepository.Create(orderItem);
            return RedirectToAction("Index", "Order");
        }

        [HttpGet]
        public async Task<ActionResult> Index(int id)
        {
            var order = await orderRepository.Get(id);
            var vehicle = await vehicleRepository.Get(order.VehicleId);
            var orderItems = await orderItemRepository.GetList();
            var parts = new List<OrderItem>();
            ViewBag.Vehicle = vehicle;
            
            foreach (var orderItem in orderItems)
            {
                if (orderItem.OrderId == id)
                {
                    orderItem.Component = await componentRepository.Get(orderItem.ComponentId); //you can create variable and call componentRepository.GetAll for it and then use created collection to add components for order items.
                    parts.Add(orderItem);
                }
            }

            return View(parts);
        }
    }
}
