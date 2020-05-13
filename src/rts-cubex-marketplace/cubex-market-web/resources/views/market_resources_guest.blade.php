@extends('layouts.guest')
@section('title-block') Resource market @endsection('title-block')
@section('content')
<div class = "align_of_buttons">
  <button type="button" class="btn btn-primary button_top_chose">Global offers</button>
  <button type="button" class="btn btn-primary button_top_chose button-your-offers">Your offers</button>
</div>
<div class = "card-body pb-0 market-card">
  <div class = "row d-flex align-items-stretch">
    @foreach($market_resources_items_info as $i)
        <div class = "col-lg-2 col-sm-6 space-between-cards {{$i->id}}">
            <div class = "product-card product__card" > 
              <h4>#{{$i->id}}</h4>
              <h4>Item seller: <b>{{$i->nickname}}</b></h4>
              <h4>Type of resource: <br/>{{$i->res_type}}</h4>
              <h4>Amount to sell: <br/>{{$i->amount_to_sell}}</h4>
              {{--<i data-toggle="modal" data-target="#infoModal{{$i->id}}" class="fa fa-info-circle info_icon cursor_module"></i>
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
                </div>--}}
              <img src = "{{ asset('dist/img/cube.png') }}" alt = "Item1" class = "product-card_image">
              <p class = "product-card_desc" >Time left:</p>
              <p id = "{{$i->transactions_resources_id}}">
              <script>jQuery(document).ready(function($){
                $("#{{$i->transactions_resources_id}}").countdowntimer({
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
                  url: "{{route('delete_resource_item')}}",
                  data:{market_resource_ID:"{{$i -> id}}", transactions_resources_ID:"{{$i ->transactions_resources_id}}"},
                  success: function () {
                  $( ".{{$i->id}}" ).remove();
                  },
                  error:function (data){
                    toastr.error('Ups item was not deleted from the market page', 'Inconceivable!', {timeOut: 2000});
                    //alert(data);
                  }
                  });
 	            }	
              });
              </script>
              </p>
              <strong id = "product_price" class = "product-card_price">{{$i->price}}&nbsp;<i class="fa fa-btc"></i></strong>
              <button id = "buy{{$i -> id}}" type = "button" class = "btn btn-primary buy_button">Buy</button> 
              <script>
                $(document).ready(function(){
                  $("#buy{{$i -> id}}").click(function(){ 
                    if("{{Auth::guest()}}"){
                      toastr.error('You can not buy it because you are not logged in', 'Inconceivable!', {timeOut: 2000});
                      return false;
                      //alert("You can not buy it because you are not logged in");
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
