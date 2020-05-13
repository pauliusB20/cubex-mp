<?php

use Illuminate\Database\Seeder;

class DatabaseSeeder extends Seeder
{
    /**
     * Seed the application's database.
     *
     * @return void
     */
    public function run()
    {
        $this->call(ItemTypesSeeder::class);
        $this->call(characteristicOfItems::class);
        $this->call(CubexSeed::class);
        $this->call(InventorySeeder::class);
        //$this->call(item_transactions::class);
        //$this->call(market_items::class);
        $this->call(itemCharacteristic::class);
        $this->call(itemClassSeeder::class);
        $this->call(cubecoinAmount::class);
    }
}
