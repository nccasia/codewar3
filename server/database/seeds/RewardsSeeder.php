<?php

use App\Models\Brand;
use App\Models\Rewards;

class RewardsSeeder extends BaseSeeder
{
    /**
     * Run the database seeds.
     *
     * @return void
     */
    public function run()
    {
       factory(Rewards::class, 5)->create();
    }
}
