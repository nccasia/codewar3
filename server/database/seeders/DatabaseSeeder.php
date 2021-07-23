<?php

namespace Database\Seeders;

use Illuminate\Database\Seeder;

class DatabaseSeeder extends Seeder
{

    public function run()
    {
         $this->call([RewardsSeeder::class]);
         $this->call([EmployeesSeeder::class]);
         $this->call([EnglishGameSeeder::class]);
         $this->call([OpentalkSeeder::class]);

    }
}
