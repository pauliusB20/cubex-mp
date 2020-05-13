@extends((!isset(Auth::user()->id))? 'layouts.guest': ((Auth::user()->role == 'admin') ? 'layouts.admin' : 'layouts.user'))
@section('content')
@if (session('status'))
    <div class="alert alert-success">
        {{ session('status') }}
    </div>
@endif
<div class="container-welcome">
  <h2>Welcome to your Cube market profile</h2>
</div>
  <div class="container-middle">
      <div class="col-md-5-custom"> 
         @foreach ($acc as $a)
        <div class="profile-username text-center">{{$a -> nickname}}</div>
        <li class="list-group-item">
            <b>Role:</b> <a class="float-right">{{$a -> role}}</a>
          </li>
          <li class="list-group-item">
            <b>Wallet address:</b> <a class="float-right">{{$a -> wallet_address}}</a>
          </li>
          <li class="list-group-item">
            <b>Email:</b> <a class="float-right">{{$a -> email}}</a>
          </li>
          <li class="list-group-item">
            <b>Status:</b> <a class="float-right">{{$a -> status_in_web}}</a>
          </li>
          <li class="list-group-item">
            <b>Private key:</b> <a class="float-right">{{$a -> private_key}}</a>
          </li>
          <li class="list-group-item">
            <b>Public key:</b> <a class="float-right">{{$a -> public_key}}</a>
          </li>
        </ul>
        @endforeach
        <!-- <a href="#" class="btn btn-primary btn-block"><b>Change password</b></a> -->
      </div>
      <!-- /.card-body -->
    </div>  
    <!-- /.card -->
  </div>
@endsection
