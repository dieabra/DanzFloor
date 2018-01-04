$(document).ready(function () {
    $("[caller-method]").on('click', callerMethod);
    $("[caller-method-get]").on('click', callerMethodGet);
    $("[caller-method-change]").on('change', callerMethodChange);
});


var callerMethodInServer = false;
var callerMethod = function () { }

var callerMethodGetInServer = false;
var callerMethodGet = function () { }

var callerMethodChangeInServer = false;
var callerMethodChange = function () {
    // La variable "callerMethodChangeInServer" tiene la utilidad de no llamar al servidor en paralelo.
    if (!callerMethodChangeInServer) {
        callerMethodChangeInServer = true;

        var method = $(this).attr('caller-method-change');
        var parametro1 = $(this).attr('caller-parameter-name');
        var control = $(this).attr('caller-control');
        var valor1 = $(this).val();
        
        limpiarControl(control);

        $.ajax({
            dataType: 'json',
            type: 'GET',
            url: core + method + '?' + parametro1 + '=' + valor1,
            success: function (data) {
                if (method.indexOf('/PuntoEntrega/ObtenerPorFiltro') !== -1)
                    completarSelectPuntoEntrega(data, $(this));
                else if (method.indexOf('/Pedido/ObtenerPorFiltro') !== -1)
                    completarSelectPedido(data, $(this));
                else if (method.indexOf('/Administrativo/ObtenerPorFiltro') !== -1)
                    completarSelectColaborador(data, $(this));
                else if (method.indexOf('/Cuenta/ObtenerParaPedidoPorFiltro') !== -1)
                    completarSelectUsuarioCliente(data, $(this));

                callerMethodChangeInServer = false;
            },
            error: function () {
                callerMethodChangeInServer = false;
            },
        });
    }
}

// FUNCIONES SUCCESS \\

var completarSelectPuntoEntrega = function (result) {
    var puntoEntrega = JSON.parse(result.Mensaje);

    var select = $('[punto-entrega-select]');
    select.find('option').remove();

    if (puntoEntrega.length > 0) {
        if (location.href.indexOf('/Cuenta/') == -1)
            select.append('<option value="00000000-0000-0000-0000-000000000000">Seleccione...</option>');
        for (var i = 0; i < puntoEntrega.length; i++) {
            cargarOptionsPuntoEntrega(puntoEntrega[i], select);
        }

    }

    select.trigger("chosen:updated");
}

var completarSelectUsuarioCliente = function (result) {
    var usuarios = JSON.parse(result.Mensaje);

    var select = $('[usuario-cliente]');
    select.find('option').remove();

    if (usuarios.length > 0) {
        select.append('<option value="00000000-0000-0000-0000-000000000000">Seleccione...</option>');
        for (var i = 0; i < usuarios.length; i++) {
            cargarOptionsUsuarioCliente(usuarios[i], select);
        }

    }

    select.trigger("chosen:updated");
}

var limpiarControl = function (control) {
    var select = $('[' + control + ']');
    select.find('option').remove();
}

var completarSelectPedido = function (result) {
    var pedido = JSON.parse(result.Mensaje);
    var select = $('[pedido-select]');

    select.find('option').remove();

    if (pedido.length > 0) {
        select.append('<option value="00000000-0000-0000-0000-000000000000">Seleccione...</option>');
        for (var i = 0; i < pedido.length; i++) {
            cargarOptionsPedido(pedido[i], select);
        }

    }

    select.trigger("chosen:updated");
}

var completarSelectColaborador = function (result) {
    var colaboradores = JSON.parse(result.Mensaje);
    var select = $('[responsable-select]');

    select.find('option').remove();

    if (colaboradores.length > 0) {
        select.append('<option value=00000000-0000-0000-0000-000000000000>Sin Asignar</option>');
        for (var i = 0; i < colaboradores.length; i++) {
            cargarOptionsColaborador(colaboradores[i], select);
        }

    }

    select.trigger("chosen:updated");
}

function cargarOptionsPuntoEntrega(item, select) {
    select.append('<option value=' + item.Id + '>' + item.NombreYDireccion + '</option>');
}

function cargarOptionsPedido(item, select) {
    select.append('<option value=' + item.Id + '>' + item.NombreCompuesto + '</option>');
}

function cargarOptionsColaborador(item, select) {
    select.append('<option value=' + item.Id + '>' + item.NombreYArea + '</option>');
}

function cargarOptionsUsuarioCliente(item, select) {
    if(item.Apellido == null)
        select.append('<option value=' + item.Id + '>' + item.Nombre + '</option>');
    else
        select.append('<option value=' + item.Id + '>' + item.NombreYCliente + '</option>');
}