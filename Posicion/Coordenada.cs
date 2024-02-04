using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SnakeGame.Posicion
{
    public class Coordenada : ICloneable
    {
        public int X { get; set; }
        public int Y { get; set; }

        public Coordenada(int x, int y)
        {
            this.X = x;
            this.Y = y;
        }

        public Coordenada MoverHacia(Direccion direccion)
        {
            switch (direccion)
            {
                case Direccion.Arriba:
                    return new Coordenada(X, Y - 1);
                case Direccion.Abajo:
                    return new Coordenada(X, Y + 1);
                case Direccion.Izquierda:
                    return new Coordenada(X - 1, Y);
                case Direccion.Derecha:
                    return new Coordenada(X + 1, Y);
                default:
                    return null;
            }
        }

        public object Clone()
        {
            return new Coordenada(X, Y);
        }
    }
}
