using Infra;
using Microsoft.AspNetCore.Mvc;

public class ProductController(ProductService service) : ControllerBase
{
    [HttpGet(nameof(GetProducts))]
    public List<Product> GetProducts()
    {
        return service.GetProducts();
    } 
}