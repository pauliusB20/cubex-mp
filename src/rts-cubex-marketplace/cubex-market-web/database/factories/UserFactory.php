<?php

/** @var \Illuminate\Database\Eloquent\Factory $factory */

use App\User;
use Faker\Generator as Faker;
use Illuminate\Support\Str;

$factory->define(\App\User::class, function (Faker $faker) {
    $pass = Str::random(5);

    return [
            'nickname'=> Str::random(10),
            'email'=> Str::random(8).'@gmail.com',
            'password'=> bcrypt($pass),
            'status_in_web'=>"offline",
            'role'=>"player",
            'reg_date'=>date("Y-m-d", mt_rand(strtotime("10 September 2000"), strtotime("22 July 2010"))),
            // 'wallet_address'=> "",
            // 'private_key'=> Str::random(10),
            // 'public_key'=> Str::random(10)

    ];
});
