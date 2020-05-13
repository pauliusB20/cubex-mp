<?php

namespace App\Http\Controllers;

use Illuminate\Http\Request;
use DB;
use Illuminate\Support\Facades\Auth;
use Illuminate\Support\Facades\Validator;
use View;
use GuzzleHttp\Exception\GuzzleException;
use GuzzleHttp\Client;

class marketItemsRetrieve extends AdminBaseController // inherits the base constructor in order to share variable
{
  public function __construct()
  {

      parent::__construct();

  }
    public function market_items()
    {
      if (Auth::check()){
      $id = Auth::id(); // currently logged in user id 
      $market_item_info = DB::select("SELECT i.id AS itemID, u.nickname, p.from_user_id AS userID, y.id, y.transaction_items_id, y.price, y.time_end, i.item_name, i.level, z.classification_name, x.item_type_name 
      FROM market_item AS y, users AS u, transactions_items AS p, inventory AS k, item AS i, item_classification AS z, item_type AS x  
      WHERE p.id = y.transaction_items_id AND p.inventory_id = k.id AND k.item_id = i.id AND i.id = z.item_id AND z.item_type_id = x.id AND p.from_user_id = u.id 
      ORDER BY y.time_end ASC");
      $item_characteristics = DB::select(" SELECT c.id, x.characteristics_name, z.value 
      FROM item_characteristics AS z, characteristics AS x, item AS c 
      WHERE c.id = z.item_id AND z.characteristics_id = x.id");
      return view('market_Items', compact('market_item_info','item_characteristics'));
      }
      else{
        $id = Auth::id(); //currently logged in user id 
        $market_item_info = DB::select("SELECT i.id AS itemID, u.nickname, p.from_user_id AS userID, y.id, y.transaction_items_id, y.price, y.time_end, i.item_name, i.level, z.classification_name, x.item_type_name 
        FROM market_item AS y, users AS u, transactions_items AS p, inventory AS k, item AS i, item_classification AS z, item_type AS x  
        WHERE p.id = y.transaction_items_id AND p.inventory_id = k.id AND k.item_id = i.id AND i.id = z.item_id AND z.item_type_id = x.id AND p.from_user_id = u.id 
        ORDER BY y.time_end ASC");
        $item_characteristics = DB::select("SELECT c.id, x.characteristics_name, z.value 
        FROM item_characteristics AS z, characteristics AS x, item AS c 
        WHERE c.id = z.item_id AND z.characteristics_id = x.id");
        return view('market_Items_guest',compact('market_item_info','item_characteristics'));
      }
    }
    public function resource_market_item(){
      if (Auth::check()){
        $id = Auth::id(); //currently logged in user id
        $market_resources_items_info = DB::select("SELECT u.nickname, market_item.id, market_item.price, market_item.time_end, market_item.amount_to_sell, trans.res_type, trans.from_user_id AS userID, market_item.transactions_resources_id
        FROM users AS u, transactions_resources AS trans, market_credits_energon_item AS market_item
        WHERE u.id = trans.from_user_id AND trans.id = market_item.transactions_resources_id
        ORDER BY market_item.time_end ASC");
        return view('market_resources', compact('market_resources_items_info'));
      }
      else{
        $id = Auth::id(); //currently logged in user id
        $market_resources_items_info = DB::select("SELECT u.nickname, market_item.id, market_item.price, market_item.time_end, market_item.amount_to_sell, trans.res_type, trans.from_user_id AS userID, market_item.transactions_resources_id
        FROM users AS u, transactions_resources AS trans, market_credits_energon_item AS market_item
        WHERE u.id = trans.from_user_id AND trans.id = market_item.transactions_resources_id
        ORDER BY market_item.time_end ASC");
        return view('market_resources_guest', compact('market_resources_items_info'));
      }
    }
    public function addItemToMarket(Request $request)
    { 
      $id = Auth::id(); // user posting item id
      $hours = $request->hours;
      $minutes = $request->minutes;
      $price = $request->price;
      $inventory_id = $request->inventory_ID;
      $nem_transaction = DB::table('nemtransactionsforitems')->where('type_of_purchasing_or_selling_offer','=','inventory(fee)')->where('id_of_purchasing_or_selling_offer','=',$inventory_id)->latest('created_at')->first();   // recognize seller of current item id
      $nem_transaction_status = $nem_transaction->status;
      // $nem_transaction_status = 'confirmed';
      if (Auth::check() && ($nem_transaction_status == "pending" || $nem_transaction_status == "confirmed")) {
          /*before this everything will be checked if ransaction successed by responce from nem server*/
          $seller_id = DB::table('inventory')->where('id','=', $inventory_id)->value('user_id');    // recognizing seller
          DB::transaction(function () use ($hours, $minutes, $price, $inventory_id, $seller_id)
            {
              \App\transactions_items::create([
                'from_user_id' => $seller_id, 
                'to_user_id' => '1', // admin user default
                'inventory_id' => $inventory_id, 
                'type_of_transaction' => 'market'
              ]);
              $startTime = now();
              $startTimeLTU = date('Y-m-d H:i:s',strtotime('+3 hour',strtotime($startTime)));
              $enddate = strtotime($startTimeLTU) + ($hours*60 + $minutes)*60;
              $end_Date_final = date('Y-m-d H:i:s',$enddate);
              \App\market_items::create([
                'transaction_items_id'=> (DB::table('transactions_items')->max('id')),
                'price' => $price,
                'time_start' =>  $startTimeLTU,
                'time_end'=> $end_Date_final
              ]);
            });
          DB::table('inventory')->where('id', $inventory_id)->update(['user_id' => 1]);
          DB::table('inventory')->where('id', $inventory_id)->update(['item_status' => 'market']);
          DB::table('nemtransactionsforitems')->where('type_of_purchasing_or_selling_offer','=','inventory(fee)')->
          where('id_of_purchasing_or_selling_offer','=',$inventory_id)->where('status','=',$nem_transaction_status)->update(['status' => 'confirmed/posted']);
          $current_item_owner = DB::table('inventory')->where('id', '=', $inventory_id)->value('user_id');
          if($current_item_owner == 1){
              sleep(6);
              return response()->json(['success'=>'Data stored']);
          }
          else{ // returning money back
            try{ 
              $posting_fee = env('TRANSACTION_FEE');
              //$seller_wallet_address = DB::table('users')->where('id','=', $id)->value('wallet_address');  //take seller's wallet_address
              $transaction = new CBlockForItems();
              $transaction->user_id =  $id; // buyer id
              $transaction->type_of_purchasing_or_selling_offer = 'inventory(fee_back)';
              $transaction->id_of_purchasing_or_selling_offer = $inventory_id;
              $transaction->recipient_address = 'SC2CMZEJCO75B3ZQ4GFAW6NOVXEPVC6RL3F5Z4N7';  // here will be nem(admin) wallet address
              //$transaction->recipient_address = $seller_wallet_address;  // here will be nem(admin) wallet address (new version)
              $transaction->namespace_name = 'cat.currency';
              $transaction->amount = $posting_fee;
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
                  "responseUrl"=> 'http://127.0.0.1:8000/api/send-retur-fee-transaction-response',
                  //"responseUrl"=> 'http://' . env('APP_HOST') . ':' . env('APP_PORT') . '/api/send-transaction-response',
                 ],
            ]
            );
              sleep(20);
              return response()->json(['error' => 'Failed to make item transaction, money have been returned back']); 
          }
          catch(\GuzzleHttp\Exception\ConnectException $e){
              return response()->json(['error' => 'Failed to make item transaction, something went wrong with nem transaction, money will be returned soon']); 
          }
        }
      }
      else{
        return response()->json(['error' => 'Failed to make item transaction, because you do not have enough Cubecoins to pay for fee']); 
      }
  }
  public function getEnergonBalance(){
     // user resources balance
    $id = Auth::id(); // current user id
    $energonEarned = DB::table('transactions_resources')->where('to_user_id', '=', $id)->where('res_type','=', 'energon')->sum('amount');
    $energonSpent = DB::table('transactions_resources')->where('from_user_id', '=', $id)->where('res_type','=', 'energon')->sum('amount');
    $energonBalance = $energonEarned - $energonSpent;
    if($energonBalance >= 0){
      return response()->json(['success'=>$energonBalance]);
    }
    else{
    return response()->json(['error' =>$energonBalance]); 
    }
  }
  public function getCreditsBalance(){
    $id = Auth::id(); // current user id
    $creditsEarned = DB::table('transactions_resources')->where('to_user_id', '=', $id)->where('res_type','=', 'credits')->sum('amount');
    $creditsSpent = DB::table('transactions_resources')->where('from_user_id', '=', $id)->where('res_type','=', 'credits')->sum('amount');
    $creditsBalance = $creditsEarned - $creditsSpent;
    if($creditsBalance >= 0){
      return response()->json(['success'=>$creditsBalance]);
    }
    else{
    return response()->json(['error' =>$creditsBalance]); 
    }
  }
  public function getMarketEnergonBalance(){
    $totalAmountofEnergonEarned = DB::table('transactions_resources')->where('to_user_id', '=', 2)->where('type_of_transaction','=','market')->where('res_type','=', 'energon')->sum('amount'); // user tansfered his resources to the market
    $totalAmountofEnergonSpent = DB::table('transactions_resources')->where('from_user_id', '=', 2)->where('type_of_transaction','=','market')->where('res_type','=', 'energon')->sum('amount'); // user got resources from the market
    $totalBalanceofEnergon = $totalAmountofEnergonEarned - $totalAmountofEnergonSpent;
    if ($totalBalanceofEnergon >= 0){
      return response()->json(['success'=>$totalBalanceofEnergon]);
    }
    else{
      return response()->json(['error' =>$totalBalanceofEnergon]); 
    }
    
  }
  public function getMarketCreditsBalance(){
    $totalAmountofCreditsEarned = DB::table('transactions_resources')->where('to_user_id', '=', 2)->where('type_of_transaction','=','market')->where('res_type','=', 'credits')->sum('amount'); // user trasfered his resources to the market
    $totalAmountofCreditsSpent = DB::table('transactions_resources')->where('from_user_id', '=', 2)->where('type_of_transaction','=','market')->where('res_type','=', 'credits')->sum('amount'); // user got resources from the market
    $totalBalanceofCredits = $totalAmountofCreditsEarned - $totalAmountofCreditsSpent;
    if ($totalBalanceofCredits >= 0){
      return response()->json(['success'=>$totalBalanceofCredits]);
    }
    else{
      return response()->json(['error' =>$totalBalanceofCredits]); 
    }
  }
}
