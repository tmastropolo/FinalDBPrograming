using System.Data.Entity;

namespace NorthwindConsole.Models
{
    public class NorthwindContext : DbContext
    {
        private static NLog.Logger logger = NLog.LogManager.GetCurrentClassLogger();
        public NorthwindContext() : base("name=NorthwindContext") { }

        public DbSet<Product> Products { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Supplier> Suppliers { get; set; }

        public void EditProduct(Product UpdatedProduct)
        {
            logger.Info("Edit Product");
            Product product = this.Products.Find(UpdatedProduct.ProductID);
            product.ProductName = UpdatedProduct.ProductName;
            product.QuantityPerUnit = UpdatedProduct.QuantityPerUnit;
            product.UnitPrice = UpdatedProduct.UnitPrice;
            product.UnitsInStock = UpdatedProduct.UnitsInStock;
            product.UnitsOnOrder = UpdatedProduct.UnitsOnOrder;
            product.ReorderLevel = UpdatedProduct.ReorderLevel;
            product.Discontinued = UpdatedProduct.Discontinued;
            product.SupplierId = UpdatedProduct.SupplierId;
            this.SaveChanges();
        }

        public void EditCatagory(Category UpdatedCatagory)
        {
            logger.Info("Edit Category");
            Category category = this.Categories.Find(UpdatedCatagory.CategoryId);
            category.CategoryName = UpdatedCatagory.CategoryName;
            category.Description = UpdatedCatagory.Description;
            this.SaveChanges();
        }
    }
}
