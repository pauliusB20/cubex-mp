@extends((!isset(Auth::user()->id))? 'layouts.guest': ((Auth::user()->role == 'admin') ? 'layouts.admin' : 'layouts.user'))

@section('content')
<div class = "card">
    <h1 class = "card-title">
         <i class="ion ion-clipboard mr-1"></i>CubeMarket: Posted items on the market
    </h1>
</div>
<div class = "card">

    <table border = "1">
        <tr>
            <td>Id</td>
            <td>item_name</td>
            <td>price</td>
            <td>time_start</td>
            <td>time_end</td>
            <td>seller_name</td>
        </tr>
    @foreach (\App\market_items::all() as $market_item)
         <tr>
            <td><button id = "btnId" class = "btn btn-primary" data-toggle="modal" data-target="#offerWindow{{$market_item->id}}">{{$market_item->id}}</button>
            <div class = "modal fade" id ="offerWindow{{$market_item->id}}" role="dialog">
                    <div class="modal-dialog">
                        <div class="modal-content">
                                <div class="modal-header">
                                    <h4 class = "modal-title">Offer information</h4>
                                    <button type = "button" class="close" data-dismiss="modal">&times</button>
                                </div>
                                <div class="modal-body">

                                        <p>
                                        @foreach(\App\User::all() as $user)
                                          @if ($market_item->seller_user_id == $user->id)
                                            Seller nickname: {{ $user->nickname }}<br/>
                                            Seller email: {{ $user->email }}<br/>
                                         @endif
                                        @endforeach
                                        </p> <br/>
                                        <p>Posted offer information</p>
                                        <table class = "cbm-admin-userinfo-table">
                                        <tr>
                                            <td>item_id</td>
                                            <td>Item_name</td>
                                            <td>Item Code</td>
                                            <td>Item Class name</td>
                                            <td>Item general type</td>
                                        </tr>
                                        @foreach ($marketItemsInfo as $marketItemInfo)
                                            @if ($market_item->item_id == $marketItemInfo->item_id)
                                            <tr>
                                                <td>{{$marketItemInfo->item_id}}</td>
                                                <td>{{$marketItemInfo->item_name}}</td>
                                                <td>{{$marketItemInfo->item_code}}</td>
                                                <td>{{$marketItemInfo->classification_name}}</td>
                                                <td>{{$marketItemInfo->item_type_name}}</td>
                                             </tr>
                                            @endif
                                        @endforeach
                                        </table>
                                </div>
                                <div clas ="modal-footer">
                                    <!-- <button type="button" class="btn btn-primary m-t-10" data-dismiss="modal">Close</button> -->
                                </div>
                            </div>
                        </div>
                    </div>

            </td>

            @foreach($marketItemNames as $mitem)
              @if ( $market_item->item_id == $mitem->item_id)
                <td>{{$mitem->item_name}}</td>
              @endif
            @endforeach
            <td>{{$market_item->price}}</td>
            <td>{{$market_item->time_start}}</td>
            <td>{{$market_item->time_end}}</td>
            @foreach($users as $user)
              @if ( $market_item->seller_user_id == $user->id)
                <td>{{$user->nickname}}</td>
              @endif
            @endforeach
         </tr>
    @endforeach
    </table>
</div>

<div class = "card">
    <hr>
    <form action = "deleteOffer" method = "post">
      <input type = "hidden"  name = "_token" value = "<?php echo csrf_token(); ?>"/>
      <h3>Delete offer(Input offer id):</<h3>  <input type = "text" name="del_id"/>
      <input type = "submit" value = "Delete offer"/>
     </form>
    <hr>
</div>

<div class = "card">
   <div class="row">
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
