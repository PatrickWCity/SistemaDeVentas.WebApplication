using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MvcApplication1.Models
{
    public class Item
    {
        private Producto producto = new Producto();
        private int cantidad;

        public Item()
        {
        }
        public Item(Producto producto, int cantidad)
        {
            Producto = producto;
            Cantidad = cantidad;

        }

        public Producto Producto
        {
            get{return producto;}
            set{producto = value;}
        }

        public int Cantidad
        {
            get{return cantidad;}
            set{cantidad = value;}
        }
    }
}