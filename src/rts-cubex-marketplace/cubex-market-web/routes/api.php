<?php

use Illuminate\Http\Request;

/*
|--------------------------------------------------------------------------
| API Routes
|--------------------------------------------------------------------------
|
| Here is where you can register API routes for your application. These
| routes are loaded by the RouteServiceProvider within a group which
| is assigned the "api" middleware group. Enjoy building your API!
|
*/

//Route::middleware('auth:api')->get('/user', function (Request $request) {
//    return $request->user();
//});

Route::post('/send-transaction-response', 'nemBlockChainServer@sendTransactionResponce')->name('api.send-transaction-response');
Route::post('/send-fee-transaction-response', 'nemBlockChainServer@sendFeeTransactionResponce')->name('api.send-fee-transaction-response');
Route::post('/sendStartingAmount', 'sendStartingAmount@sendcubes')->name('api.sendStartingAmount');
Route::post('/send-retur-fee-transaction-response', 'nemBlockChainServer@sendReturnFeeTransactionResponce')->name('api.send-retur-fee-transaction-response');
Route::post('/send-retur-transaction-response', 'nemBlockChainServer@sendReturnTransactionResponce')->name('api.send-retur-transaction-response');