<?php
/**
 * Created by PhpStorm.
 * User: Yozki
 * Date: 15/04/2015
 * Time: 07:09 PM
 */

session_start();
if(!isset($_SESSION['email'])) header('Location: /login.html');