<?php

namespace App\Http\Middleware;

use Closure;
use Illuminate\Support\Facades\Auth;

class Role
{
    /**
     * Handle an incoming request.
     *
     * @param  \Illuminate\Http\Request  $request
     * @param  \Closure  $next
     * @return mixed
     */
    public function handle($request, Closure $next)
    {
        if (!Auth::check())
            return response()->json(["result" => null,
                "success" => false,
                "error" => 'Unauthorized - not logged in!']);

        $user = Auth::user();
        if($user->isAdmin()) {
            return $next($request);
        }
        return response()->json(["result" => null,
            "success" => false,
            "error" => 'You do not have the permission to use this resource']);
    }
}
