using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SnakeGame.Entidad
{
    public abstract class EntidadViva : AbsEntidad
    {
        public bool Vivo { get; set; }

        public EntidadViva() { 
            Vivo = true;
        }

        public virtual void Morir()
        {
            this.Vivo = false;
            this.NecesitaActualizar = true;
        }
    }
}
