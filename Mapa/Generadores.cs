using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SnakeGame.Mapa
{
    public static class Generadores
    {
        public static Generador GenClasico { get; }
        public static Generador GenNevado { get; }
        public static Generador GenDesierto { get; }

        static Generadores()
        {
            GenClasico = new Clasico();
            GenNevado = new Nevado();
            GenDesierto = new Desierto();
        }


        private class Clasico : Generador
        {

            public override Color BordeUno => Color.FromArgb(87, 138, 52);
            public override Color BordeDos => Color.FromArgb(74, 177, 44);
            public override Color CasillaUno => Color.FromArgb(170, 215, 81);
            public override Color CasillaDos => Color.FromArgb(162, 209, 73);
        }

        private class Nevado : Generador
        {

            public override Color BordeUno => Color.FromArgb(135, 159, 161);
            public override Color BordeDos => Color.FromArgb(117, 137, 138);
            public override Color CasillaUno => Color.FromArgb(222, 236, 237);
            public override Color CasillaDos => Color.FromArgb(209, 228, 230);
        }

        private class Desierto : Generador
        {

            public override Color BordeUno => Color.FromArgb(151, 123, 38);
            public override Color BordeDos => Color.FromArgb(114, 94, 29);
            public override Color CasillaUno => Color.FromArgb(242, 215, 140);
            public override Color CasillaDos => Color.FromArgb(236, 205, 121);
        }
    }
}
