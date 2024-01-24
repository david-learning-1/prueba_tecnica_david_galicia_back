using System.Collections.Generic;
using System.Data;

using System;
using System.Data.SqlClient;

namespace EmpleadosAPI.Repository.Common
{
    public class ExcecSQL
    {

        private string ConnectionString { get; set; }
        public ExcecSQL(string _ConnectionString)
        {
            this.ConnectionString = _ConnectionString;
        }

        public ExcecSQL()
        {
            
        }

        public ResultSQL ExecSP(string strCommand, List<SqlParameter> LstParameters, bool outParams)
        {
            ResultSQL objResult = new ResultSQL();
            objResult.Error = string.Empty;

            try
            {

                //using SqlConnection con = new SqlConnection(this.ConnectionString);                
                using SqlConnection con = new SqlConnection("Server=localhost\\SQLEXPRESS;Database=personal;Trusted_Connection=True; Connect Timeout=300");
                using (SqlCommand command = new SqlCommand(strCommand, con)
                {
                    CommandType = CommandType.StoredProcedure
                })
                {
                    //Aumentando el timeout del comando a ejecutar (5 minutos)
                    command.CommandTimeout = 1200;

                    //Se asignan los paramertros a la lista
                    foreach (SqlParameter p in LstParameters)
                    {

                        command.Parameters.Add(p);
                    }

                    //Si se indico que tendra parametros de salida estos se crean y se agregan a la lista
                    if (outParams)
                    {

                        command.Parameters.Add(new SqlParameter("@AFFECTEDROWS", 0));
                        command.Parameters.Add(new SqlParameter("@ERROR", SqlDbType.VarChar, 5000));
                        command.Parameters.Add(new SqlParameter("@RESULT", SqlDbType.Int));
                    }

                    con.Open();

                    //Se indica que los nuevos parametros seran de salida 
                    if (outParams)
                    {
                        command.Parameters["@AFFECTEDROWS"].Direction = ParameterDirection.Output;
                        command.Parameters["@ERROR"].Direction = ParameterDirection.Output;
                        command.Parameters["@RESULT"].Direction = ParameterDirection.Output;
                    }

                    SqlDataAdapter da = new SqlDataAdapter(command);
                    objResult.TblResult = new DataTable();
                    da.Fill(objResult.TblResult);

                    // Se retornan los parametros de salida por defecto
                    if (outParams)
                    {
                        objResult.Error = command.Parameters["@ERROR"].Value.ToString();
                        objResult.ResdulId = Convert.ToInt32(command.Parameters["@RESULT"].Value);
                        objResult.Affectedrows = Convert.ToInt32(command.Parameters["@AFFECTEDROWS"].Value);
                    }

                    con.Close();
                    da.Dispose();
                    command.Dispose();

                }
            }
            catch (SqlException sqlex)
            {

                switch (sqlex.Number)
                {
                    case -2: //Error de conexion con la base de datos
                        objResult.ResdulId = -1;
                        objResult.Error = sqlex.Number + "Error al ejecutar el SP " + strCommand + ":" + sqlex.Message;
                        break;

                    default:
                        objResult.ResdulId = -1;
                        objResult.Error = "(" + sqlex.Number + ")" + "Error al ejecutar el SP " + strCommand + ": " + sqlex.Message;

                        break;
                }
            }

            catch (Exception ex)
            {
                objResult.ResdulId = -1;
                objResult.Error = ex.Message;

            }

            return objResult;
        }
    }
}
