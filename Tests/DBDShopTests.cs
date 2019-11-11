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
            //SI HACEMOS LA PRUEBA POR SEPARADO FUNCIONA PERO SI EJECUTAMOS TODOS LOS TEST JUNTOS NO 

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
            //FUNCIONA SI LO HACES POR SEPARADO PERO EN CONJUNTO NO 

            // Connect to the test database
            Client client = new Client("pBRMsmc7h2", "pBRMsmc7h2", "mQvsG2x5NR");
            //Get all the existing products
            List<Product> productsSinStock = client.GetProducts();

            client.DeleteProducts(productsSinStock);
            //Check we deleted all the products
            productsSinStock = client.GetProducts();
            Assert.IsTrue(productsSinStock.Count == 0);

            // MAL hay que ponerle mi lista no esa
  
            //Insert test data
            client.InsertTestData();
            //Check they were correctly inserted
            productsSinStock = client.GetProducts();
            Assert.IsTrue(productsSinStock.Count == 2);
        }

        [TestMethod]
        public void TestAddProduct()
        {
            // NO FUNCIONA HAY QUE CREARLE PRODUCTOS Y LISTAS PARA QUE FUNCIONE
            // Connect to the test database
            Client client = new Client("pBRMsmc7h2", "pBRMsmc7h2", "mQvsG2x5NR");
            //Get all the existing products
            List<Product> productsDB = client.GetProducts();

            client.DeleteProducts(productsDB);
            //Check we deleted all the products
            productsDB = client.GetProducts();
            Assert.IsTrue(productsDB.Count == 0);

            // MAL hay que ponerle mi lista no esa

            //Insert test data
            //crear un producto
            //client.AddProduct(Product);
            //Check they were correctly inserted
            productsDB = client.GetProducts();
            Assert.IsTrue(productsDB.Count == 2);            
        }   
    }
}
