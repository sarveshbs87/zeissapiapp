﻿using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Xunit;
using ZeissAPIApp.Controllers;
using ZeissAPIApp.Data;
using ZeissAPIApp.Models;

namespace ProductApi.Tests.Controllers
{
    public class ProductsControllerTests
    {
        private readonly ProductsController _controller;
        private readonly ProductsDBContext _context;

        public ProductsControllerTests()
        {
            var options = new DbContextOptionsBuilder<ProductsDBContext>()
                .UseInMemoryDatabase(databaseName: "TestDb")
                .Options;

            _context = new ProductsDBContext(options);
            _controller = new ProductsController(_context);
        }

        [Fact]
        public async Task AddProductDetails_GeneratesUniqueId()
        {
            // Arrange
            var newProduct = new Products { Name = "Tablet", Price = 30000, StockAvailable = 7 };

            // Act
            var result = await _controller.AddProductDetails(newProduct);

            // Assert
            var createdAtActionResult = Assert.IsType<CreatedAtActionResult>(result);
            var addedProduct = Assert.IsType<Products>(createdAtActionResult.Value);

            Assert.False(string.IsNullOrEmpty(addedProduct.Id)); // Ensure ID is generated
            Assert.Equal("Tablet", addedProduct.Name);
        }

        [Fact]
        public async Task GetProductDetailsById_ProductExists_ReturnsOkResult()
        {
            // Arrange: Add a product and get its generated ID
            var product = new Products { Name = "Laptop", Price = 50000, StockAvailable = 10 };
            var addResult = await _controller.AddProductDetails(product);
            var addedProduct = (addResult as CreatedAtActionResult).Value as Products;
            var generatedId = addedProduct.Id;  // Capture the autogenerated ID

            // Act
            var result = await _controller.GetProductDetailsById(generatedId);

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result);
            var retrievedProduct = Assert.IsType<Products>(okResult.Value);
            Assert.Equal(generatedId, retrievedProduct.Id);
            Assert.Equal("Laptop", retrievedProduct.Name);
        }

        [Fact]
        public async Task DeleteProductDetails_ProductExists_ReturnsOk()
        {
            // Arrange: Add a product first
            var product = new Products { Name = "Monitor", Price = 15000, StockAvailable = 5 };
            var addResult = await _controller.AddProductDetails(product);
            var addedProduct = (addResult as CreatedAtActionResult).Value as Products;
            var generatedId = addedProduct.Id;

            // Act
            var result = await _controller.DeleteProductDetails(generatedId);

            // Assert
            Assert.IsType<OkResult>(result);
            Assert.Null(await _context.products.FindAsync(generatedId));
        }
    }
}
