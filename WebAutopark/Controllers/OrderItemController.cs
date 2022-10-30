using Autopark.DAL.Entities;
using Autopark.DAL.Interfaces;
using Autopark.DAL.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace WebAutopark.Controllers
{
    public class OrderItemController : Controller
    {
        private readonly IRepository<Component> componentRepository;
        private readonly IRepository<Order> orderRepository;
        private readonly IRepository<OrderItem> orderItemRepository;
        public OrderItemController(IRepository<Component> _componentRepository, IRepository<Order> _orderRepository, IRepository<OrderItem> _orderItemRepository)
        {
            componentRepository = _componentRepository;
            orderRepository = _orderRepository;
            orderItemRepository = _orderItemRepository;
        }

        public async Task<IActionResult> Index(string sortOption)
        {
            var orderItems = await orderItemRepository.GetList();
            var orders = await orderRepository.GetList();
            var components = await componentRepository.GetList();

            foreach (var item in orderItems)
            {
                item.Order = await orderRepository.Get(item.OrderId);
                item.Component = await componentRepository.Get(item.ComponentId);
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

            return View(orderItems);
        }

        [HttpPost]
        public async Task<ActionResult> Create(OrderItem orderItem)
        {
            await orderItemRepository.Create(orderItem);
            return RedirectToAction("Index");
        }

        [HttpPost]
        public async Task<ActionResult> Delete(int id)
        {
            await orderItemRepository.Delete(id);
            return RedirectToAction("Index");
        }

        public async Task<ActionResult> Edit(int id)
        {
            OrderItem orderItem = await orderItemRepository.Get(id);
            return View(orderItem);
        }

        public async Task<ActionResult> ComfirmEdit(OrderItem orderItem)
        {
            await orderItemRepository.Update(orderItem);
            return RedirectToAction("Index");
        }
    }
}
