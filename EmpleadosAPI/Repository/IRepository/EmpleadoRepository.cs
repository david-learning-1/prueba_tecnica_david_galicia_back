using EmpleadosAPI.Models;
using EmpleadosAPI.Repository.Common;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Net.NetworkInformation;

namespace EmpleadosAPI.Repository.IRepository
{
    public class EmpleadoRepository : IEmpleadoRepository
    {
        //private string ConnectionString { get; set; }
        public EmpleadoRepository()
        {
            
        }

        public bool CreateEmpleado(Empleado empleado)
        {
            ResultSQL objResult = new ResultSQL();

            List<SqlParameter> lstParameter = new List<SqlParameter>();

            lstParameter.Add(new SqlParameter("@Fotografia",empleado.Fotografia ));
            lstParameter.Add(new SqlParameter("@Nombre", empleado.Nombre ));
            lstParameter.Add(new SqlParameter("@Apellido", empleado.Apellido ));
            lstParameter.Add(new SqlParameter("@PuestoId", empleado.PuestoId ));
            lstParameter.Add(new SqlParameter("@FechaNacimiento", empleado.FechaNacimiento ));
            lstParameter.Add(new SqlParameter("@FechaContratacion", empleado.FechaContratacion ));
            lstParameter.Add(new SqlParameter("@Direccion", empleado.Direccion));
            lstParameter.Add(new SqlParameter("@Telefono", empleado.Telefono ));
            lstParameter.Add(new SqlParameter("@CorreoElectronico", empleado.CorreoElectronico));
            lstParameter.Add(new SqlParameter("@EstadoId", empleado.EstadoId ));

            objResult = new ExcecSQL().ExecSP("CreateEmployee", lstParameter, true);

            return objResult.ResdulId==1;
        }

        public IEnumerable<Empleado> GetAllEmpleados()
        {
            List<Empleado> lstEmpleados = new List<Empleado>();
            List<SqlParameter> sp = new List<SqlParameter>();

            ResultSQL objResult = new ResultSQL();

            objResult = new ExcecSQL().ExecSP("GetAllEmployees", sp, false);

            foreach (DataRow row in objResult.TblResult.Rows)
            {
                Empleado empleado = new Empleado {
                    Id = Convert.ToInt32(row["Id"]),
                    Fotografia = row["Fotografia"].ToString(),
                    Nombre = row["Nombre"].ToString(),
                    Apellido = row["Apellido"].ToString(),
                    PuestoId = Convert.ToInt32(row["PuestoId"]),
                    FechaNacimiento = DateTime.Parse(row["FechaNacimiento"].ToString()),
                    FechaContratacion = DateTime.Parse(row["FechaContratacion"].ToString()),
                    Direccion = row["Direccion"].ToString(),
                    Telefono = row["Telefono"].ToString(),
                    CorreoElectronico = row["CorreoElectronico"].ToString(),
                    EstadoId = row["EstadoId"].ToString(),

                };

                lstEmpleados.Add(empleado);
            }

            return lstEmpleados;
        }

        public bool UpdateEmpleado(Empleado empleado)
        {
            ResultSQL objResult = new ResultSQL();

            List<SqlParameter> lstParameter = new List<SqlParameter>();

            lstParameter.Add(new SqlParameter("@Id", empleado.Id));
            lstParameter.Add(new SqlParameter("@Fotografia", empleado.Fotografia));
            lstParameter.Add(new SqlParameter("@Nombre", empleado.Nombre));
            lstParameter.Add(new SqlParameter("@Apellido", empleado.Apellido));
            lstParameter.Add(new SqlParameter("@PuestoId", empleado.PuestoId));
            lstParameter.Add(new SqlParameter("@FechaNacimiento", empleado.FechaNacimiento));
            lstParameter.Add(new SqlParameter("@FechaContratacion", empleado.FechaContratacion));
            lstParameter.Add(new SqlParameter("@Direccion", empleado.Direccion));
            lstParameter.Add(new SqlParameter("@Telefono", empleado.Telefono));
            lstParameter.Add(new SqlParameter("@CorreoElectronico", empleado.CorreoElectronico));
            lstParameter.Add(new SqlParameter("@EstadoId", empleado.EstadoId));

            objResult = new ExcecSQL().ExecSP("UpdateEmployee", lstParameter, true);

            return objResult.ResdulId == 1;
        
        }

        public bool DeleteEmpleado(int id)
        {
            ResultSQL objResult = new ResultSQL();

            List<SqlParameter> lstParameter = new List<SqlParameter>();

            lstParameter.Add(new SqlParameter("@Id", id));
            
            objResult = new ExcecSQL().ExecSP("DeleteEmployee", lstParameter, true);

            return objResult.ResdulId == 1;
        }

        public Empleado GetEmpladoByID(int id)
        {
            List<Empleado> lstEmpleados = new List<Empleado>();                        

            ResultSQL objResult = new ResultSQL();
            List<SqlParameter> lstParameter = new List<SqlParameter>();

            lstParameter.Add(new SqlParameter("@Id", id));
            objResult = new ExcecSQL().ExecSP("GetEmployeeById", lstParameter, false);

            foreach (DataRow row in objResult.TblResult.Rows)
            {
                Empleado empleado = new Empleado
                {
                    Id = Convert.ToInt32(row["Id"]),
                    Fotografia = row["Fotografia"].ToString(),
                    Nombre = row["Nombre"].ToString(),
                    Apellido = row["Apellido"].ToString(),
                    PuestoId = Convert.ToInt32(row["PuestoId"]),
                    FechaNacimiento = DateTime.Parse(row["FechaNacimiento"].ToString()),
                    FechaContratacion = DateTime.Parse(row["FechaContratacion"].ToString()),
                    Direccion = row["Direccion"].ToString(),
                    Telefono = row["Telefono"].ToString(),
                    CorreoElectronico = row["CorreoElectronico"].ToString(),
                    EstadoId = row["EstadoId"].ToString(),

                };

                lstEmpleados.Add(empleado);
            }

            return lstEmpleados.FirstOrDefault();
        }
    }
}
