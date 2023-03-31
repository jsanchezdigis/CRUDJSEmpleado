/// <reference path="Empleado.js" />

$(document).ready(function () { //click
    GetAll();
    EstadoGetAll();
});

function GetAll() {
    $.ajax({
        type: 'GET',
        url: 'http://localhost:5009/api/Empleado/GetAll',
        success: function (result) { //200 OK
            $('#tblEmpleado tbody').empty();
            $.each(result.objects, function (i, empleado) {
                var filas =
                    '<tr>'
                    + '<td class="text-center"> <button class="btn btn-secondary" onclick="GetById(' + empleado.idEmpleado + ')"><span class="bi bi-pencil-square" style="color:#FFFFFF"></span></button></td>'
                    + "<td  id='id' style='display: none;'> " + empleado.idEmpleado + " < /td>"
                    + "<td class='text-center'>" + empleado.numeroNomina + "</td>"
                    + "<td class='text-center'>" + empleado.nombre + "</ td>"
                    + "<td class='text-center'>" + empleado.apellidoPaterno + "</ td>"
                    + "<td class='text-center'>" + empleado.apellidoMaterno + "</ td>"
                    + "<td class='text-center'>" + empleado.idEstado.estado + "</td>"
                    + '<td class="text-center"> <button class="btn btn-danger" onclick="Eliminar(' + empleado.idEmpleado + ')"><span class="bi bi-trash2-fill" style="color:#FFFFFF"></span></button></td>'

                    + "</tr>";
                $("#tblEmpleado tbody").append(filas);
            });
        },
        error: function (result) {
            alert('Error en la consulta.' + result.responseJSON.ErrorMessage);
        }
    });
};

/*GetAll Estados*/
function EstadoGetAll() {
    $.ajax({
        type: 'GET',
        url: 'http://localhost:5009/api/CatEntidadFederativa/GetAll',
        success: function (result) {
            $("#ddlEstado").append('<option value="' + 0 + '">' + 'Seleccione una opción' + '</option>');
            $.each(result.objects, function (i, catEntidadFederativa) {
                $("#ddlEstado").append('<option value="'
                    + catEntidadFederativa.idCatEntidadFederativa + '">'
                    + catEntidadFederativa.estado + '</option>');
            });
        }
    });
}

/*Agregar*/
function Add(empleado) {

    //var empleado = {
    //    idempleado: 0,
    //    numeroNomina: $('#txtNumeroNomina').val(),
    //    nombre: $('#txtNombre').val(),
    //    apellidoPaterno: $('#txtApellidoPaterno').val(),
    //    apellidoMaterno: $('#txtApellidoMaterno').val(),
    //    catEntidadFederativa: {
    //        idCatEntidadFederativa: $('#ddlEstado').val()
    //    }
    //}
    $.ajax({
        type: 'POST',
        url: 'http://localhost:5009/api/Empleado/Add',
        /*data: JSON.stringify(empleado),*/
        /*data: empleado,*/
        contentType: 'application/json; charset-utf-8',
        dataType: 'json',
        data: JSON.stringify({
            numeroNomina: empleado.numeroNomina,
            nombre: empleado.nombre,
            apellidoPaterno: empleado.apellidoPaterno,
            apellidoMaterno: empleado.apellidoMaterno,
            idEstado: {
                idCatEntidadFederativa: empleado.idEstado.idCatEntidadFederativa
            }
        }),
        success: function (result) {
            $('#myModal').modal('show');
            $('#ModalUpdate').modal('hide');
            GetAll();
        },
        error: function (result) {
            alert('Error en la consulta.' + result.responseJSON.ErrorMessage);
        }
    });
};


function GetById(IdEmpleado) {
    $.ajax({
        type: 'GET',
        url: 'http://localhost:5009/api/Empleado/GetById/' + IdEmpleado,
        success: function (result) {
            $('#txtIdEmpleado').val(result.object.idEmpleado);
            $('#txtNumeroNomina').val(result.object.numeroNomina);
            $('#txtNombre').val(result.object.nombre);
            $('#txtApellidoPaterno').val(result.object.apellidoPaterno);
            $('#txtApellidoMaterno').val(result.object.apellidoMaterno);
            $('#ddlEstado').val(result.object.idEstado.idCatEntidadFederativa);
            $('#ModalUpdate').modal('show');
        },
        error: function (result) {
            alert('Error en la consulta.' + result.responseJSON.ErrorMessage);
        }


    });

}


function Update(empleado) {

    //var empleado = {
    //    idEmpleado: $('#txtIdEmpleado').val(),
    //    numeroNomina: $('#txtNumeroNomina').val(),
    //    nombre: $('#txtNombre').val(),
    //    apellidoPaterno: $('#txtApellidoPaterno').val(),
    //    apellidoMaterno: $('#txtApellidoMaterno').val(),
    //    catEntidadFederativa: {
    //        idCatEntidadFederativa: $('#ddlEstado').val()
    //    }
    //}

    $.ajax({
        type: 'POST',
        url: 'http://localhost:5009/api/Empleado/Update',
        datatype: 'json',
        contentType: 'application/json; charset=utf-8',
        /*data: empleado,*/
        /*data: JSON.stringify(empleado),*/
        data: JSON.stringify({
            idEmpleado: empleado.idEmpleado,
            /*numeroNomina: empleado.numeroNomina,*/
            nombre: empleado.nombre,
            apellidoPaterno: empleado.apellidoPaterno,
            apellidoMaterno: empleado.apellidoMaterno,
            idEstado: {
                idCatEntidadFederativa: empleado.idEstado.idCatEntidadFederativa
            }
        }),
        success: function (result) {
            $('#myModal').modal('show');
            $('#ModalUpdate').modal('hide');
            GetAll();
            /*Console(respond);*/
        },
        error: function (result) {
            alert('Error en la consulta.' + result.responseJSON.ErrorMessage);
        }
    });

};



function Eliminar(IdEmpleado) {

    if (confirm("¿Estas seguro de eliminar el empleado seleccionado?")) {
        $.ajax({
            type: 'DELETE',
            url: 'http://localhost:5009/api/Empleado/Delete/' + IdEmpleado,
            datatype: 'json',
            contentType: 'application/json; charset=utf-8',
            data: JSON.stringify({
                idEmpleado: IdEmpleado
            }),
            success: function (result) {
                $('#myModal').modal('show');
                GetAll();
            }
        });

    };
};

function Modal() {
    var mostrar = $('#ModalUpdate').modal('show');
    IniciarEmpleado();
};

function ModalClose() {
    var mostrar = $('#ModalUpdate').modal('hide');
};

function IniciarEmpleado() {
    var empleado = {
        idEmpleado: $('#txtIdEmpleado').val(''),
        numeroNomina: $('#txtNumeroNomina').val(''),
        nombre: $('#txtNombre').val(''),
        apellidoPaterno: $('#txtApellidoPaterno').val(''),
        apellidoMaterno: $('#txtApellidoMaterno').val(''),
        catEntidadFederativa: {
            idCatEntidadFederativa: $('#ddlEstado').val(0)
        }
    }
}

function Actualizar() {
    var empleado = {
        idEmpleado: $('#txtIdEmpleado').val(),
        numeroNomina: $('#txtNumeroNomina').val(),
        nombre: $('#txtNombre').val(),
        apellidoPaterno: $('#txtApellidoPaterno').val(),
        apellidoMaterno: $('#txtApellidoMaterno').val(),
        idEstado: {
            idCatEntidadFederativa: $('#ddlEstado').val()
        }
    }

    if (empleado.idEmpleado == '') {
        Add(empleado);
    }
    else {
        Update(empleado);
    }
}