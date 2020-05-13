<?php

namespace App;

use Illuminate\Database\Eloquent\Model;
use Kyslik\ColumnSortable\Sortable;
class transactions_resources extends Model
{
    use Sortable;
    public $timestamps = false;
    protected $table = "transactions_resources";
    protected $fillable = [
        'from_user_id', 'to_user_id', 'amount', 'res_type', 'type_of_transaction'
    ];
    public $sortable = ['id','amount'];
}
