<?php

namespace App;

use Illuminate\Database\Eloquent\Model;

class CBlockForItems extends Model
{
    protected $table = "nemtransactionsforitems";
    protected $fillable = [
        'id','user_id', 'type_of_purchasing_or_selling_offer', 'id_of_purchasing_or_selling_offer', 'recipient_address', 'namespace_name','amount', 'message', 'status'
    ];
}
