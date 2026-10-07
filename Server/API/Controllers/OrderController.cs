using System.Security.Claims;
using Infra;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Service.RequestDtos;

[ApiController]
[Route("api/[controller]")]
[Authorize]
public class OrderController(OrderService service) : ControllerBase
{
    [HttpPost(nameof(CreateOrder))]
    public ActionResult<Order> CreateOrder(CreateOrderRequestDto dto)
    {
        var buyerId =
            User.FindFirstValue(ClaimTypes.NameIdentifier);
        if (buyerId is null)
            return Unauthorized();
        var order = service.CreateOrder(dto, buyerId);
        return Ok(order);
    }
}