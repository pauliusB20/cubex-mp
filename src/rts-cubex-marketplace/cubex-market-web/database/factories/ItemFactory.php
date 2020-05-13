<?php

/** @var \Illuminate\Database\Eloquent\Factory $factory */

use App\item;
use Faker\Generator as Faker;
use Illuminate\Support\Str;

$factory->define(item::class, function (Faker $faker) {
    return [
        'item_name' => Str::random(7).'_name',
        'item_code' => Str::random(5).'-'.rand(1, 5),
        'level' => rand(1, 5)
    ];
});