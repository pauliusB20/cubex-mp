<?php

namespace App;

use Illuminate\Database\Eloquent\Model;

class CubeCoin_Amount extends Model
{
    public $timestamps = false;
    protected $table = "cubecoin_amount";
    protected $fillable = [
        'from_user_id', 'to_user_id', 'amount'
    ];
}
