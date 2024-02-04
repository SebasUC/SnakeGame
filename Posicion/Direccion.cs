using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SnakeGame.Posicion
{
    public enum Direccion
    {
        Arriba,
        Abajo,
        Izquierda,
        Derecha
    }

    public static class Direcciones
    {
        public static Direccion OpuestaDe(Direccion direccion)
        {
            switch (direccion)
            {
                case Direccion.Abajo:
                    return Direccion.Arriba;
                case Direccion.Arriba:
                    return Direccion.Abajo;
                case Direccion.Derecha:
                    return Direccion.Izquierda;
                default: // Izquierda
                    return Direccion.Derecha;
            }
        }
    }
}
