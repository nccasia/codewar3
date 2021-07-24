<?php

namespace Database\Seeders;

use Illuminate\Database\Seeder;
use Illuminate\Support\Facades\DB;

class RewardsSeeder extends Seeder
{
    /**
     * Run the database seeds.
     *
     * @return void
     */
    public function run()
    {
        $datas = [
            [
                'id' => 1,
                'name' => '1 ly nước mía 10k full topping',
                'image' => ''
            ],
            [
                'id' => 2,
                'name' => 'còn cái nịt',
                'image' => ''
            ],
            [
                'id' => 3,
                'name' => '250k mời trà sữa cả công ty',
                'image' => ''
            ],
            [
                'id' => 4,
                'name' => '200k luôn nè',
                'image' => ''
            ],
            [
                'id' => 5,
                'name' => '1 chuyến du lịch cầu Rồng 100k',
                'image' => ''
            ],
            [
                'id' => 6,
                'name' => '1 ly trà sửa 35k',
                'image' => ''
            ],
            [
                'id' => 7,
                'name' => '80k hiuhiu',
                'image' => ''
            ],
        ];

        foreach ($datas as $data) {
            DB::table('rewards')->insert($data);
        }
    }
}
