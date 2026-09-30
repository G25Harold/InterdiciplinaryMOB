
using System.ComponentModel.DataAnnotations;
using Infra;
using LinqToDB;


public class ProductService(MyDatabaseConnection db)
{
    public List<ProductDto> GetProducts(int page, int resultsPerPage)
    {
        if (page < 1)
            throw new ValidationException("Page must be 1 or higher");
        if (resultsPerPage < 1)
            throw new ValidationException("Must have 1 or more results per page");
        return db.Products
            .LoadWith(p=>p.Category)
            .ThenLoad(c => c.ProductsByCategory)
            .Take(resultsPerPage )
            .Skip((page - 1) * resultsPerPage)
            .Select(p=>new ProductDto(p)
            {
                Category = new CategoryDto(p.Category)
                
            })
            .ToList();


    }

    public ProductDto CreateProduct(CreateProductRequestDto productRequestDto)
    {
        if (productRequestDto.ProductPrice <0 )
            throw new ValidationException("Price must be greater than 0");
        
        var p = new Product()
        {
            ProductId = Guid.NewGuid().ToString(),
            ProductName = productRequestDto.ProductName,
            ProductPrice = productRequestDto.ProductPrice,
            CategoryId = productRequestDto.CategoryId,

        };
        
        db.Insert(p);
        return new ProductDto(p);
    }
}

/* public string productName { get; set; }
    public string productDescription { get; set; }
    public string productCategory { get; set; }
    public decimal productPrice { get; set; }*/