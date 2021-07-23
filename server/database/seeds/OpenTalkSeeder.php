        
<?php

use App\Models\Brand;
use App\Models\EnglishGame;
use App\Models\OpenTalk;
use App\Models\Rewards;

class OpenTalkSeeder extends BaseSeeder
{
    /**
     * Run the database seeds.
     *
     * @return void
     */
    public function run()
    {
       factory(OpenTalk::class, 5)->create();
    }
}
