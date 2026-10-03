using System.Security.AccessControl;
using Facet;
using Infra;

[Facet(sourceType: typeof(Product), exclude:[nameof(Product.Category), nameof(Product.ProductId)])]
public partial class CreateProductRequestDto;