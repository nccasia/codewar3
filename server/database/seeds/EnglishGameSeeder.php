        
<?php

use App\Models\Brand;
use App\Models\EnglishGame;
use App\Models\Rewards;

class EnglishGameSeeder extends BaseSeeder
{
    /**
     * Run the database seeds.
     *
     * @return void
     */
    public function run()
    {
       factory(EnglishGame::class, 5)->create();
    }
}
