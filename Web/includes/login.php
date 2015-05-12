<?php
extract($_POST);
# userVal
# passVal

if(isset($userVal) && isset($passVal))
{
    $usuario = CallAPI("GET", "http://api.actina.nop.al/usuarios/login");
    if($usuario)
    {
        session_start();
        $_SESSION['email']          = $usuario->email;
        $_SESSION['password']       = $usuario->password;
        $_SESSION['tipoUsuarioID']  = $usuario->tipoUsuarioID;
        header('Location: ../index.php');
    }
    else
    {
        header('Location: ../login.html?error=1');
    }
}
else
{
    header('Location: ../login.html?error=1');
}

function CallAPI($method, $url, $data = false)
{
    global $userVal;
    global $passVal;

    $curl = curl_init();

    switch ($method)
    {
        case "POST":
            curl_setopt($curl, CURLOPT_POST, 1);

            if ($data)
                curl_setopt($curl, CURLOPT_POSTFIELDS, $data);
            break;
        case "PUT":
            curl_setopt($curl, CURLOPT_PUT, 1);
            break;
        default:
            if ($data)
                /** @noinspection PhpParamsInspection */
                $url = sprintf("%s?%s", $url, http_build_query($data));
    }

    // Optional Authentication:
    curl_setopt($curl, CURLOPT_HTTPAUTH, CURLAUTH_BASIC);
    curl_setopt($curl, CURLOPT_USERPWD, $userVal.":".$passVal);

    curl_setopt($curl, CURLOPT_URL, $url);
    curl_setopt($curl, CURLOPT_RETURNTRANSFER, 1);

    $result = curl_exec($curl);

    curl_close($curl);

    return json_decode($result);
}