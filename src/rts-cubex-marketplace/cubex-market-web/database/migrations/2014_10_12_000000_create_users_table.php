<?php

use Illuminate\Database\Migrations\Migration;
use Illuminate\Database\Schema\Blueprint;
use Illuminate\Support\Facades\Schema;
use Illuminate\Support\Str;
class CreateUsersTable extends Migration
{
    /**
     * Run the migrations.
     *
     * @return void
     */
    public function up()
    {
        Schema::create('users', function (Blueprint $table) {
            $table->increments('id');
            $table->string('nickname')->unique();
            $table->string('email')->unique();
            // $table->timestamp('email_verified_at')->nullable();
            $table->string('password');
            $table->string('status_in_web')->default('offline');
            $table->string('status_in_game')->default('offline');
            $table->string('role')->default('player');
            $table->string('reg_date')->default(DB::raw('CURRENT_TIMESTAMP'));
            $table->string('wallet_address')->nullable();
            $table->string('private_key')->nullable();
            $table->string('public_key')->nullable();
            $table->rememberToken();
            $table->timestamps();
        });

        $adminUser1 =  array(       //Creating admin user
            'nickname' => 'adminp',
            'email' => 'adminb@gmail.com',
            'password' => bcrypt('adminp21'),
            'game_status' => 'offline',
            'role' => 'admin',
            'reg_date' => 'not required'

        );
        \App\User::create($adminUser1);
        $adminUser2 =  array(
            'nickname' => 'adminpp',
            'email' => 'adminpp@gmail.com',
            'password' => bcrypt('paul21'),
            'game_status' => 'offline',
            'role' => 'admin',
            'reg_date' => 'not required'

        );
        \App\User::create($adminUser2);
        $adminUser3 =  array(
            'nickname' => 'admind',
            'email' => 'admind@gmail.com',
            'password' => bcrypt('domas21'),
            'game_status' => 'offline',
            'role' => 'admin',
            'reg_date' => 'not required'

        );
        \App\User::create($adminUser3);
        Schema::create('cubecoin_amount', function (Blueprint $table) {
            $table->increments('id');
            $table->bigInteger('from_user_id')->references('id')->on('users')->onDelete('cascade');
            $table->bigInteger('to_user_id')->references('id')->on('users')->onDelete('cascade');
            $table->bigInteger('amount');
            // $table->rememberToken();
            // $table->timestamps();
        });
        Schema::create('login_history', function (Blueprint $table) {
            $table->increments('id');
            $table->bigInteger('user_id')->references('id')->on('users')->onDelete('cascade');
            $table->string('login_time');
            $table->string('logout_time');
            $table->string('ip');
            $table->string('place');
            // $table->rememberToken();
            // $table->timestamps();
        });
        Schema::create('transactions_resources', function (Blueprint $table) {
            $table->increments('id');
            $table->bigInteger('from_user_id')->references('id')->on('users')->onDelete('cascade');
            $table->bigInteger('to_user_id')->references('id')->on('users')->onDelete('cascade');
            $table->string('res_type');
            $table->bigInteger('amount');
            $table->string('type_of_transaction');
            // $table->rememberToken();
            // $table->timestamps();
        });
        Schema::create('market_credits_energon_item', function (Blueprint $table) {
            $table->increments('id');
            $table->bigInteger('transactions_resources_id')->references('id')->on('transactions_resources')->onDelete('cascade');
            $table->bigInteger('price');
            $table->string('time_start');
            $table->string('time_end');
            $table->bigInteger('amount_to_sell');
        });
        Schema::create('market_item', function (Blueprint $table) {
            $table->increments('id');
            $table->bigInteger('transaction_items_id')->references('id')->on('transactions_items')->onDelete('cascade');
            $table->bigInteger('price');
            $table->string('time_start');
            $table->string('time_end');
        });
        Schema::create('inventory', function (Blueprint $table) {
            $table->increments('id');
            $table->bigInteger('user_id')->references('id')->on('users')->onDelete('cascade');
            $table->bigInteger('item_id')->unique()->references('item_id')->on('item')->onDelete('cascade');
            $table->string('description');
            $table->string('hash_code')->nullable();
            $table->string('item_status');
        });
        Schema::create('item', function (Blueprint $table) {
            $table->increments('id');
            $table->string('item_name');
            $table->string('item_code');
            $table->bigInteger('level');
        });
        Schema::create('item_classification', function (Blueprint $table) {
            $table->increments('id');
            $table->bigInteger('item_id')->references('id')->on('item')->onDelete('cascade');
            $table->string('classification_name');
            $table->bigInteger('item_type_id')->references('id')->on('item_type')->onDelete('cascade');
        });
        Schema::create('item_type', function (Blueprint $table) {
            $table->increments('id');
            $table->string('item_type_name');
        });
        Schema::create('item_characteristics', function (Blueprint $table) {
            $table->increments('id');
            $table->bigInteger('item_id')->references('id')->on('item')->onDelete('cascade');
            $table->string('characteristics_id')->references('id')->on('characteristics')->onDelete('cascade');
            $table->bigInteger('value');
        });

        Schema::create('characteristics', function (Blueprint $table) {
            $table->increments('id');
            $table->string('characteristics_name')->unique();
        });

    }
    /**
     * Reverse the migrations.
     *
     * @return void
     */
    public function down()
    {
        Schema::dropIfExists('users');
        Schema::dropIfExists('cubecoin_amount');
        Schema::dropIfExists('login_history');
        Schema::dropIfExists('transactions_resources');
        Schema::dropIfExists('market_credits_energon_item');
        Schema::dropIfExists('market_item');
        Schema::dropIfExists('inventory');
        Schema::dropIfExists('item');
        Schema::dropIfExists('item_classification');
        Schema::dropIfExists('item_type');
        Schema::dropIfExists('item_characteristics');
        Schema::dropIfExists('characteristics');
    }
}
