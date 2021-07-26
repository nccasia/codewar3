<?php

use Illuminate\Database\Migrations\Migration;
use Illuminate\Database\Schema\Blueprint;
use Illuminate\Support\Facades\Schema;

class CreateOpentalksTable extends Migration
{
    /**
     * Run the migrations.
     *
     * @return void
     */
    public function up()
    {
        Schema::create('opentalks', function (Blueprint $table) {
            $table->uuid('id')->primary();
            $table->string('topicName')->nullable(false);
            $table->uuid('userID');
            $table->foreign('userID')->references('id')->on('employees');
            $table->uuid('rewardID');
            $table->foreign('rewardID')->references('id')->on('rewards');
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
        Schema::dropIfExists('opentalks');
    }
}
