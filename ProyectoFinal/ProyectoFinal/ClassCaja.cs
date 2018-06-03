using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProyectoFinal
{
    public class ClassCaja
    {
        DateTime inicio;
        int sucursal;
        //Constructor vacio
        public ClassCaja() {
        }

        //iniciando caja.
        public void startCaja(DateTime horaInicio, int idSucursal)
        {
            inicio = horaInicio;
            sucursal = idSucursal;
        }

        


        
    }
    
}
