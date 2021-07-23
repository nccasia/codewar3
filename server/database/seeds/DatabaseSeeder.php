<?php

use Illuminate\Database\Seeder;

class DatabaseSeeder extends Seeder
{
    /**
     * Seed the application's database.
     *
     * @return void
     */
    public function run()
    {

        $this->call(RoleTableSeeder::class);
        $this->call(BrandsSeeder::class);
        $this->call(BussinessUnitsSeeder::class);
//        $this->call(TagsSeeder::class);
        $this->call(UsersSeeder::class);
        $this->call(LanguageSeeder::class);
        $this->call(CitySeeder::class);
        $this->call(StateSeeder::class);
//        $this->call(OrganizationsSeeder::class);
//        $this->call(ContactSeeder::class);
    }
}
