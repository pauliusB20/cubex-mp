<?php

use Illuminate\Database\Migrations\Migration;
use Illuminate\Database\Schema\Blueprint;
use Illuminate\Support\Facades\Schema;

class CreateUnityDevicesTable extends Migration
{
    /**
     * Run the migrations.
     *
     * @return void
     */
    public function up()
    {
        Schema::create('unity_devices', function (Blueprint $table) {
            $table->bigIncrements('id');
            $table->string('username');
            $table->string('password');
            $table->string('email');
            $table->string('game_status');
            $table->string('reg_date');
        });

	 Schema::create('usergame_res', function (Blueprint $table) {
            $table->bigIncrements('id');
            $table->unsignedBigInteger('u_id');
            $table->bigInteger('eamount');
            $table->bigInteger('camount');
            $table->foreign('u_id')->references('id')->on('unity_devices')->onDelete('cascade');
        });
       Schema::create('user_items', function (Blueprint $table) {
            $table->bigIncrements('id');
            $table->unsignedBigInteger('u_id');
            $table->string('item_name');
            $table->string('item_type');
            $table->bigInteger('level');
            $table->string('item_code');
            $table->foreign('u_id')->references('id')->on('unity_devices')->onDelete('cascade');
        });
    }

    /**
     * Reverse the migrations.
     *
     * @return void
     */
    public function down()
    {
        Schema::dropIfExists('unity_devices');
    }
}
