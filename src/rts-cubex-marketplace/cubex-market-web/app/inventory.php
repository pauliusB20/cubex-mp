<?php

namespace App;

use Illuminate\Database\Eloquent\Model;

class inventory extends Model
{
    protected $table = "inventory";
    public $timestamps = false;
    protected $fillable = ['id', 'user_id', 'item_id', 'description' , 'hash_code', 'item_status'];
}
