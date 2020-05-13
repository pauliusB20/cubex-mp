@extends((!isset(Auth::user()->id))? 'layouts.guest': ((Auth::user()->role == 'admin') ? 'layouts.admin' : 'layouts.user'))
@section('title-block') User database manager @endsection('title-block')
@section('content')
<div class = "card admin-style-2">
    <div class = "row " style="margin-top: 5px;">
        <h1 class = "card-title admin-card-title">
            <i class="ion ion-clipboard mr-1"></i>CubeMarket registered users
        </h1>
        <br/>
        <input type="text" class="form-control" placeholder="input nickname/password/email/role/reg_date for searching the records" id="search" name="search" style="margin-bottom:5px; width:600px"></input>
    </div>
</div>

<div class="card admin-items-result2">
    <table>
         <thead>
              <tr>
                 <th>Id</th>
                 <th>Nickname</th>
                 <th>Web status</th>
                 <th>Game status</th>
                 <th>role</th>
                 <th>Reg. Date</th>
              </tr>
          </thead>

    <tbody id = "search_results">

    </tbody>
    <script>
    $(document).ready(function () {
    if ($("#search").val().length == 0) {
        var value = $(this).val();
        $.ajax({ //Sends an ajax request to a searchusers controller
            url: "{{route('searchusers', "+value+")}}", //controller route name
            method: 'get', //route type
            data: { 'search': '' }, //data for quering
            success: function (data) {
                document.getElementById('search_results').innerHTML = "";
                for (var record = 0; record < data.length; record++){
                    $('#search_results').append("<tr><th><button id = 'btnId' class = 'btn btn-info' data-toggle='modal' data-target='#userWindow"+data[record].id+"'>"+data[record].id+"</button></th><th>"+
                                                        data[record].nickname+"</th><th>"+
                                                        data[record].status_in_web+"</th><th>"+
                                                        data[record].status_in_game+"</th><th>"+
                                                        data[record].role+"</th><th>"+
                                                        data[record].reg_date+"</th><tr/>");
                    // Modal code
                    $('#search_results').append(`<div class = 'modal fade' id ='userWindow`+data[record].id+`' role='dialog'>
                                                    <div class='modal-dialog'>
                                                        <div class='modal-content'>
                                                            <div class='modal-header'>
                                                                <h4 class = 'modal-title'>
                                                                    User profile information
                                                                </h4>
                                                                <button type = 'button' class='close' data-dismiss='modal'>
                                                                    &times
                                                                </button>
                                                            </div>
                                                        <div class='modal-body'>

                                                        <p>
                                                            Nickname: `+data[record].nickname+`<br/>
                                                            Email: `+data[record].email+`<br/>
                                                            Role: `+data[record].role+`<br/>
                                                        </p>

                                                        <table class = 'cbm-admin-userinfo-table'>
                                                            <tr>
                                                                <td colspan=2 class = 'cbm-table-title'>Current user resources</td>
                                                            </tr>
                                                            <tr>
                                                                <td class = 'cbm-item-atable'>Resource type</td>
                                                                <td class = 'cbm-item-atable'>amount</td>
                                                            </tr>


                                                                <tr>
                                                                    <td class = 'cbm-item-atable'>energon</td>
                                                                    <td class = 'cbm-item-atable'>

                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td class = 'cbm-item-atable'>credits</td>
                                                                    <td class = 'cbm-item-atable'>

                                                                    </td>
                                                                </tr>
                                                        </table>

                                                            <h2>Wallet data</h2>
                                                            <p>Wallet address:<br/>`+data[record].wallet_address+`</p>
                                                            <p>Public Key:<br/>`+data[record].public_key+`</p>
                                                            <p>Private Key:<br/>`+data[record].private_key+`</p>

                                                            <button class = 'btn btn-danger' id='deluser`+data[record].id+`'>Delete user</button>


                                                        </div>
                                                        <div class = 'modal-footer'>
                                                        </div>
                                                    </div>
                                                </div>
                                            </div>`);
                }
            }
        });
    }
    $('#search').on('keyup', function () {
        var value = $(this).val();
        $.ajax({
            url : "{{route('searchusers',"+value+")}}",
            method: 'get',
            data: { 'query': value },
            success: function (data) {
                document.getElementById('search_results').innerHTML = "";
                for (var record = 0; record < data.length; record++){
                    var scriptDelete = document.createElement("script");
                    scriptDelete.type = "text/javascript";
                    scriptDelete.src = "{{ asset('dist/js/test.js') }}";

                    $('#search_results').append("<tr><th><button id = 'btnId' class = 'btn btn-info' data-toggle='modal' data-target='#userWindow"+data[record].id+"'>"+data[record].id+"</button></th><th>"+
                                                        data[record].nickname+"</th><th>"+
                                                        data[record].status_in_web+"</th><th>"+
                                                        data[record].status_in_game+"</th><th>"+
                                                        data[record].role+"</th><th>"+
                                                        data[record].reg_date+"</th><tr/>");
                    // Modal code
                    $('#search_results').append(`<div class = 'modal fade' id ='userWindow`+data[record].id+`' role='dialog'>
                                                    <div class='modal-dialog'>
                                                        <div class='modal-content'>
                                                            <div class='modal-header'>
                                                                <h4 class = 'modal-title'>
                                                                    User profile information
                                                                </h4>
                                                                <button type = 'button' class='close' data-dismiss='modal'>
                                                                    &times
                                                                </button>
                                                            </div>
                                                        <div class='modal-body'>

                                                        <p>
                                                            Nickname: `+data[record].nickname+`<br/>
                                                            Email: `+data[record].email+`<br/>
                                                            Role: `+data[record].role+`<br/>
                                                        </p>

                                                        <table class = 'cbm-admin-userinfo-table'>
                                                            <tr>
                                                                <td colspan=2 class = 'cbm-table-title'>Current user resources</td>
                                                            </tr>
                                                            <tr>
                                                                <td class = 'cbm-item-atable'>Resource type</td>
                                                                <td class = 'cbm-item-atable'>amount</td>
                                                            </tr>


                                                               <tr>
                                                                    <td class = 'cbm-item-atable'>energon</td>
                                                                    <td class = 'cbm-item-atable'>

                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td class = 'cbm-item-atable'>credits</td>
                                                                    <td class = 'cbm-item-atable'>

                                                                    </td>
                                                                </tr>
                                                        </table>

                                                            <h2>Wallet data</h2>
                                                            <p>Wallet address:<br/>`+data[record].wallet_address+`</p>
                                                            <p>Public Key:<br/>`+data[record].public_key+`</p>
                                                            <p>Private Key:<br/>`+data[record].private_key+`</p>

                                                            <button class = 'btn btn-danger' id='deluser`+data[record].id+`'>Delete user</button>
                                                            <h1 id ="testtext"></h1>

                                                        </div>
                                                        <div class = 'modal-footer'>
                                                        </div>
                                                    </div>
                                                </div>
                                            </div>`);
                            // $('#search_results').appendChild(scriptDelete);

                        }

                    }
                });
            })
        });
        $.ajaxSetup({ headers: { 'csrftoken': '{{ csrf_token() }}' } });
    </script>
    </table>
</div>


<!-- <div class = "card admin-items-result2 admin-user-mon">
    <form action = "deleteUser" method = "post">
      <input type = "hidden"  name = "_token" value = "<?php echo csrf_token(); ?>"/>
      <h3 class = "style-for-form">Delete user id:</<h3>  <input type = "text" name="del_id"/>
      <input type = "submit" value = "Delete user"/>
     </form>
</div> -->


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
