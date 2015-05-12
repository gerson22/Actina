// Avoid `console` errors in browsers that lack a console.
(function() {
    var method;
    var noop = function () {};
    var methods = [
        'assert', 'clear', 'count', 'debug', 'dir', 'dirxml', 'error',
        'exception', 'group', 'groupCollapsed', 'groupEnd', 'info', 'log',
        'markTimeline', 'profile', 'profileEnd', 'table', 'time', 'timeEnd',
        'timeStamp', 'trace', 'warn'
    ];
    var length = methods.length;
    var console = (window.console = window.console || {});

    while (length--) {
        method = methods[length];

        // Only stub undefined methods.
        if (!console[method]) {
            console[method] = noop;
        }
    }
}());

// Place any jQuery/helper plugins in here.

/** Override a los mensajes de error de jQuery.validate */
jQuery.extend(jQuery.validator.messages, {
    required: "Campo requerido.",
    remote: "Please fix this field.",
    email: "Se requiere un e-mail válido.",
    url: "Please enter a valid URL.",
    date: "Se necesita una fecha válida.",
    dateISO: "Please enter a valid date (ISO).",
    number: "Debe ingresar un número válido.",
    digits: "Por favor ingresa solo dígitos.",
    creditcard: "Please enter a valid credit card number.",
    equalTo: "Ingresa el mismo valor.",
    accept: "Please enter a value with a valid extension.",
    maxlength: jQuery.validator.format("Debe ingresar un máximo de {0} caracteres."),
    minlength: jQuery.validator.format("Debe ingresar al menos {0} caracteres."),
    rangelength: jQuery.validator.format("Please enter a value between {0} and {1} characters long."),
    range: jQuery.validator.format("Please enter a value between {0} and {1}."),
    max: jQuery.validator.format("Please enter a value less than or equal to {0}."),
    min: jQuery.validator.format("Please enter a value greater than or equal to {0}.")
});