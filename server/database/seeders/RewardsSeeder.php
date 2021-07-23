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
            [   'id' => 1,
                'name' => 'Tà tữa',
                'image' => ''
            ],
            [
                'id' => 2,
                'name' => 'Gà',
                'image' => ''
            ],
            [
                'id' => 3,
                'name' => 'Vịt',
                'image' => ''
            ]
        ];

        foreach ($datas as $data) {
            DB::table('rewards')->insert($data);
        }

    }
}
