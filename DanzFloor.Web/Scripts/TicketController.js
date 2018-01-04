

function cargarOptionsPuntoEntrega(item, select) {
    select.append('<option value=' + item.Id + '>' + item.NombreYDireccion + '</option>');
}

var completarSelectResponsable = function (result) {
    var usuarios = JSON.parse(result.Mensaje);

    var select = $('[responsable-ticket]');
    select.find('option').remove();

    if (usuarios.length > 0) {
        if (usuarios.length > 1)
            select.append('<option value="00000000-0000-0000-0000-000000000000">Seleccione...</option>');

        for (var i = 0; i < usuarios.length; i++) {
            select.append('<option value=' + usuarios[i].Id + '>' + usuarios[i].NombreYApellido + '</option>');
        }

    }

    select.trigger("chosen:updated");
}

var completarSelectSubtipo = function (result) {
    var usuarios = JSON.parse(result.Mensaje);

    var select = $('[subtipo-ticket]');
    select.find('option').remove();

    if (usuarios.length > 0) {
        if (usuarios.length > 1 && document.URL.toLowerCase().indexOf('edit') == -1)
            select.append('<option value="00000000-0000-0000-0000-000000000000">Seleccione...</option>');

        for (var i = 0; i < usuarios.length; i++) {
            select.append('<option value=' + usuarios[i].Id + '>' + usuarios[i].Nombre + '</option>');
        }

    }

    select.trigger("chosen:updated");
}

var completarSelectPuntoEntrega = function (result) {
    var puntoEntrega = JSON.parse(result.Mensaje);

    var select = $('[punto-entrega-select]');
    select.find('option').remove();

    if (puntoEntrega.length > 0) {

        if (puntoEntrega.length > 1)
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
        if (usuarios.length > 1)
            select.append('<option value="00000000-0000-0000-0000-000000000000">Seleccione...</option>');

        for (var i = 0; i < usuarios.length; i++) {
            select.append('<option value=' + usuarios[i].Id + '>' + usuarios[i].NombreYCliente + '</option>');
        }

    }

    select.trigger("chosen:updated");
}

var completarSelectPedido = function (result) {
    var usuarios = JSON.parse(result.Mensaje);

    var select = $('[pedido-ticket]');
    select.find('option').remove();

    if (usuarios.length > 0) {
        if (usuarios.length > 1)
            select.append('<option value="00000000-0000-0000-0000-000000000000">Seleccione...</option>');

        for (var i = 0; i < usuarios.length; i++) {
            select.append('<option value=' + usuarios[i].Id + '>' + usuarios[i].Nombre + '</option>');
        }

    }

    select.trigger("chosen:updated");
}

var completarSelectArticulo = function (result) {
    var articulos = JSON.parse(result.Mensaje);

    var select = $('[articulo-ticket]');
    select.find('option').remove();

    if (articulos.length > 0) {
        for (var i = 0; i < articulos.length; i++) {
            select.append('<option value=' + articulos[i].Id + '>' + articulos[i].Nombre + '</option>');
        }
    }

    select.trigger("chosen:updated");
}

var AdministrativosPorArea = function (areaId) {
    $.ajax({
        dataType: 'json',
        type: 'GET',
        url: core + '/Administrativo/ObtenerPorFiltro' + '?areaId=' + areaId,
        success: function (data) {
            completarSelectResponsable(data, $(this));
        },
        error: function (ex) {
            console.log(ex)
        },
    });
}

$(document).ready(function () {
    $('#TipoTicketId').change(function () {
        var tipoTicketId = $(this).val();

        $.ajax({
            dataType: 'json',
            type: 'GET',
            url: core + '/Area/PorTipoTicket' + '?tipoTicketId=' + tipoTicketId,
            success: function (result) {
                var entidad = JSON.parse(result.Mensaje);
                var select = $('#AreaId');

                select.find('option:selected').removeAttr('selected');

                select.find('option').each(function () {

                    if (entidad.Id == $(this).val()) {
                        $(this).attr("selected", "selected");
                        AdministrativosPorArea($(this).val());
                    }
                });

                select.trigger("chosen:updated");
            },
            error: function (ex) {
                console.log(ex)
            },
        });
    })

    $('#AreaId').change(function () {
        var areaId = $(this).val();
        AdministrativosPorArea(areaId);

        $.ajax({
            dataType: 'json',
            type: 'GET',
            url: core + '/SubtipoTicket/PorFiltro' + '?areaId=' + areaId,
            success: function (data) {
                completarSelectSubtipo(data, $(this));
            },
            error: function (ex) {
                console.log(ex)
            },
        });
    })

    $('#ClienteId').change(function () {
        var clienteId = $(this).val();


        $('[punto-entrega-select]').find('option').remove();
        $('[punto-entrega-select]').trigger("chosen:updated");

        $('[usuario-cliente]').find('option').remove();
        $('[usuario-cliente]').trigger("chosen:updated");

        $('[pedido-ticket]').find('option').remove();
        $('[pedido-ticket]').trigger("chosen:updated");

        $('[articulo-ticket]').find('option').remove();
        $('[articulo-ticket]').trigger("chosen:updated");

        $.ajax({
            dataType: 'json',
            type: 'GET',
            url: core + '/PuntoEntrega/ObtenerPorFiltro' + '?clienteId=' + clienteId,
            success: function (result) {
                completarSelectPuntoEntrega(result);
            },
            error: function (ex) {
                console.log(ex)
            },
        });

        $.ajax({
            dataType: 'json',
            type: 'GET',
            url: core + '/Cuenta/ObtenerPorFiltro' + '?clienteId=' + clienteId,
            success: function (result) {
                completarSelectUsuarioCliente(result);
            },
            error: function (ex) {
                console.log(ex)
            },
        });

        $.ajax({
            dataType: 'json',
            type: 'GET',
            url: core + '/Pedido/PorFiltro' + '?clienteId=' + clienteId,
            success: function (result) {
                completarSelectPedido(result);
            },
            error: function (ex) {
                console.log(ex)
            },
        });

        $.ajax({
            dataType: 'json',
            type: 'GET',
            url: core + '/Articulo/PorFiltro' + '?clienteId=' + clienteId,
            success: function (result) {
                completarSelectArticulo(result);
            },
            error: function (ex) {
                console.log(ex)
            },
        });
    })
});

