using examen_web.Data;
using examen_web.Model;
using examen_web.Model.DTOs;
using examen_web.Model.One_to_Many;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;

namespace examen_web.Controllers
{
    public class DatabaseController : ControllerBase
    {
        private readonly AppDbContext _appContext;

        public DatabaseController(AppDbContext appContext)
        {
            _appContext = appContext;
        }

        [HttpPost("user")]
        public async Task<IActionResult> CreateUser([FromBody] UserDto userDto)
        {
            Console.WriteLine("userDto.Name: " + userDto.Name);
            var newUser = new User
            {
                Id = Guid.NewGuid(),
                Name = userDto.Name,
                DateCreated = new DateTime()
            };

            await _appContext.AddAsync(newUser);
            await _appContext.SaveChangesAsync();

            return Ok(newUser);
        }

        [HttpPost("product")]
        public async Task<IActionResult> CreateProduct([FromBody] ProductDto productDto)
        {
            Console.WriteLine(productDto);
            var newProduct = new Product
            {
                Id = Guid.NewGuid(),
                Name = productDto.Name,
                Price = productDto.Price,
                DateCreated = new DateTime()
            };

            await _appContext.AddAsync(newProduct);
            await _appContext.SaveChangesAsync();

            return Ok(newProduct);
        }

        [HttpPost("order")]
        public async Task<IActionResult> CreateOrder([FromBody] OrderDto orderDto)
        {
            var user = await _appContext.Users.FindAsync(orderDto.UserId);
            if (user == null)
            {
                return NotFound("User not found");
            }

            var order = new Order
            {
                Id = Guid.NewGuid(),
                UserId = orderDto.UserId,
                DateCreated = DateTime.UtcNow
            };

            foreach (var productId in orderDto.ProductIds)
            {
                var product = await _appContext.Products.FindAsync(productId);
                if (product == null)
                {
                    return NotFound($"Product with ID {productId} not found");
                }
                var existingOrderProduct = await _appContext.OrdersProducts
   .FirstOrDefaultAsync(op => op.OrderId == order.Id && op.ProductId == productId);

                if (existingOrderProduct != null)
                {
                    return BadRequest("This product is already in the order");
                }

                var orderProduct = new OrderProduct
                {
                    OrderId = order.Id,
                    ProductId = productId
                };

                await _appContext.OrdersProducts.AddAsync(orderProduct);
            }

            await _appContext.Orders.AddAsync(order);
            await _appContext.SaveChangesAsync();

            return Ok(order);
        }


        // simple gets
        [HttpGet("orders")]
        public async Task<IActionResult> GetOrders()
        {
            var orders = await _appContext.Orders
                   .Include(o => o.OrdersProducts)
                   .ThenInclude(op => op.Product)
                   .ToListAsync();

            return Ok(orders);
        }

        [HttpGet("products")]
        public async Task<IActionResult> GetProducts()
        {
            var products = await _appContext.Products
                            .Include(p => p.OrdersProducts)
                            .ThenInclude(op => op.Order)
                            .ToListAsync();

            return Ok(products);
        }

        [HttpGet("users")]
        public async Task<IActionResult> GetUsers()
        {
            var users = await _appContext.Users
                    .Include(u => u.Orders)
                    .ThenInclude(o => o.OrdersProducts)
                    .ToListAsync();

            return Ok(users);
        }

        [HttpPost("orderproduct")]
        public async Task<IActionResult> AddProductToOrder([FromBody] OrderProductDto orderProductDto)
        {
            Console.WriteLine(orderProductDto);
            var order = await _appContext.Orders.FindAsync(orderProductDto.OrderId);
            var product = await _appContext.Products.FindAsync(orderProductDto.ProductId);

            if (order == null || product == null)
            {
                return NotFound();
            }

            var existingOrderProduct = await _appContext.OrdersProducts
    .FirstOrDefaultAsync(op => op.OrderId == orderProductDto.OrderId && op.ProductId == orderProductDto.ProductId);

            if (existingOrderProduct != null)
            {
                return BadRequest("This product is already in the order");
            }

            var orderProduct = new OrderProduct
            {
                OrderId = orderProductDto.OrderId,
                ProductId = orderProductDto.ProductId
            };

            await _appContext.OrdersProducts.AddAsync(orderProduct);
            await _appContext.SaveChangesAsync();

            return Ok(orderProduct);
        }
    }
}