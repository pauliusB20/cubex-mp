<?php

use Illuminate\Database\Seeder;

class CubexSeed extends Seeder
{
    /**
     * Run the database seeds.
     *
     * @return void
     */
    public function run()
    {
        factory(App\User::class, 10)->create();
        factory(App\item::class, 10)->create();

    }
}
