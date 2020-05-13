<?php

namespace App;

use Illuminate\Database\Eloquent\Model;

class market_users extends Model
{
    protected $fillable = [
        'id',
        'uname',
        'email',
        'psw',
        'folder_name'
    ];
}
