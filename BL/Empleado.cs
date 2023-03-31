using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BL
{
    public class Empleado
    {
        public static ML.Result Add(ML.Empleado empleado)
        {
            ML.Result result = new ML.Result();

            try
            {
                using (DL.JsanchezEmpleadoContext context = new DL.JsanchezEmpleadoContext())
                {
                    int query = context.Database.ExecuteSqlRaw($"EmpleadoAdd " +
                        //$"'{empleado.NumeroNomina}'," +
                        $"'{empleado.Nombre}'," +
                        $"'{empleado.ApellidoPaterno}'," +
                        $"'{empleado.ApellidoMaterno}'," +
                        $"'{empleado.IdEstado.IdCatEntidadFederativa}'");
                    if (query >= 1)
                    {
                        result.Correct = true;
                    }
                    else
                    {
                        result.Correct = false;
                        result.ErrorMessage = "No se inserto el registro";
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

        public static ML.Result Update(ML.Empleado empleado)
        {
            ML.Result result = new ML.Result();

            try
            {
                using (DL.JsanchezEmpleadoContext context = new DL.JsanchezEmpleadoContext())
                {
                    int query = context.Database.ExecuteSqlRaw($"EmpleadoUpdate " +
                        $"'{empleado.IdEmpleado}'," +
                        //$"'{empleado.NumeroNomina}'," +
                        $"'{empleado.Nombre}'," +
                        $"'{empleado.ApellidoPaterno}'," +
                        $"'{empleado.ApellidoMaterno}'," +
                        $"'{empleado.IdEstado.IdCatEntidadFederativa}'");
                    if (query >= 1)
                    {
                        result.Correct = true;
                    }
                    else
                    {
                        result.Correct = false;
                        result.ErrorMessage = "No se Actualizo el registro";
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

        public static ML.Result Delete(ML.Empleado empleado)
        {
            ML.Result result = new ML.Result();

            try
            {
                using (DL.JsanchezEmpleadoContext context = new DL.JsanchezEmpleadoContext())
                {
                    int query = context.Database.ExecuteSqlRaw($"EmpleadoDelete '{empleado.IdEmpleado}'");
                    if (query >= 1)
                    {
                        result.Correct = true;
                    }
                    else
                    {
                        result.Correct = false;
                        result.ErrorMessage = "No se Elimino el registro";
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

        public static ML.Result GetAll()
        {
            ML.Result result = new ML.Result();

            try
            {
                using (DL.JsanchezEmpleadoContext context = new DL.JsanchezEmpleadoContext())
                {
                    var query = context.Empleados.FromSqlRaw($"EmpleadoGetAll").ToList();

                    if (query != null)
                    {
                        result.Objects = new List<object>();
                        foreach (var obj in query)
                        {
                            ML.Empleado empleado = new ML.Empleado();

                            empleado.IdEmpleado = obj.IdEmpleado;
                            empleado.NumeroNomina = obj.NumeroNomina;
                            empleado.Nombre = obj.Nombre;
                            empleado.ApellidoPaterno = obj.ApellidoPaterno;
                            empleado.ApellidoMaterno = obj.ApellidoMaterno;

                            empleado.IdEstado = new ML.CatEntidadFederativa();
                            empleado.IdEstado.IdCatEntidadFederativa = obj.IdEstado.Value;
                            empleado.IdEstado.Estado = obj.Estado;

                            result.Objects.Add(empleado);
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

        public static ML.Result GetById(int IdEmpleado)
        {
            ML.Result result = new ML.Result();

            try
            {
                using (DL.JsanchezEmpleadoContext context = new DL.JsanchezEmpleadoContext())
                {
                    var query = context.Empleados.FromSqlRaw($"EmpleadoGetById '{IdEmpleado}'").AsEnumerable().FirstOrDefault();

                    if (query != null)
                    {
                        result.Object = new List<object>();
                        var obj = query;
                        {
                            ML.Empleado empleado = new ML.Empleado();

                            empleado.IdEmpleado = obj.IdEmpleado;
                            empleado.NumeroNomina = obj.NumeroNomina;
                            empleado.Nombre = obj.Nombre;
                            empleado.ApellidoPaterno = obj.ApellidoPaterno;
                            empleado.ApellidoMaterno = obj.ApellidoMaterno;

                            empleado.IdEstado = new ML.CatEntidadFederativa();
                            empleado.IdEstado.IdCatEntidadFederativa = obj.IdEstado.Value;
                            empleado.IdEstado.Estado = obj.Estado;

                            result.Object = empleado;
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
