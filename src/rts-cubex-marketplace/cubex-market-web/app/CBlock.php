<?php

namespace App;

use Illuminate\Database\Eloquent\Model;

class CBlock extends Model
{
   protected $table = "nemtransactions_for_users";
   protected $fillable = ['id', 'user_id', 'recipient_address' , 'namespace_name', 'amount', 'message', 'status'];
}
