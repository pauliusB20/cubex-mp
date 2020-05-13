<?php

namespace App\Http\Controllers;

use Illuminate\Http\Request;
use Illuminate\Support\Facades\Auth;
use DB;
use View;
use GuzzleHttp\Exception\GuzzleException;
use GuzzleHttp\Client;
use Illuminate\Pagination\Paginator;
use Illuminate\Pagination\LengthAwarePaginator;
class AdminBaseController extends Controller
{
  protected $userBelongingItems;

  //Main admin data

  public function __construct()
  {
    $userResources = \App\transactions_resources::sortable()->paginate(15);
    // Add user belonging items by id user
    $transactionedItems = DB::select("select tr.id, fromusers.nickname as from_user, tousers.nickname as to_user, titems.item_name, tr.type_of_transaction from transactions_items as tr, users as fromusers, users as tousers, item as titems, inventory as inv where (fromusers.id = tr.from_user_id and tousers.id = tr.to_user_id) AND (titems.id = inv.item_id and inv.id = tr.inventory_id)");
    $this->userBelongingItems = DB::select("select item_id, user_id, item_name, item_code from inventory inner join item on inventory.item_id = item.id;");
    $tritems = DB::select("select * from transactions_items");
    $items = DB::select("select * from item");
    $historyTable = DB::select("select * from login_history");
    $itemsTable = DB::select("select id, item_name, item_code, level from item");
    $usersOnlineInGame = \App\User::where('status_in_game', '=', 'online')->get();
    $usersOnlineInWeb = \App\User::where('status_in_web', '=', 'online')->get();
    $usersOfflineInGame = \App\User::where('status_in_game', '=', 'offline')->get();
    $usersOfflineInWeb = \App\User::where('status_in_web', '=', 'offline')->get();
    $usersRegistered = \App\User::where('role', '=', 'player')->get();
    $users = \App\User::all();
    $totalUserRecordCount = $usersRegistered->count();
    // Selecting all posted news
    // $allAdminPostedNews = DB::select("SELECT web_news.id, users.nickname,
    //                                          web_news.news_title, web_news.news_message,
    //                                          web_news.posted_news_date
    //                                  FROM web_news INNER JOIN users
    //                                  ON users.id = web_news.user_id")->get()->paginate(5);
    $allAdminPostedNews = DB::table('web_news')->
                          join('users', 'web_news.user_id', '=', 'users.id')
                          ->select('web_news.id',
                                   'users.nickname',
                                   'web_news.news_title', 
                                   'web_news.news_message', 
                                   'web_news.posted_news_date')
                          ->orderBy('web_news.posted_news_date', 'desc')
                          ->paginate(5);
    // $totalUserOnlineCount = $usersOnline->count();
    // $totalUserOfflineCount = $userOffline->count();

    View::Share('userResources', $userResources);
    View::Share('totalUserRecordCount', $usersRegistered->count());
    View::Share('totalUserOnlineInGameCount', $usersOnlineInGame->count());
    View::Share('totalUserOnlineInWebCount', $usersOnlineInWeb->count());
    View::Share('totalUserOfflineInGameCount', $usersOfflineInGame->count());
    View::Share('totalUserOfflineInWebCount', $usersOfflineInWeb->count());
    View::Share('transactionedItems', $transactionedItems);
    View::Share('tr_items', $tritems);
    View::Share('items', $items);
    View::Share('historyTable', $historyTable);
    View::Share('users', $users);
    View::Share('userBelongingItems', $this->userBelongingItems);
    View::Share('adminNews', $allAdminPostedNews);
    // Sharing generated diagrams

  }
  function getResourceCountByUserId($res_type, $user_id, $countkey)
  {

      if ($countkey == "web")
      {
        $amount = 0;
        foreach (\App\transactions_resources::all() as $ures)
        {
            if (($ures->to_user_id == $user_id && $ures->res_type == $res_type) &&
                 $ures->type_of_transaction == "from_admin_to_web_user")
            {
                $amount += $ures->amount;
            }
        }
        return $amount;
      }
      else if ($countkey == "game")
      {
        $amount = 0;
        foreach (\App\transactions_resources::all() as $ures)
        {
            if ($ures->to_user_id == $user_id && $ures->res_type == $res_type &&
            $ures->type_of_transaction == "from_admin_to_game")
       {
           $amount += $ures->amount;
       }
        }
        return $amount;
      }
      else
      {
          return 0;
      }

  }
  function getUserBalance($address)
  {
    //NEM balance code goes here
    $client = new \GuzzleHttp\Client();
               // Create a POST request
                    $response = $client->request(
                        'GET',
                        'http://'.env('NEM_HOST').':'.env('NEM_PORT').'/api/account/getBalance',
                        [
                            'json' => [

                                    "address"=> $address,
                            ],
                        ]
                    );

                   $accountBalance = json_decode($response->getBody()); //Add valeu to global variable
     $userMosaic = $accountBalance->account->mosaics;
     return $userMosaic[0]->amount;
}
//Method for getting user item data by id
  function getUserItemById($userid)
  {
      $data = '';
      //if array is null
    if (count($this->userBelongingItems) == 0) return '';
    foreach ($this->userBelongingItems as $uitem)
    {
        if ($uitem->user_id == $userid)//NOTE:Change this later
        {
            $data = '<table class = "cbm-admin-userinfo-table">
            <tr>
                <td colspan=3 class = "cbm-table-title">Current user items</td>
            </tr>
            <tr>
                <td class = "cbm-item-atable">Item id</td>
                <td class = "cbm-item-atable">Item name</td>
                <td class = "cbm-item-atable">Item code</td>
            </tr>
                <td class = "cbm-item-atable">'.$uitem->item_id.'</td>
                <td class = "cbm-item-atable">'.$uitem->item_name.'</td>
                <td class = "cbm-item-atable">'.$uitem->item_code.'</td>
            </tr>
             </table>';
        }
    }
    return $data;
  }
}
