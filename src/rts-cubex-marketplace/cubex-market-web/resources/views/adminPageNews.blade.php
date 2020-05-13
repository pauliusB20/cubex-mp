@extends((!isset(Auth::user()->id))? 'layouts.guest': ((Auth::user()->role == 'admin') ? 'layouts.admin' : 'layouts.user'))
@section('title-block') Admin news posting panel @endsection('title-block')
@section('content')
<!-- <h1 class = "m-0 text-dark">
         <i class="ion ion-clipboard mr-1"></i>CubeMarket Admin news posting panel
 </h1> -->
<div class = "card">
    <div class = "col-md-6">
    <!-- Form for posting news on the CubeMarket platform -->
    <!-- NOTE: Fix css -->
        <div class="box box-primary">
            <div class="row justify-content-center">
                <div class="col-md-8">
                        <div class="card-header">News posting tool</div>
                        <!-- News form -->

                        <div class="form-group">
                            <form action = "postsmnews" method = "post" id = "news_postform">
                                <input type = "hidden"  name = "_token" value = "<?php echo csrf_token(); ?>"/>
                                <label>News title</label><br/>
                                <input type = "text" name="news_title" class = "form-control" required><br/>
                                <label>News article</label><br/>
                                    <textarea class = "textarea" name="newsarticle" col="30" form="news_postform" placeholder="Enter article here..." required></textarea><br/>
                                <input type = "submit" value = "Post" id = "newsformbtn"/>
                            </form>
                        <div>
                        <!-- NOTE: Not working yet -->
                        <script> 
                         $(document).ready(function () {                             
                            $('#newsformbtn')
                            .on('click',function() {
                                url : "{{route('newsposting')}}",
                                success : function (response) {
                                    // alert("The server says: " + response);
                                    if(response == "done")
                                    {
                                        toastr.success('Success! article was posted on the platform!\nNow players can view it.', 'Success Alert', {timeOut: 5000});
                                    }
                                }
                            });

                         });                        
                        </script>
                </div>
            </div>
        </div>
        <!-- end code for news form -->
    </div>
</div>
<!-- <div class ="card">
<h2 class = "m-0 text-dark">
    Current posted news
 </h2>
{{--@isset($adminNews)
    @foreach($adminNews as $news)
    <div class ="small-box bg-success">
        <h3>{{$news->id}}</h3><br/>
        <h1>{{$news->news_title}}</h1><br/>
        <p>{{$news->news_message}}</p><br/>
        <h3>Author: {{$news->nickname}}</h3>
        <h2>Posted article date: {{$news->posted_news_date}}</h2>
    </div>
    @endforeach
@endisset--}}
</div> -->
@endsection
