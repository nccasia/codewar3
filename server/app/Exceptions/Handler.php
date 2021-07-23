<?php

namespace App\Exceptions;

use Dingo\Api\Exception\InternalHttpException;
use http\Exception;
use Illuminate\Auth\AuthenticationException;
use Illuminate\Database\Eloquent\ModelNotFoundException;
use Illuminate\Foundation\Exceptions\Handler as ExceptionHandler;
use Illuminate\Http\Client\RequestException;
use Illuminate\Validation\ValidationException;
use Symfony\Component\HttpFoundation\Exception\BadRequestException;
use Symfony\Component\HttpFoundation\File\Exception\AccessDeniedException;
use Symfony\Component\HttpKernel\Exception\NotFoundHttpException;
use Throwable;

/**
 * This is your application's exception handler
 *
 * Class Handler
 */
class Handler extends ExceptionHandler
{
    /**
     * A list of the exception types that are not reported.
     *
     * @var array
     */
    protected $dontReport = [
        //
    ];

    /**
     * A list of the inputs that are never flashed for validation exceptions.
     *
     * @var array
     */
    protected $dontFlash = [
        'password',
        'password_confirmation',
    ];

    /**
     * Report or log an exception.
     *
     * @param  Throwable  $exception
     * @return void
     *
     * @throws \Exception
     */
    public function report(Throwable $exception)
    {
        parent::report($exception);
    }

    /**
     * Render an exception into an HTTP response.
     *
     * @param  \Illuminate\Http\Request  $request
     * @param  Throwable  $exception
     * @return \Illuminate\Http\Response
     */
    public function render($request, Throwable $exception)
    {
        return parent::render($request, $exception);
//        $response = $this->handleException($request, $exception);
//        return $response;

    }
    public function handleException($request, $exception){
        $message  = "something wrong";
        $code = '';
        $result = '';
        $success = false;
        switch (true) {
            case $exception instanceof AuthenticationException :
                $message = 'Current user did not login to the application';
                $success = false;
                $result = null;
                $code = 401;
                break;
            case $exception instanceof BadRequestException :
                $message = 'Bad request';
                $success = false;
                $result = null;
                $code = 400;
                break;
            case $exception instanceof AccessDeniedException :
                $message = 'Access is Denied';
                $success = false;
                $result = null;
                $code = 403;
                break;
            case $exception instanceof RequestException :
                $message = $exception->getMessage();
                $success = false;
                $result = null;
                $code = 500;
                break;
            default:
                $message;
                $code;
                break;
        }

        return response()->json(['error' => $message,
            'result' => $result,
            'success' => $success]);
    }
}
