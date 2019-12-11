using NorthwindConsole.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace Final19
{
    class Program
    {
        private static NLog.Logger logger = NLog.LogManager.GetCurrentClassLogger();
        static void Main(string[] args)
        {
            logger.Info("Start of Program");
            int choice = 0;
            int cc = 0;
            int pc = 0;

            do
            {
                logger.Info("First Choice");

                Console.WriteLine("1) Products");
                Console.WriteLine("2) Categories");
                choice = Convert.ToInt32(Console.ReadLine());

                //Products
                if (choice == 1)
                {
                    logger.Info("Enter product");
                    do
                    {
                        Console.WriteLine("1) Add Product");
                        Console.WriteLine("2) Edit Product");
                        Console.WriteLine("3) Display All Products");
                        Console.WriteLine("4) Display Specific Product");
                        pc = Convert.ToInt32(Console.ReadLine());

                        //Add Product
                        if (pc == 1)
                        {
                            logger.Info("Add Product");
                            var db = new NorthwindContext();
                            Product product = InputProduct(db);


                        }
                        //Edit Specific Product
                        else if (pc == 2)
                        {
                            logger.Info("Edit Product");
                            Console.WriteLine("Choose Profuct to edit");
                            var db = new NorthwindContext();
                            var product = GetProduct(db);
                            if (product != null)
                            {
                                Product UpdatedProduct = InputProduct(db);
                                if (UpdatedProduct != null)
                                {
                                    UpdatedProduct.ProductID = product.ProductID;
                                    db.EditProduct(UpdatedProduct);

                                }
                            }


                        }

                        //Display all records
                        else if (pc == 3)
                        {
                            logger.Info("Display products");
                            int prod;
                            do
                            {
                                Console.WriteLine("1) All Products");
                                Console.WriteLine("2) All Discontinued Products");
                                Console.WriteLine("3) All Active Products");
                                prod = Convert.ToInt16(Console.ReadLine());

                                //All Products
                                if (prod == 1)
                                {
                                    logger.Info("Display all products");
                                    var db = new NorthwindContext();
                                    var query = db.Products.OrderBy(p => p.ProductID);

                                    foreach (var item in query)
                                    {
                                        Console.WriteLine($"{item.ProductID}, {item.ProductName}");
                                    }
                                }

                                //discontinued Products
                                else if (prod == 2)
                                {
                                    logger.Info("Display discounted products");
                                    var db = new NorthwindContext();
                                    var query = db.Products.OrderBy(p => p.Discontinued == true);

                                    Console.WriteLine("Discontinued Products");

                                    foreach (var item in query)
                                    {
                                        Console.WriteLine($"{item.ProductName}");
                                    }

                                }

                                // Active Products
                                else if (prod == 3)
                                {
                                    logger.Info("Display activate products");
                                    var db = new NorthwindContext();
                                    var query = db.Products.OrderBy(p => p.Discontinued == false);

                                    Console.WriteLine("Activate Products");

                                    foreach (var item in query)
                                    {
                                        Console.WriteLine($"{item.ProductName}");
                                    }

                                }
                            } while (prod != 4);
                        }
                    } while (pc == 4);
                }


                //Categories
                else if (choice == 2)
                {
                    do
                    {
                        logger.Info("Display Category");
                        Console.WriteLine("1) Display Categories");
                        Console.WriteLine("2) Add Category");
                        Console.WriteLine("3) Display Categorie and items in list");
                        Console.WriteLine("4) Display all Categories and items in their list");
                        Console.WriteLine("5) Edit Category");
                        cc = Convert.ToInt32(Console.ReadLine());

                        //Display all categories
                        if (cc == 1)
                        {
                            logger.Info("Display all Category");

                            var db = new NorthwindContext();
                            var query = db.Categories.OrderBy(p => p.CategoryName);

                            Console.WriteLine($"{query.Count()} records returned");
                            foreach (var item in query)
                            {
                                Console.WriteLine($"{item.CategoryName} - {item.Description}");
                            }
                        }

                        //Add Categories
                        else if (cc == 2)
                        {

                            logger.Info("add Category"); 
                            
                            var db = new NorthwindContext();
                            Category category = InputCategory(db);
                        }

                        //Display Categorie and iteims in list
                        else if (cc == 3)
                        {
                            logger.Info("Display Category and Items");
                            var db = new NorthwindContext();
                            var query = db.Categories.OrderBy(p => p.CategoryId);

                            Console.WriteLine("Select the category whose products you want to display:");
                            foreach (var item in query)
                            {
                                Console.WriteLine($"{item.CategoryId}) {item.CategoryName}");
                            }
                            int id = int.Parse(Console.ReadLine());
                            Console.Clear();
                            logger.Info($"CategoryId {id} selected");
                            Category category = db.Categories.FirstOrDefault(c => c.CategoryId == id);
                            Console.WriteLine($"{category.CategoryName} - {category.Description}");
                            foreach (Product p in category.Products)
                            {
                                Console.WriteLine(p.ProductName);
                            }
                        }

                        //Display all Categories
                        else if (cc == 4)
                        {
                            var db = new NorthwindContext();
                            var query = db.Categories.Include("Products").OrderBy(p => p.CategoryId);
                            foreach (var item in query)
                            {
                                Console.WriteLine($"{item.CategoryName}");
                                foreach (Product p in item.Products)
                                {
                                    Console.WriteLine($"\t{p.ProductName}");
                                }
                            }
                        }

                        //Edit Category
                        else if (cc == 5)
                        {
                            logger.Info("Edit Category");
                            Console.WriteLine("Choose Category to edit");
                            var db = new NorthwindContext();
                            var cat = GetCategory(db);
                            if (cat != null)
                            {
                                Category UpdatedCategory = InputCategory(db);
                                if (UpdatedCategory != null)
                                {
                                    UpdatedCategory.CategoryId = cat.CategoryId;
                                    db.EditCatagory(UpdatedCategory);

                                }
                            }
                        }

                    } while (cc != 5);


                }
            } while (choice != 6);
            }

       


        public static Product InputProduct(NorthwindContext db)
        {
            logger.Info("Input Product");
            Product product = new Product();
            Console.WriteLine("Enter Product Name");
            product.ProductName = Console.ReadLine();
            Console.WriteLine("Enter quantity per unit");
            product.QuantityPerUnit = Console.ReadLine();
            Console.WriteLine("Entr unit price");
            product.UnitPrice = Convert.ToDecimal(Console.ReadLine());
            Console.WriteLine("Enter Units in Stock");
            product.UnitsInStock = Convert.ToInt16(Console.ReadLine());
            Console.WriteLine("Units on order");
            product.UnitsOnOrder = Convert.ToInt16(Console.ReadLine());
            Console.WriteLine("Unit Reorder level");
            product.ReorderLevel = Convert.ToInt16(Console.ReadLine());

            Console.WriteLine("Discontinued True (1) or False()");
            int dis = Convert.ToInt16(Console.ReadLine());

            if (dis == 1)
            {
                product.Discontinued = false;
            }
            else if (dis == 2)
            {
                product.Discontinued = true;

            }

            Console.WriteLine("Choose Category");
            Console.WriteLine("1) Beverages \n2) Condeiments \n3)Confections \n4) Dairy Products" +
                "\n5) Grains/Cereals \n6) Meat/Poultry \n7) Produce \n8) Seafood ");
            int cat = Convert.ToInt16(Console.ReadLine());


            switch (cat)
            {
                case 1:
                    product.CategoryId = 1;
                    break;
                case 2:
                    product.CategoryId = 2;
                    break;
                case 3:
                    product.CategoryId = 3;
                    break;
                case 4:
                    product.CategoryId = 4;
                    break;
                case 5:
                    product.CategoryId = 5;
                    break;
                case 6:
                    product.CategoryId = 6;
                    break;
                case 7:
                    product.CategoryId = 7;
                    break;
                case 8:
                    product.CategoryId = 8;
                    break;
                default:
                    break;
            }

            ValidationContext context = new ValidationContext(product, null, null);
            List<ValidationResult> results = new List<ValidationResult>();
            var isValid = Validator.TryValidateObject(product, context, results, true);
            if (isValid)
            {
                db = new NorthwindContext();
                // check for unique name
                if (db.Categories.Any(c => c.CategoryName == product.ProductName))
                {
                    // generate validation error
                    isValid = false;
                    results.Add(new ValidationResult("Name exists", new string[] { "Product Name" }));
                }
                else
                {
                    logger.Info("Validation passed");
                    // TODO: save category to db
                }
            }
            if (!isValid)
            {
                foreach (var result in results)
                {
                    logger.Error($"{result.MemberNames.First()} : {result.ErrorMessage}");
                }
            }

            return null;
        }

        public static Category InputCategory(NorthwindContext db)
        {
            logger.Info("input Category");
            Category category = new Category();
            Console.WriteLine("Enter Category Name:");
            category.CategoryName = Console.ReadLine();
            Console.WriteLine("Enter the Category Description:");
            category.Description = Console.ReadLine();

            ValidationContext context = new ValidationContext(category, null, null);
            List<ValidationResult> results = new List<ValidationResult>();

            var isValid = Validator.TryValidateObject(category, context, results, true);
            if (isValid)
            {
                 db = new NorthwindContext();
                // check for unique name
                if (db.Categories.Any(c => c.CategoryName == category.CategoryName))
                {
                    // generate validation error
                    isValid = false;
                    results.Add(new ValidationResult("Name exists", new string[] { "CategoryName" }));
                }
                else
                {
                    logger.Info("Validation passed");
                    // TODO: save category to db
                }
            }
            if (!isValid)
            {
                foreach (var result in results)
                {
                    logger.Error($"{result.MemberNames.First()} : {result.ErrorMessage}");
                }
            }
            return null;
        }

        public static Product GetProduct(NorthwindContext db)
        {
            db = new NorthwindContext();
            var query = db.Products.OrderBy(p => p.ProductID);

            foreach (var item in query)
            {
                Console.WriteLine($"{item.ProductID},  {item.ProductName}");
            }
            return null;
        }

        public static Category GetCategory(NorthwindContext db)
        {
            db = new NorthwindContext();
            var query = db.Categories.OrderBy(c => c.CategoryId);

            foreach (var cat in query)
            {
                Console.WriteLine($"{cat.CategoryId}, {cat.CategoryName}");
            }
            return null;
        }
        
    }


   

}