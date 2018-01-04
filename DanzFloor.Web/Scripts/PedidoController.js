var completarSelectPuntoEntrega = function (result) {
    var puntoEntrega = JSON.parse(result.Mensaje);

    var select = $('[punto-entrega-select]');
    select.find('option').remove();

    if (puntoEntrega.length > 0) {

        if (puntoEntrega.length > 1)
            select.append('<option value="00000000-0000-0000-0000-000000000000">Seleccione...</option>');

        for (var i = 0; i < puntoEntrega.length; i++) {
            select.append('<option value=' + usuarios[i].Id + '>' + usuarios[i].NoNombreYDireccionmbre + '</option>');
        }

    }

    select.trigger("chosen:updated");
}

$(document).ready(function () {
    $('#ClienteId').change(function () {
        var clienteId = $(this).val();


        $('[punto-entrega-select]').find('option').remove();
        $('[punto-entrega-select]').trigger("chosen:updated");

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
    })
});

