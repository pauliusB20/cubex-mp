@extends((!isset(Auth::user()->id))? 'layouts.guest': ((Auth::user()->role == 'admin') ? 'layouts.admin' : 'layouts.user'))
@section('title-block') User database manager @endsection('title-block')
@section('content')
<div class = "card admin-style-2">
    <div class = "row " style="margin-top: 5px;">
        <h1 class = "card-title admin-card-title">
            <i class="ion ion-clipboard mr-1"></i>CubeMarket registered users
        </h1>
        <br/>
        <input type="text" class="form-control" placeholder="input nickname/email/role/reg_date for searching the records" id="search" name="search" style="margin-bottom:5px; width:600px"></input>
    </div>
</div>

<div class="card admin-items-result2">
    <table>
         <thead>
              <tr>
                 <th>@sortablelink('id')</th>
                 <th>@sortablelink('nickname')</th>
                 <th>Web status</th>
                 <th>Game status</th>
                 <th>role</th>
                 <th>@sortablelink('reg_date')</th>
              </tr>
          </thead>
    <tbody>

    </tbody>
    </table>
</div>



<div class = "card admin-items-result2 admin-user-mon">
    <form action = "deleteUser" method = "post">
      <input type = "hidden"  name = "_token" value = "<?php echo csrf_token(); ?>"/>
      <h3 class = "style-for-form">Delete user id:</<h3>  <input type = "text" name="del_id"/>
      <input type = "submit" value = "Delete user"/>
     </form>
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
