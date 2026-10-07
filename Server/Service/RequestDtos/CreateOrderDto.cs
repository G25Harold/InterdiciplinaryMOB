namespace Service.RequestDtos;

public class CreateOrderDto
{
    public string ProductId { get; set; }
    public int Quantity { get; set; }
}