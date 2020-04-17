// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.

function SelectCandidato(UrlSendSelection) {
    if (confirm("Esta seguro de su seleccion\nSi acepta esta seleccion no podra volver a cambiarla.")) {
        return $.post(UrlSendSelection);
    }
    return;
}

//<script src="https://code.highcharts.com/highcharts.js"></script>
//<script src="https://code.highcharts.com/modules/exporting.js"></script>
//<script src="https://code.highcharts.com/modules/export-data.js"></script>
//<script src="https://code.highcharts.com/modules/accessibility.js"></script>

