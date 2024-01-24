using System;

namespace EmpleadosAPI.Models
{
    public class Empleado
    {
        public int Id { get; set; }
        public string Fotografia { get; set; }
        public string Nombre { get; set; }
        public string Apellido { get; set; }
        public int PuestoId { get; set; }
        public DateTime FechaNacimiento{ get; set; }
        public DateTime FechaContratacion { get; set; }
        public string Direccion { get; set; }
        public string Telefono { get; set; }
        public string CorreoElectronico { get; set; }
        public string EstadoId { get; set; }

    }
}
