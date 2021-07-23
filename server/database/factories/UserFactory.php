<?php

/** @var \Illuminate\Database\Eloquent\Factory $factory */
use App\Models\User;
use App\Models\BussinessUnit;
use App\Models\Role;
use Faker\Generator as Faker;
use Illuminate\Support\Str;

/*
|--------------------------------------------------------------------------
| Model Factories
|--------------------------------------------------------------------------
|
| This directory should contain each of the model factory definitions for
| your application. Factories provide a convenient way to generate new
| model instances for testing / seeding your application's database.
|
*/

$factory->define(User::class, function (Faker $faker) {
    return [
        'user_name' => $faker->unique()->userName,
        'first_name' => $faker->firstName,
        'second_name' => $faker->lastName,
        'linked_in_url' => "'https://' . $faker->word . '.com'",
        'business_unit_id' => function () {
            return BussinessUnit::inRandomOrder()->first()->id;
        },
        'email' => $faker->unique()->freeEmail,
        'email_verified_at' => now(),
        'password' => '123456a@', // password
        'primary_role' => function () {
            return Role::where('name', 'basic')->first()->role_id;
        },
        'phone_number' => $faker->phoneNumber,
        'gender'=> $faker->randomElement(['Male', 'Female']),
        'remember_token' => Str::random(10),
    ];
});
