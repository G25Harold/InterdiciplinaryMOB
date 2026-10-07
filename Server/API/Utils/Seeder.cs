using Infra;
using LinqToDB;
using Service.Security;

public class Seeder(
    MyDatabaseConnection db,
    IPasswordHasher passwordHasher,
    IConfiguration configuration)
{
    public void Seed()
    {
        db.CreateTable<Product>(tableOptions: TableOptions.CreateIfNotExists);
        db.CreateTable<Category>(tableOptions: TableOptions.CreateIfNotExists);
        db.CreateTable<User>(tableOptions: TableOptions.CreateIfNotExists);
        db.CreateTable<Order>(tableOptions: TableOptions.CreateIfNotExists);
        
        var adminPassword = configuration["SeedAdmin:Password"];

        if (!string.IsNullOrWhiteSpace(adminPassword) &&
            !db.Users.Any(u => u.Username == "admin"))
        {
            db.Insert(new User
            {
                UserId = Guid.NewGuid().ToString(),
                Username = "admin",
                PasswordHash =
                    passwordHasher.HashAndSaltPassword(adminPassword),
                Role = UserRoles.Admin
            });
        }

        var userPassword = configuration["SeedUser:Password"];

        if (!string.IsNullOrWhiteSpace(userPassword) &&
            !db.Users.Any(u => u.Username == "user"))
        {
            db.Insert(new User
            {
                UserId = Guid.NewGuid().ToString(),
                Username = "user",
                PasswordHash =
                    passwordHasher.HashAndSaltPassword(userPassword),
                Role = UserRoles.User
            });
        }

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
            var seller =db.Users.First(u=>u.Username == "user");
            db.Insert(new Product
            {
                ProductId = "1",
                ProductName = "Apple",
                ProductPrice = 10,
                Inventory = 100,
                CategoryId = "1",
                SellerId = seller.UserId
            });
        }

        
    }
}