<?php

use Illuminate\Database\Seeder;

class market_Resources extends Seeder
{
    /**
     * Run the database seeds.
     *
     * @return void
     */
    public function run()
    {
        factory(App\market_resources_items::class, 4)->create();
    }
}
