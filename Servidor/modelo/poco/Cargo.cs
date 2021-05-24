﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Servidor.modelo.poco
{
    public class Cargo
    {
        private int idCargo;
        private string tipoCargo;

        public int IdCargo { get => idCargo; set => idCargo = value; }
        public string TipoCargo { get => tipoCargo; set => tipoCargo = value; }

        public Cargo()
        {
        }

        public override string ToString()
        {
            return tipoCargo;
        }
    }
}
