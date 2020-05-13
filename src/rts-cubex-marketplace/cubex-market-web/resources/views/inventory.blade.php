@extends((!isset(Auth::user()->id))? 'layouts.guest': ((Auth::user()->role == 'admin') ? 'layouts.admin' : 'layouts.user'))
@section('title-block', 'Users inventory') 
@section('content')
<div class="container-welcome">
  <h2>Cube inventory</h2>
</div>
<!-- Main content -->
  <!-- Default box -->
    <div class="card-body pb-0 market-card">
      <div class="row  d-flex align-items-stretch">
        @foreach ($inv as $i)
        <div class="col-12 col-sm-6 col-md-4 space-between-cards d-flex align-items-stretch {{$i -> inventory_id}}">
          <div class="card bg-light">
            <div class="card-header text-muted border-bottom-0">
              {{$i -> item_name}}
            </div>
            <div class="card-body pt-0">
              <div class="row">
                <div class="col-7">
                  <h2 class="lead"><b></b></h2>
                  <p class="text-muted text-sm"><b>Level: {{$i -> level}}</b> </p>
                  <p class="text-muted text-sm"><b>Item code: {{$i -> item_code}}</b> </p>
                  <p class="text-muted text-sm"><b>Item status: {{$i -> item_status}}</b> </p>
                  <br>
                  <br>
                  <div class="modal fade" id="info{{$i->item_id}}">
                    <div class="modal-dialog">
                      <div class="modal-content bg-info">
                        <div class="modal-header">
                          <h4 class="modal-title">Item Info</h4>
                          <button type="button" class="close" data-dismiss="modal" aria-label="Close">
                            <span aria-hidden="true">&times;</span></button>
                        </div>
                        <div class="modal-body">
                          <p>Item description: {{$i -> description}}</p>
                          <p>Item classification: {{$i -> classification_name}}</p>
                          <p>Item type: {{$i -> item_type_name}}</p>
                        </div>
                        <div class="modal-body2">
                            <h5>Characteristics</h5><br />
                            @foreach($item_characteristics as $c)
                            @if ($i->item_id == $c->id)
                            <p>{{$c->characteristics_name}}: {{$c->value}}</p>
                            @endif
                            @endforeach
                        </div>
                        <div class="modal-footer justify-content-between">
                          <button type="button" class="btn btn-outline-light" data-dismiss="modal">Close</button>
                        </div>
                      </div>
                      <!-- /.modal-content -->
                    </div>
                    <!-- /.modal-dialog -->
                  </div>
                  <!-- /.modal -->
                  <button type="button" class="btn btn-info" data-toggle="modal" data-target="#info{{$i->item_id}}">More info</button>
                  <button id = "transfer{{$i->item_id}}" type="button" class="btn btn-primary">Transfer to game</button>
                  <script>
                  $(document).ready(function(){
                    $('#transfer{{$i->item_id}}').click(function(e) {
                      e.preventDefault();
                      toastr.info('Making transfer to game...', 'Information', {timeOut: 6000});
                      $.ajaxSetup({
                      headers: {
                      'X-CSRF-TOKEN': $('meta[name="csrf-token"]').attr('content')
                      }
                      });
                      $.ajax({
                      type: 'POST',
                      url: "{{route('transferToGame', $i->item_id)}}",
                      data:{itemID:"{{$i -> item_id}}", inventoryID: "{{$i -> inventory_id}}"},
                      success: function () {
                      toastr.success('Item was sent to game, check your game account', 'Success Alert', {timeOut: 5000});
                      $(".{{$i -> inventory_id}}").remove();
                      },
                      error: function(){
                      toastr.error('Something went wrong with transfering item to game', 'Inconceivable!', {timeOut: 5000});
                      }
                      });
                    });
                  });
                  </script>
                </div>
                <div class="col-5 text-center">
                  <img src="../../dist/img/preke_1.png" alt="" class="img-circle img-fluid">
                </div>
              </div>
            </div>
            <div class="card-footer">
              <div class="text-right">
                <button type="button" class="btn btn-default" data-toggle="modal" data-target="#sell{{$i->item_id}}">
                  Sell
                  </button>
                  
              </div>
            </div>
          </div>
          <!-- /.content -->
        </div>

        <div class="modal fade" id="sell{{$i->item_id}}" tabindex="-1" role="dialog" aria-labelledby="exampleModalLabel" aria-hidden="true">
          <div class="modal-dialog" role="document">
            <div class="modal-content">
              <div class="modal-header">
                <h4 class="modal-title">Sell your item</h4>
                <button type="button" class="close" data-dismiss="modal" aria-label="Close">
                  <span aria-hidden="true">×</span>
                </button>
              </div>
              <div class="modal-body">
                <br/>
                <form id = "form{{$i->item_id}}" action="" method="">
                  <div>
                    <label for="hours">Enter hours:</label>
                    <input type="text" id="hours{{$i->item_id}}" name="hours"></input>
                    <h6> Min value is 1 and max value is 168</6>
                  </div>
                  <br/>
                  <div>
                    <label for="minutes">Enter minutes:</label>
                    <input type="text" id="minutes{{$i->item_id}}" name="minutes"></input>
                    <h6> Min value is 1 and max value is 59</6>
                  </div>
                  <br/>
                  <div>
                    <label for="price">Enter selling price:</label>
                    <input type="text" id="price{{$i->item_id}}" name="price"></input>
                    <h6> Min value is 1 and max value is 99999</6>
                  </div>
                  <br/>
                </form>
              </div>
                <div class="modal-footer justify-content-between">
                    <button type="button" class="btn btn-secondary" data-dismiss="modal">Close</button>
                    <button id = "sellButton{{$i->item_id}}" type="button" class="btn btn-primary ">Sell</button>
                </div>
                <div class = "fee-text">
                <i>*Posting fee is 1 CubeCoin</i>
                </div>
                <script>
                  $(document).ready(function(){
                    $('#sellButton{{$i->item_id}}').click(function(e) {
                      e.preventDefault();
                      var hours = $("#hours{{$i->item_id}}").val();
                      var minutes = $("#minutes{{$i->item_id}}").val();
                      var price = $("#price{{$i->item_id}}").val();
                      $(".alert-danger").remove();
                      
                      if (hours.length < 1){
                        $('#hours{{$i->item_id}}').after('<div class="alert alert-danger"><ul><li>Hours field is required</li></ul></div>');
                       // alert("Hours field is required");
                      }
                      else if (!$.isNumeric(hours)){
                        $('#hours{{$i->item_id}}').after('<div class="alert alert-danger"><ul><li>Hours field should be numeric only!</li></ul></div>');
                      }
                      else if (hours < 1 || hours > 168){
                        $('#hours{{$i->item_id}}').after('<div class="alert alert-danger"><ul><li>Min value of hours is 1 and max value of hours is 168</li></ul></div>');
                       // alert("Min value of hours is 1 and max value of hours is 168");
                      }
                      else if (minutes.length < 1){
                        $('#minutes{{$i->item_id}}').after('<div class="alert alert-danger"><ul><li>Minutes field is required</li></ul></div>');
                      // alert("Minutes field is required");
                      }
                      else if(!$.isNumeric(minutes)){
                        $('#minutes{{$i->item_id}}').after('<div class="alert alert-danger"><ul><li>Minutes field should be numeric only!</li></ul></div>');
                      }
                      else if (minutes < 0 || minutes > 59){
                        $('#minutes{{$i->item_id}}').after('<div class="alert alert-danger"><ul><li>Min value of minutes is 0 and max value of minutes is 59</li></ul></div>');
                       // alert("Min value of minutes is 1 and max value of minutes is 59");
                      }
                      else if (price.length < 1){
                        $('#price{{$i->item_id}}').after('<div class="alert alert-danger"><ul><li>Price field is required</li></ul></div>');
                       //alert("Price field is required");
                      }
                      else if (!$.isNumeric(price)){
                        $('#price{{$i->item_id}}').after('<div class="alert alert-danger"><ul><li>Price field should be numeric only!</li></ul></div>'); 
                      }
                      else if (price < 1 || price > 99999){
                        $('#price{{$i->item_id}}').after('<div class="alert alert-danger"><ul><li>Min value of price is 1 and max value of price is 99999</li></ul></div>');
                      //  alert("Min value of price is 1 and max value of price is 99999");
                      }
                      else{
                      toastr.info('Sending posting fee...', 'Information', {timeOut: 23000});
                      $("#sellButton{{$i->item_id}}").html("Sending posting fee..."); 
                      $("#sellButton{{$i->item_id}}").attr("disabled", true); 
                      $.ajaxSetup({
                      headers: {
                      'X-CSRF-TOKEN': $('meta[name="csrf-token"]').attr('content')
                      }
                      });
                      $.ajax({
                      type: 'POST',
                      url: "{{route('send_fee')}}",
                      dataType: "json",
                      data:{inventoryID:"{{$i -> inventory_id}}"},
                      success: function (data) {
                        //toastr.success('Fee sent successfully! ' + data.success, 'Success Alert', {timeOut: 10000});
                        toastr.info('Making item transaction...', 'Information', {timeOut: 10000});
                        $("#sellButton{{$i->item_id}}").html("Making items transaction...");
                        $.ajax({
                            type: 'POST',
                            url: "{{route('items_market')}}",
                            data:{hours:hours, minutes:minutes, price:price, inventory_ID:"{{$i -> inventory_id}}"},
                            dataType: "json",
                            success: function () {
                            toastr.success('Fee sent successfully!', 'Success Alert', {timeOut: 5000});
                            toastr.success('Item was posted on the market!', 'Success Alert', {timeOut: 5000});
                            $( ".{{$i -> inventory_id}}" ).remove(); // removing card from users inventory
                            window.location.href = "/market";// referencing to market
                            },
                            error:function (data){
                            toastr.error('Something went wrong. ' + data.error, 'Inconceivable!', {timeOut: 5000});
                            //toastr.error('Money have been returned back', 'Inconceivable!', {timeOut: 5000});
                            $("#sellButton{{$i->item_id}}").html("Sell");
                            $("#sellButton{{$i->item_id}}").attr("disabled", false); 
                            //alert(data.error);
                            }
                          });
                      },
                      error: function (data){
                        toastr.error('Something went wrong while making CubeCoin transaction (server connection was not established!)', 'Inconceivable!', {timeOut: 5000});
                        $("#sellButton{{$i->item_id}}").html("Sell");
                        $("#sellButton{{$i->item_id}}").attr("disabled", false); 
                        //alert(data.error);
                      }
                    });
                    }
                  });
                  });
              </script>
            </div>
            <!-- /.modal-content -->
          </div>
          <!-- /.modal-dialog -->
        </div>
        @endforeach
      </div>
    </div>
@endsection