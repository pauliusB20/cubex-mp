@extends((!isset(Auth::user()->id))? 'layouts.guest': ((Auth::user()->role == 'admin') ? 'layouts.admin' : 'layouts.user'))

@section('content')
<div class = "card admin-style-1">
    <div class = "row" style="margin-top: 5px;">
        <h1 class = "card-title admin-card-title">
            <i class="ion ion-clipboard mr-1"></i>User login history monitor in the CubeMarket platform<br/>and in the Cubex game
        </h1>
        <br/>
        <form action="/searchUserHistory" method="POST" role="search">
            {{ csrf_field() }}
            <div class="input-group">
                <input type="text" class="form-control" name="userHistory"
                    placeholder="Input nickname for login history search" style="margin-bottom: 5px;"> <span class="input-group-btn">
                    <button type="submit" class="btn btn-navbar" style = "margin-left:5px">
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
            <h2>Found user history records</h2>
            <table border="1">
                <thead>
                    <tr>
                        <td>h_id</td>
                        <td>user nickname</td>
                        <td>Login time</td>
                        <td>Logout time</td>
                        <td>Ip</td>
                        <td>User place</td>
                    </tr>
                </thead>
                <tbody>
                   @foreach ($details as $history)
                              <tr>
                                <td><b>{{$history->id}}</b></td>
                                <td><b>{{$history->nickname}}</b></td>
                                <td><b>{{$history->login_time}}</b></td>
                                <td><b>{{$history->logout_time}}</b></td>
                                <td><b>{{$history->ip}}</b></td>
                                <td><b>{{$history->place}}</b></td>
                              </tr>
                    @endforeach
                </tbody>
            </table>

        </div>
@endif
<div class = "card admin-items-result">
<!-- History monitor code goes here -->
 <table border = "1">
   <tr>
      <td>h_id</td>
      <td>registered username</td>
      <td>Login time</td>
      <td>Logout time</td>
      <td>Ip</td>
      <td>User place</td>
   </tr>
   <!-- Code for displaying all happened transactions -->
   @php
    $usernickname = ""
   @endphp
   @foreach (\App\login_history::all() as $h)
    @foreach (\App\User::all() as $user)
        @if ($h->user_id == $user->id)
         <?php $usernickname = $user->nickname; ?>
        @endif
    @endforeach
    <tr>
        <td>{{$h->id}}</td>
        <td>{{$usernickname}}</td>
        <td>{{$h->login_time}}</td>
        <td>{{$h->logout_time}}</td>
        <td>{{$h->ip}}</td>
        <td>{{$h->place}}</td>
    </tr>
   @endforeach

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
<!--Callender-->

@endsection
