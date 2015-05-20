<?php

class usuarioModelo
{
    public $usuarioID;
    public $password;
    public $nombre;
    public $email;
    public $peso;
    public $altura;
    public $IMC;
    public $talla;
    public $tipoUsuarioID;
    public $tipoSubscripcionID;
    public $fotoURL;
    public $vencimiento;
    public $ultimoAcceso;
    public $clienteDesde;

    public function __construct($usuarioID)
    {
        $query = "SELECT * FROM usuario WHERE usuarioID = $usuarioID";
        $res = APIDatabase::select($query);

        $this->usuarioID            = $res[0]["usuarioID"];
        $this->password             = $res[0]["password"];
        $this->nombre               = $res[0]["nombre"];
        $this->email                = $res[0]["email"];
        $this->peso                 = $res[0]["peso"];
        $this->altura               = $res[0]["altura"];
        $this->IMC                  = $res[0]["IMC"];
        $this->talla                = $res[0]["talla"];
        $this->tipoUsuarioID        = $res[0]["tipoUsuarioID"];
        $this->tipoSubscripcionID   = $res[0]["tipoSubscripcionID"];
        $this->fotoURL              = $res[0]["fotoURL"];
        $this->vencimiento          = $res[0]["vencimiento"];
        $this->ultimoAcceso         = $res[0]["ultimoAcceso"];
        $this->clienteDesde         = $res[0]["clienteDesde"];
    }

    public static function getUsuarioLogin($_email, $_password)
    {
        $query = "SELECT * FROM usuario WHERE email = '$_email' AND password = '$_password'";
        $res = APIDatabase::select($query);
        $usuario = new self($res[0]['usuarioID']);
        $usuario->actualizarLastLogin();
        return $usuario;
    }

    public function actualizarLastLogin()
    {
        $query = "UPDATE usuario SET ultimoAcceso = NOW() WHERE usuarioID = $this->usuarioID";
        APIDatabase::update($query);
    }

    public static function getLista()
    {
        $query = "SELECT * FROM usuario
            JOIN tipo_subscripcion ON tipo_subscripcion.tipoSubscripcionID = usuario.tipoSubscripcionID
            WHERE tipoUsuarioID = 2";
        return APIDatabase::select($query);
    }

    public static function inscribir($nombre, $email, $peso, $altura, $talla, $tipoSubscripcionID, $FMD)
    {
        $password = "temp_pass";
        $IMC = 30;
        $vencimiento = null;
        $query = "INSERT INTO usuario SET password = '$password', nombre = '$nombre', email = '$email', peso = $peso, altura = $altura,
                  IMC = $IMC, talla = '$talla', tipoUsuarioID = 2, tipoSubscripcionID = $tipoSubscripcionID, fotoURL = '',
                  vencimiento = '$vencimiento', ultimoAcceso = NOW(), clienteDesde = NOW()";
        $userID = APIDatabase::insert($query);

        if($FMD != null)
        {
            $usuario = new self($userID);
            $usuario->setFMD($FMD);
        }
        return $password;
    }

    public function setFMD($FMD)
    {
        echo "tres";
        $query = "INSERT INTO bio_fmd (usuarioID, FMD) VALUES($this->usuarioID, '$FMD')
            ON DUPLICATE KEY
            UPDATE usuarioID = $this->usuarioID, FMD = '$FMD'";
        return APIDatabase::update($query);
    }

    public static function getFMD()
    {
        $query = "SELECT * FROM bio_fmd";
        return APIDatabase::select($query);
    }
}