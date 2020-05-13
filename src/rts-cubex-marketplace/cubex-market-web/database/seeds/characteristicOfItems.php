<?php

use Illuminate\Database\Seeder;

class characteristicOfItems extends Seeder
{
    /**
     * Run the database seeds.
     *
     * @return void
     */
    public function run()
    {
        DB::table('characteristics')->insert([
            'characteristics_name' => 'HP'
        ]);
        DB::table('characteristics')->insert([
            'characteristics_name' => 'DMG'
        ]);
        DB::table('characteristics')->insert([
            'characteristics_name' => 'SHD'
        ]);
    }
}
