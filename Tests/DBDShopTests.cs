using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using DBDShopLib;
using System.Collections.Generic;

namespace Tests
{
    [TestClass]
    public class DBDShopTests
    {
        [TestMethod]
        public void Conexion()
        {
            //Connect to the test database
            Client client = new Client("pBRMsmc7h2", "pBRMsmc7h2", "mQvsG2x5NR");
        }

        [TestMethod]
        public void AddAndTestData()
        {
            //Connect to the test database
            Client client = new Client("pBRMsmc7h2", "pBRMsmc7h2", "mQvsG2x5NR");
            //Get all the existing products
            List<Product> products = client.GetProducts();
            //Delete all the products
            client.DeleteProducts(products);
            //Check we deleted all the products
            products = client.GetProducts();
            Assert.IsTrue(products.Count == 0);

            //Insert test data
            client.InsertTestData();
            //Check they were correctly inserted
            products = client.GetProducts();
            Assert.IsTrue(products.Count == 2);
        }

        [TestMethod]
        public void TestSoldOut()
        {
            // Connect to the test database
            Client client = new Client("pBRMsmc7h2", "pBRMsmc7h2", "mQvsG2x5NR");
            //Get all the existing products
            List<Product> productsSinStock = client.GetProducts();

            client.DeleteProducts(productsSinStock);
            //Check we deleted all the products
            productsSinStock = client.GetProducts();
            Assert.IsTrue(productsSinStock.Count == 0);

            //Insert test data
            client.InsertTestData();
            //Check they were correctly inserted
            productsSinStock = client.GetProducts();
            Assert.IsTrue(productsSinStock.Count == 2);
        }

        [TestMethod]
        public void TestAddProduct()
        {

            // Connect to the test database
            Client client = new Client("pBRMsmc7h2", "pBRMsmc7h2", "mQvsG2x5NR");
            //Get all the existing products
            List<Product> productsDB = client.GetProducts();
           
            client.DeleteProducts(productsDB);
            //Check we deleted all the products
            productsDB = client.GetProducts();
            Assert.IsTrue(productsDB.Count == 0);
            
            //crear un producto
            Product prod = new Product(3, "Pan", 3);
            client.AddProduct(prod);
            //Check they were correctly inserted
            productsDB = client.GetProducts();
            Assert.IsTrue(productsDB.Count == 1);
        }

        [TestMethod]
        public void TestChangePrice()
        {

        }

        [TestMethod]
        public void TestWriteDownProducts()
        {

        }

    }
}