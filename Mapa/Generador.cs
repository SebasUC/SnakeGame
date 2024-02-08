using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SnakeGame.Mapa
{
    public abstract class Generador
    {
        public abstract Color BordeUno { get; }
        public abstract Color BordeDos { get; }
        public abstract Color CasillaUno { get; }
        public abstract Color CasillaDos { get; }
    }
}
