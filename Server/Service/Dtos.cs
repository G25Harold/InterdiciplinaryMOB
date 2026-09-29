using System.Security.AccessControl;
using Facet;
using Infra;

[Facet(sourceType:typeof(Category),exclude:nameof(Category.ProductsByCategory))]
public partial class CategoryDto;

[Facet(sourceType: typeof(Product), exclude: nameof(Product.Category))]
public partial class ProductDto
{
    public CategoryDto  Category { get; set; }
}