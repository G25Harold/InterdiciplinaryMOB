using Infra;
using LinqToDB;
using Service.Security;

var builder = WebApplication.CreateBuilder(args);

var options = new DataOptions<MyDatabaseConnection>(
    new DataOptions().UseSQLite (" Data Source=../Infra/db.db"));

builder.Services.AddScoped<MyDatabaseConnection>(_ => 
    new MyDatabaseConnection(options));

builder.Services.AddScoped<ProductService>();
builder.Services.AddScoped<UserService>();
builder.Services.AddScoped<CategoryService>();
builder.Services.AddScoped<IPasswordHasher,Argon2PasswordHasher>();
builder.Services.AddControllers();
builder.Services.AddOpenApiDocument();
builder.Services.AddCors();
builder.Services.AddProblemDetails();
builder.Services.AddExceptionHandler<MyExceptionHandler>();

var app = builder.Build();

//where to move this to?? arrow down : create seeder
using (var scope = app.Services.CreateScope())
{
    var db = scope.ServiceProvider.GetService<MyDatabaseConnection>();
    db.CreateTable<Product>(tableOptions:TableOptions.CreateIfNotExists);
    db.CreateTable<Category>(tableOptions:TableOptions.CreateIfNotExists);
    db.CreateTable<User>(tableOptions:TableOptions.CreateIfNotExists);
    
    if (db.Categories.Count() == 0)
    {
        db.Insert(new Category()
        {
            CategoryId = "1",
            CategoryName = "Tree"
        });
    }
    
    if (db.Products.Count() == 0)
    {
        db.Insert(new Product()
            {
                ProductId = "1",
                ProductName = "Apple",
                CategoryId = "1"
            });
    }
    
    
    
}

app.UseExceptionHandler();
app.UseCors(config =>config.AllowAnyHeader().AllowAnyMethod().AllowAnyOrigin().SetIsOriginAllowed(_ => true));
app.MapControllers();
app.UseOpenApi();
app.UseSwaggerUi();
app.Run();