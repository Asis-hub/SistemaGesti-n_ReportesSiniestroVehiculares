﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DelegacionMunicipal.modelo.poco
{
    /// <summary>
    /// POCO para operaciones de Fotografia
    /// </summary>


    class Fotografia
    {
        private int idFotografia;
        private int idReporte;

        public int IdFotografia { get => idFotografia; set => idFotografia = value; }
        public int IdReporte { get => idReporte; set => idReporte = value; }

        public Fotografia()
        {
        }

        public override string ToString()
        {
            return idFotografia.ToString();
        }
    }
}
