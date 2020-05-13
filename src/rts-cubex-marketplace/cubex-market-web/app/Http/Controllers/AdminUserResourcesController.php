<?php

namespace App\Http\Controllers;

use Illuminate\Http\Request;
use DB;
use Kyslik\ColumnSortable\Sortable;

class AdminUserResourcesController extends AdminBaseController 
{
    public function searchResources(Request $req)
    {
        
        $q = $req->input('tres');
        //Query to select all informations about the current occured transactions
        $transactionsResNames = DB::select("select tr.id, fromusers.nickname as from_username, tousers.nickname as to_username, tr.amount, tr.res_type, tr.type_of_transaction from transactions_resources as tr, users as fromusers, users as tousers where tr.from_user_id = fromusers.id and tr.to_user_id = tousers.id;");
        $transactionsres = array();
        //Searching inputed substring from the html search input box
        foreach ($transactionsResNames as $tresNames)
        {
            if (strpos($tresNames->from_username, $q) !== false)
            {
                $transactionsres[] = $tresNames;
            }
            else if (strpos($tresNames->to_username, $q) !== false)
            {
                $transactionsres[] = $tresNames;
            }
        }
    
       if (count ( $transactionsres ) > 0)
            return view ('admTMonitor')->withDetails ($transactionsres)->withQuery ( $q );
        else
            return view ('admTMonitor');
    }
}
