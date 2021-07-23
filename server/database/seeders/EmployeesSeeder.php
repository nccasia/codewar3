<?php

namespace Database\Seeders;


use Illuminate\Support\Facades\DB;
use Illuminate\Database\Seeder;

class OpentalkSeeder extends Seeder
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
                'id'=>1,
                'fullName' => 'Nhan',
                'image' => 'koco',
                'funnyImage' => 'koco',
                'phone' => 'koco'
            ],
            [
                'id'=>2,
                'fullName' => 'Oanh',
                'image' => 'koco',
                'funnyImage' => 'koco',
                'phone' => 'koco'
            ],
            [
                'id'=>3,
                'fullName' => 'Manh',
                'image' => 'koco',
                'funnyImage' => 'koco',
                'phone' => 'koco'
            ]
        ];

        foreach ($datas as $data) {
            DB::table('employees')->insert($data);
        }
    }
}
