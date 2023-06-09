﻿using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;

namespace SL.Controllers
{
    public class EmpleadoController : Controller
    {
        [EnableCors("API")]
        [HttpGet]
        [Route("api/Empleado/GetAll")]
        public ActionResult GetAll()
        {
            ML.Result result = BL.Empleado.GetAll();
            if (result.Correct)
            {
                return Ok(result);
            }
            else
            {
                return NotFound(result);
            }
        }
        [EnableCors("API")]
        [HttpGet]
        [Route("api/Empleado/GetById/{IdEmpleado}")]
        public ActionResult GetById(int IdEmpleado)
        {
            ML.Result result = BL.Empleado.GetById(IdEmpleado);
            if (result.Correct)
            {
                return Ok(result);
            }
            else
            {
                return NotFound(result);
            }
        }
        [EnableCors("API")]
        [HttpPost]
        [Route("api/Empleado/Add")]
        public ActionResult Add([FromBody] ML.Empleado empleado)
        {
            ML.Result result = BL.Empleado.Add(empleado);
            if (result.Correct)
            {
                return Ok(result);
            }
            else
            {
                return NotFound(result);
            }
        }
        [EnableCors("API")]
        [HttpPost]
        [Route("api/Empleado/Update")]
        public ActionResult Update([FromBody] ML.Empleado empleado)
        {
            ML.Result result = BL.Empleado.Update(empleado);
            if (result.Correct)
            {
                return Ok(result);
            }
            else
            {
                return NotFound(result);
            }
        }
        [EnableCors("API")]
        [HttpDelete]
        [Route("api/Empleado/Delete/{IdEmpleado}")]
        public ActionResult Delete([FromBody] ML.Empleado empleado)
        {
            ML.Result result = BL.Empleado.Delete(empleado);
            if (result.Correct)
            {
                return Ok(result);
            }
            else
            {
                return NotFound(result);
            }
        }
    }
}
