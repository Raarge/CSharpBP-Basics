﻿using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Acme.Biz.Tests
{
    [TestClass()]
    public class ProductTests
    {
        [TestMethod()]
        public void SayHelloTest()
        {
            //Arrange **** Uses the Setting Properties Technique of Object Initialization *****
            var currentProduct = new Product();
            currentProduct.ProductName = "Saw";
            currentProduct.ProductId = 1;
            currentProduct.Description = "15-inch steel blade hand saw";
            currentProduct.ProductVendor.CompanyName = "ABC Corp";  // added for always needed related objects 
            var expected = "Hello Saw (1): 15-inch steel blade hand saw" + " Available on: ";

            //Act
            var actual = currentProduct.SayHello();

            //Assert
            Assert.AreEqual(expected, actual);
        }

        [TestMethod()]
        public void SayHello_ParameterizedConstructor()
        {
            //Arrange  **** Uses the Parameterized Constructor for Object Initialization ****
            var currentProduct = new Product(1, "Saw", "15-inch steel blade hand saw");
            
            var expected = "Hello Saw (1): 15-inch steel blade hand saw" + " Available on: ";

            //Act
            var actual = currentProduct.SayHello();

            //Assert
            Assert.AreEqual(expected, actual);
        }

        [TestMethod()]
        public void SayHello_ObjectInitializer()
        {
            //Arrange **** Uses straight up Object Initialization 
            var currentProduct = new Product  //instead of () we are using { } with the properties given at runtime
            {
                ProductId = 1,
                ProductName = "Saw",
                Description = "15-inch steel blade hand saw"
            };

            var expected = "Hello Saw (1): 15-inch steel blade hand saw" + " Available on: ";

            //Act
            var actual = currentProduct.SayHello();

            //Assert
            Assert.AreEqual(expected, actual);
        }

        [TestMethod()]
        public void Product_Null()
        {
            //Arrange
            Product currentProduct = null;  //explicitly sets to null
            var companyName = currentProduct?.ProductVendor?.CompanyName;

            string expected = null;

            //Act
            var actual = companyName;

            //Assert
            Assert.AreEqual(expected, actual);
        }

        [TestMethod()]
        public void ConvertMetersToInchesTest()
        {
            // Arrange
            var expected = 78.74;

            // Act
            var actual = 2 * Product.InchesPerMeter;

            // Assert
            Assert.AreEqual(expected, actual);
        }

        [TestMethod()]
        public void MinimumPriceTest_Default()
        {
            // Arrange
            var currentProduct = new Product();
            var expected = .96m;

            // Act
            var actual = currentProduct.MinimumPrice;

            // Assert
            Assert.AreEqual(expected, actual);

        }

        [TestMethod()]
        public void MinimumPriceTest_Bulk()
        {
            // Arrange
            var currentProduct = new Product(1, "Bulk Tools", "");
            var expected = 9.99m;

            // Act
            var actual = currentProduct.MinimumPrice;

            // Assert
            Assert.AreEqual(expected, actual);

        }

        [TestMethod()]
        public void ProductName_Format()
        {
            //Arrange
            var currentProduct = new Product();
            currentProduct.ProductName = "   Steel Hammer   ";  //set the name with spaces to see if our fix works

            var expected = "Steel Hammer";

            // Act
            var actual = currentProduct.ProductName;

            // Assert
            Assert.AreEqual(expected, actual);
        }

        [TestMethod()]
        public void ProductName_TooShort()
        {
            // Arrange
            var currentProduct = new Product();
            currentProduct.ProductName = "aw";

            string expected = null;
            string expectedMessage = "Product Name must be at least 3 characters";

            // Act
            var actual = currentProduct.ProductName;
            var actualMessage = currentProduct.ValidationMessage;

            // Assert
            Assert.AreEqual(expected, actual);
            Assert.AreEqual(expectedMessage, actualMessage);
        }

        [TestMethod()]
        public void ProductName_TooLong()
        {
            // Arrange
            var currentProduct = new Product();
            currentProduct.ProductName = "Steel Bladed Hand Saw";

            string expected = null;
            string expectedMessage = "Product Name cannot be more than 20 characters";
            
            // Act
            var actual = currentProduct.ProductName;
            var actualMessage = currentProduct.ValidationMessage;

            // Assert
            Assert.AreEqual(expected, actual);
            Assert.AreEqual(expectedMessage, actualMessage);
        }

        [TestMethod()]
        public void ProductName_JustRight()
        {
            // Arrange
            var currentProduct = new Product();
            currentProduct.ProductName = "Saw";

            string expected = "Saw";
            string expectedMessage = null;

            // Act
            var actual = currentProduct.ProductName;
            var actualMessage = currentProduct.ValidationMessage;

            // Assert
            Assert.AreEqual(expected, actual);
            Assert.AreEqual(expectedMessage, actualMessage);
        }
    }
}