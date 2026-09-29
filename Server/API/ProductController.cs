using Infra;
using Microsoft.AspNetCore.Mvc;

public class ProductController(ProductService service) : ControllerBase
{
    [HttpGet(nameof(GetProducts))]
    public List<Product> GetProducts(int page, int resultsPerPage)
    {
        return service.GetProducts(page, resultsPerPage);
    } 
}