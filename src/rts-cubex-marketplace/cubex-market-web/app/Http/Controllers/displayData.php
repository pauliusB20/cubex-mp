<?php

namespace App\Http\Controllers;

use Illuminate\Http\Request;
use DB;
use Redirect;
use Illuminate\Support\Facades\Auth;

class displayData extends AdminBaseController
{
    //Nepamirsti useriu online status,turi būti skiriama, kuris online būna, ar in game ar in web
    public function __construct()
    {

        parent::__construct();
    }
    public function loadDashBoard()
    {
        if(Auth::user()) {
            if (Auth::user()->role === 'admin') {
                return view('adminPage');
            } else {
                return back()->with('status', 'You need to be an admin to enter!');
            }
        }
        else {
            return redirect()->route('login')->with('status', 'Please login first!');
        }
    }
    public function loadUDBPage()
    {
        if(Auth::user()) {
            if (Auth::user()->role === 'admin') {
                return view('adminPageUDB');
            } else {
                return back()->with('status', 'You need to be an admin to enter!');
            }
        }
        else {
            return redirect()->route('login')->with('status', 'Please login first!');
        }
    }
    public function loadTMonitor()
    {
        if(Auth::user()) {
            if (Auth::user()->role === 'admin') {
                return view('admTMonitor');
            } else {
                return back()->with('status', 'You need to be an admin to enter!');
            }
        }
        else {
            return redirect()->route('login')->with('status', 'Please login first!');
        }
    }
    public function loadTItemsMonitor()
    {
        if(Auth::user()) {
            if (Auth::user()->role === 'admin') {
                return view('admTitMonitor');
            } else {
                return back()->with('status', 'You need to be an admin to enter!');
            }
        }
        else {
            return redirect()->route('login')->with('status', 'Please login first!');
        }
    }
    public function loadHistoryMonitor()
    {
        if(Auth::user()) {
            if (Auth::user()->role === 'admin') {
                return view('admHMonitor');
            } else {
                return back()->with('status', 'You need to be an admin to enter!');
            }
        }
        else {
            return redirect()->route('login')->with('status', 'Please login first!');
        }
    }
    // public function countAllRegisteredUsers()
    // {
    //     $users = DB::select('select * from users');
    //     $userTable = DB::select('select * from users');
    //     $userTableOnline = DB::select("select * from users where game_status like 'online'");
    //     $totalUserOnlineCount = count($userTableOnline);
    //     $totalUserRecordCount = count($userTable);

    //     return view('adminPage', compact('totalUserRecordCount', 'users', 'totalUserOnlineCount'));
    // }
    //NOTE:: add this as a seperate function
    public function transactionToGame()
    {
        //change
        $currentUserID = 19;
        $itemInInventory = "";
        foreach(\App\inventory::all() as $invItem)
        {
            if ($invItem->item_status == "game" && $invItem->user_id == $currentUserID)
            {
                $itemInInventory = $invItem;
            }
        }
        $itemInInventory = DB::select("SELECT * FROM inventory WHERE item_status = 'game' AND user_id = ".$currentUserID."");
        if ($itemInInventory)
        {
            $adminUser = \App\User::where('role', '=','admin')->where('id', '=', 1)->value('id');
            $inv_id = $itemInInventory->inventory_id;
                 DB::transaction(function () use ($adminUser, $currentUserID, $inv_id)
                {
                    $sentToAdmin = \App\transactions_items::create([
                        "from_user_id" => $currentUserID,
                        "to_user_id" =>$adminUser,
                        "inventory_id" =>$inv_id,
                        "type_of_transaction" =>"from_web_to_admin"
                        ]);
                    $sentToGame =\App\transactions_items::create([
                        "from_user_id" => $adminUser,
                        "to_user_id" => $currentUserID,
                        "inventory_id" =>$inv_id,
                        "type_of_transaction" =>"from_admin_to_game"
                        ]);
                    if ((!$sentToAdmin || !$sentToGame) || (!$sentToAdmin && !$sentToGame)) {
                        throw new \Exception('Item was not sent to game! ERROR');
                    }
                    else
                    {
                        $wasTransactionsucessful = true;
                    }
                });
        }

    }
    public function retrieveUserItemInfo(){
        /*
        if find items with game status to game, begin transactions
        Once transaction is done, game can retrive items
        */

            $_userBelongingItems = DB::select("select tritems.to_user_id as user_id, useritems.id as item_id, useritems.item_name, useritems.item_code, inv.item_status, class.classification_name, itype.item_type_name, useritems.level from transactions_items as tritems, item as useritems, item_classification as class, item_type as itype, inventory as inv where ((tritems.inventory_id = inv.id and tritems.type_of_transaction = 'from_admin_to_game') and (inv.item_id = useritems.id and useritems.id = class.item_id)) and (class.item_type_id = itype.id);");

            foreach($_userBelongingItems as $useritems)
            {
                echo $useritems->user_id.' '.
                    $useritems->item_id.' '.
                    $useritems->item_name.' '.
                    $useritems->item_code.' '.
                    $useritems->item_status.' '.
                    $useritems->classification_name.' '.
                    $useritems->item_type_name.' '.
                    $useritems->level
                    .'<br/>';
            }

    }
    //For item charakteristic retrival
    public function retrieveGameItemCharacteristics(){
        $gameItemCharacteristics = DB::select("select inventory.item_id, characteristics.characteristics_name, item_characteristics.value from transactions_items inner join inventory on transactions_items.inventory_id = inventory.id inner join item_characteristics on inventory.item_id = item_characteristics.item_id inner join characteristics on item_characteristics.characteristics_id = characteristics.id where transactions_items.type_of_transaction ='from_admin_to_game';");
        foreach ($gameItemCharacteristics as $gameItemCh)
        {
            echo $gameItemCh->item_id.'-'.$gameItemCh->characteristics_name.'-'.$gameItemCh->value.'<br/>';
        }
    }
    public function retrieveUserDataBaseTable()
    {
        if (Auth::user()->role === 'admin') {
            //Selecting users from the table
            $users = DB::select('select * from users');

            return view('adminPageUDB', compact('users'));
        } else {
            return redirect()->route('login')->with('status', 'Please login first!');
        }
    }
    public function retrieveUResDataBaseTable($id)
    {
        if (Auth::user()->role === 'admin') {
            $userres = DB::table('transactions_resources')->where('id', $id)->get();

            return view('adminPageUDB')->with(compact('userres'));
        } else {
            return redirect()->route('login')->with('status', 'Please login first!');
        }
    }
    public function destroy($id)
    {
        DB::delete('delete from unity_devices where id = ?', [$id]);
        echo "Record deleted successfully.";
    }
    public function destroySpecUser(Request $request)
    {

            // if (Auth::user()->role === 'admin') {
                if ($request->ajax())
                {
                    $id = $request->input('id');

                    // DB::delete('delete from unity_devices where id = ?',[$id]);
                        if (Auth::id() == $id)
                            return "notrefresh";

                        $doesUsesExist = \App\User::where('id', '=', $id)->where('role', '!=', 'admin')->select('id')->get();
                        if (empty($doesUsesExist))
                            return "notrefresh";
                        // FIx delete
                        \App\transactions_items::where('from_user_id', '=', $id)->delete();
                        \App\transactions_items::where('to_user_id', '=', $id)->delete();
                        \App\transactions_resources::where('from_user_id', '=', $id)->delete();
                        \App\transactions_resources::where('to_user_id', '=', $id)->delete();
                        \App\inventory::where('user_id', '=', $id)->delete();
                        \App\item::where('id', '=', \App\inventory::where('user_id', '=', $id)->value('item_id'))->delete();
                        \App\login_history::where('user_id', '=', $id)->delete();
                        $stateUser = \App\User::where('id', '=', $id)->where('role', '!=', 'admin')->delete();
                        if($stateUser)
                         return "refresh";
                        else
                         return "notrefresh";

                    // echo "Record deleted successfully.";
                    //Redirect to specific page by success

                }
            // }
            // else {
            //         return redirect()->route('login')->with('status', 'Please login first!');
            // }


    }
}
