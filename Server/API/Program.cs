using Infra;
using LinqToDB;

var builder = WebApplication.CreateBuilder(args);

var options = new DataOptions<MyDatabaseConnection>(
    new DataOptions().UseSQLite (" Data Source=db.db"));

builder.Services.AddScoped<MyDatabaseConnection>(_ => 
    new MyDatabaseConnection(options));

builder.Services.AddScoped<ProductService>();
builder.Services.AddControllers();

var app = builder.Build();
//where to move this to?? arrow down
using (var scope = app.Services.CreateScope())
{
    var db = scope.ServiceProvider.GetService<MyDatabaseConnection>();
    db.CreateTable<Product>(tableOptions:TableOptions.CreateIfNotExists);
    if (db.Products.Count() == 0)
    {
        db.Insert(new Product()
            {
                ProductId = Guid.NewGuid().ToString(),
                ProductName = "Apple"
            });
    }
    
}


app.MapControllers();

app.Run();