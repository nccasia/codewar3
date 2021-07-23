<?php

namespace Database\Seeders;

use App\Models\Rewards;
use Illuminate\Database\Seeder;
use RewardsSeeder;

class DatabaseSeeder extends Seeder
{
    /**
     * Seed the application's database.
     *
     * @return void
     */
    public function run()
    {
        \App\Models\Rewards::factory(10)->create();
        \App\Models\Employees::factory(10)->create();
        \App\Models\EnglishGame::factory(10)->create();
        \App\Models\Opentalk::factory(10)->create();
        // $this->call(RewardsSeeder::class);
        // $this->call(RewardsSeeder::class);
    }
}
