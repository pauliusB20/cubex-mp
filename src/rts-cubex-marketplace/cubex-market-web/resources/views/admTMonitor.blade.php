@extends( Auth::user()->role === 'admin'  ?  'layouts.admin' : 'layouts.user' )
@section('title-block') Game resources transaction window @endsection('title-block')
@section('content')
<div class = "card admin-style-1">
    <div class = "row" style="margin-top: 5px;">
        <h1 class = "card-title admin-card-title">
            <i class="ion ion-clipboard mr-1"></i>CubeMarket resource transactions
        </h1>
        <br/>
        <form action="/searchtrusers" method="POST" role="search">
            {{ csrf_field() }}
            <div class="input-group">
                <input type="text" class="form-control" name="tres"
                    placeholder="Input user nickname for resource transaction search" style="margin-bottom: 5px;"> <span class="input-group-btn">
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
                {{-- <a class="btn btn-info" href = "{{route('reset')}}">Reset</a>  --}}
            <h2>Found user transactions</h2>
            <table border="1">
                <thead>
                    <tr>
                        <th>@sortablelink('id')</th>
                        <th>From user name</th>
                        <th>To user name</th>
                        <th>@sortablelink('amount')</th>
                        <th>Resource type</th>
                        <th>Transaction type</th>

                    </tr>
                </thead>
                <tbody>
                        @foreach ($details as $ures)
                                <tr>
                                <td><b>{{$ures->id}}</b></td>
                                <td><b>{{$ures->from_username}}</b></td>
                                <td><b>{{$ures->to_username}}</b></td>
                                <td><b>{{$ures->amount}}</b></td>
                                <td><b>{{$ures->res_type}}</b></td>
                                <td><b>{{$ures->type_of_transaction}}</b></td>
                                </tr>
                        @endforeach
                       
                </tbody>
            </table>

        </div>
@else
 <div class = "card admin-items-result">
<!-- Transaction monitor code goes here -->
 <table border = "1">
   <tr>
      <td>@sortablelink('id')</td>
      <td>From user name</td>
      <td>To user name</td>
      <th>@sortablelink('amount')</th>
      <td>Resource type</td>
      <td>Transaction type</td>
   </tr>
   <!-- Code for displaying all happened transactions -->
   @php
    $fromUserName = "";
    $toUserName = "";
    $type = "";
    $amount = "";
   @endphp
   @foreach ($userResources as $ures)
    @foreach ($users as $user)
      @if ($ures->to_user_id == $user->id)
        <?php
          $toUserName = $user->nickname;
          $type = $ures->res_type;
          $amount = $ures->amount;
          $transType = $ures->type_of_transaction;
        ?>
      @endif
      @if ($ures->from_user_id == $user->id)
        <?php $fromUserName = $user->nickname; ?>
      @endif
    @endforeach
        <tr>
          <td>{{$ures->id}}</td>
          <td>{{$fromUserName}}</td>
          <td>{{$toUserName}}</td>
          <td>{{$amount}}</td>
          <td>{{$type}}</td>
          <td>{{$transType}}</td>
        </tr>
   @endforeach
   {!! $userResources->appends(request()->except('page'))->render() !!}
 </table>
 </div>
 @endif
 <div class = "card">
   <div class="row_admin_cards">
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
<div class = "card">
<!--Callender-->

@endsection
