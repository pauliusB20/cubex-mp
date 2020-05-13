<?php

namespace App\Http\Controllers;

use Illuminate\Http\Request;
use Carbon\Carbon;
use Auth;
class CubeNewsController extends AdminBaseController
{
    public function __construct()
    {

        parent::__construct();
    }
    public function loadPage()
    {
        if (Auth::user()->role === 'admin') {
         return view('adminPageNews');
        }
        else
        {
            return back()->with('status', 'You need to be an admin to enter!');
        }
    }
    public function loadPostedNewsPage()
    {
        if (Auth::user()->role === 'admin') {
            return view('adminPagePostedNews');
           }
           else
           {
               return back()->with('status', 'You need to be an admin to enter!');
           } 
    }
    public function deletePost(Request $req) //For deleting posted admin news
    {
        if($req->ajax())
        {
            $postId = $req->input('id');
            $deletetion = \App\CubeNews::where('id', $postId)->delete();
            if($deletetion)
            {
                return "refresh";
            }
            else
            {
                return "notrefresh";
            }
        }
    }
    public function post(Request $request)
    {
        if (Auth::user()->role === 'admin') {
            $newsTitle = $request->input('news_title');
            $newsArticle = $request->input('newsarticle');
            $date=Carbon::now()->toDateTimeString();

            $dataToInsert = \App\CubeNews::create([
                "user_id" => Auth::id(),
                "news_title" => $newsTitle,
                "news_message" => $newsArticle,
                "posted_news_date" => $date
                ]);

            if(!empty($dataToInsert))
            {
                echo "done";
                // Later use Toast
                return redirect()->route('postednews');
            }
        }

    }
}
