<?php

use Illuminate\Database\Migrations\Migration;
use Illuminate\Database\Schema\Blueprint;
use Illuminate\Support\Facades\Schema;

class Nemtransactionsfroitems extends Migration
{
    /**
     * Run the migrations.
     *
     * @return void
     */
    public function up()
    {
        Schema::create('nemtransactionsforitems', function (Blueprint $table) {
            $table->bigIncrements('id');
            $table->timestamps();
            $table->unsignedBigInteger('user_id')->references('id')->on('users')->onDelete('cascade');
            $table->string('type_of_purchasing_or_selling_offer'); // either inventory(fee) or market item(buying) or resource item(buying)
            $table->unsignedBigInteger('id_of_purchasing_or_selling_offer'); // inv id, market id, resource id
            $table->string('recipient_address');
            $table->string('namespace_name');
            $table->float('amount');
            $table->string('message')->nullable();
            $table->string('status');
        });
    }

    /**
     * Reverse the migrations.
     *
     * @return void
     */
    public function down()
    {
        Schema::dropIfExists('nemtransactionsforitems');
    }
}
