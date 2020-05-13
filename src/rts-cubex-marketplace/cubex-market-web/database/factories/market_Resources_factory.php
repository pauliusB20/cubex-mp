<?php

/** @var \Illuminate\Database\Eloquent\Factory $factory */

use App\market_resources_items;
use Faker\Generator as Faker;

$factory->define(market_resources_items::class, function (Faker $faker) {
    $transaction_ids = App\transactions_resources::where('type_of_transaction','=','market')->pluck('id')->all();
    $startingDate = $faker->dateTimeBetween('this week', '+6 days');
    return [
        'transactions_resources_id' => $faker->unique()->randomElement($transaction_ids),
        'price' => rand(0, 100) / 10,
        'time_start'=> $startingDate,
        'time_end'=> $faker->dateTimeBetween($startingDate, '+6 days'),
        'amount_to_sell'=> $faker->numberBetween($min = 100, $max = 1000) // 833,
    ];    
});
