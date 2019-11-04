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

            string query = "SELECT idProd,descripcion FROM PRODUCTO";
            MySqlCommand cmd = new MySqlCommand(query, m_connection);
            MySqlDataReader reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                
                int id= int.Parse(reader.GetValue(0).ToString());
                string name = reader.GetValue(1).ToString();
                Product product = new Product();
                product.Id = id;
                product.Name = name;
                products.Add(product);
            }
            reader.Close();
            return products;
        }

        public void DeleteProducts(List<Product> products)
        {
            foreach(Product product in products)
            {
                string query = "DELETE FROM PRODUCTO WHERE idProd =" + product.Id + ";";
                MySqlCommand cmd = new MySqlCommand(query, m_connection);
                cmd.ExecuteNonQuery();
            }
        }

        public void changePrice(Product newProduct, double newPrice)
        {
            List<Product> productsDB = new List<Product>();
            productsDB = GetProducts();

            foreach (Product product in productsDB)
            {
                if(newProduct.Id == product.Id)
                {
                    //CAMBIAR LOS NOMBRES DE LAS TABLAS Y ATRIBUTOS
                    "UPDATE PRODUCTO_DISTRIBUIDOR SET precioPD = " + newPrice + "WHERE idProd = " + newProduct.Id + ";";
                }
                else
                {
                    Console.WriteLine("No existe producto");
                }
            }
    }
}
