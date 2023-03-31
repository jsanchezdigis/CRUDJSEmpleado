﻿using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;

namespace SL.Controllers
{
    public class CatEntidadFederativaController : Controller
    {
        [EnableCors("API")]
        [HttpGet]
        [Route("api/CatEntidadFederativa/GetAll")]
        public ActionResult GetAll()
        {
            ML.Result result = BL.CatEntidadFederativa.GetAll();
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
