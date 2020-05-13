@extends((!isset(Auth::user()->id))? 'layouts.guest': ((Auth::user()->role == 'admin') ? 'layouts.admin' : 'layouts.user'))
@section('title-block', 'Market items') 
@section('content')
<div class = "align_of_buttons">
  <button type="button" class="btn btn-primary button_top_chose">Global offers</button>
  <button type="button" class="btn btn-primary button_top_chose button-your-offers">Your offers</button>
</div>
<div class = "card-body pb-0 market-card">
  <div class = "row d-flex align-items-stretch">
    @foreach($market_item_info as $i)
        <div class = "col-lg-2 col-sm-6 space-between-cards {{$i->id}}">
            <div class = "product-card product__card" > 
              <h4>#{{$i->id}}</h4>
              <h4>Item name: <br/>{{$i->item_name}}</h4>
              <h4>Item seller: <b>{{$i->nickname}}</b></h4>
              <i data-toggle="modal" data-target="#infoModal{{$i->id}}" class="fa fa-info-circle info_icon cursor_module"></i>
                <div class="modal fade" id="infoModal{{$i->id}}" tabindex="-1" role="dialog" aria-labelledby="exampleModalLabel" aria-hidden="true">
                  <div class="modal-dialog" role="document">
                    <div class="modal-content">
                        <div class="modal-header">
                          <h5 class="modal-title">{{$i->item_name}}</h5>
                          <button type="button" class="close" data-dismiss="modal" aria-label="Close">
                          <span aria-hidden="true">&times;</span>
                          </button>
                        </div>
                        <div class="modal-body">
                          <h5>Description</h5><br/>
                          <p>Type: {{$i->item_type_name}}</p>
                          <p>Classification: {{$i->classification_name}}</p>
                          <p>Level: {{$i->level}}</p>
                        </div>
                        <div class="modal-body2">
                        <h5>Characteristics</h5><br/>
                        @foreach($item_characteristics as $y)
                        @if ($i->itemID == $y->id)
                          <p>{{$y->characteristics_name}} = {{$y->value}}</p>
                        @endif  
                        @endforeach
                        </div>
                    </div>
                  </div>
                </div>
              <img src = "{{ asset('dist/img/cube.png') }}" alt = "Item1" class = "product-card_image">
              <p class = "product-card_desc" >Time left:</p>
              <p id = "{{$i->transaction_items_id}}">
              <script>jQuery(document).ready(function($){
                $("#{{$i->transaction_items_id}}").countdowntimer({
                dateAndTime : "{{$i->time_end}}", //time end 
                timeUp : timeIsUp 
 	            });
              function timeIsUp() {
                $.ajaxSetup({
                headers: {
                'X-CSRF-TOKEN': $('meta[name="csrf-token"]').attr('content')
                }
                });
                $.ajax({
                  type: 'POST',
                  url: "{{route('delete_item')}}",
                  data:{market_ItemID:"{{$i -> id}}", transactions_ItemsID:"{{$i ->transaction_items_id}}"},
                  success: function () {
                  $( ".{{$i->id}}" ).remove();
                  },
                  error:function (data){
                    alert(data);
                  }
                  });
 	            }	
              });
              </script>
              </p>
              <strong id = "product_price" class = "product-card_price">{{$i->price}}&nbsp;<i class="fa fa-btc"></i></strong>
              <button id = "buy{{$i -> id}}" type = "button" class = "btn btn-primary buy_button">Buy</button> 
               <!--Scriptas mygtuko paspaudimui -->
              <script>
                $(document).ready(function(){
                  $("#buy{{$i -> id}}").click(function(e){ 
                    e.preventDefault();
                    var price = "{{$i->price}}";
                    if("{{Auth::user()->role}}" == "admin"){
                      toastr.error('You can not buy it because you are admin', 'Inconceivable!', {timeOut: 2000});
                      return false;
                     // alert("You can not buy it because you are admin");
                    } 
                    else if ("{{Auth::user()}}" && "{{Auth::id()}}" == "{{$i->userID}}"){ // selleris negali pirkti pats pas save
                      toastr.error('You can not buy it because you are seller', 'Inconceivable!', {timeOut: 2000});
                      return false;
                      //alert("You can not buy because you are seller");
                    }
                    else{
                    toastr.info('Making CubeCoin transaction...', 'Information', {timeOut: 23000});  
                    $("#buy{{$i -> id}}").html("Buying...");
                    $("#buy{{$i -> id}}").attr("disabled", true);
                    $.ajaxSetup({
                    headers: {
                    'X-CSRF-TOKEN': $('meta[name="csrf-token"]').attr('content')
                    }
                    });
                    $.ajax({
                      type: 'POST',
                      url: "{{route('nem_transaction')}}",
                      data:{transactions_ItemsID:"{{$i ->transaction_items_id}}",price:"{{$i ->price}}", marketItemID:"{{$i -> id}}"},
                      success: function () {
                        //toastr.success('CubeCoin transaction successed!', 'Success Alert', {timeOut: 10000});
                        toastr.info('Making items transaction...', 'Information', {timeOut: 10000});
                        $.ajax({
                            type: 'POST',
                            url: "{{route('buy_items')}}",
                            data:{market_ItemID:"{{$i -> id}}", transactions_ItemsID:"{{$i ->transaction_items_id}}", price:"{{$i ->price}}"},
                            success: function () {
                            $( ".{{$i->id}}" ).remove();
                            toastr.success('CubeCoin transaction successed!', 'Success Alert', {timeOut: 5000});
                            toastr.success('Item is in your inventory!', 'Success Alert', {timeOut: 5000});
                            window.location.href = "/inventory";// referencing to user's inventory
                            },
                            error:function (data){
                            toastr.error('Something went wrong ' + data.error, 'Inconceivable!', {timeOut: 5000});
                            //toastr.error('Money have been returned back', 'Inconceivable!', {timeOut: 5000})
                            $("#buy{{$i -> id}}").html("Buy");
                            $("#buy{{$i -> id}}").attr("disabled", false);
                            //alert(data.error);
                            }
                          });
                      },
                      error: function (data) {
                        $("#buy{{$i -> id}}").html("Buy");
                        $("#buy{{$i -> id}}").attr("disabled", false);
                        toastr.error('Something went wrong while making CubeCoin transaction (server connection was not established!)', 'Inconceivable!', {timeOut: 5000})
                        //alert(data.error);
                      }
                    });
                    }
                });
              });
              </script>
             <!--Scriptas mygtuko paspaudimui -->
             </form>   
            </div>
        </div>
    @endforeach
  </div>  
</div>
@endsection
