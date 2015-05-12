<?php
include_once("includes/validar_sesion.php");
if($_SESSION["tipoUsuarioID"] == 1) include_once("includes/indexAdmin.php");
if($_SESSION["tipoUsuarioID"] == 2) include_once("includes/indexCliente.html");