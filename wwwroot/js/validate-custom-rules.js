$(document).ready(function () {
    $.validator.setDefaults({
        errorPlacement: function (error, element) {
            var name = element.attr("name");
            var errorSelector = '.error_message[for="' + name + '"]';
            var $element = $(errorSelector);
            if ($element.length) {
                $(errorSelector).html(error.html());
            } else {
                error.insertAfter(element);
            }
        },
        success: function (label, element) {
            var name = $(element).attr("name");
            var errorSelector = '.error_message[for="' + name + '"]';
            $(errorSelector).html("");
        },
    });
    $.validator.addMethod(
        "password",
        function (value, element) {
            return (
                this.optional(element) ||
                /^(?=.*[A-Z])(?=.*\d)(?=.*[^\w\d\s]).{8,}$/.test(value)
            );
        },
        "La contraseña debe tener al menos 8 caracteres, una letra mayúscula y un número"
    );
});
