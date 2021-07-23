<?php

use App\Models\Brand;

class EmployeesSeeder extends BaseSeeder
{
    /**
     * Run the database seeds.
     *
     * @return void
     */
    public function run()
    {
        factory(Employees::class)->create([
            'fullName' => 'Admin',
            'phone' => 'Admin',
            'image' => 'Admin',
            'funnyImage' => 'admin',
        ]);
       factory(User::class, 5)->create();
    }
}
