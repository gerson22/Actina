<?php session_start(); ?>
<!DOCTYPE html>
<!--[if lt IE 7]>      <html class="no-js lt-ie9 lt-ie8 lt-ie7"> <![endif]-->
<!--[if IE 7]>         <html class="no-js lt-ie9 lt-ie8"> <![endif]-->
<!--[if IE 8]>         <html class="no-js lt-ie9"> <![endif]-->
<!--[if gt IE 8]><!--> <html class="no-js"> <!--<![endif]-->
<head>
    <meta charset="utf-8">
    <meta http-equiv="X-UA-Compatible" content="IE=edge">
    <title>Actina</title>
    <meta name="description" content="">
    <meta name="viewport" content="width=device-width, initial-scale=1">
    <!-- Favicon -->
    <link href='https://fonts.googleapis.com/css?family=Open+Sans:300,400,600' rel='stylesheet' type='text/css'>
    <link rel="stylesheet" href="//maxcdn.bootstrapcdn.com/bootstrap/3.2.0/css/bootstrap.min.css">
    <link rel="stylesheet" href="/c/normalize.css">
    <link rel="stylesheet" href="/c/main.css">
    <link rel="stylesheet" href="/c/general.css">
    <link rel="stylesheet" href="/c/jquery.dataTables.min.css">
    <link rel="stylesheet" href="/c/indexAdmin.css">
    <script src="/lib/modernizr-2.6.2.min.js"></script>
</head>
<body>

<nav id="menu_top" class="navbar navbar-default navbar-fixed-top" role="navigation" >
    <div class="container">
        <a href="/index.php">
            <img class="icono_left" src="i/icon_home.png" />
        </a>
        <a href="/includes/logout.php">
            <img class="icono" src="i/icon_logout.png" />
        </a>
        <a href="/administracion.html">
            <img class="icono" src="i/icon_administracion.png" />
        </a>
    </div>
</nav>

<div class="container" id="container">
    <div class="container-fluid">

        <div class="col-md-8">
            <a href="/inscribir.html" ><button>Inscribir</button></a>
            <a href="/acceso.html" ><button style="float: right" >Acceso</button></a>

            <table id="tablaClientes">
                <!-- AJAX -->
            </table>

            <h3>Estadísticas</h3>

            <div id="graficas_wrapper">
                <div class="chart_inner_div">
                    <div class="chart_inner_div_title">Tipo de subscriptor</div>
                    <canvas id="canvas_subscripcion_pie" width="480" height="400"></canvas>
                </div>
            </div>
        </div>

        <div class="col-md-4" style="margin-top: 10px">

            <img src="/i/logo_placeholder.png" class="center" id="negocio_logo" >

            <div id="news_ticker">
                <!-- Subscripciones próximas a vencer -->
                <div id="calendario">

                </div>
                <div id="vencidos">
                    <!-- AJAX -->
                </div>
            </div>

        </div>

    </div>
</div>

<script src="//ajax.googleapis.com/ajax/libs/jquery/1.10.2/jquery.min.js"></script>
<script src="/lib/main.js"></script>
<script src="/lib/jquery.validate.min.js"></script>
<script src="/lib/Chart.min.js"></script>
<script src="/lib/plugins.js"></script>
<script src="/lib/jquery.dataTables.min.js"></script>
<script src="/lib/Chart.min.js" ></script>
<script>
    var email = sessionStorage.getItem("email");
    var password = sessionStorage.getItem("password");
    var connection;
    var listaUsuarios = [];
    var ctx_subscripciones_pie;

    cargarListaUsuarios();

    function crearChartSubscripciones()
    {
        ctx_subscripciones_pie = $("#canvas_subscripcion_pie").get(0).getContext("2d");
        //var chart_subscripciones_pie = new Chart(ctx_subscripciones_pie);

        var chart_pie_data = [];
        console.dir(listaUsuarios);
        for(u in listaUsuarios)
        {
            var mensuales = 0;
            var semanales = 0;
            var diarias = 0;
            switch(listaUsuarios[u].tipoSubscripcion)
            {
                case "Mensual": mensuales++; break;
                case "Semanal": semanales++; break;
                case "Diaria": diarias++; break;
                default: break;
            }

            chart_pie_data.push(
                { value: mensuales, color: "#FF3333", highlight: "#FF5555", label: "Mensuales" },
                { value: semanales, color: "#3333FF", highlight: "#5555FF", label: "Semanales" },
                { value: diarias, color: "#33FF33", highlight: "#55FF55", label: "Diarias" }
            );
        }

        var options_pie = {animationSteps  : 180};

        console.dir(chart_pie_data);
        var myPieChart = new Chart(ctx_subscripciones_pie).Pie(chart_pie_data, options_pie);
    }

    function socketStuff()
    {
        connection = new WebSocket('ws://localhost:8080');

        // When the connection is open, send some data to the server
        connection.onopen = function () {
            $("#sockets").append("<span>Conexión creada</span>");
        };

        // Log errors
        connection.onerror = function (error) {
            $("#sockets").append("<span>Error: "+error+"</span>");
        };

        // Log messages from the server
        connection.onmessage = function (e) {
            $("#sockets").append("<span>"+ e.data+"</span>");
        };
    }

    function cargarListaUsuarios()
    {
        $.ajax({
            type: "GET",
            url: "http://api.actina.nop.al/usuarios",
            headers: { "Authorization": "Basic " + btoa(email + ":" + password) },
            crossDomain: true
        }).done(function(usuarios)
        {
            listaUsuarios = usuarios;
            var datos = [];
            var user_temp = [];
            for(i in usuarios)
            {
                user_temp.push(usuarios[i].nombre);
                user_temp.push(usuarios[i].tipoSubscripcion);
                user_temp.push(usuarios[i].fechaVencimiento);
                datos.push(user_temp);

                var dias_restantes = Math.floor((user_temp[i].fechaVencimientoRaw - Math.floor($.now() / 1000)) / 86400);

                if(dias_restantes <= 0)
                {
                    // Usuario con subscripción vencida
                    $("#vencidos").append("<div class='vencido'>" +
                        "<div class='nombre'>"+listaUsuarios[i].nombre+"</div>" +
                        "<div class='dias'>Subscripción vencida hace <b>"+Math.abs(dias_restantes)+"</b> dias</div>" +
                    "</div>");
                }
            }

            $('#tablaClientes').dataTable({
                "data": datos,
                "columns": [
                    { "title": "Nombre del cliente" },
                    { "title": "Tipo de subscripción", "class": "min650" },
                    { "title": "Vencimiento", "class": "min350" }
                ]
            });

            crearChartSubscripciones();
        }).fail(function( jqXHR, textStatus, errorThrown )
        {
            ;
        });
    }
</script>
</body>
</html>