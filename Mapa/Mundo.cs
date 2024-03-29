﻿using SnakeGame.Entidad;
using SnakeGame.Entidad.Jugador;
using SnakeGame.Mapa;
using SnakeGame.Posicion;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace SnakeGame.Mapa
{
    public class Mundo
    {

        public delegate void AparicionEntidadHandler(AbsEntidad sender, EventArgs args);
        public delegate void EliminacionEntidadHandler(AbsEntidad sender, EventArgs args);
        public delegate void SerpienteMuertaHandler(Serpiente sender);

        public event AparicionEntidadHandler EventoAparicionEntidad;
        public event EliminacionEntidadHandler EventoEliminacionEntidad;
        public event SerpienteMuertaHandler EventoSerpienteMuerta;

        private Generador _generador;
        public Generador Generador { 
            get
            {
                return _generador;
            }
            set
            {
                _generador = value;
                ActualizarColores();
            }
        }
        public int TamañoX { get; set; }
        public int TamañoY { get; set; }

        public Casilla[,] Casillas { get; set; }

        public List<AbsEntidad> Entidades = new List<AbsEntidad>();

        private int contadorEntidades;

        public Mundo(int tamañoX = 20, int tamañoY = 20) {
            this.TamañoX = tamañoX;
            this.TamañoY = tamañoY;
            this.Casillas = GenerarMapa(TamañoX, TamañoY);
            this.contadorEntidades = 0;
        }

        public AbsEntidad GenerarEntidad(TipoEntidad tipoEntidad, Coordenada poscion, Direccion direccion)
        {
            return GenerarEntidad(tipoEntidad, poscion, direccion, null);
        }

        public AbsEntidad GenerarEntidad(TipoEntidad tipoEntidad, Coordenada poscion, Direccion direccion, Action<AbsEntidad> preAparicion)
        {
            AbsEntidad entidad = ConstruirEntidadPorTipo(tipoEntidad);

            entidad.Id = ++contadorEntidades;
            entidad.Posicion = poscion;
            entidad.Mundo = this;
            entidad.Valida = true;
            entidad.Direccion = direccion;
            if (preAparicion != null)
            {
                preAparicion.Invoke(entidad); // Llamar al callback
            }

            this.Entidades.Add(entidad);
            if (EventoAparicionEntidad != null)
            {
                EventoAparicionEntidad(entidad, EventArgs.Empty);
            }

            return entidad;
        }

        public void EliminarEntidad(AbsEntidad entidad)
        {
            entidad.Valida = false;
            this.Entidades.Remove(entidad);
            if (EventoEliminacionEntidad != null)
            {
                EventoEliminacionEntidad(entidad, EventArgs.Empty);
            }
        }

        private AbsEntidad ConstruirEntidadPorTipo(TipoEntidad tipoEntidad)
        {
            switch (tipoEntidad)
            {
                case TipoEntidad.Serpiente:
                    return new Serpiente(this);
                case TipoEntidad.Cola:
                    return new Cola();
                case TipoEntidad.Comida:
                    return new Fruta();
                default:
                    throw new Exception($"No se encontró una entidad registrada para el tipo de entidad \"{tipoEntidad}\"");
            }
        }

        public bool EstaDentroDelMapa(int x, int y)
        {
            return XDentroDelMapa(x) && YDentroDelMapa(y);
        }

        public bool XDentroDelMapa(int x)
        {
            return x >= 0 && x < Casillas.GetLength(0);
        }

        public bool YDentroDelMapa(int y)
        {
            return y >= 0 && y < Casillas.GetLength(1);
        }

        public Casilla ConsultarPosicion(Coordenada coordenada)
        {
            return ConsultarPosicion(coordenada.X, coordenada.Y);
        }

        public Casilla ConsultarPosicion(int x, int y)
        {
            return this.Casillas[x, y];
        }

        public List<AbsEntidad> ConsultarEntidadesEn(Casilla casilla)
        {
            return ConsultarEntidadesEn(casilla.Posicion);
        }

        public List<AbsEntidad> ConsultarEntidadesEn(Coordenada coordenada)
        {
            return ConsultarEntidadesEn(coordenada.X, coordenada.Y);
        }

        public List<AbsEntidad> ConsultarEntidadesEn(int x, int y)
        {
            return Entidades.FindAll((entidad) =>
            {
                return entidad.Posicion.X == x && entidad.Posicion.Y == y;
            });
        }

        public void ActualizarColores()
        {
            bool bandera = false;

            for (int i = 0; i < Casillas.GetLength(0); i++)
            {
                for (int j = 0; j < Casillas.GetLength(1); j++)
                {
                    // Bordes
                    if (i == 0 || j == 0 || i == Casillas.GetLength(0) - 1 || j == Casillas.GetLength(1) - 1)
                    {
                        Casillas[i, j].Color = Generador.BordeUno;
                    }
                    else
                    {
                        // Casillas "normales"
                        Casillas[i, j].Color = (bandera ? Generador.CasillaUno : Generador.CasillaDos);
                    }
                    bandera = !bandera;
                }
                bandera = !bandera;
            }
        }

        private static Casilla[,] GenerarMapa(int x, int y)
        {
            Casilla[,] mapa = new Casilla[x, y];

            for (int i = 0; i < mapa.GetLength(0); i++)
            {
                for (int j = 0; j < mapa.GetLength(1); j++)
                {
                    // Bordes
                    if (i == 0 || j == 0 || i == mapa.GetLength(0) - 1 || j == mapa.GetLength(1) - 1)
                    {
                        mapa[i, j] = new Casilla(Color.Black, new Coordenada(i, j), false);
                    }
                    else
                    {
                        // Casillas "normales"
                        mapa[i, j] = new Casilla(Color.Black, new Posicion.Coordenada(i, j));
                    }
                }
            }

            return mapa;
        }
    }
}
