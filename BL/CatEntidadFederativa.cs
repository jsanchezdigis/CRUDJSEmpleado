﻿using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BL
{
    public class CatEntidadFederativa
    {
        public static ML.Result GetAll()
        {
            ML.Result result = new ML.Result();

            try
            {
                using (DL.JsanchezEmpleadoContext context = new DL.JsanchezEmpleadoContext())
                {
                    var query = context.CatEntidadFederativas.FromSqlRaw($"CatEntidadFederativaGetAll").ToList();

                    if (query != null)
                    {
                        result.Objects = new List<object>();
                        foreach (var obj in query)
                        {
                            ML.CatEntidadFederativa catEntidadFederativa = new ML.CatEntidadFederativa();

                            catEntidadFederativa.IdCatEntidadFederativa = obj.IdCatEntidadFederativa;
                            catEntidadFederativa.Estado = obj.Estado;

                            result.Objects.Add(catEntidadFederativa);
                        }
                    }
                    result.Correct = true;
                }
            }
            catch (Exception ex)
            {
                result.Correct = false;
                result.ErrorMessage = ex.Message;
            }
            return result;
        }
    }
}
