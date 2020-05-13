<?php

namespace App\Http\Controllers;

use Illuminate\Http\Request;
use Illuminate\Support\Facades\Auth;
use DB;
use GuzzleHttp\Exception\GuzzleException;
use GuzzleHttp\Client;
use App\transactions_resources;

class buyItemFromMarket extends AdminBaseController // inherits the base constructor in order to share variable
{
  public function __construct()
  {

      parent::__construct();

  }
    public function buyItemsFromMarket(Request $request)
    {
      $id = Auth::id(); // buyer id 
      $price = $request->price;
      $itemTransactionID = $request->transactions_ItemsID;
      $marketItemID = $request->market_ItemID;
      $nem_transaction = DB::table('nemtransactionsforitems')->where('type_of_purchasing_or_selling_offer','=','market_item(buying)')->where('id_of_purchasing_or_selling_offer','=',$marketItemID)->latest('created_at')->first();   // recognize seller of current item id
      $nem_transaction_status = $nem_transaction->status;
     // $transaction_state = "confirmed";
      if (Auth::check() && ($nem_transaction_status == "pending" || $nem_transaction_status == "confirmed")) {
      /*before this everything will be checked if ransaction successed by responce from nem server*/
      $seller_id = DB::table('transactions_items')->where('id','=', $itemTransactionID)->value('from_user_id');   // recognize seller of current item id
      $inventory_id = DB::table('transactions_items')->where('id','=', $itemTransactionID)->value('inventory_id'); // recognize inventory_id of selling item
      DB::transaction(function () use ($marketItemID, $itemTransactionID, $id, $seller_id, $inventory_id, $price)
        {
            /*we are making two transactions of items first form admin o seller and then from seller to buyer and one transaction of cubecoin */
          \App\transactions_items::create([
            'from_user_id' => '1', // admin user reseive item back
            'to_user_id' => $seller_id, 
            'inventory_id' => $inventory_id, 
            'type_of_transaction' => 'market'
          ]);
          \App\transactions_items::create([
            'from_user_id' => $seller_id, 
            'to_user_id' => $id, 
            'inventory_id' => $inventory_id, 
            'type_of_transaction' => 'market'
          ]);
          \App\CubeCoin_Amount::create([
            'from_user_id' => $id, 
            'to_user_id' => $seller_id, 
            'amount' => $price
          ]);
        });
      DB::table('inventory')->where('id', $inventory_id)->update(['user_id' => $id]); // updating inventory table
      DB::table('inventory')->where('id', $inventory_id)->update(['item_status' => 'web']); // updating item status
      DB::table('nemtransactionsforitems')->where('type_of_purchasing_or_selling_offer','=', 'market_item(buying)')->
      where('id_of_purchasing_or_selling_offer','=',$marketItemID)->where('status','=',$nem_transaction_status)->update(['status' => 'confirmed/bought']);
      DB::table('market_item')->where('id', '=', $marketItemID)->delete(); // deleting record from market items table
      $current_item_owner = DB::table('inventory')->where('id', '=', $inventory_id)->value('user_id');
          if($current_item_owner == $id){
              sleep(6);
              return response()->json(['success'=>'Data stored']);
          }
          else{ // return spent money back
            try{ 
              //$seller_wallet_address = DB::table('users')->where('id','=', $id)->value('wallet_address');  //take seller's wallet_address
              $transaction = new CBlockForItems();
              $transaction->user_id =  $id; // buyer id
              $transaction->type_of_purchasing_or_selling_offer = 'inventory(fee_back)';
              $transaction->id_of_purchasing_or_selling_offer = $inventory_id;
              $transaction->recipient_address = 'SC2CMZEJCO75B3ZQ4GFAW6NOVXEPVC6RL3F5Z4N7';  // here will be nem(admin) wallet address
              //$transaction->recipient_address = $seller_wallet_address;  // here will be nem(admin) wallet address (new version)
              $transaction->namespace_name = 'cat.currency';
              $transaction->amount = $price;
              $transaction->message = 'sending coins';
              $transaction->status = 'pending';
              $transaction->save();
           
            $client = new \GuzzleHttp\Client(); // if something went wrong with item posting return money back
            $response = $client->request(
            'PUT',
            'http://' . env('NEM_HOST') . ':' . env('NEM_PORT') . '/api/transaction/sendAndRespond',
            [
                 'json' => [
                  "senderPrivateKey"=> env('ADMIN_PRIVATE_KEY2'), // here will be nem(admin) private key (new version)
                  "recipientAddress"=> "SC2CMZEJCO75B3ZQ4GFAW6NOVXEPVC6RL3F5Z4N7", // here will be seller's wallet address
                 // "recipientAddress"=> $seller_wallet_address, // here will be seller's wallet address(new version)
                  "namespaceName"=> "cat.currency",
                  "amount"=> $posting_fee,
                  "message"=> "message",
                  "transactionId" => $transaction->id,
                  "responseUrl"=> 'http://127.0.0.1:8000/api/send-retur-transaction-response',
                  //"responseUrl"=> 'http://' . env('APP_HOST') . ':' . env('APP_PORT') . '/api/send-transaction-response',
                 ],
            ]
            );
              sleep(20);
              return response()->json(['error' => 'Failed to make item transaction, money have been returned back']); 
          }
          catch(\GuzzleHttp\Exception\ConnectException $e){
              return response()->json(['error' => 'Failed to make item transaction, something went wrong with nem transaction, money will be returned back soon']); 
          }
        }
     }
     else{
        return response()->json(['error' => 'Failed to make item transaction, because you do not have enough Cubecoins']); 
     }
    }
    public function buyResourceItem(Request $request){
      $id = Auth::id(); // buyer id 
      $price = $request->price;
      $market_Item_ID = $request->market_resource_ID;
      $transacions_item_ID = $request->transactions_resources_ID;
      $amount = DB::table('transactions_resources')->where('id','=', $transacions_item_ID)->value('amount');   // amount of resources to return
      $resource_type = DB::table('transactions_resources')->where('id','=', $transacions_item_ID)->value('res_type');   // amount of resources to return
      $nem_transaction = DB::table('nemtransactionsforitems')->where('type_of_purchasing_or_selling_offer','=','market_resource_item(buying)')->where('id_of_purchasing_or_selling_offer','=',$market_Item_ID)->latest('created_at')->first();   
      $nem_transaction_status = $nem_transaction->status;
     // $transaction_state = "confirmed";
      if (Auth::check() && ($nem_transaction_status == "pending" || $nem_transaction_status == "confirmed")) {
      /*before this everything will be checked if ransaction successed by responce from nem server*/
      $seller_id = DB::table('transactions_resources')->where('id','=', $transacions_item_ID)->value('from_user_id');   // recognize seller of current item id
      // making transacition from admin to user
      $resourceTransaction1 = new transactions_resources();
      $resourceTransaction1->from_user_id = '2';
      $resourceTransaction1->to_user_id = $seller_id;
      $resourceTransaction1->amount = $amount;
      $resourceTransaction1->res_type = $resource_type;
      $resourceTransaction1->type_of_transaction = 'market';
      $resourceTransaction1->save();
      // making transaction from seller to buyer
      $resourceTransaction2 = new transactions_resources();
      $resourceTransaction2->from_user_id = $seller_id;
      $resourceTransaction2->to_user_id = $id;
      $resourceTransaction2->amount = $amount;
      $resourceTransaction2->res_type = $resource_type;
      $resourceTransaction2->type_of_transaction = 'market';
      $resourceTransaction2->save();
      DB::transaction(function () use ($id, $seller_id, $price)
        {
          \App\CubeCoin_Amount::create([
            'from_user_id' => $id, 
            'to_user_id' => $seller_id, 
            'amount' => $price
          ]);
        });
      DB::table('nemtransactionsforitems')->where('type_of_purchasing_or_selling_offer','=', 'market_resource_item(buying)')->
      where('id_of_purchasing_or_selling_offer','=',$market_Item_ID)->where('status','=',$nem_transaction_status)->update(['status' => 'confirmed/bought']);
      DB::table('market_credits_energon_item')->where('id', '=', $market_Item_ID)->delete(); // deleting record from market items table
        sleep(6);
        return response()->json(['success'=>'Data stored']);
      }
      else{
        return response()->json(['error' => 'Failed to make item transaction, because you do not have enough Cubecoins']); 
      }
    }
    public function deleteItem (Request $request){
      $market_Item_ID = $request->market_ItemID;
      $transacions_item_ID = $request->transactions_ItemsID;
      $seller_id = DB::table('transactions_items')->where('id','=', $transacions_item_ID)->value('from_user_id');   // recognize seller of current item id
      $inventory_id = DB::table('transactions_items')->where('id','=', $transacions_item_ID)->value('inventory_id'); // recognize inventory_id of selling item
      DB::transaction(function () use ($market_Item_ID, $transacions_item_ID, $seller_id, $inventory_id)
        {
            /*we are making one transacion between admin and seller to get the item back*/
          \App\transactions_items::create([
            'from_user_id' => '1', // admin user reseive item back
            'to_user_id' => $seller_id, 
            'inventory_id' => $inventory_id, 
            'type_of_transaction' => 'market'
          ]);
        });
      DB::table('inventory')->where('id', $inventory_id)->update(['user_id' => $seller_id]); // updating inventory table
      DB::table('inventory')->where('id', $inventory_id)->update(['item_status' => 'web']); // updating item status
      DB::table('market_item')->where('id', '=', $market_Item_ID)->delete(); // deleting record from market items table
      /*delete item and check market item table*/ 
      $marketITEMIDDeleted = DB::table('market_item')->where('id', '=', $market_Item_ID)->first();
      if($marketITEMIDDeleted === null){
        return response()->json(['success'=>'Item deleted from the market']);
      }
      else{
        return response()->json(['error' => 'Failed to delete']);
      }
    }
    public function deleteResourceItem (Request $request){
      $market_Item_ID = $request->market_resource_ID;
      $transacions_item_ID = $request->transactions_resources_ID;
      $seller_id = DB::table('transactions_resources')->where('id','=', $transacions_item_ID)->value('from_user_id');   // recognize seller of current item id
      $amount = DB::table('transactions_resources')->where('id','=', $transacions_item_ID)->value('amount');   // amount of resources to return
      $resource_type = DB::table('transactions_resources')->where('id','=', $transacions_item_ID)->value('res_type');   // amount of resources to return
      DB::transaction(function () use ($market_Item_ID, $transacions_item_ID, $seller_id, $amount, $resource_type)
        {
            /*we are making one transacion between admin and seller to get the item back*/
          \App\transactions_resources::create([
            'from_user_id' => '1', // admin user reseive item back
            'to_user_id' => $seller_id, 
            'res_type' => $resource_type,
            'amount' => $amount, 
            'type_of_transaction' => 'market'
          ]);
        });
      DB::table('market_credits_energon_item')->where('id', '=', $market_Item_ID)->delete(); // deleting record from market items table
      /*delete item and check market item table*/ 
      $marketITEMIDDeleted = DB::table('market_credits_energon_item')->where('id', '=', $market_Item_ID)->first();
      if($marketITEMIDDeleted === null){
        return response()->json(['success'=>'Item deleted from the market']);
      }
      else{
        return response()->json(['error' => 'Failed to delete']);
      }
    }
}

