$(document).ready(function () {
    $("#form-register").validate({
        rules: {
            Name: {
                required: true,
            },
            Email: {
                required: true,
                email: true,
            },
            Username: {
                required: true,
                remote: {
                    // Agrega esta regla
                    url: "/Account/CheckUsername", // Cambia esto por la ruta a tu servidor que verifica si el usuario existe
                    type: "post",
                    data: {
                        username: function () {
                            return $("#Username").val();
                        },
                    },
                },
            },
            Password: {
                required: true,
                minlength: 8,
                password: true,
            },
        },
        messages: {
            Email: {
                required: "Se requiere ingresar el nombre de usuario",
                email: "Ingresa un correo electrónico válido",
            },
            Username: {
                required: "Se requiere ingresar el nombre de usuario",
                remote: "Este nombre de usuario ya existe",
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
