using Infra;
using LinqToDB;

public class Seeder(MyDatabaseConnection db)
{
    public void Seed()
    {
        db.CreateTable<Product>(tableOptions: TableOptions.CreateIfNotExists);
        db.CreateTable<Category>(tableOptions: TableOptions.CreateIfNotExists);
        db.CreateTable<User>(tableOptions: TableOptions.CreateIfNotExists);

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
}