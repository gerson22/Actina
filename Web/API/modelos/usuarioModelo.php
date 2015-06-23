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

    public static function inscribir($nombre, $email, $peso, $altura, $talla, $tipoSubscripcionID, $FMD, $sexo)
    {
        $password = "temp_pass";
        $IMC = 30;
        $vencimiento = null;
        $query = "INSERT INTO usuario SET password = '$password', nombre = '$nombre', email = '$email', peso = $peso, altura = $altura,
                  IMC = $IMC, talla = '$talla', tipoUsuarioID = 2, tipoSubscripcionID = $tipoSubscripcionID, fotoURL = '',
                  vencimiento = '$vencimiento', ultimoAcceso = NOW(), clienteDesde = NOW(), sexo = '$sexo'";
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

    public static function getStats()
    {
        $query = "SELECT
            (SELECT COUNT(*) FROM usuario WHERE vencimiento >= NOW()) AS activos,
            (SELECT COUNT(*) FROM usuario WHERE vencimiento < NOW()) AS inactivos,
            (SELECT COUNT(*) FROM usuario WHERE tipoSubscripcionID = 1) AS mensuales,
            (SELECT COUNT(*) FROM usuario WHERE tipoSubscripcionID = 2) AS semanales,
            (SELECT COUNT(*) FROM usuario WHERE (clienteDesde between DATE_FORMAT(NOW() ,'%Y-%m-01') AND NOW())) AS nuevosMes";
        return APIDatabase::select($query);
    }

    public static function getAsistencia()
    {
        $query = "SELECT
            (SELECT COUNT(*) FROM asistencia WHERE YEAR(TIMESTAMP) = YEAR(NOW()) AND MONTH(timestamp) = 1) AS enero,
            (SELECT COUNT(*) FROM asistencia WHERE YEAR(TIMESTAMP) = YEAR(NOW()) AND MONTH(timestamp) = 2) AS febrero,
            (SELECT COUNT(*) FROM asistencia WHERE YEAR(TIMESTAMP) = YEAR(NOW()) AND MONTH(timestamp) = 3) AS marzo,
            (SELECT COUNT(*) FROM asistencia WHERE YEAR(TIMESTAMP) = YEAR(NOW()) AND MONTH(timestamp) = 4) AS abril,
            (SELECT COUNT(*) FROM asistencia WHERE YEAR(TIMESTAMP) = YEAR(NOW()) AND MONTH(timestamp) = 5) AS mayo,
            (SELECT COUNT(*) FROM asistencia WHERE YEAR(TIMESTAMP) = YEAR(NOW()) AND MONTH(timestamp) = 6) AS junio,
            (SELECT COUNT(*) FROM asistencia WHERE YEAR(TIMESTAMP) = YEAR(NOW()) AND MONTH(timestamp) = 7) AS julio,
            (SELECT COUNT(*) FROM asistencia WHERE YEAR(TIMESTAMP) = YEAR(NOW()) AND MONTH(timestamp) = 8) AS agosto,
            (SELECT COUNT(*) FROM asistencia WHERE YEAR(TIMESTAMP) = YEAR(NOW()) AND MONTH(timestamp) = 9) AS septiembre,
            (SELECT COUNT(*) FROM asistencia WHERE YEAR(TIMESTAMP) = YEAR(NOW()) AND MONTH(timestamp) = 10) AS octubre,
            (SELECT COUNT(*) FROM asistencia WHERE YEAR(TIMESTAMP) = YEAR(NOW()) AND MONTH(timestamp) = 11) AS noviembre,
            (SELECT COUNT(*) FROM asistencia WHERE YEAR(TIMESTAMP) = YEAR(NOW()) AND MONTH(timestamp) = 12) AS diciembre";
        return APIDatabase::select($query);
    }

    public static function getInscripciones()
    {
        $query = "SELECT
            (SELECT COUNT(*) FROM usuario WHERE sexo = 'H' AND YEAR(clienteDesde) = YEAR(NOW()) AND MONTH(clienteDesde) = 1) AS enero,
            (SELECT COUNT(*) FROM usuario WHERE sexo = 'H' AND YEAR(clienteDesde) = YEAR(NOW()) AND MONTH(clienteDesde) = 2) AS febrero,
            (SELECT COUNT(*) FROM usuario WHERE sexo = 'H' AND YEAR(clienteDesde) = YEAR(NOW()) AND MONTH(clienteDesde) = 3) AS marzo,
            (SELECT COUNT(*) FROM usuario WHERE sexo = 'H' AND YEAR(clienteDesde) = YEAR(NOW()) AND MONTH(clienteDesde) = 4) AS abril,
            (SELECT COUNT(*) FROM usuario WHERE sexo = 'H' AND YEAR(clienteDesde) = YEAR(NOW()) AND MONTH(clienteDesde) = 5) AS mayo,
            (SELECT COUNT(*) FROM usuario WHERE sexo = 'H' AND YEAR(clienteDesde) = YEAR(NOW()) AND MONTH(clienteDesde) = 6) AS junio,
            (SELECT COUNT(*) FROM usuario WHERE sexo = 'H' AND YEAR(clienteDesde) = YEAR(NOW()) AND MONTH(clienteDesde) = 7) AS julio,
            (SELECT COUNT(*) FROM usuario WHERE sexo = 'H' AND YEAR(clienteDesde) = YEAR(NOW()) AND MONTH(clienteDesde) = 8) AS agosto,
            (SELECT COUNT(*) FROM usuario WHERE sexo = 'H' AND YEAR(clienteDesde) = YEAR(NOW()) AND MONTH(clienteDesde) = 9) AS septiembre,
            (SELECT COUNT(*) FROM usuario WHERE sexo = 'H' AND YEAR(clienteDesde) = YEAR(NOW()) AND MONTH(clienteDesde) = 10) AS octubre,
            (SELECT COUNT(*) FROM usuario WHERE sexo = 'H' AND YEAR(clienteDesde) = YEAR(NOW()) AND MONTH(clienteDesde) = 11) AS noviembre,
            (SELECT COUNT(*) FROM usuario WHERE sexo = 'H' AND YEAR(clienteDesde) = YEAR(NOW()) AND MONTH(clienteDesde) = 12) AS diciembre";
        $hombres = APIDatabase::select($query);

        $query = "SELECT
            (SELECT COUNT(*) FROM usuario WHERE sexo = 'M' AND YEAR(clienteDesde) = YEAR(NOW()) AND MONTH(clienteDesde) = 1) AS enero,
            (SELECT COUNT(*) FROM usuario WHERE sexo = 'M' AND YEAR(clienteDesde) = YEAR(NOW()) AND MONTH(clienteDesde) = 2) AS febrero,
            (SELECT COUNT(*) FROM usuario WHERE sexo = 'M' AND YEAR(clienteDesde) = YEAR(NOW()) AND MONTH(clienteDesde) = 3) AS marzo,
            (SELECT COUNT(*) FROM usuario WHERE sexo = 'M' AND YEAR(clienteDesde) = YEAR(NOW()) AND MONTH(clienteDesde) = 4) AS abril,
            (SELECT COUNT(*) FROM usuario WHERE sexo = 'M' AND YEAR(clienteDesde) = YEAR(NOW()) AND MONTH(clienteDesde) = 5) AS mayo,
            (SELECT COUNT(*) FROM usuario WHERE sexo = 'M' AND YEAR(clienteDesde) = YEAR(NOW()) AND MONTH(clienteDesde) = 6) AS junio,
            (SELECT COUNT(*) FROM usuario WHERE sexo = 'M' AND YEAR(clienteDesde) = YEAR(NOW()) AND MONTH(clienteDesde) = 7) AS julio,
            (SELECT COUNT(*) FROM usuario WHERE sexo = 'M' AND YEAR(clienteDesde) = YEAR(NOW()) AND MONTH(clienteDesde) = 8) AS agosto,
            (SELECT COUNT(*) FROM usuario WHERE sexo = 'M' AND YEAR(clienteDesde) = YEAR(NOW()) AND MONTH(clienteDesde) = 9) AS septiembre,
            (SELECT COUNT(*) FROM usuario WHERE sexo = 'M' AND YEAR(clienteDesde) = YEAR(NOW()) AND MONTH(clienteDesde) = 10) AS octubre,
            (SELECT COUNT(*) FROM usuario WHERE sexo = 'M' AND YEAR(clienteDesde) = YEAR(NOW()) AND MONTH(clienteDesde) = 11) AS noviembre,
            (SELECT COUNT(*) FROM usuario WHERE sexo = 'M' AND YEAR(clienteDesde) = YEAR(NOW()) AND MONTH(clienteDesde) = 12) AS diciembre";
        $mujeres = APIDatabase::select($query);

        $respuesta = array();
        $respuesta["hombres"] = array_values($hombres[0]);
        $respuesta["mujeres"] = array_values($mujeres[0]);
        return $respuesta;
    }
}