<?php

use Illuminate\Database\Seeder;

class market_items extends Seeder
{
    /**
     * Run the database seeds.
     *
     * @return void
     */
    public function run()
    {
        factory(App\market_items::class, 7)->create();
    }
}
