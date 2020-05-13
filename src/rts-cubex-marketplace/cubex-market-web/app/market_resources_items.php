<?php

namespace App;

use Illuminate\Database\Eloquent\Model;

class market_resources_items extends Model
{
    protected $table = "market_credits_energon_item";
    public $timestamps = false;
    protected $fillable = ['id', 'transactions_resources_id', 'price', 'time_start' , 'time_end', 'amount_to_sell']; 
}
