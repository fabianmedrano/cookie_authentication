$(document).ready(function () {
    $("#form-login").validate({
        rules: {
            Username: {
                required: true,
            },
            Password: {
                required: true,
                minlength: 8,
            },
        },
        messages: {
            Username: {
                required: "Se requiere ingresar el nombre de usuario",
                email: "Ingresa un correo electrónico válido",
            },
            Password: {
                required: "Se requiere ingresar una contraseña",
                minlength: jQuery.validator.format(
                    "Ingrese menos de {0} caracteres"
                ),
            },
        },
        submitHandler: function (form) {
            form.submit();
        },
    });
});
