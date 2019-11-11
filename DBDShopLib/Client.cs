using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySql.Data;
using MySql.Data.MySqlClient;

namespace DBDShopLib
{
    public class Client
    {
        MySqlConnection m_connection = null;

        public Client(string databasename, string username, string password, string server= "remotemysql.com")
        {
            m_connection = new MySqlConnection();
            m_connection.ConnectionString =
            "Server=" + server + ";" +
            "database=" + databasename + ";" +
            "UID=" + username + ";" +
            "password=" + password + ";";
            m_connection.Open();
        }

        public void InsertTestData()
        {
            string query = "CREATE TABLE IF NOT EXISTS PRODUCTO (idProd int,descripcion TEXT, numArticulosStock int check (numArticulosStock>0), primary key (idProd))";
            MySqlCommand cmd = new MySqlCommand(query, m_connection);
            cmd.ExecuteNonQuery();
            query = "INSERT INTO PRODUCTO (idProd, descripcion) VALUES(1,'Nocilla');";
            cmd = new MySqlCommand(query, m_connection);
            cmd.ExecuteNonQuery();
            query = "INSERT INTO PRODUCTO (idProd, descripcion) VALUES (2,'Patata');";
            cmd = new MySqlCommand(query, m_connection);
            cmd.ExecuteNonQuery();
        }

        public List<Product> GetProducts()
        {
           List<Product> products = new List<Product>();

            string query = "SELECT idProd,descripcion, numArticulosStock FROM PRODUCTO";
            MySqlCommand cmd = new MySqlCommand(query, m_connection);
            MySqlDataReader reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                
                int id= int.Parse(reader.GetValue(0).ToString());
                string desc = reader.GetValue(1).ToString();
                int numStock = int.Parse(reader.GetValue(2).ToString());
                Product product = new Product();
                product.idProd = id;
                product.descripcion = desc;
                product.numArticulosStock = numStock;
                products.Add(product);
            }
            reader.Close();
            return products;
        }

        public void DeleteProducts(List<Product> products)
        {
            foreach (Product product in products)
            {
                string query = "DELETE FROM PRODUCTO WHERE idProd =" + product.idProd + ";";
                MySqlCommand cmd = new MySqlCommand(query, m_connection);
                cmd.ExecuteNonQuery();
            }
        }

        public List<Product> SoldOutProducts()
        {
            
            List<Product> productsSinStock = new List<Product>();
            productsSinStock = GetProducts();           
            string query = "SELECT idProd FROM PRODUCTO Where numArticulosStock = 0";
            MySqlCommand cmd = new MySqlCommand(query, m_connection);
            MySqlDataReader reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                int id = int.Parse(reader.GetValue(0).ToString());
                string desc = reader.GetValue(1).ToString();
                Product product = new Product();
                product.idProd = id;
                product.descripcion = desc;
                productsSinStock.Add(product);
            }
            reader.Close();
            return productsSinStock;

        }

        public void AddProduct(Product producto)
        {
            List<Product> productsDB = new List<Product>();
            productsDB = GetProducts();
            
            foreach (Product product in productsDB)
            {
                if (producto.idProd == product.idProd)
                {
                    string query1 = "UPDATE PRODUCTO SET numArticulosStock = numArticulosStock +1 WHERE idProd = " + producto.idProd + ";";
                    MySqlCommand cmd = new MySqlCommand(query1, m_connection);
                    cmd.ExecuteNonQuery();
                }
                 else
                {
                    productsDB.Add(producto);
                }
            }            
        }

        public void changePrice(Product newProduct, double newPrice)
        {
            List<Product> productsDB = new List<Product>();
            productsDB = GetProducts();

            foreach (Product product in productsDB)
            {
                if(newProduct.idProd == product.idProd)
                {
                    "UPDATE PRODUCTO_DISTRIBUIDOR SET precioPD = " + newPrice + "WHERE idProd = " + newProduct.idProd + ";";
                }
                else
                {
                    Console.WriteLine("No existe producto");
                }
            }
        }

        public List<Product> WriteDownProducts(Product productBuy)
        {
            List<Product> productsDB = new List<Product>();
            List<Product> productosPedidos = new List<Product>();
            productsDB = GetProducts();

            foreach (Product product in productsDB)
            {
                if(productBuy.idProd == product.idProd)
                {
                    //HACER COSIS EN LA DB
                    product.numArticulosStock = product.numArticulosStock - 1;
                    productosPedidos.Add(productBuy);
                }
            }
            return productosPedidos;
         }
    }
}
