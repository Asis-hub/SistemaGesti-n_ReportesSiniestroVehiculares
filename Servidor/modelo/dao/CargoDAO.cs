using Servidor.modelo.dao.db;
using Servidor.modelo.poco;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Servidor.modelo.dao
{
    public class CargoDAO
    {
        public static List<Cargo> ConsultarCargos(string consulta)
        {
            List<Cargo> lista = new List<Cargo>();
            SqlConnection conn = null;

            try
            {
                conn = ConexionBD.GetConnection();
                if (conn != null)
                {
                    SqlCommand comando;
                    SqlDataReader dataReader;
                    comando = new SqlCommand(consulta, conn);
                    dataReader = comando.ExecuteReader();

                    while (dataReader.Read())
                    {
                        Cargo cargo = new Cargo();
                        cargo.IdCargo = (!dataReader.IsDBNull(0)) ? dataReader.GetInt32(0) : 0;
                        cargo.TipoCargo = (!dataReader.IsDBNull(1)) ? dataReader.GetString(1) : "";
                        lista.Add(cargo);
                    }
                    dataReader.Close();
                    comando.Dispose();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            finally
            {
                if (conn != null)
                {
                    conn.Close();
                }
            }
            return lista;
        }
    }
}
