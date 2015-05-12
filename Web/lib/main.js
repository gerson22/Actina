

function cargarUsuarioIndex()
{
    contenido.load("usuarioIndex.html", function()
    {
        cargarDatosUsuario();
        cargarGraficaPeso(90);
    });
}

function cargarDatosUsuario()
{
    $.ajax({
        url: "http://gym.nop.al/API/usuarios/yo",
        beforeSend: function (xhr)
        {
            /** Se envia el usuario y password (de las cookies)  */
            var username = localStorage.getItem("usuarioID");
            var password = localStorage.getItem("password");
            xhr.setRequestHeader ("Authorization", "Basic " + btoa(username + ":" + password));
        },
        statusCode:
        {
            // 401 - Error de autorización
            401:function() { contenido.load("login.html"); }
        },

        success: function (usuario)
        {
            contenido.find("#clienteFoto").attr('src', usuario.fotoURL);
            contenido.find('#clienteNombreVal').html(usuario.nombre);
            contenido.find("#clienteMiembroDesdeVal").html(usuario.clienteDesde);
            contenido.find("#clientePesoVal").html(usuario.peso + " kg.");
            contenido.find("#clienteAlturaVal").html(usuario.altura + " mts.");
            contenido.find("#clienteTallaVal").html(usuario.talla);
            contenido.find("#clienteIMCVal").html(usuario.IMC);
            contenido.find("#clienteTipoSubscripcionVal").html(usuario.tipoSubscripcion);
            contenido.find("#clienteFechaVencimientoVal").html(usuario.vencimiento);
        }
    });
}

function cargarGraficaPeso(dias)
{
    $.ajax({
        url: "http://gym.nop.al/API/usuarios/yo/peso/" + dias,
        beforeSend: function (xhr)
        {
            var username = localStorage.getItem("usuarioID");
            var password = localStorage.getItem("password");
            xhr.setRequestHeader ("Authorization", "Basic " + btoa(username + ":" + password));
        },
        statusCode:{ 401: function(){ contenido.load("login.html"); }},
        success: function (mediciones)
        {
            var ctx = document.getElementById("clientePesoChart").getContext("2d");

            var etiquetas = [];
            var pesos = [];
            for(i in mediciones)
            {
                etiquetas.push(mediciones[i].fechaLabel);
                pesos.push(mediciones[i].peso);
            }
            var data = {
                labels: etiquetas,
                datasets: [
                    {
                        label: "Peso",
                        fillColor: "rgba(220,220,220,0.2)",
                        strokeColor: "rgba(220,220,220,1)",
                        pointColor: "#428bca", //"rgba(220,220,220,1)",
                        pointStrokeColor: "#fff",
                        pointHighlightFill: "#fff",
                        pointHighlightStroke: "rgba(220,220,220,1)",
                        data: pesos
                    }
                ]
            };

            var chartPeso = new Chart(ctx).Line(data, {
                responsive: "true",
                maintainAspectRatio: "true"
            });
        }
    });
}

function login()
{
    if($("#form_login").valid())
    {
        var username = $("#userVal").val();
        var password = $("#passVal").val();
        $.ajax({
            type: "GET",
            url: "http://gym.nop.al/API/usuarios/yo",
            beforeSend: function (xhr)
            {
                /** Se envia el usuario y password (de las cookies)  */
                xhr.setRequestHeader ("Authorization", "Basic " + btoa(username + ":" + password));
            },
            statusCode:
            {
                // 401 - Error de autorización
                401:function() { $("#error_login").fadeIn(); }
            },
            success: function (usuario)
            {
                localStorage.setItem("usuarioID", usuario.usuarioID);
                localStorage.setItem("password", usuario.password);
                localStorage.setItem("tipoUsuario", usuario.tipoUsuario);
                comprobarSesionLocal();
            }
        });
    }
}

function inscribirClicked()
{
    contenido.load("inscripcion.html");
}

function activarModoEntradas()
{

}

function inscribir()
{
    if($("#form_inscripcion").valid())
    {
        var nombre = $("#nombreVal").val();
        var password = $("#passVal").val();
        var email = $("#emailVal").val();
        var peso = $("#pesoVal").val();
        var altura = $("#alturaVal").val();
        var talla = $("#tallaVal").val();
        var tipoSub = $("#subscripcionVal").val();

        $.ajax({
            type: "POST",
            url: "http://gym.nop.al/API/usuarios",
            beforeSend: function (xhr) {
                var username = localStorage.getItem("usuarioID");
                var password = localStorage.getItem("password");
                xhr.setRequestHeader("Authorization", "Basic " + btoa(username + ":" + password));
            },
            data: "nombre=" + nombre + "&password=" + password + "&email=" + email + "&peso=" + peso
            + "&altura=" + altura + "&talla=" + talla + "&tipoSub=" + tipoSub,
            success: function (data) {
                alert("Cliente registrado");
                /*contenido.load("administradorIndex.html", function () {
                    cargarListaUsuarios();
                });*/
            }
        });
    }
}