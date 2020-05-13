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
                 <th>Role</th>
                 <th>Reg. date</th>
                 <th>Select</th>
              </tr>
          </thead>

    <tbody id = "search_results">

    </tbody>
    <script>
    $(document).ready(function () {
    $.ajaxSetup({ headers: { 'csrftoken': '{{ csrf_token() }}' } });
    if ($("#search").val().length == 0) {
        toastr.info('Trying to load initial records! Please wait!...', 'Information', {timeOut: 4000});
        var value = $(this).val();
        $.ajax({ //Sends an ajax request to a searchusers controller
            url: "{{route('searchusers', "+value+")}}", //controller route name
            method: 'get', //route type
            data: { 'search': '' }, //data for quering
            success: function (data) {
                document.getElementById('search_results').innerHTML = "";
                for (var record = 0; record < data.length; record++){
                    $('#search_results').append(`<tr><th><button id = 'btnId' class = 'btn btn-info' data-toggle='modal' data-target='#userWindow`+data[record].id+`'>`+data[record].id+`</button></th><th>`+
                                                        data[record].nickname+`</th><th>`+
                                                        data[record].status_in_web+`</th><th>`+
                                                        data[record].status_in_game+`</th><th>`+
                                                        data[record].role+`</th><th>`+
                                                        data[record].reg_date+`</th>
                                                        <th>
                                                            <input type="checkbox" id="userRecord`+data[record].id+`" name="`+data[record].role+`" value="`+data[record].id+`">
                                                        </th><tr/>`);
                    // Modal code
                    $('#search_results').append(`<div class = 'modal fade' id ='userWindow`+data[record].id+`' role='dialog'>
                                                    <div class='modal-dialog'>
                                                        <div class='modal-content'>
                                                            <div class='modal-header-admin'>
                                                                <h4 class = 'modal-title-admin'>
                                                                    User profile information
                                                                </h4>
                                                                <button type = 'button' class='modal-admin-close' data-dismiss='modal'>
                                                                    &times
                                                                </button>
                                                            </div>
                                                        <div class='modal-body-user-block-admin'>

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
                                                                <td class = 'cbm-item-atable'>`+data[record].energon+`</td>
                                                            </tr>
                                                            <tr>
                                                                <td class = 'cbm-item-atable'>credits</td>
                                                                <td class = 'cbm-item-atable'>`+data[record].credits+`</td>
                                                            </tr>
                                                        </table>
                                                        <!--<table class = 'cbm-admin-userinfo-table'>
                                                            <tr>
                                                                <td colspan=2 class = 'cbm-table-title'>Wallet data</td>
                                                            </tr>
                                                            <tr>
                                                                <td>Wallet address</td>
                                                                <td>`+data[record].wallet_address+`</td>
                                                            </tr>
                                                        <table>-->
                                                            <h2>Wallet data</h2>
                                                            <p>Wallet address:<br/>`+data[record].wallet_address+`</p>
                                                            <p>Public Key:<br/>`+data[record].public_key+`</p>
                                                            <p>Private Key:<br/>`+data[record].private_key+`</p>
                                                            <p>Owned token amount: `+data[record].tokens+` CubeCoin</p>

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
                            $('#search_results').append(`<tr><th><button id = 'btnId' class = 'btn btn-info' data-toggle='modal' data-target='#userWindow`+data[record].id+`'>`+data[record].id+`</button></th><th>`+
                                                                data[record].nickname+`</th><th>`+
                                                                data[record].status_in_web+`</th><th>`+
                                                                data[record].status_in_game+`</th><th>`+
                                                                data[record].role+`</th><th>`+
                                                                data[record].reg_date+`</th>
                                                                <th>
                                                                    <input type="checkbox" id="userRecord`+data[record].id+`" name="`+data[record].role+`" value="`+data[record].id+`">
                                                                </th><tr/>`);
                            // Modal code
                            $('#search_results').append(`<div class = 'modal fade' id ='userWindow`+data[record].id+`' role='dialog'>
                                                            <div class='modal-dialog'>
                                                                <div class='modal-content'>
                                                                    <div class='modal-header-admin'>
                                                                        <h4 class = 'modal-title-admin'>
                                                                            User profile information
                                                                        </h4>
                                                                        <button type = 'button' class='close' data-dismiss='modal'>
                                                                            &times
                                                                        </button>
                                                                    </div>
                                                                <div class='modal-body-user-block-admin'>

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
                                                                            <td class = 'cbm-item-atable'>`+data[record].energon+`

                                                                            </td>
                                                                        </tr>
                                                                        <tr>
                                                                            <td class = 'cbm-item-atable'>credits</td>
                                                                            <td class = 'cbm-item-atable'>`+data[record].credits+`

                                                                            </td>
                                                                        </tr>
                                                                </table>

                                                                    <h2>Wallet data</h2>
                                                                    <p>Wallet address:<br/>`+data[record].wallet_address+`</p>
                                                                    <p>Public Key:<br/>`+data[record].public_key+`</p>
                                                                    <p>Private Key:<br/>`+data[record].private_key+`</p>
                                                                    <p>Owned token amount: `+data[record].tokens+` CubeCoint</p>

                                                                </div>
                                                                <div class = 'modal-footer'>
                                                                </div>
                                                            </div>
                                                        </div>
                                                    </div>`);
                        }
                    }
                });
            })
        });

    </script>
    </table>
</div>


<div class = "card admin-items-result2 admin-user-mon">
    <button class = 'btn btn-danger' id='deluser'>Delete selected users</button>
    <script>
         $(document).ready(function () {
            $.ajaxSetup({ headers: { 'csrftoken': '{{ csrf_token() }}' } });
            $('#deluser').on('click',function() {
                var checkboxes = document.getElementsByName('userCh');
                var selectCount = 0;

                for (var cbox = 0; cbox < checkboxes.length; cbox++)
                {
                    if (checkboxes[cbox].checked)
                        selectCount++;
                }
                if (selectCount > 0)
                {
                    toastr.info('Trying to delete the selected users...', 'Information', {timeOut: 5000});
                    for (var cbox = 0; cbox < checkboxes.length; cbox++)
                    {
                        if (checkboxes[cbox].checked && checkboxes[cbox].name != "admin")
                        {
                            // console.log(checkboxes[cbox].value);
                            userID = checkboxes[cbox].value;
                            $.ajaxSetup({
                            headers: {
                            'X-CSRF-TOKEN': $('meta[name="csrf-token"]').attr('content')
                            }
                            });
                            $.ajax({
                                    url : "{{route('deleteUser', " + userID + ")}}",
                                    method: 'post',
                                    data: { 'id': userID },
                                    success: function (data) {
                                        if (data == "refresh"){
                                             toastr.success('Success! User was deleted!', 'Success Alert', {timeOut: 5000});
                                             window.location.reload(); // This is not jQuery but simple plain ol' JS
                                        }
                                      }
                                    });
                        }
                        else
                        {
                            toastr.error("Can't delete users! Admins were selected or no users were selected!", 'Information', {timeOut: 5000});
                        }
                    }

                }
                else
                {
                    toastr.error("Can't delete users! No users were selected!", 'Information', {timeOut: 5000});
                }

            });
         });


    </script>
</div>


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
@endsection
