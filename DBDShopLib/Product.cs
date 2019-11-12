using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DBDShopLib
{
    public class Product
    {
        public int idProd = 0;
        public string descripcion = null;
        public int numArticulosStock =0;

        public Product(int idProduct, string descr, int numArticulos)
        {
            idProd = idProduct;
            descripcion = descr;
            numArticulosStock = numArticulos;
        }

        public int GetId()
        {
            return idProd;
        }

        public string GetDesc()
        {
            return descripcion;
        }

        public int GetStock()
        {
            return numArticulosStock;
        }

        public int StockAddOne()
        {
            numArticulosStock ++;
            return numArticulosStock;
        }
        public int StockRemoveOne()
        {
            numArticulosStock--;
            return numArticulosStock;
        }
    }
}

   

