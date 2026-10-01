using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;

namespace CRM_Nexus_Retail.DataAccess
{
    public class ReportesDAL
    {
        private readonly string _cadenaConexion;

        public ReportesDAL()
        {
            _cadenaConexion = ConfigurationManager
                .ConnectionStrings["CadenaPortalNexus"].ConnectionString;
        }

        public DataTable EjecutarReporte(string nombreProcedimiento, SqlParameter[] parametros)
        {
            var tablaResultado = new DataTable();

            using (var conexion = new SqlConnection(_cadenaConexion))
            using (var comando = new SqlCommand(nombreProcedimiento, conexion))
            {
                comando.CommandType = CommandType.StoredProcedure;

                if (parametros != null)
                {
                    foreach (var param in parametros)
                    {
                        if (param.Value == null)
                        {
                            param.Value = DBNull.Value;
                        }
                        comando.Parameters.Add(param);
                    }
                }

                conexion.Open();
                using (var adaptador = new SqlDataAdapter(comando))
                {
                    adaptador.Fill(tablaResultado);
                }
            }

            return tablaResultado;
        }
    }
}