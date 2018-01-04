$(document).ready(function () {
    var entidadGrilla = 'escenario';

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
                commands = "<a type=\"button\" class=\"btn btn-icon command-edit waves-effect waves-circle\" href=\"/" + entidadGrilla + "/Edit/" + row.Id.trim() + "\"><span class=\"zmdi zmdi-edit\"></span></a> " +
                        "<a type=\"button\" caller-method=\""+entidadGrilla+"\" caller-parameter=\"" + row.Id.trim() + "\" class=\"btn btn-icon command-delete waves-effect waves-circle\"><span class=\"zmdi zmdi-delete\"></span></a>";
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
                title: "¿Está seguro de que desea eliminar el escenario?",
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
                        swal("¡Eliminado!", "El escenario fue eliminado.", "success");
                        window.location.assign(window.location.origin + '/' + entidad);
                    },
                    error: function (error) {
                        swal("¡Error!", "El escenario no fue eliminado.", "error");
                    }
                });
            });
        }

        $("a[caller-method]").on('click', callerMethod);
    });
});