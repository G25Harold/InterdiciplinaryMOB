
using System.ComponentModel.DataAnnotations;
using Infra;
using LinqToDB;


public class ProductService(MyDatabaseConnection db)
{
    public List<ProductDto> GetProducts(int page, int resultsPerPage)
    {
        if (page < 1)
            throw new ValidationException("Page must be 1 or higher");
        if (resultsPerPage < 20)
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

    public List<ProductDto> GetMyProducts(string sellerId)
    {
        return db.Products
            .Where(p=> p.SellerId == sellerId)
            .Select(p=> new ProductDto(p)
            {
                Category = new CategoryDto(p.Category)
            })
            .ToList();
    }

    public ProductDto CreateProduct(CreateProductRequestDto productRequestDto, string sellerId)
    {
        if (productRequestDto.ProductPrice <0 )
            throw new ValidationException("Price must be greater than 0");
        if (productRequestDto.Inventory <0)
            throw new ValidationException("Inventory cant't be less than 0");
        if (!db.Categories.Any(
                c=> c.CategoryId == productRequestDto.CategoryId))
            throw new ValidationException("Category doesn't exist");
        
        var p = new Product
        {
            ProductId = Guid.NewGuid().ToString(),
            ProductName = productRequestDto.ProductName,
            ProductPrice = productRequestDto.ProductPrice,
            CategoryId = productRequestDto.CategoryId,
            Inventory = productRequestDto.Inventory,
            SellerId = sellerId

        };
        
        db.Insert(p);
        return new ProductDto(p);
    }
}
