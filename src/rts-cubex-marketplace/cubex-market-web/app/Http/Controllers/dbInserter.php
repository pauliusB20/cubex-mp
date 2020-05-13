<?php

namespace App\Http\Controllers;

use Illuminate\Http\Request;
use DB;
use Hash;
use Illuminate\Support\Str;
class dbInserter extends AdminBaseController
{

    public function __construct()
    {

        parent::__construct();

    }

    public function getAllUserTokienBalances()
    {
        $users = DB::select('select * from users');
        foreach ($users as $user)
        {
            if ($user->wallet_address != NULL)
                echo $user->id.' '.$this->getUserBalance($user->wallet_address).'<br/>';

        }

    }
    public function retrieveUserDataBaseTable()
    {
        $users = DB::select('select * from users');
        foreach ($users as $user)
        {
            echo $user->id.'-'.$user->nickname.'-'.$user->password.'-'.$user->status_in_game.'-'.$user->role.'-'.$user->email.'-'.
            $user->wallet_address.'-'.$user->private_key.'-'.$user->public_key.'<br/>';

        }
    }
    public function dbAddResourcsFromGame(Request $request) //Adding resources from game to web
    {
        $userID = $request->input('userid');
        $energonAmount = $request->input('energon');
        $creditsAmount = $request->input('credits');
        $adminID =  \App\User::where('role', '=', 'admin')->min('id');
        //Syncronization transaction begins
        DB::transaction(function () use ($userID, $energonAmount, $creditsAmount, $adminID)
        {
            \App\transactions_resources::create(["from_user_id" => $userID,
                                                "to_user_id" => $adminID,
                                                "res_type" => "energon",
                                                "amount" => $energonAmount,
                                                "type_of_transaction" => "from_user_to_admin"
                                                ]);
             \App\transactions_resources::create(["from_user_id" => $adminID,
                                                "to_user_id" => $userID,
                                                "res_type" => "energon",
                                                "amount" => $energonAmount,
                                                "type_of_transaction" => "from_admin_to_web_user"
                                                ]);
            \App\transactions_resources::create(["from_user_id" => $userID,
                                                "to_user_id" => $adminID,
                                                "res_type" => "credits",
                                                "amount" => $creditsAmount,
                                                "type_of_transaction" => "from_user_to_admin"
                                                ]);
             \App\transactions_resources::create(["from_user_id" => $adminID,
                                                "to_user_id" => $userID,
                                                "res_type" => "credits",
                                                "amount" => $creditsAmount,
                                                "type_of_transaction" => "from_admin_to_web_user"
                                                ]);
        });

    }
    public function deleteExistingTransactions(Request $request)
    {
        $itemid = $request->input('item_id');
        $userid = $request->input('user_id');
        //$itemidFromDb = DB::select('SELECT id FROM  inventory where item_id = '.$itemid.' AND item_status = "game"');
        //$itemidFromDB = array();
        $itemidFromDB = \App\inventory::where("item_id", $itemid)->where('user_id',$userid)->get();

        //Cleaning inventory
        if (!empty($itemidFromDB))
        {
           $tr = \App\transactions_items::where('inventory_id',$itemidFromDB[0]->id)->where('type_of_transaction','from_web_to_admin')->delete();
           if (!empty($tr))
           {
            $tr1 =  \App\transactions_items::where('inventory_id',$itemidFromDB[0]->id)->where('type_of_transaction','from_admin_to_game')->delete();
             if (!empty($tr1))
             {
                 $char = \App\itemCharacteristics::where('item_id', $itemid)->delete();
                 if (!empty($char))
                 {
                    $class = \App\item_classification::where('item_id', $itemid)->delete();
                    if (!empty($class))
                    {
                        $itemDeleted = \App\item::where('id',$itemid)->delete();
                        if(!empty($itemDeleted))
                        {
                            \App\inventory::where("item_id", $itemid)->delete();
                        }

                    }
                 }

             }
           }
        }
        //Cleaning item table



    }
    public function dbTableInsert(Request $request)
    {
            // NOTE: Wallet name, Wallet ID, Public Key, Private Key will come from NEM block chain platform
          //  $user = Auth::user();//Aunhenticate new user from game

            $username = $request->input('nickname');
            $password = $request->input('password');
            $email = $request->input('email');
            $gameStatus = $request->input('game_status');
            $regDate = $request->input('reg_date');
            $role = 'player';
            $data=array(
                    'nickname'=>$username,
                    'email'=>$email,
                    'password'=>bcrypt($password),
                    'status_in_web'=>'offline',
                    'status_in_game'=>'offline',
                    'role' => $role,
                    'reg_date'=>$regDate//,
                    // 'wallet_name' => 'pending',
                    // 'wallet_id' => Str::random(15),
                    // 'private_key' => Str::random(10),
                    // 'public_key' => Str::random(10)
            );

         try
         {
            $newAccount = \App\User::create($data);
            //NEM wallet creaton goes here..
            if (!empty($newAccount))
            {
                try {
                    $client = new \GuzzleHttp\Client();
                    // Create a POST request
                    $response = $client->request(
                        'POST',
                        'http://'.env('NEM_HOST').':'.env('NEM_PORT').'/api/account/create',
                        [
                            'json' => [
                                'userId' => "1",
                            ],
                        ]
                    );
                    //NOTE: FIX pending problem
                    //Account creatable, transaction happens
                    //Display data in game
                    $account = json_decode($response->getBody()); //Add valeu to global variable
                    if (!empty($account)){
                        echo "BlockChain account created";
                        echo "Begining to create transaction!";
                        //Updating data in database users table NOTE: For good practise update this code later
                        $updateWAddress = \App\User::where('id', \App\User::max('id'))->update(['wallet_address' => $account->address->address]);
                        $updatePrivKey = \App\User::where('id', \App\User::max('id'))->update(['private_key' => $account->privateKey]);
                        $updatePubKey = \App\User::where('id', \App\User::max('id'))->update(['public_key' => $account->publicKey]);

                        if ((!empty($updateWAddress) && !empty($updatePrivKey)) && !empty($updatePubKey))
                        {
                        //Inserting starting transaction status
                            $startingTransRecord=array(
                                    'user_id'=>\App\User::max('id'),
                                    'recipient_address'=>$account->address->address,
                                    'namespace_name	'=>'cat.currency',
                                    'amount'=>100,
                                    'message' => "User ".$username." token starting amount transaction",
                                    'status'=>"pending"
                                );
                                $trStartingRecord = \App\CBlock::create($startingTransRecord);
                                //echo $account->address->address;
                                if (!empty($trStartingRecord))
                                {
                                    $response =  $client->request(
                                        'PUT', //Nemesis account
                                        'http://'.env('NEM_HOST').':'.env('NEM_PORT').'/api/transaction/sendAndRespond',
                                        [
                                            'json' => [
                                                'senderPrivateKey' => env('ADMIN_PRIVATE_KEY'),
                                                'recipientAddress' => $account->address->address,
                                                'namespaceName' => env('MOSAIC'),
                                                'amount' => 100,
                                                'message' => "Sending to user ".$username,
                                                'transactionId' => \App\CBlock::max('id'),
                                                'responseUrl' => env('APP_URL') . '/api/sendStartingAmount',
                                            ],
                                        ]
                                    );
                                    if (!empty($response))
                                    {
                                        echo "Starting amount is set for user: ".$username;
                                    }
                                }
                        }
                 }
                } catch (\GuzzleHttp\Exception\ConnectException $e) {
                    // return redirect('events/create')
                    //     ->with(['toast' => "error", 'toastTitle' => "Error", 'toastText' => "Event creation failed"]);
                    echo "Failed to create NEM account!";
                }

            }
         }
          catch(Exception $e)
         {
          echo 'Caught exception: ',  $e->getMessage(), "\n";
         }

        //    DB::table('usergame_res')->insert($dataResources);

           // return response()->json(['status'=>true,'UploadStatus'=>$device]);

    }
    public function retrieveResourcesData(Request $request)
    {
       $res_types = array("energon", "credits");
       $totalUserResourcesSums = array();

       foreach (\App\User::all() as $user)
       {
           $userEnergon = $this->getResourceCountByUserId($res_types[0], $user->id, 'web');
           $userCredits = $this->getResourceCountByUserId($res_types[1], $user->id, 'web');
           $userRes = $user->id.' '.$userEnergon.' '.$userCredits;
           $totalUserResourcesSums[] = $userRes;
       }

       if (count($totalUserResourcesSums) > 0)
       {
           foreach($totalUserResourcesSums as $resSum)
           {
               echo $resSum.'<br/>';
           }
       }

    }
    public function updateHistoryTable(Request $request)
    {
        $userId = $request->input('user_id');
        $logInTime = $request->input('login_time');
        $logOutTime = $request->input('logout_time');
        $userIp = $request->input('ip');
        $place = 'game';
        try{
           \App\login_history::create([
              "user_id" => $userId,
              "login_time" => $logInTime,
              "logout_time" => $logOutTime,
              "ip" => $userIp,
              "place" => $place
           ]);
        }
        catch(Exception $e)
        {
            echo "Error: ".$e;
        }
    }
    public function addItemToDatabase(Request $request)
    {

        //Item transaction from game to database..
        $puserid = $request->input('puser_id');
        $itemName = $request->input('item_name');
        $itemCode = $request->input('item_code');
        $level = $request->input('level');
        $itemType = $request->input('item_type');
        $classification_name = $request->input('classification_name');
        //Item characteristics:
        $HPval = $request->input('HP_val');
        $DMGval = $request->input('DMG_val');
        $Shield_val = $request->input('Shield_val');
        $characteristicData = array($HPval, $DMGval, $Shield_val);
        //Item information storing process..
        //Storing item type name..


        DB::transaction(function () use ($itemType, $classification_name, $itemName, $itemCode, $level, $puserid, $characteristicData)
        {
                $transaction_type = "from_game_to_web";
                $default_item_status = "web";
                // Storing item type information
                \App\item_type::updateOrCreate(['item_type_name' => $itemType]);

                \App\item::create(
                    [
                        'item_name' => $itemName,
                        'item_code' => $itemCode,
                        'level' => $level
                    ]
                );
                \App\item_classification::updateOrCreate(
                    [
                        'item_id' => (DB::table('item')->max('id')),
                        'classification_name' => $classification_name,
                        'item_type_id' => (DB::table('item_type')->where('item_type_name', '=', $itemType)->value('id'))
                    ]
                );
                \App\inventory::create(
                    ['user_id' => $puserid,
                    'item_id' => (DB::table('item')->max('id')),
                    'description'=> $itemName,
                    // 'hash_code'=> (Hash::make($itemName, ['rounds' => 12])),
                    'item_status' => $default_item_status]
                );
                //Storing information about the item transaction from the game
                \App\transactions_items::create(
                    ['from_user_id' => $puserid,
                    'to_user_id' => (DB::table('users')->select('id')->where('role', '=', 'admin')->value('id')),
                    'inventory_id' => (DB::table('inventory')->max('id')),
                    'type_of_transaction' => $transaction_type]
                );
                $transaction_type = "from_admin_to_user";
                \App\transactions_items::create(
                    ['from_user_id' => (DB::table('users')->select('id')->where('role', '=', 'admin')->value('id')),
                    'to_user_id' => $puserid,
                    'inventory_id' => (DB::table('inventory')->max('id')),
                    'type_of_transaction' => $transaction_type]
                );

                //Inserting the data of item characteristics
                $ch_id = 1; //In loop it is required that certain records would start from 1 - 3 and again from 1 -3
                foreach ($characteristicData as $ch)
                {
                    $charDetails = explode(':', $ch);
                    \App\characteristics::updateOrCreate(
                        [
                            'characteristics_name' => $charDetails[0]
                        ]
                    );
                    DB::table('item_characteristics')->insert(
                        [
                            'characteristics_id' => $ch_id,
                            'value' => $charDetails[1],
                            'item_id' => (DB::table('item')->max('id'))
                        ]
                    );
                    $ch_id++;
                }
            });

       }

      //Later on will be used for splitting...
        //From Unity it will get a big item info string, which will contain the names and values of
        //it's characteristics
        // $ch_names= $request->input('ch_values');
        // if (strlen($ch_names) > 0)
        // {
        //     list($)
        // }

        // $uid = $request->input('u_id');
        // $itemName = $request->input('item_name');
        // $itemType = $request->input('item_type');
        // $level = $request->input('level');
        // $itemCode = $request->input('item_code');

        // $data=array(
        //     'u_id'=>$uid,
        //     'item_name'=>$itemName,
        //     'item_type'=>$itemType,
        //     'level'=>$level,
        //     'item_code'=>$itemCode
        // );
        // try
        // {
        //     DB::table('user_items')->insert($data);
        // }
        // catch(Exception $e)
        // {
        //  echo 'Caught exception: ',  $e->getMessage(), "\n";
        // }


    // public function dbTableUpdater(Request $request)
    // {
    //     $eamount = $request->input('eamount');
	//     $camount = $request->input('camount');
    //     $id = $request->input('id');
    //   try{
    //     DB::update('update usergame_res set eamount = ?,camount = ? where id = ?',[$eamount,$camount,$id]);
    //         //$data=array('eamount'=>$eamount,'camount'=>$camount);
    //         //DB:table('usergame_res')->whereIn('id', $id)->update($request->all());
    //      }
    //     catch(Exception $e)
    //     {
    //     echo "Error: " + $e;
    //     }
    // }

    public function dbUpdateGameStatus(Request $request) //Updating user game status activity
    {
        $id = $request->input('id');
        $gameStatus = $request->input('status_in_game');

        try
        {
            DB::update('update users set status_in_game = ? where id = ?',[$gameStatus,$id]);
        }
        catch(Exception $e)
        {
            echo "Error: " + $e;
        }
    }
}


