using SuperMarket.API.Persistence.Contexts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SuperMarket.API.Domain.Models
{
    public static class DbInitialiser
    {
        public static void Seed(AppDbContext context)
        {
            if (!context.Categories.Any())
                addCategories(ref context);


            if (!context.Products.Any())
                addProducts(ref context);
        }

        private static void addCategories(ref AppDbContext context)
        {
            context.AddRange
            (
                new Category { Name = "Fruits and Vegetables" },
                new Category { Name = "Dairy" },
                new Category { Name = "Desserts" }
            );
            
            context.SaveChanges();
        }

        private static void addProducts(ref AppDbContext context)
        {
            context.AddRange
            (
                new Product
                {
                    
                    Name = "Apple",
                    QuantityInPackage = 1,
                    UnitOfMeasurement = EUnitOfMeasurement.Unity,
                    Price = 0.50M,
                    ShortDescription = "Mmmm, Apple",
                    LongDescription = "Mmmmmmmmm, Apple",
                    ImageUrl = "https://5.imimg.com/data5/LM/DU/MY-22954806/apple-fruit-500x500.jpg",
                    ImageThumbUrl = "https://5.imimg.com/data5/LM/DU/MY-22954806/apple-fruit-500x500.jpg",
                    CategoryId = 1
                },
                new Product
                {
                    
                    Name = "Milk",
                    QuantityInPackage = 2,
                    UnitOfMeasurement = EUnitOfMeasurement.Liter,
                    Price = 1.50M,
                    ShortDescription = "Mmmm, Milk",
                    LongDescription = "Mmmmmmmmm, Milk",
                    ImageUrl = "https://elmatadorrestaurant.com/wp-content/uploads/2017/12/MILK.jpg",
                    ImageThumbUrl = "https://elmatadorrestaurant.com/wp-content/uploads/2017/12/MILK.jpg",

                    CategoryId = 2
                },
                new Product
                {
                    
                    Name = "Apple Pie",
                    Price = 12.95M,
                    CategoryId = 3,
                    ShortDescription = "Our famous apple pies!",
                    LongDescription = "Icing carrot cake jelly-o cheesecake. Sweet roll marzipan marshmallow toffee brownie brownie candy tootsie roll. Chocolate cake gingerbread tootsie roll oat cake pie chocolate bar cookie dragée brownie. Lollipop cotton candy cake bear claw oat cake. Dragée candy canes dessert tart. Marzipan dragée gummies lollipop jujubes chocolate bar candy canes. Icing gingerbread chupa chups cotton candy cookie sweet icing bonbon gummies. Gummies lollipop brownie biscuit danish chocolate cake. Danish powder cookie macaroon chocolate donut tart. Carrot cake dragée croissant lemon drops liquorice lemon drops cookie lollipop toffee. Carrot cake carrot cake liquorice sugar plum topping bonbon pie muffin jujubes. Jelly pastry wafer tart caramels bear claw. Tiramisu tart pie cake danish lemon drops. Brownie cupcake dragée gummies.",
                    ImageUrl = "https://gillcleerenpluralsight.blob.core.windows.net/files/applepie.jpg",
                    IsProductOfTheWeek = true,
                    ImageThumbUrl = "https://gillcleerenpluralsight.blob.core.windows.net/files/applepiesmall.jpg"
                },
                new Product
                {
                    
                    Name = "Blueberry Cheese Cake",
                    Price = 18.95M,
                    CategoryId = 3,
                    ShortDescription = "You'll love it!",
                    LongDescription = "Icing carrot cake jelly-o cheesecake. Sweet roll marzipan marshmallow toffee brownie brownie candy tootsie roll. Chocolate cake gingerbread tootsie roll oat cake pie chocolate bar cookie dragée brownie. Lollipop cotton candy cake bear claw oat cake. Dragée candy canes dessert tart. Marzipan dragée gummies lollipop jujubes chocolate bar candy canes. Icing gingerbread chupa chups cotton candy cookie sweet icing bonbon gummies. Gummies lollipop brownie biscuit danish chocolate cake. Danish powder cookie macaroon chocolate donut tart. Carrot cake dragée croissant lemon drops liquorice lemon drops cookie lollipop toffee. Carrot cake carrot cake liquorice sugar plum topping bonbon pie muffin jujubes. Jelly pastry wafer tart caramels bear claw. Tiramisu tart pie cake danish lemon drops. Brownie cupcake dragée gummies.",
                    ImageUrl = "https://gillcleerenpluralsight.blob.core.windows.net/files/blueberrycheesecake.jpg",
                    IsProductOfTheWeek = false,
                    ImageThumbUrl = "https://gillcleerenpluralsight.blob.core.windows.net/files/blueberrycheesecakesmall.jpg"
                },
                new Product
                {
                    
                    Name = "Cheese Cake",
                    Price = 18.95M,
                    CategoryId = 3,
                    ShortDescription = "Plain cheese cake. Plain pleasure.",
                    LongDescription = "Icing carrot cake jelly-o cheesecake. Sweet roll marzipan marshmallow toffee brownie brownie candy tootsie roll. Chocolate cake gingerbread tootsie roll oat cake pie chocolate bar cookie dragée brownie. Lollipop cotton candy cake bear claw oat cake. Dragée candy canes dessert tart. Marzipan dragée gummies lollipop jujubes chocolate bar candy canes. Icing gingerbread chupa chups cotton candy cookie sweet icing bonbon gummies. Gummies lollipop brownie biscuit danish chocolate cake. Danish powder cookie macaroon chocolate donut tart. Carrot cake dragée croissant lemon drops liquorice lemon drops cookie lollipop toffee. Carrot cake carrot cake liquorice sugar plum topping bonbon pie muffin jujubes. Jelly pastry wafer tart caramels bear claw. Tiramisu tart pie cake danish lemon drops. Brownie cupcake dragée gummies.",
                    ImageUrl = "https://gillcleerenpluralsight.blob.core.windows.net/files/cheesecake.jpg",
                    IsProductOfTheWeek = false,
                    ImageThumbUrl = "https://gillcleerenpluralsight.blob.core.windows.net/files/cheesecakesmall.jpg"
                },
                new Product
                {
                    
                    Name = "Cherry Pie",
                    Price = 15.95M,
                    CategoryId = 3,
                    ShortDescription = "A summer classic!",
                    LongDescription = "Icing carrot cake jelly-o cheesecake. Sweet roll marzipan marshmallow toffee brownie brownie candy tootsie roll. Chocolate cake gingerbread tootsie roll oat cake pie chocolate bar cookie dragée brownie. Lollipop cotton candy cake bear claw oat cake. Dragée candy canes dessert tart. Marzipan dragée gummies lollipop jujubes chocolate bar candy canes. Icing gingerbread chupa chups cotton candy cookie sweet icing bonbon gummies. Gummies lollipop brownie biscuit danish chocolate cake. Danish powder cookie macaroon chocolate donut tart. Carrot cake dragée croissant lemon drops liquorice lemon drops cookie lollipop toffee. Carrot cake carrot cake liquorice sugar plum topping bonbon pie muffin jujubes. Jelly pastry wafer tart caramels bear claw. Tiramisu tart pie cake danish lemon drops. Brownie cupcake dragée gummies.",
                    ImageUrl = "https://gillcleerenpluralsight.blob.core.windows.net/files/cherrypie.jpg",
                    IsProductOfTheWeek = false,
                    ImageThumbUrl = "https://gillcleerenpluralsight.blob.core.windows.net/files/cherrypiesmall.jpg"
                }
            );
            context.SaveChanges();
        }

    }
}
