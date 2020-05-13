@extends((!isset(Auth::user()->id))? 'layouts.guest': ((Auth::user()->role == 'admin') ? 'layouts.admin' : 'layouts.user'))
@section('title-block') News article viewer @endsection('title-block')
@section('content')
<section class ="content-header">
<h3 class = "admin-posted-news-header">
    <b>Your posted news articles on the platform</b>
 </h3>
</section>
 <section class="content">
@isset($adminNews)
{!! $adminNews->render() !!}
    @foreach($adminNews as $news)
  
    <!-- <table>   
        <th style="background-color:white;"><h1 style="">{{$news->id}}</h1></th>
        <th style="width:60em;"> -->
        <div class = "box box-widget">
            <div class = "box-header with-border">
                <!-- <h3>{{$news->id}}</h3> -->
                <div class="user-block">
                    <span class = "description">
                        <h2><b>{{$news->news_title}}</b></h2>
                    </span>
                    <span class="username">                    
                        Posted by {{$news->nickname}}<br/>  
                        Posted on {{$news->posted_news_date}}          
                    </span>
                </div>
                <div class="box-tools">
                    <button  button type="button" class="btn btn-box-tool bg-danger" id = "deletePostNews{{$news->id}}" name="deletenews"><i class="fa fa-times"></i></button>
                </div>
            </div>
            <div class ="box-body">   
                <div class="posted-article-btn">    
                    <button class="btn btn-info" id = "moreinfobtn{{$news->id}}" data-toggle="modal" data-target="#postWindow{{$news->id}}">More details</button>
                    <!-- Modal -->
                    <div class = "modal fade" id ="postWindow{{$news->id}}" role="dialog">
                            <div class="modal-dialog">
                                <div class="modal-content">
                                        <div class="modal-header">
                                            <h4 class = "modal-title">Posted news article information</h4>
                                            <button type = "button" class="close" data-dismiss="modal">&times</button>
                                        </div>
                                        <div class="modal-body">
                                            <p>
                                                Title: {{$news->news_title}}<br/>
                                                Posted by: {{$news->nickname}}<br/>
                                                Posted on the date: {{$news->posted_news_date}}<br/>
                                            </p>
                                            <p class="posted-article-text">
                                                {!! $news->news_message !!}
                                            </p>                                          
                                        
                                        </div>                                  
                                        <div clas ="modal-footer">
                                            <!-- <button type="button" class="btn btn-primary m-t-10" data-dismiss="modal">Close</button> -->
                                        </div>
                                    </div>
                                </div>
                            </div>


                    <!-- ./ModalEnd -->
                </div>       
            </div>
            <div class="box-footer">
            </div>
            <!-- Script -->
            <script>
            $(document).ready(function () {
                $.ajaxSetup({
                        headers: {
                        'X-CSRF-TOKEN': $('meta[name="csrf-token"]').attr('content')
                        }
                        });
                $('#deletePostNews{{$news->id}}').on('click',function() {
                    toastr.info('Trying to delete the article...', 'Information', {timeOut: 5000});
                    postId = "{{$news->id}}"
                    $.ajax({
                            url : "{{route('deletePostNews', '$news->id')}}",
                            method: 'post',
                            data: { 'id': postId },
                            success: function (data) {                            
                                if (data == "refresh"){
                                    toastr.success('Success! article was deleted!', 'Success Alert', {timeOut: 5000});
                                    window.location.reload(); // This is not jQuery but simple plain ol' JS
                                    
                                }
                            }
                        });
                        // console.log("user had pressed the button");
                        // clicked=true;
                });
            });
        </script>
            <!-- /endscript -->
        </div>
        <!-- </th>
       </tr>
    </table> -->
    @endforeach
    {!! $adminNews->render() !!}
   
@endisset
</section>
@endsection
