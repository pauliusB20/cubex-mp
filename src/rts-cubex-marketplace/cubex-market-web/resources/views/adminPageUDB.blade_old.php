@extends( Auth::user()->role === 'admin'  ?  'layouts.admin' : 'layouts.user' )

@section('content')
<div class = "card">
    <h1 class = "box-title">
                CubeMarket registered users<i class="ion ion-clipboard mr-1"></i>
     </h1><br/>
     <input type="text" class="form-control input-sm" id="search" name="search"></input>
    <!-- <div class="row">
        <h1 class = "box-title">
                CubeMarket registered users<i class="ion ion-clipboard mr-1"></i>
        </h1><br/>
        <form action="/searchuser" method="POST" role="search"> {{ csrf_field() }}
            <div class = "row_search">
                <label>Search:</label>
                <input type="search" name="q" class="form-control input-sm" placeholder="" required>
                <button type="submit" class="btn btn-navbar">
                            <i class="fa fa-search"></i>
                </button>
             </div>
         </form>
    </div> -->
 </div>

        <!-- <form class="form-inline ml-3">
        <div class="input-group input-group-sm">
            <input class="form-control form-control-navbar" type="text" placeholder="Search" aria-label="Search" name = "q">
            <div class="input-group-append">
            <button class="btn btn-navbar" type="submit">
                <i class="fa fa-search"></i>
            </button>
            </div>
        </div>
        </form> -->
    <!-- </div> -->
<div class="card">
    <table>
    <thead>

    </thead>
    <tbody>
    </tbody>
    </table>
</div>

@if(isset($details))
<div class="card">

            <p> The Search results for your search query <b> {{ $query }} </b> are:</p>
            <h2>Found User data records</h2>
            <table border="1">
                <thead>
                    <tr>
                        <td>Id</td>
                        <td>Nickname</td>
                        <td>Email</td>
                        <td>Password</td>
                        <td>Game status</td>
                        <td>role</td>
                        <td>Reg. Date</td>
                    </tr>
                </thead>
                <tbody>
                    @foreach($details as $user)
                    <tr>
                        <td class="text"><button id = "btnId" class = "btn btn-success" data-toggle="modal" data-target="#userWindow{{ $user->id }}">{{ $user->id }}</button>
                            <div class = "modal fade" id ="userWindow{{ $user->id }}" role="dialog">
                            <div class="modal-dialog">
                                <div class="modal-content">
                                        <div class="modal-header">
                                            <h4 class = "modal-title">User profile information</h4>
                                            <button type = "button" class="close" data-dismiss="modal">&times</button>
                                        </div>
                                        <div class="modal-body">

                                            <p>
                                                Nickname: {{ $user->nickname }}<br/>
                                                Email: {{ $user->email }}<br/>
                                                Password: {{ $user->password }}<br/>
                                            </p>

                                                <table class = "cbm-admin-userinfo-table">
                                                    <tr>
                                                        <td colspan=2 class = "cbm-table-title">Current user resources</td>
                                                    </tr>
                                                    <tr>
                                                        <td class = "cbm-item-atable">Resource type</td>
                                                        <td class = "cbm-item-atable">amount</td>
                                                    </tr>
                                                    <!--Initial variables for resources-->
                                                @php
                                                    $res_types = array("energon", "credits");
                                                    $energonSum = 0;
                                                    $creditsSum = 0;
                                                @endphp
                                                @foreach ($userResources as $ures)
                                                    <!--If user id and to_user_id and if the resource type matches, then it calculates the total sums of these resource values-->
                                                    @if ( $ures->to_user_id  ==  $user->id && $ures->res_type == $res_types[0])
                                                        <?php $energonSum += $ures->amount ?>

                                                    @elseif ($ures->to_user_id  ==  $user->id && $ures->res_type == $res_types[1])
                                                        <?php  $creditsSum  += $ures->amount ?>
                                                    @endif
                                                @endforeach
                                                        <tr>
                                                            <td class = "cbm-item-atable">{{$res_types[0]}}</td>
                                                            <td class = "cbm-item-atable">{{$energonSum}}</td>
                                                        </tr>
                                                        <tr>
                                                            <td class = "cbm-item-atable">{{$res_types[1]}}</td>
                                                            <td class = "cbm-item-atable">{{$creditsSum}}</td>
                                                        </tr>
                                                </table>
                                                Current user owned items:<br/>
                                                <table class = "cbm-admin-userinfo-table">
                                                    <tr>
                                                        <td colspan=3 class = "cbm-table-title">Current user items</td>
                                                    </tr>
                                                    <tr>
                                                        <td class = "cbm-item-atable">Item id</td>
                                                        <td class = "cbm-item-atable">Item name</td>
                                                        <td class = "cbm-item-atable">Item code</td>
                                                    </tr>

                                                @foreach ($userBelongingItems as $uitems)
                                                    <tr>
                                                    @if ($user->id == $uitems->user_id)
                                                        <td class = "cbm-item-atable">{{$uitems->item_id}}</td>
                                                        <td class = "cbm-item-atable">{{$uitems->item_name}}</td>
                                                        <td class = "cbm-item-atable">{{$uitems->item_code}}</td>
                                                    @endif
                                                    </tr>
                                                @endforeach
                                            </table>
                                        <!-- Wallet info -->
                                            <table class = "cbm-admin-userinfo-table">
                                                <tr>
                                                <td colspan=3 class = "cbm-table-title"> User:{{$user->nickname}} wallet information</td>
                                                </tr>
                                                <tr>
                                                    <td>Wallet name</td>
                                                    <td>Wallet ID</td>
                                                    <td>public key</td>
                                                    <td>private key</td>
                                                </tr>
                                                <tr>
                                                    <td>{{$user->wallet_name}}</td>
                                                    <td>{{$user->wallet_id}}</td>
                                                    <td>{{$user->public_key}}</td>
                                                    <td>{{$user->private_key}}</td>
                                                </tr>
                                            </table>
                                         <!--  -->
                                        </div>
                                        <div clas ="modal-footer">
                                            <!-- <button type="button" class="btn btn-primary m-t-10" data-dismiss="modal">Close</button> -->
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </td>
                        <td class="text">{{$user->nickname}}</td>
                        <td class="text">{{$user->email}}</td>
                        <td class="text">{{$user->password}}</td>
                        <td class="text">{{$user->game_status}}</td>
                        <td class="text">{{$user->role}}</td>
                        <td class="text">{{$user->reg_date}}</td>
                    </tr>
                    @endforeach
                </tbody>
            </table>

        </div>
        @endif
<div class = "card">

    <table border = "1">
      <tr>
        <td>Id</td>
        <td>Nickname</td>
        <td>Email</td>
        <td>Password</td>
        <td>Game status</td>
        <td>role</td>
        <td>Reg. Date</td>
        <!-- <td>Wallet name</td>
        <td>Wallet ID</td>
        <td>Private Key</td>
        <td>Public Key</td>                            -->
      </tr>
        @foreach (\App\User::all() as $user)
            <tr>
                <td class="text">
                    <button id = "btnId" class = "btn btn-primary" data-toggle="modal" data-target="#userWindow{{ $user->id }}">{{ $user->id }}</button>
                    <div class = "modal fade" id ="userWindow{{ $user->id }}" role="dialog">
                    <div class="modal-dialog">
                        <div class="modal-content">
                                <div class="modal-header">
                                    <h4 class = "modal-title">User profile information</h4>
                                    <button type = "button" class="close" data-dismiss="modal">&times</button>
                                </div>
                                <div class="modal-body">

                                     <p>
                                        Nickname: {{ $user->nickname }}<br/>
                                        Email: {{ $user->email }}<br/>
                                        Password: {{ $user->password }}<br/>
                                    </p>

                                         <table class = "cbm-admin-userinfo-table">
                                            <tr>
                                                <td colspan=2 class = "cbm-table-title">Current user resources</td>
                                            </tr>
                                            <tr>
                                                <td class = "cbm-item-atable">Resource type</td>
                                                <td class = "cbm-item-atable">amount</td>
                                            </tr>
                                             <!--Initial variables-->
                                        @php
                                            $res_types = array("energon", "credits");
                                            $energonSum = 0;
                                            $creditsSum = 0;
                                        @endphp
                                        @foreach (\App\transactions_resources::all() as $ures)
                                            <!--If user id and to_user_id and if the resource type matches, then it calculates the total sums of these resource values-->
                                            @if ( $ures->to_user_id  ==  $user->id && $ures->res_type == $res_types[0])
                                                <?php $energonSum += $ures->amount ?>

                                            @elseif ($ures->to_user_id  ==  $user->id && $ures->res_type == $res_types[1])
                                                 <?php  $creditsSum  += $ures->amount ?>
                                            @endif
                                        @endforeach
                                                <tr>
                                                    <td class = "cbm-item-atable">{{$res_types[0]}}</td>
                                                    <td class = "cbm-item-atable">{{$energonSum}}</td>
                                                </tr>
                                                <tr>
                                                    <td class = "cbm-item-atable">{{$res_types[1]}}</td>
                                                    <td class = "cbm-item-atable">{{$creditsSum}}</td>
                                                </tr>
                                          </table>
                                        <table class = "cbm-admin-userinfo-table">
                                            <tr>
                                                <td colspan=3 class = "cbm-table-title">Current user items</td>
                                            </tr>
                                            <tr>
                                                <td class = "cbm-item-atable">Item id</td>
                                                <td class = "cbm-item-atable">Item name</td>
                                                <td class = "cbm-item-atable">Item code</td>
                                            </tr>

                                        @foreach ($userBelongingItems as $uitems)
                                            <tr>
                                            @if ($user->id == $uitems->user_id)
                                                  <td class = "cbm-item-atable">{{$uitems->item_id}}</td>
                                                  <td class = "cbm-item-atable">{{$uitems->item_name}}</td>
                                                  <td class = "cbm-item-atable">{{$uitems->item_code}}</td>
                                            @endif
                                            </tr>
                                        @endforeach
                                     </table>
                                      <!-- Wallet info -->
                                      <table class = "cbm-admin-userinfo-table">
                                         <tr>
                                           <td colspan=3 class = "cbm-table-title"> User:{{$user->nickname}} wallet information</td>
                                         </tr>
                                         <tr>
                                            <td>Wallet name</td>
                                            <td>Wallet ID</td>
                                            <td>public key</td>
                                            <td>private key</td>
                                         </tr>
                                         <tr>
                                            <td>{{$user->wallet_name}}</td>
                                            <td>{{$user->wallet_id}}</td>
                                            <td>{{$user->public_key}}</td>
                                            <td>{{$user->private_key}}</td>
                                         </tr>
                                       </table>
                                        <!--  -->
                                </div>
                                <div clas ="modal-footer">
                                    <!-- <button type="button" class="btn btn-primary m-t-10" data-dismiss="modal">Close</button> -->
                                </div>
                            </div>
                        </div>
                    </div>
                </td>
                <td class="text">{{$user->nickname}}</td>
                <td class="text">{{$user->email}}</td>
                <td class="text">{{$user->password}}</td>
                <td class="text">{{$user->game_status}}</td>
                <td class="text">{{$user->role}}</td>
                <td class="text">{{$user->reg_date}}</td>
                <!-- <td class="text">{{$user->wallet_name}}</td>
                <td class="text">{{$user->wallet_id}}</td>
                <td class="text">{{$user->private_key}}</td>
                <td class="text">{{$user->public_key}}</td> -->
            </tr>
            @endforeach
    </table>
</div>

<div class = "card">
    <hr>
    <form action = "deleteUser" method = "post">
      <input type = "hidden"  name = "_token" value = "<?php echo csrf_token(); ?>"/>
      <h3>Delete user id:</<h3>  <input type = "text" name="del_id"/>
      <input type = "submit" value = "Delete user"/>
     </form>
    <hr>
</div>

<div class = "card">
   <div class="row row_pad">
        @isset($totalUserRecordCount)
        <div class = "col-lg-3 col-6">
            <div class = "small-box bg-info">
            <div class="inner">
                <h3>{{$totalUserRecordCount}}</h3>
                <p>Registered users count</p>
                </div>
            </div>
        </div>
        @endisset

            @isset($totalUserOnlineCount)
                <div class = "col-lg-3 col-6">
                    <div class = "small-box bg-success">
                        <div class="inner">
                            <h3>{{$totalUserOnlineCount}}</h3>
                            <p>Online user count</p>
                        </div>
                    </div>
                </div>
            @endisset

            @isset($totalUserOfflineCount)
                <div class = "col-lg-3 col-6">
                    <div class = "small-box bg-danger">
                        <div class="inner">
                            <h3>{{$totalUserOfflineCount}}</h3>
                            <p>Offline user count</p>
                        </div>
                    </div>
                </div>
            @endisset
    </div>

@endsection
