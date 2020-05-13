<?php

namespace App;

use Illuminate\Database\Eloquent\Model;

class item_type extends Model
{
    protected $table = 'item_type';

    public $timestamps = false;
    protected $fillable = [
        'item_type_name'
    ];
}
