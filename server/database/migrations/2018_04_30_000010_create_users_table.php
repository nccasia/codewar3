<?php

use Illuminate\Database\Migrations\Migration;
use Illuminate\Database\Schema\Blueprint;
use Illuminate\Support\Facades\Schema;

class CreateUsersTable extends Migration
{
    const TABLE_NAME = 'users';

    /**
     * Run the migrations.
     *
     * @return void
     */
    public function up()
    {
        Schema::create(static::TABLE_NAME, function (Blueprint $table) {
            $table->uuid('user_id');
//            $table->string('name')->nullable();
            $table->string('email')->nullable()->unique();
            $table->timestamp('email_verified_at')->nullable();
            $table->string('password');
            $table->uuid('primary_role')->nullable();
            $table->foreign('primary_role')->references('role_id')->on('roles');
            $table->primary('user_id');
            $table->string('user_name')->nullable(false);
            $table->string('full_name')->nullable(false);
            $table->timestamp('birthday')->nullable();
            $table->string('gender')->nullable();
            $table->string('phone_number')->nullable();
            $table->string('profile_picture')->nullable();
            $table->string('position')->nullable();
            $table->boolean('is_active')->default(true);
            $table->softDeletes();

            $table->rememberToken();
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
        Schema::dropIfExists(static::TABLE_NAME);
    }
}
