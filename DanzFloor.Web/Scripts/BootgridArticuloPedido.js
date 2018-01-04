$(document).ready(function () {
    var entidadGrilla = 'articuloPedido';

    $("#data-table-basic-" + entidadGrilla).bootgrid({
        caseSensitive: false,
        rowCount: 25,

        columnSelection: false,
        css: {
            icon: 'zmdi icon',
            iconColumns: 'zmdi-view-module',
            iconDown: 'zmdi-sort-amount-desc',
            iconRefresh: 'zmdi-refresh',
            iconUp: 'zmdi-sort-amount-asc'
        },
        formatters: {
            "commands": function (column, row) {
                commands = "<a type=\"button\" caller-modal=\"" + entidadGrilla + "\" caller-parameter=\"" + row.Id.trim() + "\" class=\"btn btn-icon command-edit waves-effect waves-circle\"><span class=\"zmdi zmdi-edit\"></span></a> " +
                        "<a type=\"button\" caller-method=\"" + entidadGrilla + "\" caller-parameter=\"" + row.Id.trim() + "\" class=\"btn btn-icon command-delete waves-effect waves-circle\"><span class=\"zmdi zmdi-delete\"></span></a>";
                return commands;
            }
        },
        searchSettings: {
            delay: 500,
            characters: 3
        },
        labels: {
            noResults: "No se han encontrado resultados para su búsqueda",
            loading: "Cargando...",
            search: "Buscar",
            infos: "Mostrando {{ctx.start}} a {{ctx.end}} de {{ctx.total}} registros",
            all: "Todos",
            refresh: "Actualizar"
        }
    }).on("loaded.rs.jquery.bootgrid", function () {

        var callerMethod = function () {
            var entidad = $(this).attr('caller-method');
            var id = $(this).attr('caller-parameter');
            
            swal({
                title: "¿Está seguro de que desea eliminar el artículo?",
                type: "warning",
                showCancelButton: true,
                confirmButtonColor: "#DD6B55",
                confirmButtonText: "Sí",
                cancelButtonText: "No",
                closeOnConfirm: false
            }, function () {
                $.ajax({
                    dataType: 'json',
                    type: 'GET',
                    url: window.location.origin + '/' + entidad + '/delete/' + id,
                    success: function (result) {
                        swal("¡Eliminado!", "El artículo fue eliminado.", "success");
                        window.location.assign(window.location.href);
                    },
                    error: function (error) {
                        swal("¡Error!", "El artículo no fue eliminado.", "error");
                    }
                });
            });
        }
        var callerModal = function () {
            var id = $(this).attr('caller-parameter');

            $('#modalWider_' + id).modal('show');
        }

        $("a[caller-method]").on('click', callerMethod);
        $("a[caller-modal]").on('click', callerModal);
    });
});

$(document).ready(function () {
    var entidadGrilla = 'articuloPedidoDetalle';

    $("#data-table-basic-" + entidadGrilla).bootgrid({
        caseSensitive: false,
        rowCount: 25,

        columnSelection: false,
        css: {
            icon: 'zmdi icon',
            iconColumns: 'zmdi-view-module',
            iconDown: 'zmdi-sort-amount-desc',
            iconRefresh: 'zmdi-refresh',
            iconUp: 'zmdi-sort-amount-asc'
        },
        formatters: {
            "commands": function (column, row) {
                commands = "";
                return commands;
            }
        },
        searchSettings: {
            delay: 500,
            characters: 3
        },
        labels: {
            noResults: "No se han encontrado resultados para su búsqueda",
            loading: "Cargando...",
            search: "Buscar",
            infos: "Mostrando {{ctx.start}} a {{ctx.end}} de {{ctx.total}} registros",
            all: "Todos",
            refresh: "Actualizar"
        }
    });
});