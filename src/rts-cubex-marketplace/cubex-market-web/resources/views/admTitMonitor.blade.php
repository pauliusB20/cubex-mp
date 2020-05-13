@extends((!isset(Auth::user()->id))? 'layouts.guest': ((Auth::user()->role == 'admin') ? 'layouts.admin' : 'layouts.user'))
@section('title-block') Game item transaction monitor @endsection('title-block')
@section('content')
<div class = "card admin-style-1">
    <div class = "row " style="margin-top: 5px;">
        <h1 class = "card-title admin-card-title">
            <i class="ion ion-clipboard mr-1"></i>CubeMarket user item transactions
        </h1>
        <br/>
        <form action="/searchTrItemUsers" method="POST" role="search">
            {{ csrf_field() }}
            <div class="input-group">
                <input type="text" class="form-control" name="titem"
                    placeholder="Input nickname for item transaction search" style="margin-bottom: 5px;"> <span class="input-group-btn">
                    <button type="submit" class="btn btn-navbar" style="margin-left:5px">
                         <i class="fa fa-search"></i>
                    </button>
                </span>
            </div>
        </form>
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
    </div>

</div>

@if(isset($details))
    <div class="card">
                <p> The Search results for your query <b> {{ $query }} </b> are :</p>
            <h2>Found user transactions</h2>
            <table border="1">
                <thead>
                    <tr>
                        <th>tr_id</th>
                        <th>From user name</th>
                        <th>To user name</th>
                        <th>Item name</th>
                        <th>Type of transaction</th>
                    </tr>
                </thead>
                <tbody>
                        @foreach ($details as $item)
                                <tr>
                                
                                        <td><b>{{$item->id}}</b></td>
                                        <td><b>{{$item->from_user}}</b></td>
                                        <td><b>{{$item->to_user}}</b></td>
                                        <td><b>{{$item->item_name}}</b></td>
                                        <td><b>{{$item->type_of_transaction}}</b></td>
                                    
                                </tr>
                    @endforeach
                </tbody>
            </table>

        </div>
@endif
 <div class = "card admin-items-result2">
<!-- Transaction monitor code goes here -->
 <table border = "1">
   <tr>
      <thead>
        <th>tr_id</th>
        <th>From user name</th>
        <th>To user name</th>
        <th>Transfered item</th>
        <th>Transaction type</th>
      </thead>
   </tr>
   <!-- Code for displaying all happened item transactions -->
   <tbody>
   @foreach($transactionedItems as $uitems)        
            <tr>          
                <td>{{$uitems->id}}</td>
                <td>{{$uitems->from_user}}</td>
                <td>{{$uitems->to_user}}</td>
                <td>{{$uitems->item_name}}</td>
                <td>{{$uitems->type_of_transaction}}</td>         
            </tr>
   @endforeach
   </tbody>
 </table>
 </div>

 <div class = "card admin-items-result2">
   <div class="row">
        @isset($totalUserRecordCount)
        <div class = "col-lg-3 col-6">
            <div class = "small-box bg-info">
            <div class="inner">
                <h3>{{$totalUserRecordCount}}</h3>
                <p>Registered player users count</p>
                </div>
            </div>
        </div>
        @endisset

            @isset($totalUserOnlineInGameCount)
                <div class = "col-lg-3 col-6">
                    <div class = "small-box bg-success">
                        <div class="inner">
                            <h3>{{$totalUserOnlineInGameCount}}</h3>
                            <p>Online users in game count</p>
                        </div>
                    </div>
                </div>
            @endisset
            @isset($totalUserOnlineInWebCount)
                <div class = "col-lg-3 col-6">
                    <div class = "small-box bg-success">
                        <div class="inner">
                            <h3>{{$totalUserOnlineInWebCount}}</h3>
                            <p>Online users in web count</p>
                        </div>
                    </div>
                </div>
            @endisset
            @isset($totalUserOfflineInGameCount)
                <div class = "col-lg-3 col-6">
                    <div class = "small-box bg-danger">
                        <div class="inner">
                            <h3>{{$totalUserOfflineInGameCount}}</h3>
                            <p>Offline user count in game</p>
                        </div>
                    </div>
                </div>
            @endisset
            @isset($totalUserOfflineInWebCount)
                <div class = "col-lg-3 col-6">
                    <div class = "small-box bg-danger">
                        <div class="inner">
                            <h3>{{$totalUserOfflineInWebCount}}</h3>
                            <p>Offline user count in web platform</p>
                        </div>
                    </div>
                </div>
            @endisset
    </div>
</div>
@endsection
