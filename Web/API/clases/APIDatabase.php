<?php

class APIDatabase
{
    private static $db_host        = "127.0.0.1";
    private static $db_user        = "prevenla_gym";
    private static $db_password    = "BrosW150!";
    private static $db_database    = "prevenla_gym";

    static function select($query)
    {
        $mysqli = new mysqli(self::$db_host, self::$db_user, self::$db_password, self::$db_database);
        echo $mysqli->connect_error;
        $result = $mysqli->query($query);
        if($result)
        {
            $rows = self::resultToArray($result);
            $result->free();
            $mysqli->close();
            return $rows;
        }
        return null;
    }

    static function insert($query)
    {
        $mysqli = new mysqli(self::$db_host, self::$db_user, self::$db_password, self::$db_database);
        $mysqli->multi_query($query);
        $insert_id = $mysqli->insert_id;
        $mysqli->close();
        return $insert_id;
    }

    static function update($query)
    {
        $mysqli = new mysqli(self::$db_host, self::$db_user, self::$db_password, self::$db_database);
        $success = $mysqli->query($query);
        $mysqli->close();
        return $success;
    }

    static function resultToArray($result)
    {
        $rows = array();
        /** @noinspection PhpUndefinedMethodInspection */
        while($row = $result->fetch_assoc())
        {
            $rows[] = $row;
        }
        return $rows;
    }
}