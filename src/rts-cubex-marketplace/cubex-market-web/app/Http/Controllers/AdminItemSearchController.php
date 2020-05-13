<?php

namespace App\Http\Controllers;

use Illuminate\Http\Request;
use DB;
class AdminItemSearchController extends AdminBaseController
{
      public function searchItems(Request $req)
      {
        $q = $req->input('titem');
        //Query to select all informations about the current occured transactions
        $transactionsItemNames = DB::select("select tr.id, fromusers.nickname as from_user, tousers.nickname as to_user, titems.item_name, tr.type_of_transaction from transactions_items as tr, users as fromusers, users as tousers, item as titems, inventory as inv where (fromusers.id = tr.from_user_id and tousers.id = tr.to_user_id) and (titems.id = inv.item_id and inv.id = tr.inventory_id) ;");
        $transactionsitems = array();
        //Searching inputed substring from the html search input box
        foreach ($transactionsItemNames as $tresNames)
        {
            if (strpos($tresNames->from_user, $q) !== false)
            {
                $transactionsitems[] = $tresNames;
            }
            else if (strpos($tresNames->to_user, $q) !== false)
            {
                $transactionsitems[] = $tresNames;
            }
            else if (strpos($tresNames->type_of_transaction, $q) !== false)
            {
                $transactionsitems[] = $tresNames;
            }
        }

        if (count ( $transactionsitems ) > 0)
                return view ('admTitMonitor')->withDetails ( $transactionsitems )->withQuery ( $q );
            else
                return view ('admTitMonitor');
    }
}
