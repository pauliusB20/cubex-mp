<?php

namespace App\Http\Controllers;

use Illuminate\Http\Request;
use App\Category;
use Illuminate\Support\Facades\Auth;

class CategoriesController extends Controller
{
    public function __construct()
    {
        $this->middleware('auth');
    }
    public function index()
    {
        if (Auth::user()->role === 'admin') {
            $arr['categories'] = Category::all();
            return view('admin.categories.index')->with($arr);
        } else {
            return redirect()->route('login')->with('status', 'Please login first!');
        }
    }
    public function detail($id)
    {
        $cat = Category::find($id);
        echo $cat->title;
    }
}
