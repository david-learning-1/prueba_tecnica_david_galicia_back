using EmpleadosAPI.Models;
using System.Collections.Generic;

namespace EmpleadosAPI.Repository.IRepository
{
    public interface IEmpleadoRepository
    {
        IEnumerable<Empleado> GetAllEmpleados();
        Empleado GetEmpladoByID(int id);
        bool CreateEmpleado(Empleado empleado);

        bool UpdateEmpleado(Empleado empleado);

        bool DeleteEmpleado(int id);
    }
}
