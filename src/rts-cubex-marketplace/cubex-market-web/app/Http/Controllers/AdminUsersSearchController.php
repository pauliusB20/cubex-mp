<?php

namespace App\Http\Controllers;

use Illuminate\Http\Request;
use DB;
class AdminUsersSearchController extends AdminBaseController
{
    // Add aditional search results
    //Fix table style
    //Use models for data selection
    //Fix other search engines as well
    //FIX: MIgration!
    /*
    Posted market items query:
    select market_item.id, market_item.time_start, market_item.time_end, useritem.item_name, useritem.level, chnames.characteristics_name, chval.value from market_item inner join transactions_items on market_item.transaction_items_id = transactions_items.id, inventory as inv, item as useritem, characteristics as chnames, item_characteristics as chval where ((((transactions_items.inventory_id = inv.id) and (inv.item_id = useritem.id)) and (chval.item_id = useritem.id)) and (chval.characteristics_id = chnames.id)) ;

    */
    public function __construct()
    {

        parent::__construct();

    }

    public function userSearch(Request $req)
    {
        if($req->ajax())
        {
            $res_types = array("energon", "credits");
            $query = $req->input('query');
            $output="";
            $users=\App\User::where('nickname','LIKE','%'.$query."%")->
                              orWhere('email','LIKE','%'.$query."%")->
                              orWhere('role','LIKE','%'.$query."%")->
                              orWhere('reg_date','LIKE','%'.$query."%")->
                              get();
            if($users)
            {
                foreach($users as $user)
                {
                    // Expanding the created query by adding additional attributes to user records
                    $user["credits"] = $this->getResourceCountByUserId("credits", $user->id, "web");
                    $user["energon"] = $this->getResourceCountByUserId("energon", $user->id, "web");
                    if (!empty($user->wallet_address))
                        $user["tokens"] = $this->getUserBalance($user->wallet_address);
                    else
                        $user["tokens"] = 0;
                }
                // foreach ($users as $user) {
                // $output.='<tr>'.
                //         '<td><button id = "btnId" class = "btn btn-info" data-toggle="modal" data-target="#userWindow'.$user->id.'">'.$user->id.'</button>
                //         <div class = "modal fade" id ="userWindow'.$user->id.'" role="dialog">
                //         <div class="modal-dialog">
                //             <div class="modal-content">
                //                     <div class="modal-header">
                //                         <h4 class = "modal-title">User profile information</h4>
                //                         <button type = "button" class="close" data-dismiss="modal">&times</button>
                //                     </div>
                //                     <div class="modal-body">

                //                         <p>
                //                             Nickname: '.$user->nickname.'<br/>
                //                             Email: '.$user->email.'<br/>
                //                             Password: '.$user->password.'<br/>
                //                             Role: '.$user->role.'<br/>
                //                         </p>

                //                             <table class = "cbm-admin-userinfo-table">
                //                                 <tr>
                //                                     <td colspan=2 class = "cbm-table-title">Current user resources</td>
                //                                 </tr>
                //                                 <tr>
                //                                     <td class = "cbm-item-atable">Resource type</td>
                //                                     <td class = "cbm-item-atable">amount</td>
                //                                 </tr>


                //                                     <tr>
                //                                         <td class = "cbm-item-atable">energon</td>
                //                                         <td class = "cbm-item-atable">'
                //                                         .$this->getResourceCountByUserId("energon", $user->id, "web").
                //                                         '</td>
                //                                     </tr>
                //                                     <tr>
                //                                         <td class = "cbm-item-atable">credits</td>
                //                                         <td class = "cbm-item-atable">'
                //                                         .$this->getResourceCountByUserId("credits", $user->id, "web").
                //                                        '</td>
                //                                     </tr>
                //                             </table>
                //                             '.$this->getUserItemById($user->id).'

                //                     <!-- Wallet info -->
                //                     <h2>Wallet data</h2>
                //                     <p>Wallet address:<br/>'.$user->wallet_address.'</p>
                //                     <p>Public Key:<br/>'.$user->public_key.'</p>
                //                     <p>Private Key:<br/>'.$user->private_key.'</p>
                //                      <!--  -->
                //                     </div>
                //                     <div clas ="modal-footer">
                //                         <!-- <button type="button" class="btn btn-primary m-t-10" data-dismiss="modal">Close</button> -->
                //                     </div>
                //                 </div>
                //             </div>
                //         </div>
                //         </td>'.
                //         '<td>'.$user->nickname.'</td>'.
                //         '<td>'.$user->status_in_web.'</td>'.
                //         '<td>'.$user->status_in_game.'</td>'.
                //         '<td>'.$user->role.'</td>'.
                //         '<td>'.$user->reg_date.'</td>'.
                //         '</tr>';
                // }
                return Response($users);
                // return response()->view('adminPageUDB', [ 'users '=> $users]);
            }
            else
            {
                return Response("");
            }
        }

        // $q = $req->input('q');
        // $user = \App\User::where ( 'nickname', 'LIKE', '%' . $q . '%' )->orWhere ( 'email', 'LIKE', '%' . $q . '%' )->orWhere('wallet_name', 'LIKE', '%'.$q.'%')->orWhere('game_status', 'LIKE', $q)->orWhere('reg_date', 'LIKE', '%'.$q.'%')->get ();
        // if (count ( $user ) > 0)
        //     return view ('adminPageUDB')->withDetails ( $user )->withQuery ( $q );
        // else
        //     $message = "Error! no users were found by your inputed query: ".$q."!";
        //     echo "<script type='text/javascript'>alert('$message');</script>";
        //     return view ('adminPageUDB');
        /*
          @isset($users)
   @foreach($users as $user)
                <tr>
                        <td><button id = "btnId" class = "btn btn-info" data-toggle="modal" data-target="#userWindow{{$user->id}}">{{$user->id}}</button>
                        <div class = "modal fade" id ="userWindow{{$user->id}}" role="dialog">
                        <div class="modal-dialog">
                            <div class="modal-content">
                                    <div class="modal-header">
                                        <h4 class = "modal-title">User profile information</h4>
                                        <button type = "button" class="close" data-dismiss="modal">&times</button>
                                    </div>
                                    <div class="modal-body">

                                        <p>
                                            Nickname: {{$user->nickname}}<br/>
                                            Email: {{$user->email}}<br/>
                                            Password: {{$user->password}}<br/>
                                            Role: {{$user->role}}<br/>
                                        </p>

                                            <table class = "cbm-admin-userinfo-table">
                                                <tr>
                                                    <td colspan=2 class = "cbm-table-title">Current user resources</td>
                                                </tr>
                                                <tr>
                                                    <td class = "cbm-item-atable">Resource type</td>
                                                    <td class = "cbm-item-atable">amount</td>
                                                </tr>


                                                    <tr>
                                                        <td class = "cbm-item-atable">energon</td>
                                                        <td class = "cbm-item-atable">
                                                        {{--$this->getResourceCountByUserId("energon", $user->id, "web")--}}
                                                        '</td>
                                                    </tr>
                                                    <tr>
                                                        <td class = "cbm-item-atable">credits</td>
                                                        <td class = "cbm-item-atable">'
                                                        {{--$this->getResourceCountByUserId("credits", $user->id, "web")--}}
                                                       </td>
                                                    </tr>
                                            </table>
                                            {{--$this->getUserItemById($user->id)--}}

                                    <!-- Wallet info -->
                                    <h2>Wallet data</h2>
                                    <p>Wallet address:<br/>{{$user->wallet_address}}</p>
                                    <p>Public Key:<br/>{{$user->public_key}}</p>
                                    <p>Private Key:<br/>{{$user->private_key}}</p>
                                     <!--Delete user   -->
                                     <!--end delete  -->
                                    </div>
                                    <button class = "btn btn-danger" id="deluser{{$user->id}}">Delete user</button>
                                    <script>
                                    $(document).ready(function () {
                                        $.ajaxSetup({
                                                headers: {
                                                'X-CSRF-TOKEN': $('meta[name="csrf-token"]').attr('content')
                                                }
                                                });
                                        $('#deluser{{$user->id}}').on('click',function() {
                                            toastr.info('Trying to delete the user...', 'Information', {timeOut: 5000});
                                            userID = "{{$user->id}}"
                                            $.ajax({
                                                    url : "{{route('deleteUser', '$user->id')}}",
                                                    method: 'post',
                                                    data: { 'id': userID },
                                                    success: function (data) {
                                                        if (data == "refresh"){
                                                            toastr.success('Success! User was deleted!', 'Success Alert', {timeOut: 5000});
                                                            window.location.reload(); // This is not jQuery but simple plain ol' JS
                                                        }
                                                    }
                                                });
                                                // console.log("user had pressed the button");
                                                // clicked=true;
                                        });
                                    });
                                </script>
                                    <div clas ="modal-footer">
                                        <!-- <button type="button" class="btn btn-primary m-t-10" data-dismiss="modal">Close</button> -->
                                    </div>
                                </div>
                            </div>
                        </div>
                        </td>
                        <td>{{$user->nickname}}</td>
                        <td>{{$user->status_in_web}}</td>
                        <td>{{$user->status_in_game}}</td>
                        <td>{{$user->role}}</td>
                        <td>{{$user->reg_date}}</td>
                        </tr>
    @endforeach
    @endisset

        */
    }
}
