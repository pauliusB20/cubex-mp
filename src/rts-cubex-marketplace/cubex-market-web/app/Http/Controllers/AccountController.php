<?php

namespace App\Http\Controllers;

use Illuminate\Http\Request;
use Illuminate\Support\Facades\Storage;
use Symfony\Component\Console\Input\Input;
use File;
use DB;
use Illuminate\Support\Facades\Auth;
use App\transactions_items;

class AccountController extends AdminBaseController // inherits the base constructor in order to share variable
{
    public function __construct()
    {

      parent::__construct();

    }
    public function inventory()
    {
        if (Auth::check()) {
            $id = Auth::id();
            $inv = DB::select("SELECT DISTINCT u.id, u.nickname, inv.item_id, inv.item_status, inv.id AS inventory_id, uitem.item_name, uitem.item_code, uitem.level, inv.description,
                itemcl.classification_name, itemt.item_type_name
                FROM users AS u, inventory AS inv, item AS uitem, item_classification AS itemcl, item_characteristics AS ichar, characteristics, item_type AS itemt
                WHERE u.id = $id AND inv.user_id = u.id AND inv.item_id = uitem.id AND itemcl.item_id=uitem.id AND uitem.id=ichar.item_id AND ichar.characteristics_id=characteristics.id
                AND itemcl.item_type_id=itemt.id AND inv.item_status='web'");
            $item_characteristics = DB::select("SELECT c.id, x.characteristics_name, z.value FROM item_characteristics AS z, characteristics AS x, item AS c WHERE c.id = z.item_id AND z.characteristics_id = x.id");
            return view('inventory', compact('inv', 'item_characteristics'));
        } else {
            return redirect()->route('login')->with('status', 'Please login first!');
        }
    }
    public function retrieveInventory(Request $request)
    {
        $inv = DB::select('select * from inventory');
        foreach ($inv as $i) {
            echo $i->id . ' ' . $i->user_id . ' ' . $i->item_id . ' ' . $i->description . ' ' . $i->hash_code . '<br/>';
        }
    }
    public function account()
    {
        if (Auth::check()) {
            $id = Auth::id();
            $acc = DB::select("SELECT id, nickname, role, wallet_address, email, status_in_web, private_key, public_key FROM users WHERE id=$id");
            return view('account')->with('acc', $acc);

        } else {
            return redirect()->route('login')->with('status', 'Please login first!');
        }
    }
    public function transfer(Request $request)
    {
        $inventoryID = $request->inventoryID;
        $itemId = $request->itemID;
        $currentUserID = Auth::id();
        DB::table('inventory')->where('user_id','=',$currentUserID)->where('item_id','=',$itemId)->update(['item_status' => 'game']);
        if ($inventoryID > 0) {
            // making transacion from web to admin
            $itemsTransaction1 = new transactions_items();
            $itemsTransaction1->from_user_id = $currentUserID;
            $itemsTransaction1->to_user_id = '1';
            $itemsTransaction1->inventory_id = $inventoryID;
            $itemsTransaction1->type_of_transaction = 'from_web_to_admin';
            $itemsTransaction1->save();
            // making transaction from admin to game
            $itemsTransaction2 = new transactions_items();
            $itemsTransaction2->from_user_id = '1';
            $itemsTransaction2->to_user_id = $currentUserID;
            $itemsTransaction2->inventory_id = $inventoryID;
            $itemsTransaction2->type_of_transaction = 'from_admin_to_game';
            $itemsTransaction2->save();
            $insertedId = $itemsTransaction2->id;  
            if($insertedId > 0){
              sleep(3);
              return response()->json(['success'=>'Item was sent to game, check your game account']);   
            }
            else{
              return response()->json(['error' =>'Item was not sent to the game']); 
            } 
        }
        else{
            return response()->json(['error' =>'Item was not sent to the game']); 
        } 
    }
}
