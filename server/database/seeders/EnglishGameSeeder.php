<?php

namespace Database\Seeders;

use Illuminate\Database\Seeder;
use Illuminate\Support\Facades\DB;

class EnglishGameSeeder extends Seeder
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
                'name' => 'Nhan',
                'voted' => 0.6,
                'status' => true,
                'userID' => 1,
            ],
            [
                'id' => 2,
                'name' => 'Oanh',
                'voted' => 0.2,
                'status' => false,
                'userID' => 2,
            ],
            [
                'id' => 3,
                'name' => 'Manh',
                'voted' => 0.2,
                'status' => false,
                'userID' => 3,
            ]
        ];

        foreach ($datas as $data) {
            DB::table('english_games')->insert($data);
        }
    }
}
