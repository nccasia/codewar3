<?php

namespace App\Providers;

use App\Exceptions\ApiExceptionHandler;
use App\Helpers\Typesence;
use Illuminate\Support\ServiceProvider;
use Illuminate\Support\Facades\Schema;

class AppServiceProvider extends ServiceProvider
{
    /**
     * Register any application services.
     *
     * @return void
     */
    public function register()
    {
        $this->registerExceptionHandler();
        $this->registerTelescope();
        $this->app->bind('App\Helpers\Typesence', function ($app) {
            return new Typesence([
                'api_key' => env('TYPESENSE_SERVICE_API'),
                'nodes' => [
                    [
                        'host' => env('TYPESENSE_SERVICE_HOST'),
                        'port' => env('TYPESENSE_SERVICE_PORT'),
                        'protocol' => 'http',
                    ],
                ],
                'connection_timeout_seconds' => 5
            ]);
        });
        // $this->app->bind('App\Repositories\ContactHistory\ContactHistoryRepoInterface', 'App\Repositories\ContactHistory\ContactHistoryRepository');
    }

    /**
     * Bootstrap any application services.
     *
     * @return void
     */
    public function boot()
    {
        //
        Schema::defaultStringLength(191);
    }

    /**
     * Register the exception handler - extends the Dingo one
     *
     * @return void
     */
    protected function registerExceptionHandler()
    {
        $this->app->singleton('api.exception', function ($app) {
            return new ApiExceptionHandler($app['Illuminate\Contracts\Debug\ExceptionHandler'], Config('api.errorFormat'), Config('api.debug'));
        });
    }

    /**
     * Conditionally register the telescope service provider
     */
    protected function registerTelescope()
    {
        if ($this->app->environment('local', 'testing')) {
            $this->app->register(TelescopeServiceProvider::class);
        }
    }
}