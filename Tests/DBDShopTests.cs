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
            Client client= new Client("pBRMsmc7h2", "pBRMsmc7h2", "mQvsG2x5NR");
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
            products= client.GetProducts();
            Assert.IsTrue(products.Count == 2);
        }
        [TestMethod]
        public void TestSoldOut()
        {
            // Connect to the test database
            Client client = new Client("pBRMsmc7h2", "pBRMsmc7h2", "mQvsG2x5NR");
            //Get all the existing products
            List<Product> products = client.GetProducts();

            client.DeleteProducts(products);
            //Check we deleted all the products
            products = client.GetProducts();
            Assert.IsTrue(products.Count == 0);

            //Insert test data
            client.SoldOutProducts();
            //Check they were correctly inserted
            products = client.GetProducts();
            Assert.IsTrue(products.Count == 2);




        }
        
    }
}
