<?php

/** @var \Illuminate\Database\Eloquent\Factory $factory */

use App\inventory;
use Faker\Generator as Faker;
use Illuminate\Support\Str;

$factory->define(inventory::class, function (Faker $faker) {
    $users_id = App\User::where('role','=','player')->pluck('id')->all();
    $items_id = App\item::pluck('id')->all();
    $item_status = ['game','web'];
    return [
        'user_id' => $faker->randomElement($users_id),
        'item_id' => $faker-> unique()-> randomElement($items_id),
        'description'=> Str::random(5).'desc',
        'hash_code'=> (Hash::make(Str::random(5), ['rounds' => 12])),
        'item_status' => $faker->randomElement($item_status)
    ];
    
});
