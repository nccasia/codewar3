<?php

namespace App\Models;

class EnglishGame extends BaseModel
{
    protected $table = 'english_game';
    protected $fillable = ['userID', 'name', 'voted', 'status'];
    public $primaryKey= 'id';
    public $timestamps = true;

    public function employees()
    {
        return $this->hasMany(Employees::class, 'userID');
    }
}
