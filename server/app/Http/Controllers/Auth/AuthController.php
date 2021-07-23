<?php

namespace App\Http\Controllers\Auth;

use App\Http\Controllers\Controller;
use Illuminate\Http\JsonResponse;
use Illuminate\Http\Request;
use Illuminate\Support\Facades\Auth;

use Illuminate\Support\Facades\Hash;
use Specialtactics\L5Api\Http\Controllers\Features\JWTAuthenticationTrait;
use Symfony\Component\HttpKernel\Exception\UnauthorizedHttpException;

class AuthController extends Controller
{
    use JWTAuthenticationTrait;

    public function login(Request $request)
    {
        $authHeader = $request->header('Authorization');

        // Get for Auth Basic
        if (strtolower(substr($authHeader, 0, 5)) !== 'basic') {
            throw new UnauthorizedHttpException('Invalid authorization header, should be type basic');
        }

        // Get credentials
        $credentials = base64_decode(trim(substr($authHeader, 5)));

        [$login, $password] = explode(':', $credentials, 2);

        // Do auth
        if (!$token = auth()->attempt(['user_name' => $login, 'password' => $password])) {
            return response()->json([
                "result" => null,
                "success" => false,
                "error" => "Username or password is incorrect"
            ]);
        }
        $tokenReponse = new \Stdclass;

        $tokenReponse->jwt = $token;
        $tokenReponse->token_type = 'bearer';
        $tokenReponse->expires_in = auth()->factory()->getTTL();
        $user = Auth::user();
        $response = new JsonResponse([
            'results' => [
                'accessToken' => $tokenReponse->jwt,
                'expireInSeconds' => $tokenReponse->expires_in,
                'userId' => $user->user_id
            ],
            "success" => true,
            "error" => null
        ]);

        return $response;
    }

    public function logout()
    {
        auth()->logout();

        return response()->json([
            "success" => true,
            "message" => "successful logout!"
        ]);
    }

    public function changePassword(Request $request)
    {
        $request->validate([
            'password' => 'required|string|min:6|confirmed',
            'password_confirmation' => 'required',
        ]);

        $user = Auth::user();

        if (is_null($user)) {
            return response()->json([
                "success" => false,
                "message" => "Please login again to access this!"
            ]);
        }

        $user->password = Hash::make($request->password);
        $user->save();

        return response()->json([
            "success" => true,
            "message" => "Password change successfully.!"
        ]);
    }
}
