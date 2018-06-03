using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProyectoFinal
{
    internal class ProductosVenta
    {
        int asignacion;
        string nombre;
        int cantidad;
        decimal precio;
        decimal total;

        public int Asignacion
        {
            get { return asignacion; }
            set { asignacion = value; }
        }
        public string Nombre
        {
            get { return nombre; }
            set { nombre = value; }
        }
        public int Cantidad
        {
            get { return cantidad; }
            set { cantidad = value; }
        }
        public decimal Precio
        {
            get { return precio; }
            set { precio = value; }
        }
        public decimal Total
        {
            get { return total; }
            set { total = value; }
        }
    }
}
