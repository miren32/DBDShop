﻿using System;
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
            string query = "CREATE TABLE IF NOT EXISTS Products (Id int,Name TEXT)";
            MySqlCommand cmd = new MySqlCommand(query, m_connection);
            cmd.ExecuteNonQuery();
            query = "INSERT INTO Products VALUES(1,'Nocilla');";
            cmd = new MySqlCommand(query, m_connection);
            cmd.ExecuteNonQuery();
            query = "INSERT INTO Products VALUES(2,'Patata');";
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
                product.idProd = idProd;
                product.descripcion = desc;
                product.numArticulosStock = numStock;
                products.Add(product);
            }
            reader.Close();
            return products;
        }

        public void DeleteProducts(List<Product> products)
        {
            foreach(Product product in products)
            {
                string query = "DELETE FROM Products WHERE Id =" + product.Id + ";";
                MySqlCommand cmd = new MySqlCommand(query, m_connection);
                cmd.ExecuteNonQuery();
            }
        }

        public List<Product> SoldOutProducts()
        {
            List<Product> products = new List<Product>();
            products = GetProducts();           
            string query = "SELECT idProd, descripcion FROM PRODUCTO Where numArticulosStock = 0";
            MySqlCommand cmd = new MySqlCommand(query, m_connection);
            MySqlDataReader reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                int id = int.Parse(reader.GetValue(0).ToString());
                string desc = reader.GetValue(1).ToString();
                Product product = new Product();
                product.idProd = id;
                product.descripcion = desc;
                products.Add(product);
            }
            reader.Close();
            return products;

        }
    }
}
