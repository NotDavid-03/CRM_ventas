using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using CRM_Nexus_Retail.Models;

namespace CRM_Nexus_Retail.DataAccess
{
    public class Nexus_ClienteDAL
    {
        private readonly string _cadenaConexion;

        public Nexus_ClienteDAL()
        {
            _cadenaConexion = ConfigurationManager
                .ConnectionStrings["CadenaPortalNexus"].ConnectionString;
        }

        // ── Listar todos los clientes ──
        public List<Omni_Cliente> ListarTodos()
        {
            var listaClientes = new List<Omni_Cliente>();

            using (var conexion = new SqlConnection(_cadenaConexion))
            using (var comando = new SqlCommand("SP_Listar_Clientes", conexion))
            {
                comando.CommandType = CommandType.StoredProcedure;
                conexion.Open();

                using (var lector = comando.ExecuteReader())
                {
                    while (lector.Read())
                    {
                        listaClientes.Add(MapearCliente(lector));
                    }
                }
            }

            return listaClientes;
        }

        // ── Consultar un cliente por ID ──
        public Omni_Cliente ConsultarPorId(int idCliente)
        {
            Omni_Cliente cliente = null;

            using (var conexion = new SqlConnection(_cadenaConexion))
            using (var comando = new SqlCommand("SP_Consultar_Cliente", conexion))
            {
                comando.CommandType = CommandType.StoredProcedure;
                comando.Parameters.AddWithValue("@Id_Nexus_Cliente", idCliente);
                conexion.Open();

                using (var lector = comando.ExecuteReader())
                {
                    if (lector.Read())
                    {
                        cliente = MapearCliente(lector);
                    }
                }
            }

            return cliente;
        }

        // ── Registrar un nuevo cliente ──
        public int Registrar(Omni_Cliente cliente)
        {
            int idGenerado = 0;

            using (var conexion = new SqlConnection(_cadenaConexion))
            using (var comando = new SqlCommand("SP_Registrar_Cliente", conexion))
            {
                comando.CommandType = CommandType.StoredProcedure;

                comando.Parameters.AddWithValue("@Codigo_Ref", cliente.Codigo_Ref);
                comando.Parameters.AddWithValue("@Nombre_Pila", cliente.Nombre_Pila);
                comando.Parameters.AddWithValue("@Apellido_Paterno", cliente.Apellido_Paterno);
                comando.Parameters.AddWithValue("@Apellido_Materno", (object)cliente.Apellido_Materno ?? DBNull.Value);
                comando.Parameters.AddWithValue("@Correo_Contacto", cliente.Correo_Contacto);
                comando.Parameters.AddWithValue("@Linea_Directa", (object)cliente.Linea_Directa ?? DBNull.Value);
                comando.Parameters.AddWithValue("@Domicilio_Fiscal", (object)cliente.Domicilio_Fiscal ?? DBNull.Value);
                comando.Parameters.AddWithValue("@Saldo_Puntos", cliente.Saldo_Puntos);
                comando.Parameters.AddWithValue("@Observaciones", (object)cliente.Observaciones ?? DBNull.Value);

                conexion.Open();
                var resultado = comando.ExecuteScalar();
                idGenerado = Convert.ToInt32(resultado);
            }

            return idGenerado;
        }

        // ── Modificar un cliente existente ──
        public void Modificar(Omni_Cliente cliente)
        {
            using (var conexion = new SqlConnection(_cadenaConexion))
            using (var comando = new SqlCommand("SP_Modificar_Cliente", conexion))
            {
                comando.CommandType = CommandType.StoredProcedure;

                comando.Parameters.AddWithValue("@Id_Nexus_Cliente", cliente.Id_Nexus_Cliente);
                comando.Parameters.AddWithValue("@Codigo_Ref", cliente.Codigo_Ref);
                comando.Parameters.AddWithValue("@Nombre_Pila", cliente.Nombre_Pila);
                comando.Parameters.AddWithValue("@Apellido_Paterno", cliente.Apellido_Paterno);
                comando.Parameters.AddWithValue("@Apellido_Materno", (object)cliente.Apellido_Materno ?? DBNull.Value);
                comando.Parameters.AddWithValue("@Correo_Contacto", cliente.Correo_Contacto);
                comando.Parameters.AddWithValue("@Linea_Directa", (object)cliente.Linea_Directa ?? DBNull.Value);
                comando.Parameters.AddWithValue("@Domicilio_Fiscal", (object)cliente.Domicilio_Fiscal ?? DBNull.Value);
                comando.Parameters.AddWithValue("@Saldo_Puntos", cliente.Saldo_Puntos);
                comando.Parameters.AddWithValue("@Estado_Cuenta", cliente.Estado_Cuenta);
                comando.Parameters.AddWithValue("@Observaciones", (object)cliente.Observaciones ?? DBNull.Value);

                conexion.Open();
                comando.ExecuteNonQuery();
            }
        }

        // ── Dar de baja un cliente ──
        public void DarDeBaja(int idCliente)
        {
            using (var conexion = new SqlConnection(_cadenaConexion))
            using (var comando = new SqlCommand("SP_Dar_Baja_Cliente", conexion))
            {
                comando.CommandType = CommandType.StoredProcedure;
                comando.Parameters.AddWithValue("@Id_Nexus_Cliente", idCliente);

                conexion.Open();
                comando.ExecuteNonQuery();
            }
        }

        // ── Método privado: mapear fila del lector a objeto ──
        private Omni_Cliente MapearCliente(SqlDataReader lector)
        {
            return new Omni_Cliente
            {
                Id_Nexus_Cliente = Convert.ToInt32(lector["Id_Nexus_Cliente"]),
                Codigo_Ref = lector["Codigo_Ref"].ToString(),
                Nombre_Pila = lector["Nombre_Pila"].ToString(),
                Apellido_Paterno = lector["Apellido_Paterno"].ToString(),
                Apellido_Materno = lector["Apellido_Materno"] == DBNull.Value ? null : lector["Apellido_Materno"].ToString(),
                Correo_Contacto = lector["Correo_Contacto"].ToString(),
                Linea_Directa = lector["Linea_Directa"] == DBNull.Value ? null : lector["Linea_Directa"].ToString(),
                Domicilio_Fiscal = lector["Domicilio_Fiscal"] == DBNull.Value ? null : lector["Domicilio_Fiscal"].ToString(),
                Fecha_Alta = Convert.ToDateTime(lector["Fecha_Alta"]),
                Saldo_Puntos = Convert.ToInt32(lector["Saldo_Puntos"]),
                Estado_Cuenta = Convert.ToBoolean(lector["Estado_Cuenta"]),
                Observaciones = lector["Observaciones"] == DBNull.Value ? null : lector["Observaciones"].ToString()
            };
        }
    }
}