<?php

use Illuminate\Http\Request;
/*
|--------------------------------------------------------------------------
| Web Routes
|--------------------------------------------------------------------------
|
| Here is where you can register web routes for your application. These
| routes are loaded by the RouteServiceProvider within a group which
| contains the "web" middleware group. Now create something great!
|
*/
// Search route for users
Route::get('/searchusers/{query}', 'AdminUsersSearchController@userSearch')->name("searchusers");
// Route::any ( '/searchuser', 'AdminUsersSearchController@userSearch');
// User resources transaction engine
Route::any('/searchtrusers', 'AdminUserResourcesController@searchResources');
//Item transaction search engine
Route::any('/searchTrItemUsers', 'AdminItemSearchController@searchItems');
// History search engine
Route::any('/searchUserHistory', function (Request $req) {
    $q = $req->input('userHistory');
    //Query to select all informations about the current occured transactions
    $userHistoryRecordNames = DB::select("SELECT h.id, usersh.nickname, h.login_time, h.logout_time, h.ip, h.place FROM
                              login_history AS h, users AS usersh WHERE usersh.id = h.user_id;");
    $userHistoryRecords = array();
    //Searching inputed substring from the html search input box
    foreach ($userHistoryRecordNames as $hnames) {
        if (strpos($hnames->nickname, $q) !== false) {
            $userHistoryRecords[] = $hnames;
        } else if (strpos($hnames->login_time, $q) !== false) {
            $userHistoryRecords[] = $hnames;
        } else if (strpos($hnames->logout_time, $q) !== false) {
            $userHistoryRecords[] = $hnames;
        }
        else if (strpos($hnames->place, $q) !== false)
        {
            $userHistoryRecords[] = $hnames;
        }
    }

    if (count($userHistoryRecords) > 0)
        return view('admHMonitor')->withDetails($userHistoryRecords)->withQuery($q);
    else
        return view('admHMonitor');
});
Route::get('/', function () {
    return view('about');
});
Auth::routes();
// Reseting search box fields
Route::get('/resets', 'resetSearchFields@resetField')->name('reset');

Route::get('/home', 'HomeController@index')->name('home');

Route::get('/adminPage', 'CubeChartController@index')->name('adminPage'); //Fix the to long loading chart


Route::get('/adminPageUDB', 'displayData@loadUDBPage')->name('adminPageUDB');

Route::get('/admtm', 'displayData@loadTMonitor')->name('admtmot');

Route::get('/admtitem', 'displayData@loadTItemsMonitor')->name('admtitem');
Route::get('/admh', 'displayData@loadHistoryMonitor')->name('admh');

Route::get('/login', function () {
    return view('Auth/login');
});
Route::get('/register', function () {
    return view('Auth/register');
});

/*showing all market items*/
Route::get('/market', 'marketItemsRetrieve@market_Items');
/*showing all market resource items*/
Route::get('/marketResources','marketItemsRetrieve@resource_market_item');

/*posting item to the market*/
Route::post('/inventory', 'marketItemsRetrieve@addItemToMarket')->name('items_market');
Route::post('/feeSending', 'nemBlockChainServer@sendTransactionOfFee')->name('send_fee');

/*market items buying and offer deleting when item offer is over*/
Route::post('/buyItem', 'buyItemFromMarket@buyItemsFromMarket')->name('buy_items');
Route::post('/market', 'nemBlockChainServer@sendTransaction')->name('nem_transaction');
Route::post('/deleteMarketItem', 'buyItemFromMarket@deleteItem')->name('delete_item');

/*resource items buying and offer deleting*/
Route::post('/buyResItem', 'buyItemFromMarket@buyResourceItem')->name('buy_resource_items');
Route::post('/marketResources', 'nemBlockChainServer@sendTransactionForResource')->name('nem_transaction_for_resource');
Route::post('/deleteResourceMarketItem', 'buyItemFromMarket@deleteResourceItem')->name('delete_resource_item');

/*Showing data to user*/
Route::get('/getResources', 'nemBlockChainServer@getCubeBalance')->name('getBalance');
Route::get('/getUserEnergonBalance', 'marketItemsRetrieve@getEnergonBalance')->name('getEnergonBalance');
Route::get('/getUserCreditsBalance', 'marketItemsRetrieve@getCreditsBalance')->name('getCreditsBalance');

/*Showing data to admin (cubecoin, energon, credits)*/
Route::get('/getTotalEnergonBalance', 'marketItemsRetrieve@getMarketEnergonBalance')->name('getTotalEnergonBalance');
Route::get('/getTotalCreditsBalance', 'marketItemsRetrieve@getMarketCreditsBalance')->name('getTotalCreditsBalance');

Route::get('/about', 'HelloController@about');
Route::get('/services', 'HelloController@services');
Route::post('/insert', 'DataController@insert');
Route::get('/user', 'HelloController@user');

Auth::routes();

Route::namespace('Admin')->prefix('admin')->as('admin.')->middleware('auth')->group(function () {
    Route::resource('/categories', 'CategoriesController');
    Route::resource('/news', 'NewsController');
});

/*Unity data sending and retrival interface routes*/
/*Data retrival methods*/
Route::get('rData', 'dbInserter@retrieveUserDataBaseTable');
Route::get('resData', 'dbInserter@retrieveResourcesData');
//All user tokien balances
Route::get('userTokien', 'dbInserter@getAllUserTokienBalances');

/* Data inserting methods*/

Route::post('/create', 'dbInserter@dbTableInsert'); //For user creating from game
Route::post('/updateStatus', 'dbInserter@dbUpdateGameStatus');
Route::post('/cit', 'dbInserter@addItemToDatabase');
Route::post('/historyUpdate', 'dbInserter@updateHistoryTable');
Route::post('/sendres', 'dbInserter@dbAddResourcsFromGame'); //Sending resources from game to web method
/*Admin panel Cubex */

// Route::get('/adminPage', 'displayData@countAllRegisteredUsers') ->name('userCount');
// Route::get('getUserRes/{id}', 'displayData@retrieveUResDataBaseTable')->name('userres');

// User deleting function
Route::post('deleteUser/{id}', 'displayData@destroySpecUser')->name('deleteUser');

//Items for game retriving
Route::get('gameUserItems', 'displayData@retrieveUserItemInfo');
//Item trash transaction deleting
Route::post('deleteItemTr', 'dbInserter@deleteExistingTransactions');
//Items for game item characteristics retriving
Route::get('gitemch', 'displayData@retrieveGameItemCharacteristics');
/* Inventory*/
Route::get('/inventory', 'AccountController@inventory')->name('users_inventory');

/*Account*/
Route::get('/account', 'AccountController@account');

Route::post('/transfer/{item_id}', 'AccountController@transfer')->name('transferToGame');

/* For checking NEM data in game*/
Route::get('/nemdata', 'dbInserter@receiveNEMData');

// News posting routes
Route::get('/newspost', 'CubeNewsController@loadPage')->name('newspost');

Route::post('postsmnews', 'CubeNewsController@post')->name('newsposting');

Route::get('/postednews', 'CubeNewsController@loadPostedNewsPage')->name('postednews');

Route::post('/deleteNewsFromWeb/{id}','CubeNewsController@deletePost')->name('deletePostNews');
