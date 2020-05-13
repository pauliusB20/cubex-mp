<?php

namespace App;

use Illuminate\Database\Eloquent\Model;

class unityDevices extends Model
{
    protected $fillable = [
        'id',
        'username',
        'password',
        'email',
        'game_status',
        'reg_date'
    ];
}
