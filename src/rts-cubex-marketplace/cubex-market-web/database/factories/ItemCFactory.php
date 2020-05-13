<?php

/** @var \Illuminate\Database\Eloquent\Factory $factory */

use App\item_classification;
use Faker\Generator as Faker;

$factory->define(item_classification::class, function (Faker $faker) {
    $item_types = App\item_type::pluck('id')->all();
    $item_ids = App\item::pluck('id')->all();
    return [
        'classification_name' => Str::random(6).'_class',
        'item_id' => $faker-> unique() -> randomElement($item_ids),
        'item_type_id' =>  $faker->randomElement($item_types)
       
    ];
});
