<?php

use Illuminate\Database\Migrations\Migration;
use Illuminate\Database\Schema\Blueprint;
use Illuminate\Support\Facades\Schema;

class CreateEnglishGameTable extends Migration
{
    /**
     * Run the migrations.
     *
     * @return void
     */
    public function up()
    {
        Schema::table('english_game', function (Blueprint $table) {
            //
            $table->uuid('id')->primary();
            $table->string('name')->nullable(false);
            $table->float('voted')->nullable(false);
            $table->string('status')->nullable(false);
            $table->uuid('userID');
            $table->foreign('userID')->references('id')->on('employees');
            $table->timestamps();
        });
    }

    /**
     * Reverse the migrations.
     *
     * @return void
     */
    public function down()
    {
        Schema::table('english_game', function (Blueprint $table) {
            //
        });
    }
}
