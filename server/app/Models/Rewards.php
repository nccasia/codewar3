<?php

namespace App\Models;

class Rewards extends BaseModel
{
    protected $table = 'rewards';
    protected $fillable = ['name', 'image'];
    public $primaryKey= 'id';
    public $timestamps = true;
}
