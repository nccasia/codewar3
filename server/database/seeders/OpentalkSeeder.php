<?php

namespace Database\Seeders;

use Illuminate\Database\Seeder;
use Illuminate\Support\Facades\DB;

class OpentalkSeeder extends Seeder
{
    /**
     * Run the database seeds.
     *
     * @return void
     */
    public function run()
    {
        //
        $datas = [
            [
                'id' => 1,
                'topicName' => 'Ẩm thực',
                'rewardID' => 1,
                'userID' => 1,
            ],
            [
                'id' => 2,
                'topicName' => 'Phim ảnh',
                'rewardID' => 2,
                'userID' => 2,
            ],
            [
                'id' => 3,
                'topicName' => 'Động vật',
                'rewardID' => 3,
                'userID' => 3,
            ]
        ];

        foreach ($datas as $data) {
            DB::table('opentalks')->insert($data);
        }
    }
}
