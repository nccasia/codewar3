<?php
namespace App\Helpers;

use Illuminate\Database\ConnectionInterface;
use Illuminate\Support\Facades\DB;

class OBuilder
{
    protected static $provider = 'mysql_old';
    protected static $instance = null;

    private function __construct()
    {
    }

    public static function getConnection(): ConnectionInterface {
        if (OBuilder::$instance) {
            return OBuilder::$instance;
        }

        OBuilder::$instance = DB::connection(OBuilder::$provider);
        return OBuilder::$instance;
    }
}
