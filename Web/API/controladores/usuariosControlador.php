<?php

class usuariosControlador
{
    public function procesar($metodo, $verbo, $args)
    {
        if($metodo == "GET")
        {
            switch($verbo)
            {
                case "":
                    if(!is_null($args[0]))
                    {
                        // [GET] usuarios/usuarioID
                        return new UsuarioModelo($args[0]);
                    }
                    else
                    {
                        // [GET] usuarios
                        return $this->getUsuarios();
                    }
                    break;
                case "login":
                    // [GET] usuarios/login
                    return $this->login();
                    break;
                case "FMD":
                    // [GET] usuarios/FMD
                    return $this->getFMD();
                    break;
                case "stats":
                    // [GET] usuarios/status
                    return $this->getStats();
                    break;
                default:
                    return 404;
                    break;
            }
        }
        else if($metodo == "POST")
        {
            switch($verbo)
            {
                default:
                    // [POST] usuarios
                    return $this->inscribir();
                    break;
            }
        }
        return 404;
    }

    protected function getUsuarios()
    {
        $usuario = $this->login();
        if($usuario->tipoUsuarioID == 1)
        {
            $usuarios = usuarioModelo::getLista();
            if(count($usuarios) > 0)
            {
                $lista = array();
                foreach($usuarios as $usuario)
                {
                    $tmp = array();
                    $tmp['usuarioID']           = $usuario['usuarioID'];
                    $tmp['nombre']              = $usuario['nombre'];
                    $tmp['tipoSubscripcion']    = $usuario['tipoSubscripcion'];
                    $tmp['fechaVencimientoRaw'] = strtotime($usuario['vencimiento']);
                    $tmp['fechaVencimiento']    = date('d M', strtotime($usuario['vencimiento']));
                    array_push($lista, $tmp);
                }
                return $lista;
            }
            return [];
        }
        return 401; // Autorización
    }

    protected function login()
    {
        return usuarioModelo::getUsuarioLogin($_SERVER['PHP_AUTH_USER'], $_SERVER['PHP_AUTH_PW']);
    }

    protected function inscribir()
    {
        $nombre = $_POST["nombre"];
        $email = $_POST["email"];
        $peso = $_POST["peso"];
        $altura = $_POST["altura"];
        $talla = $_POST["talla"];
        $tipoSubscripcionID = $_POST["tipoSubscripcionID"];
        $FMD = $_POST["FMD"];

        return usuarioModelo::inscribir($nombre, $email, $peso, $altura, $talla, $tipoSubscripcionID, $FMD);
    }

    protected function getFMD()
    {
        return usuarioModelo::getFMD();
    }

    protected function getStats()
    {
        return usuarioModelo::getStats();
    }
}