using EmpleadosAPI.Models;
using EmpleadosAPI.Repository.IRepository;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace EmpleadosAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]

    public class EmpleadosController : ControllerBase
    {
        private readonly IEmpleadoRepository empleadoRepository;

        public EmpleadosController(IEmpleadoRepository _empleadoRepository)
        {
            this.empleadoRepository= _empleadoRepository;
        }

        [HttpGet]
        public IActionResult GetAllEmployees()
        {
            var empleados = empleadoRepository.GetAllEmpleados();
            return Ok(empleados);
        }

        [HttpGet("[action]/{id}")]
        public ActionResult GetEmployeeById(int id) {
            var empleado = empleadoRepository.GetEmpladoByID(id);
            return Ok(empleado);    
        }

        [HttpPost]
        public IActionResult CreateEmployee([FromBody] Empleado empleado) {
            if (!empleadoRepository.CreateEmpleado(empleado)) {
                ModelState.AddModelError(string.Empty, $"Ha ocurrido un error al intentar guardar el empleado: {empleado.Nombre}");
                return StatusCode(500, ModelState);
            }
            return Ok(new
            {
                message="Empleado guardado correctamente"
            });
        }


        [HttpPut]
        public IActionResult UpdateEmployee([FromBody] Empleado empleado)
        {
            if (!empleadoRepository.UpdateEmpleado(empleado))
            {
                ModelState.AddModelError(string.Empty, $"Ha ocurrido un error al intentar actualizar el empleado: {empleado.Nombre}");
                return StatusCode(500, ModelState);
            }
            return Ok(new
            {
                message = "Empleado actualizado correctamente"
            });
        }


        [HttpDelete]
        public IActionResult DeleteEmployee(int id)
        {
            if (!empleadoRepository.DeleteEmpleado(id))
            {
                ModelState.AddModelError(string.Empty, $"Ha ocurrido un error al intentar borrar el empleado seleccionado");
                return StatusCode(500, ModelState);
            }
            return Ok(new
            {
                message = "Empleado borrado correctamente"
            });
        }
    }
}
