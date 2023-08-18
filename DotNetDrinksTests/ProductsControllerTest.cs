using DotNetDrinks.Controllers;
using DotNetDrinks.Data;
using DotNetDrinks.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;

namespace DotNetGrillTests
{
    [TestClass]
    public class ProductsControllerTest
    {
        private ApplicationDbContext _context;
        private ProductsController _controller;
        private List<Product> _products = new List<Product>();

        [TestInitialize]
        public void TestInitialize()
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                                .UseInMemoryDatabase(Guid.NewGuid().ToString())
                                .Options;
            _context = new ApplicationDbContext(options);

            var category = new Category { Id = 1, Name = "Soda" };
            _context.Categories.Add(category);
            _context.SaveChanges();

            var product1 = new Product
            {
                Id = 1,
                Name = "Test Drink",
                Price = 4,
                Stock = 10,
                Image = "216348fc-37a9-48a2-b428-01027f6170f9-canadian.png",
                BrandId = 1,
                CategoryId = 1,
                Category = category
            };

            _context.Products.Add(product1);
            _context.SaveChanges();


            _products.Add(product1);

            _controller = new ProductsController(_context);
        }

        [TestMethod]
        public void IndexReturnsView()
        {
            var result = _controller.Index();
            Assert.IsNotNull(result);
        }       
        

        [TestMethod]
        public void DeleteConfirmedRemovesProductFromDatabase()
        {
            var productToDelete = _context.Products.First();
            var result = _controller.DeleteConfirmed(productToDelete.Id);
            var prod = _context.Products.Find(productToDelete.Id);
            Assert.IsNull(prod);
        }
    }
}
