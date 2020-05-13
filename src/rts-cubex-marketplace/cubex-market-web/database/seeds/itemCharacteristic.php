<?php

use Illuminate\Database\Seeder;

class itemCharacteristic extends Seeder
{
    /**
     * Run the database seeds.
     *
     * @return void
     */
    public function run()
    {
        factory(App\itemCharacteristics::class, 10)->create();
    }
}
