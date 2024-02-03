using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SnakeGame.Entidad
{
    internal class Fruta : AbsEntidad, IRenderizable<Image>
    {
        public override string Nombre => "Fruta";

        public Image ObtenerImagen()
        {
            throw new NotImplementedException();
        }
    }
}
