<?php

namespace App;

use Illuminate\Database\Eloquent\Model;

class CubeNews extends Model
{
    public $timestamps = false;
    protected $table = "web_news";
    protected $fillable = [
        'user_id', 'news_title', 'news_message', 'posted_news_date'
    ];
}
