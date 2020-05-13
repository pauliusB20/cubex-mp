<?php

namespace App;

use Illuminate\Database\Eloquent\Model;

class login_history extends Model
{
    protected $table = 'login_history';

    public $timestamps = false;
    protected $fillable = [
       'id', 'user_id', 'login_time', 'logout_time', 'ip', 'place'
    ];
}
