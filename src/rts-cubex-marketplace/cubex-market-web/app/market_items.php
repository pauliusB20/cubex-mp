<?php

namespace App;

use Illuminate\Database\Eloquent\Model;

class market_items extends Model
{
    protected $table = "market_item";
    public $timestamps = false;
    protected $fillable = ['id', 'transaction_items_id', 'price', 'time_start' , 'time_end']; 
   // protected $fillable = ['id', 'transaction_items_id', 'price', 'offer_Hours_left' , 'offer_Minutes_left']; 
}
