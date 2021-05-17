using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DelegacionMunicipal.modelo.poco
{
    public class Usuario
    {
        private string username;
        private string password;
        private string nombreCompleto;
        private int idDelegacion;
        private string cargo;

        public string Username { get => username; set => username = value; }
        public string Password { get => password; set => password = value; }
        public string NombreCompleto { get => nombreCompleto; set => nombreCompleto = value; }
        public int IdDelegacion { get => idDelegacion; set => idDelegacion = value; }
        public string Cargo { get => cargo; set => cargo = value; }

        public Usuario()
        {

        }

        public override string ToString()
        {
            return nombreCompleto + " - " + idDelegacion;
        }
    }
}
