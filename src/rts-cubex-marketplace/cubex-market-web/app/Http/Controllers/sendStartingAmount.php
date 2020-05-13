<?php

namespace App\Http\Controllers;

use Illuminate\Http\Request;

class sendStartingAmount extends Controller //later change sendStartingAmountAndRespond
{
    public function sendcubes(Request $request)
    {
         //Changing the status
        $cblock = \App\CBlock::where('id', $request->id)->first();
        $cblock->status = $request->status;

        $cblock->save();

        //
        // $cblock = CBlock::where('id', $request->id)->first();
        // $cblock->status = $request->status;

        return response()->json($cblock, 200);
    }

}
