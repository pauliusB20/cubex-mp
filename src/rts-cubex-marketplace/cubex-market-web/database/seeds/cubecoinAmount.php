<?php

use Illuminate\Database\Seeder;

class cubecoinAmount extends Seeder
{
    /**
     * Run the database seeds.
     *
     * @return void
     */
    public function run()
    {
        DB::table('cubecoin_amount')->insert([
            'from_user_id' => '1',
            'to_user_id' => '15', // has to be 100 user
            'amount'=>'100'
        ]);
        DB::table('cubecoin_amount')->insert([
            'from_user_id' => '1',
            'to_user_id' => '15', // has to be 100 user
            'amount'=>'100'
        ]);
        DB::table('cubecoin_amount')->insert([
            'from_user_id' => '1',
            'to_user_id' => '15', // has to be 100 user
            'amount'=>'100'
        ]);
        DB::table('cubecoin_amount')->insert([
            'from_user_id' => '1',
            'to_user_id' => '15', // has to be 100 user
            'amount'=>'100'
        ]);
        DB::table('cubecoin_amount')->insert([
            'from_user_id' => '1',
            'to_user_id' => '15', // has to be 100 user
            'amount'=>'100'
        ]);
        DB::table('cubecoin_amount')->insert([
            'from_user_id' => '1',
            'to_user_id' => '15', // has to be 100 user
            'amount'=>'100'
        ]);
        DB::table('cubecoin_amount')->insert([
            'from_user_id' => '1',
            'to_user_id' => '15', // has to be 100 user
            'amount'=>'100'
        ]);
    }
}
