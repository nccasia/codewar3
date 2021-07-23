<?php

namespace App\Models;

class Employees extends BaseModel
{
    protected $table = 'employees';
    protected $fillable = ['fullName', 'phone', 'image', 'funnyImage'];
    public $primaryKey= 'id';
    public $timestamps = true;
}
