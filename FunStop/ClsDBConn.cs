using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace FunStop
{
    public class ClsDBConn
    {
        public SqlConnection conexion = new SqlConnection(ConfigurationManager.ConnectionStrings["Conexion"].ConnectionString);

        public void Conectar()
        {

            if (conexion.State == ConnectionState.Closed)
                conexion.Open();

        }

        public void Desconectar()
        {

            if (conexion.State == ConnectionState.Open)
                conexion.Close();

        }

        public DataTable Listado(String NombreSP, List<ClsParams> lst)
        {
            DataTable dt = new DataTable();
            SqlDataAdapter da;

            Conectar();
            da = new SqlDataAdapter(NombreSP, conexion);
            da.SelectCommand.CommandType = CommandType.StoredProcedure;
            if (lst != null)
            {
                for (int i = 0; i < lst.Count; i++)
                {
                    da.SelectCommand.Parameters.AddWithValue(lst[i].Nombre, lst[i].Valor);
                }
            }
            da.Fill(dt);

            Desconectar();
            return dt;
        }

        public void EjecutarSP(String NombreSP, ref List<ClsParams> lst)
        {
            SqlCommand cmd;

            Conectar();
            cmd = new SqlCommand(NombreSP, conexion);
            cmd.CommandType = CommandType.StoredProcedure;
            if (lst != null)
            {
                for (int i = 0; i < lst.Count; i++)
                {
                    if (lst[i].Direccion == ParameterDirection.Input)
                        cmd.Parameters.AddWithValue(lst[i].Nombre, lst[i].Valor);
                    if (lst[i].Direccion == ParameterDirection.Output)
                        cmd.Parameters.Add(lst[i].Nombre, lst[i].TipoDato, lst[i].Tamaño).Direction = ParameterDirection.Output;
                }
                cmd.ExecuteNonQuery();
                for (int i = 0; i < lst.Count; i++)
                {
                    if (cmd.Parameters[i].Direction == ParameterDirection.Output)
                        lst[i].Valor = cmd.Parameters[i].Value;
                }
            }

            Desconectar();
        }
    }
}