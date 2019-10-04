using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

using SuperMarket.API.Domain.Models;

namespace SuperMarket.API.Persistence.Contexts
{
    public class AppDbContext :DbContext
    {
        public DbSet<Category> Categories { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<Feedback> Feedbacks { get; set; }

        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        //protected override void OnModelCreating(ModelBuilder builder)
        //{


        //    base.OnModelCreating(builder);

        //    builder.Entity<Feedback>().ToTable("Feedback");
        //    builder.Entity<Feedback>().HasKey(p => p.FeedbackId);
        //    builder.Entity<Feedback>().Property(p => p.FeedbackId).IsRequired().ValueGeneratedOnAdd();
        //    builder.Entity<Feedback>().Property(p => p.Name).IsRequired().HasMaxLength(50);
        //    builder.Entity<Feedback>().Property(p => p.Email).IsRequired();
        //    builder.Entity<Feedback>().Property(p => p.Message).IsRequired().HasMaxLength(250);
        //    builder.Entity<Feedback>().Property(p => p.ContactMe).IsRequired();

        //    builder.Entity<Category>().ToTable("Catergories");
        //    builder.Entity<Category>().HasKey(p => p.Id);
        //    builder.Entity<Category>().Property(p => p.Id).IsRequired().ValueGeneratedOnAdd();
        //    builder.Entity<Category>().Property(p => p.Name).IsRequired().HasMaxLength(30);
        //    builder.Entity<Category>().HasMany(p => p.Products).WithOne(p => p.Category).HasForeignKey(p => p.CategoryId);

        //    builder.Entity<Category>().HasData
        //    (
        //        new Category { Id = 1, Name = "Fruits and Vegetables" },
        //        new Category { Id = 2, Name = "Dairy" },
        //        new Category { Id = 3, Name = "Desserts" }
        //    );

        //    builder.Entity<Product>().ToTable("Products");
        //    builder.Entity<Product>().HasKey(p => p.Id);
        //    builder.Entity<Product>().Property(p => p.Id).IsRequired().ValueGeneratedOnAdd();
        //    builder.Entity<Product>().Property(p => p.Name).IsRequired().HasMaxLength(50);
        //    builder.Entity<Product>().Property(p => p.QuantityInPackage).IsRequired();
        //    builder.Entity<Product>().Property(p => p.UnitOfMeasurement).IsRequired();

        //    builder.Entity<Product>().HasData
        //    (
        //    #region ProductsDetails
        //        new Product
        //        {
        //            Id = 1,
        //            Name = "Apple",
        //            QuantityInPackage = 1,
        //            UnitOfMeasurement = EUnitOfMeasurement.Unity,
        //            Price = 0.50M,
        //            ShortDescription = "Mmmm, Apple",
        //            LongDescription = "Mmmmmmmmm, Apple",
        //            ImageUrl = "https://5.imimg.com/data5/LM/DU/MY-22954806/apple-fruit-500x500.jpg",
        //            ImageThumbUrl = "https://5.imimg.com/data5/LM/DU/MY-22954806/apple-fruit-500x500.jpg",
        //            CategoryId = 1
        //        },
        //        new Product
        //        {
        //            Id = 2,
        //            Name = "Milk",
        //            QuantityInPackage = 2,
        //            UnitOfMeasurement = EUnitOfMeasurement.Liter,
        //            Price = 1.50M,
        //            ShortDescription = "Mmmm, Milk",
        //            LongDescription = "Mmmmmmmmm, Milk",
        //            ImageUrl = "https://elmatadorrestaurant.com/wp-content/uploads/2017/12/MILK.jpg",
        //            ImageThumbUrl = "https://elmatadorrestaurant.com/wp-content/uploads/2017/12/MILK.jpg",

        //            CategoryId = 2
        //        },
        //        new Product
        //        {
        //            Id = 3,
        //            Name = "Apple Pie",
        //            Price = 12.95M,
        //            CategoryId = 3,
        //            ShortDescription = "Our famous apple pies!",
        //            LongDescription = "Icing carrot cake jelly-o cheesecake. Sweet roll marzipan marshmallow toffee brownie brownie candy tootsie roll. Chocolate cake gingerbread tootsie roll oat cake pie chocolate bar cookie dragée brownie. Lollipop cotton candy cake bear claw oat cake. Dragée candy canes dessert tart. Marzipan dragée gummies lollipop jujubes chocolate bar candy canes. Icing gingerbread chupa chups cotton candy cookie sweet icing bonbon gummies. Gummies lollipop brownie biscuit danish chocolate cake. Danish powder cookie macaroon chocolate donut tart. Carrot cake dragée croissant lemon drops liquorice lemon drops cookie lollipop toffee. Carrot cake carrot cake liquorice sugar plum topping bonbon pie muffin jujubes. Jelly pastry wafer tart caramels bear claw. Tiramisu tart pie cake danish lemon drops. Brownie cupcake dragée gummies.",
        //            ImageUrl = "https://gillcleerenpluralsight.blob.core.windows.net/files/applepie.jpg",
        //            IsProductOfTheWeek = true,
        //            ImageThumbUrl = "https://gillcleerenpluralsight.blob.core.windows.net/files/applepiesmall.jpg"
        //        },
        //        new Product
        //        {
        //            Id = 4,
        //            Name = "Blueberry Cheese Cake",
        //            Price = 18.95M,
        //            CategoryId = 3,
        //            ShortDescription = "You'll love it!",
        //            LongDescription = "Icing carrot cake jelly-o cheesecake. Sweet roll marzipan marshmallow toffee brownie brownie candy tootsie roll. Chocolate cake gingerbread tootsie roll oat cake pie chocolate bar cookie dragée brownie. Lollipop cotton candy cake bear claw oat cake. Dragée candy canes dessert tart. Marzipan dragée gummies lollipop jujubes chocolate bar candy canes. Icing gingerbread chupa chups cotton candy cookie sweet icing bonbon gummies. Gummies lollipop brownie biscuit danish chocolate cake. Danish powder cookie macaroon chocolate donut tart. Carrot cake dragée croissant lemon drops liquorice lemon drops cookie lollipop toffee. Carrot cake carrot cake liquorice sugar plum topping bonbon pie muffin jujubes. Jelly pastry wafer tart caramels bear claw. Tiramisu tart pie cake danish lemon drops. Brownie cupcake dragée gummies.",
        //            ImageUrl = "https://gillcleerenpluralsight.blob.core.windows.net/files/blueberrycheesecake.jpg",
        //            IsProductOfTheWeek = false,
        //            ImageThumbUrl = "https://gillcleerenpluralsight.blob.core.windows.net/files/blueberrycheesecakesmall.jpg"
        //        },
        //        new Product
        //        {
        //            Id = 5,
        //            Name = "Cheese Cake",
        //            Price = 18.95M,
        //            CategoryId = 3,
        //            ShortDescription = "Plain cheese cake. Plain pleasure.",
        //            LongDescription = "Icing carrot cake jelly-o cheesecake. Sweet roll marzipan marshmallow toffee brownie brownie candy tootsie roll. Chocolate cake gingerbread tootsie roll oat cake pie chocolate bar cookie dragée brownie. Lollipop cotton candy cake bear claw oat cake. Dragée candy canes dessert tart. Marzipan dragée gummies lollipop jujubes chocolate bar candy canes. Icing gingerbread chupa chups cotton candy cookie sweet icing bonbon gummies. Gummies lollipop brownie biscuit danish chocolate cake. Danish powder cookie macaroon chocolate donut tart. Carrot cake dragée croissant lemon drops liquorice lemon drops cookie lollipop toffee. Carrot cake carrot cake liquorice sugar plum topping bonbon pie muffin jujubes. Jelly pastry wafer tart caramels bear claw. Tiramisu tart pie cake danish lemon drops. Brownie cupcake dragée gummies.",
        //            ImageUrl = "https://gillcleerenpluralsight.blob.core.windows.net/files/cheesecake.jpg",
        //            IsProductOfTheWeek = false,
        //            ImageThumbUrl = "https://gillcleerenpluralsight.blob.core.windows.net/files/cheesecakesmall.jpg"
        //        },
        //        new Product
        //        {
        //            Id = 6,
        //            Name = "Cherry Pie",
        //            Price = 15.95M,
        //            CategoryId = 3,
        //            ShortDescription = "A summer classic!",
        //            LongDescription = "Icing carrot cake jelly-o cheesecake. Sweet roll marzipan marshmallow toffee brownie brownie candy tootsie roll. Chocolate cake gingerbread tootsie roll oat cake pie chocolate bar cookie dragée brownie. Lollipop cotton candy cake bear claw oat cake. Dragée candy canes dessert tart. Marzipan dragée gummies lollipop jujubes chocolate bar candy canes. Icing gingerbread chupa chups cotton candy cookie sweet icing bonbon gummies. Gummies lollipop brownie biscuit danish chocolate cake. Danish powder cookie macaroon chocolate donut tart. Carrot cake dragée croissant lemon drops liquorice lemon drops cookie lollipop toffee. Carrot cake carrot cake liquorice sugar plum topping bonbon pie muffin jujubes. Jelly pastry wafer tart caramels bear claw. Tiramisu tart pie cake danish lemon drops. Brownie cupcake dragée gummies.",
        //            ImageUrl = "https://gillcleerenpluralsight.blob.core.windows.net/files/cherrypie.jpg",
        //            IsProductOfTheWeek = false,
        //            ImageThumbUrl = "https://gillcleerenpluralsight.blob.core.windows.net/files/cherrypiesmall.jpg"
        //        }
        //        #endregion
        //    );

        //}
    }
}
