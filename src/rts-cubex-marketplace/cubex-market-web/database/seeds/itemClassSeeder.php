<?php

use Illuminate\Database\Seeder;

class itemClassSeeder extends Seeder
{
    /**
     * Run the database seeds.
     *
     * @return void
     */
    public function run()
    {
        factory(App\item_classification::class, 10)->create();
    }
}
