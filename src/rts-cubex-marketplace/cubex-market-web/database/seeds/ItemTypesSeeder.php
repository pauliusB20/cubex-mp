<?php

use Illuminate\Database\Seeder;

class ItemTypesSeeder extends Seeder
{
    /**
     * Run the database seeds.
     *
     * @return void
     */
    public function run()
    {
        DB::table('item_type')->insert([
            'item_type_name' => 'Building'
        ]);
        DB::table('item_type')->insert([
            'item_type_name' => 'Collector'
        ]);
        DB::table('item_type')->insert([
            'item_type_name' => 'Troop'
        ]);
        DB::table('item_type')->insert([
            'item_type_name' => 'Worker'
        ]);
    }
}
