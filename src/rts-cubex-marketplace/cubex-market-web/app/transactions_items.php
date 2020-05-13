<?php

namespace App;

use Illuminate\Database\Eloquent\Model;

class transactions_items extends Model
{
    public $timestamps = false;
    protected $table = "transactions_items";
    protected $fillable = [
        'id','from_user_id', 'to_user_id', 'inventory_id', 'type_of_transaction'
    ];
}
