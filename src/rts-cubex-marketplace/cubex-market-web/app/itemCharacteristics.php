<?php

namespace App;

use Illuminate\Database\Eloquent\Model;

class itemCharacteristics extends Model
{
    protected $table = "item_characteristics";
    public $timestamps = false;
    protected $fillable = [
       'item_id','characteristics_id','value'
    ];
}
