using Facet;
using Infra;

[Facet(
    sourceType: typeof(Category),
    exclude:[
        nameof(Category.CategoryId),
        nameof(Category.ProductsByCategory)
    ])]
public partial class UpdateCategoryRequestDto;