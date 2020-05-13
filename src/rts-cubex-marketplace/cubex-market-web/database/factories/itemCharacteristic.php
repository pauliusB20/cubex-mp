<?php

/** @var \Illuminate\Database\Eloquent\Factory $factory */

use App\itemCharacteristics;
use Faker\Generator as Faker;

$factory->define(itemCharacteristics::class, function (Faker $faker) {
    $items_id = App\item::pluck('id')->all();
    $item_characteristic_ids = App\characteristics::pluck('id')->all();
    return [
        'item_id' =>$faker->unique()->randomElement($items_id),
        'characteristics_id' =>$faker->randomElement($item_characteristic_ids),
        'value' =>$faker->numberBetween($min = 100, $max = 1000)
    ];
});