<?php

namespace App\Http\Controllers;

use Illuminate\Http\Request;
use GuzzleHttp\Exception\GuzzleException;
use GuzzleHttp\Client;
use App\CBlock;
use App\CBlockForItems;
use DB;
use Illuminate\Support\Facades\Auth;

class nemBlockChainServer extends Controller
{
    public function sendTransactionResponce(Request $request){
        DB::table('nemtransactionsforitems')->where('id', $request->id)->update(['status' =>$request->status]); // updating item status
    }
    public function sendFeeTransactionResponce(Request $request){
        DB::table('nemtransactionsforitems')->where('id', $request->id)->update(['status' =>$request->status]); // updating item status
    }
    public function sendReturnTransactionResponce(Request $request){
        DB::table('nemtransactionsforitems')->where('id', $request->id)->update(['status' =>$request->status]); // updating item status
    }
    public function sendReturnFeeTransactionResponce(Request $request){
        DB::table('nemtransactionsforitems')->where('id', $request->id)->update(['status' =>$request->status]); // updating item status
    }
    public function sendTransaction(Request $request){
    
    try{
        $id = Auth::id(); // buyer id
        $price = $request->price;
        $market_item_id = $request->marketItemID; // taking market item id
        $itemTransactionID = $request->transactions_ItemsID; // in order to know who is seller of particular item
        $seller_id = DB::table('transactions_items')->where('id','=', $itemTransactionID)->value('from_user_id');   // recognize seller of current item id
        //$seller_wallet_address = DB::table('users')->where('id','=', $seller_id)->value('wallet_address');  //take seller's wallet_address
        //$buyer_private_key = DB::table('users')->where('id','=', $id)->value('private_key');  //take buyer's private key
        $transaction = new CBlockForItems();
        $transaction->user_id =  $id; // buyer id
        $transaction->type_of_purchasing_or_selling_offer = 'market_item(buying)';
        $transaction->id_of_purchasing_or_selling_offer = $market_item_id;
        $transaction->recipient_address = 'SC2CMZEJCO75B3ZQ4GFAW6NOVXEPVC6RL3F5Z4N7'; // here will be seller's address
        //$transaction->recipient_address = $seller_wallet_address; // here will be seller's address (new version)
        $transaction->namespace_name = 'cat.currency';
        $transaction->amount = $price;
        $transaction->message = 'sending coins';
        $transaction->status = 'pending';
        $transaction->save();
        $client = new \GuzzleHttp\Client();
        $response = $client->request(
        'PUT',
        'http://' . env('NEM_HOST') . ':' . env('NEM_PORT') . '/api/transaction/sendAndRespond',
        [
             'json' => [
              "senderPrivateKey"=> "B51A4EDA92E3B8D689C8C1579A97C860A4CC8B02A84C874D5DD8B3600C484951", // here will be buyer's private key
             //"senderPrivateKey"=> $buyer_private_key, // here will be buyer's private key(new version)
              "recipientAddress"=> "SC2CMZEJCO75B3ZQ4GFAW6NOVXEPVC6RL3F5Z4N7", // here will be seller's wallet address
             // "recipientAddress"=> $seller_wallet_address, // here will be seller's wallet address(new version)
              "namespaceName"=> "cat.currency",
              "amount"=> $price,
              "message"=> "message",
              "transactionId" => $transaction->id,
              "responseUrl"=> 'http://127.0.0.1:8000/api/send-transaction-response',
              //"responseUrl"=> 'http://' . env('APP_HOST') . ':' . env('APP_PORT') . '/api/send-transaction-response',
             ],
        ]
        );
        sleep(20);
        return response()->json(['success'=>'Transaction completed']);
    }
    catch (\GuzzleHttp\Exception\ConnectException $e){
        return response()->json(['error'=>'Transaction failed']);
    }
    }
    
    public function sendTransactionOfFee(Request $request){
       try{
        $id = Auth::id(); // seller id and buyer at the same time
        $inventory_ID = $request->inventoryID; 
        $posting_fee = env('TRANSACTION_FEE');
        $transaction = new CBlockForItems();
        $transaction->user_id =  $id; // buyer id
        $transaction->type_of_purchasing_or_selling_offer = 'inventory(fee)';
        $transaction->id_of_purchasing_or_selling_offer = $inventory_ID;
        $transaction->recipient_address = env('ADMIN_ADDRESS');  // here will be nem(admin) wallet address (new version)
        $transaction->namespace_name = 'cat.currency';
        $transaction->amount = $posting_fee;
        $transaction->message = 'sending coins';
        $transaction->status = 'pending';
        $transaction->save();
        //$buyer_private_key = DB::table('users')->where('id','=', $id)->value('private_key');  //take buyer's private key
        $client = new \GuzzleHttp\Client();
        $response = $client->request(
        'PUT',
        'http://' . env('NEM_HOST') . ':' . env('NEM_PORT') . '/api/transaction/sendAndRespond',
        [
             'json' => [
              "senderPrivateKey"=> "B51A4EDA92E3B8D689C8C1579A97C860A4CC8B02A84C874D5DD8B3600C484951", // here will be buyer's private key
              //"senderPrivateKey"=> $buyer_private_key, // here will be buyer's private key (new version)
              "recipientAddress"=> env('ADMIN_ADDRESS'), // here will be nem(admin) wallet address (new version)
              "namespaceName"=> "cat.currency",
              "amount"=> $posting_fee,
              "message"=> "message",
              "transactionId" => $transaction->id,
              "responseUrl"=> 'http://127.0.0.1:8000/api/send-fee-transaction-response',
              //"responseUrl"=> 'http://' . env('APP_HOST') . ':' . env('APP_PORT') . '/api/send-fee-transaction-response',
             ],
        ]
        );
        sleep(20);
        return response()->json(['success'=>'Transaction completed']);
       }
       catch(\GuzzleHttp\Exception\ConnectException $e){
          return response()->json(['error'=>'Transaction failed']);
       }
    }
    public function getCubeBalance(Request $request){
    $walletAddress = $request->wallet_address;
    //NEM balance code goes here
    //$id = Auth::id();
    try{
    $client = new \GuzzleHttp\Client();
    //$user_address = DB::table('users')->where('id','=', $id)->value('wallet_address');  //take seller's wallet_address
     // Create a POST request
      $response = $client->request(
          'GET',
          'http://' . env('NEM_HOST') . ':' . env('NEM_PORT') . '/api/account/getBalance',
          [
              'json' => [

                      "address"=> $walletAddress // here will be taken auth user address
              ],
          ]
      );
    $accountBalance = json_decode($response->getBody()); //Add valeu to global variable
    $userMosaic = $accountBalance->account->mosaics;
    if (empty($userMosaic))
      {
        $finalAmount = 0;
      }
    else{
        $finalAmount = $userMosaic[0]->amount;
    }
      return response()->json(['success'=>$finalAmount]);
    }
    catch(\GuzzleHttp\Exception\ConnectException $e){
      return response()->json(['error'=>'Something happened with data retrieving']);
    }
    }
    public function sendTransactionForResource(Request $request){
        try{
            $id = Auth::id(); // buyer id
            $price = $request->price;
            $market_Item_ID = $request->market_resource_ID; // takin market resource id
            $transacions_item_ID = $request->transactions_resources_ID; // taking market resource ransaction id
            $seller_id = DB::table('transactions_resources')->where('id','=', $transacions_item_ID)->value('from_user_id');   // recognize seller of current item id
            //$seller_wallet_address = DB::table('users')->where('id','=', $seller_id)->value('wallet_address');  //take seller's wallet_address
            //$buyer_private_key = DB::table('users')->where('id','=', $id)->value('private_key');  //take buyer's private key
            $transaction = new CBlockForItems();
            $transaction->user_id =  $id; // buyer id
            $transaction->type_of_purchasing_or_selling_offer = 'market_resource_item(buying)';
            $transaction->id_of_purchasing_or_selling_offer = $market_Item_ID;
            $transaction->recipient_address = 'SC2CMZEJCO75B3ZQ4GFAW6NOVXEPVC6RL3F5Z4N7'; // here will be seller's address
            //$transaction->recipient_address = $seller_wallet_address; // here will be seller's address (new version)
            $transaction->namespace_name = 'cat.currency';
            $transaction->amount = $price;
            $transaction->message = 'sending coins';
            $transaction->status = 'pending';
            $transaction->save();
            $client = new \GuzzleHttp\Client();
            $response = $client->request(
            'PUT',
            'http://' . env('NEM_HOST') . ':' . env('NEM_PORT') . '/api/transaction/sendAndRespond',
            [
                 'json' => [
                  "senderPrivateKey"=> "B51A4EDA92E3B8D689C8C1579A97C860A4CC8B02A84C874D5DD8B3600C484951", // here will be buyer's private key
                 //"senderPrivateKey"=> $buyer_private_key, // here will be buyer's private key(new version)
                  "recipientAddress"=> "SC2CMZEJCO75B3ZQ4GFAW6NOVXEPVC6RL3F5Z4N7", // here will be seller's wallet address
                 // "recipientAddress"=> $seller_wallet_address, // here will be seller's wallet address(new version)
                  "namespaceName"=> "cat.currency",
                  "amount"=> $price,
                  "message"=> "message",
                  "transactionId" => $transaction->id,
                  "responseUrl"=> 'http://127.0.0.1:8000/api/send-transaction-response',
                  //"responseUrl"=> 'http://' . env('APP_HOST') . ':' . env('APP_PORT') . '/api/send-transaction-response',
                 ],
            ]
            );
            sleep(20);
            return response()->json(['success'=>'Transaction completed']);
        }
        catch (\GuzzleHttp\Exception\ConnectException $e){
            return response()->json(['error'=>'Transaction failed']);
        }
    }
}