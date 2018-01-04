var mailValido = false;

var validarAgregarMail = function () {

    var inputMail = $('[add-mail-input]');
    var mail = inputMail.val();

    if (mail == null || mail == '') {
        $('[add-mail-a]').attr('disabled', 'disabled')
        mailValido = false;
    }
    else {
        $('[add-mail-a]').removeAttr('disabled');
        mailValido = true;
    }

};

var telefonoValido = false;

var validarAgregarTelefono = function () {

    var inputMail = $('[add-telefono-input]');
    var telefono = inputMail.val();

    if (telefono == null || telefono == '') {
        $('[add-telefono-a]').attr('disabled', 'disabled')
        telefonoValido = false;
    }
    else {
        $('[add-telefono-a]').removeAttr('disabled');
        telefonoValido = true;
    }

};

var verificarCampoAlerta = function () {

    var sectionCamposAlerta = $('[campos-alerta]');

    sectionCamposAlerta.removeClass('hide');

    if ($('[chk-alerta]').prop('checked')) {
        sectionCamposAlerta.removeClass('hide');
    } else {
        sectionCamposAlerta.addClass('hide');
        limpiarCamposAlerta();
    }
}

var limpiarCamposAlerta = function () {
    $('[add-mail-input]').val('');
    $('[add-telefono-input]').val('');
    $('[add-mail-chosen]').find('option').remove();
    $('[add-mail-chosen]').trigger("chosen:updated");
    $('[add-telefono-chosen]').find('option').remove();
    $('[add-telefono-chosen]').trigger("chosen:updated");

}

$(document).ready(function () {
    validarAgregarMail();
    validarAgregarTelefono();
    verificarCampoAlerta();

    $('[chk-alerta]').change(function () {
        verificarCampoAlerta();
    });

    $('[add-mail-a]').click(function () {

        if (!mailValido)
            return;

        var inputMail = $('[add-mail-input]');
        var mail = inputMail.val();
        var chosen = $('[add-mail-chosen]');

        chosen.append('<option selected="selected" value="' + mail + '">' + mail + '</option>');
        chosen.trigger("chosen:updated");

        inputMail.val('');
        validarAgregarMail();
    });

    $('[add-mail-input]').keyup(function () {

        validarAgregarMail($(this));
    });

    $('[add-telefono-a]').click(function () {

        if (!telefonoValido)
            return;

        var inputTelefono = $('[add-telefono-input]');
        var telefono = inputTelefono.val();
        var chosen = $('[add-telefono-chosen]');

        chosen.append('<option selected="selected" value="' + telefono + '">' + telefono + '</option>');
        chosen.trigger("chosen:updated");
        inputTelefono.val('');
        validarAgregarTelefono();
    });

    $('[add-telefono-input]').keyup(function () {

        validarAgregarTelefono($(this));
    });
});
