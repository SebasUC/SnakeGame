using SnakeGame.Posicion;
using SnakeGame.Properties;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SnakeGame.Personalizacion
{
    public static class Skins
    {
        public static Skin SkinAzul { get; }
        public static Skin SkinVerde { get; }
        public static Skin SkinAmarillo { get; }

        static Skins()
        {
            SkinAzul = new Azul();
            SkinVerde = new Verde();
            SkinAmarillo = new Amarillo();
        }
        
        public abstract class Skin
        {
            public abstract Dictionary<Direccion, Image> TexturasCabeza { get; }
            public abstract Dictionary<Direccion, Image> TexturasCabezaMuerta { get; }
            public abstract Image Cuerpo { get; }
            public abstract Image CuerpoMuerto { get; }
        }

        private class Azul : Skin
        {
            public override Dictionary<Direccion, Image> TexturasCabeza => new Dictionary<Direccion, Image>()
            {
                { Direccion.Arriba, Resources.azul_cabeza_arriba },
                { Direccion.Abajo, Resources.azul_cabeza_abajo },
                { Direccion.Izquierda, Resources.azul_cabeza_izquierda },
                { Direccion.Derecha, Resources.azul_cabeza_derecha }
            };
            public override Dictionary<Direccion, Image> TexturasCabezaMuerta => new Dictionary<Direccion, Image>()
            {
                { Direccion.Arriba, Resources.azul_cabeza_muerto_arriba },
                { Direccion.Abajo, Resources.azul_cabeza_muerto },
                { Direccion.Izquierda, Resources.azul_cabeza_muerto_izquierda },
                { Direccion.Derecha, Resources.azul_cabeza_muerto_derecha }
            };

            public override Image Cuerpo => Resources.azul_cuerpo;

            public override Image CuerpoMuerto => Resources.azul_cuerpo_muerto;
        }

        private class Verde : Skin
        {
            public override Dictionary<Direccion, Image> TexturasCabeza => new Dictionary<Direccion, Image>()
            {
                { Direccion.Arriba, Resources.verde_cabeza_arriba },
                { Direccion.Abajo, Resources.verde_cabeza_abajo },
                { Direccion.Izquierda, Resources.verde_cabeza_izquierda },
                { Direccion.Derecha, Resources.verde_cabeza_derecha }
            };
            public override Dictionary<Direccion, Image> TexturasCabezaMuerta => new Dictionary<Direccion, Image>()
            {
                { Direccion.Arriba, Resources.verde_cabeza_muerto_arriba },
                { Direccion.Abajo, Resources.verde_cabeza_muerto },
                { Direccion.Izquierda, Resources.verde_cabeza_muerto_izquierda },
                { Direccion.Derecha, Resources.verde_cabeza_muerto_derecha }
            };

            public override Image Cuerpo => Resources.verde_cuerpo;

            public override Image CuerpoMuerto => Resources.verde_cuerpo_muerto;
        }

        private class Amarillo : Skin
        {
            public override Dictionary<Direccion, Image> TexturasCabeza => new Dictionary<Direccion, Image>()
            {
                { Direccion.Arriba, Resources.amarillo_cabeza_arriba },
                { Direccion.Abajo, Resources.amarillo_amarillo_cabeza_abajo },
                { Direccion.Izquierda, Resources.amarillo_cabeza_izquierda },
                { Direccion.Derecha, Resources.amarillo_cabeza_derecha }
            };
            public override Dictionary<Direccion, Image> TexturasCabezaMuerta => new Dictionary<Direccion, Image>()
            {
                { Direccion.Arriba, Resources.amarillo_cabeza_muerto_arriba },
                { Direccion.Abajo, Resources.amarillo_cabeza_muerto },
                { Direccion.Izquierda, Resources.amarillo_cabeza_muerto_izquierda },
                { Direccion.Derecha, Resources.amarillo_cabeza_muerto_derecha }
            };

            public override Image Cuerpo => Resources.amarillo_cuerpo;

            public override Image CuerpoMuerto => Resources.amarillo_cuerpo_muerto;
        }
    }
}