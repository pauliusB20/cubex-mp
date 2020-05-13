<?php

namespace App;

use Illuminate\Database\Eloquent\Model;

class item_classification extends Model
{
    protected $table = 'item_classification';

    public $timestamps = false;

    protected $fillable = [
       'item_id',
       'classification_name',
       'item_type_id'
    ];
}
