$(document).ready(function () {
    //Basic Example
    var entidadGrilla = 'puntoentrega';

    $("#data-table-basic-" + entidadGrilla).bootgrid({
        caseSensitive: false,
        rowCount: 10,

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
                commands = "<a type=\"button\" class=\"btn btn-icon command-edit waves-effect waves-circle\" href=\"/" + entidadGrilla + "/Edit/" + row.Id + "\"><span class=\"zmdi zmdi-edit\"></span></a> ";
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