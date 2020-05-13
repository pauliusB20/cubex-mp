<?php

namespace App;

use Illuminate\Database\Eloquent\Model;

class characteristics extends Model
{
    public $timestamps = false;
    protected $fillable = [
       'characteristics_name'
    ];
}
