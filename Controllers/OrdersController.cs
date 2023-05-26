using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using FlashDeals.Data;
using FlashDeals.Data.Entities;


namespace FlashDeals.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrdersController : ControllerBase
    {
        private readonly DataContext _dataContext;

        public OrdersController(DataContext dataContext)
        {
            _dataContext = dataContext;
        }

        [HttpGet]
        public async Task<IActionResult> GetAsync()
        {
            var orders = await _dataContext.Orders.ToListAsync();
            return Ok(orders);
        }

        [HttpGet]
        [Route("{CustomerId}")]
        public async Task<IActionResult> GetOrderByCustomerIdAsync(int CustomerId)
        {
            var orders = await _dataContext.Orders.Where(order => order.CustomerId == CustomerId).ToListAsync();
            return Ok(orders);
        }

        [HttpPost]
        [Route("Create")]
        public async Task<IActionResult> PostAsync(Orders order)
        {
            var orderedProduct = await _dataContext.Products.Where(product => product.ProductId == order.ProductId).FirstOrDefaultAsync();
            if(orderedProduct == null)
            {
                return Ok("Product that you ordered does not exist.");
            }

            int year = orderedProduct.ExpireTime.Value.Year;
            int month = orderedProduct.ExpireTime.Value.Month;
            int day = orderedProduct.ExpireTime.Value.Day;
            DateTime FlashStartDate = new DateTime(year, month, day, 0, 0, 0);

            if (!(DateTime.UtcNow >= FlashStartDate && DateTime.UtcNow <= orderedProduct.ExpireTime)) // Checking Expiry time.
                return Ok("You can not order product.");

            if (orderedProduct.AvailableUnits < order.RequiredUnits) // Checking available units.
            {
                return Ok($"There are only exist {orderedProduct.AvailableUnits.ToString} available units.");
            }
            else
            {
                order.CreatedAt = DateTime.UtcNow;
                _dataContext.Orders.Add(order);
                await _dataContext.SaveChangesAsync();
                return Created($"/getOrderById?id={order.OrderId}", order);
            }
        }

        [HttpPost]
        [Route("Approve/{OrderId}")]
        public async Task<IActionResult> PostApproveAsync(int OrderId)
        {
            var singleOrder = await _dataContext.Orders.FindAsync(OrderId);
            if (singleOrder == null)
            {
                return Ok("This order does not exist");
            }
            singleOrder.UpdatedAt = DateTime.UtcNow;

            singleOrder.State = "Approved";
            _dataContext.Entry(singleOrder).CurrentValues.SetValues(singleOrder);
            await _dataContext.SaveChangesAsync();
            return Ok(singleOrder);
        }

        [HttpPut]
        [Route("Update")]
        public async Task<IActionResult> PutAsync(Orders order)
        {
            var singleOrder = await _dataContext.Orders.FindAsync(order.OrderId);
            if (singleOrder == null)
            {
                return Ok("This order does not exist");
            }
            order.UpdatedAt = DateTime.UtcNow;
            _dataContext.Entry(singleOrder).CurrentValues.SetValues(order);
            await _dataContext.SaveChangesAsync();
            return Ok(order);
        }

        [HttpDelete]
        [Route("{OrderId}")]
        public async Task<IActionResult> DeleteAsync(int OrderId)
        {
            var orderToDelete = await _dataContext.Orders.FindAsync(OrderId);
            if (orderToDelete == null)
            {
                return Ok("Order does not exist");
            }
            _dataContext.Orders.Remove(orderToDelete);
            await _dataContext.SaveChangesAsync();
            return Ok("Order deleted successfully");
        }
    }
}
