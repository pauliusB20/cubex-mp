<?php

/** @var \Illuminate\Database\Eloquent\Factory $factory */

use App\item_type;
use Faker\Generator as Faker;
use Illuminate\Support\Str;


$item_type_name = "";

$factory->define(item_type::class, function (Faker $faker) {
    $item_type_name = Str::random(4)."type";
    return [
        'item_type_name' => $item_type_name
    ];
});


