<?php

use Illuminate\Database\Seeder;

class item_transactions extends Seeder
{
    /**
     * Run the database seeds.
     *
     * @return void
     */
    public function run()
    {
        DB::table('transactions_items')->insert([
            'from_user_id' => '10',
            'to_user_id' => '1', // has to be market user
            'inventory_id' => '1',
            'type_of_transaction'=>'market'
        ]);
        DB::table('transactions_items')->insert([
            'from_user_id' => '6',
            'to_user_id' => '1', // has to be market user
            'inventory_id' => '2',
            'type_of_transaction'=>'market'
        ]);
        DB::table('transactions_items')->insert([
            'from_user_id' => '7',
            'to_user_id' => '1', // has to be market user
            'inventory_id' => '3',
            'type_of_transaction'=>'market'
        ]);
        DB::table('transactions_items')->insert([
            'from_user_id' => '5',
            'to_user_id' => '1', // has to be market user
            'inventory_id' => '4',
            'type_of_transaction'=>'market'
        ]);
        DB::table('transactions_items')->insert([
            'from_user_id' => '4',
            'to_user_id' => '1', // has to be market user
            'inventory_id' => '5',
            'type_of_transaction'=>'market'
        ]);
        DB::table('transactions_items')->insert([
            'from_user_id' => '12',
            'to_user_id' => '1', // has to be market user
            'inventory_id' => '6',
            'type_of_transaction'=>'market'
        ]);
        DB::table('transactions_items')->insert([
            'from_user_id' => '6',
            'to_user_id' => '1', // has to be market user
            'inventory_id' => '7',
            'type_of_transaction'=>'market'
        ]);
    }
}
