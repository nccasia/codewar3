<?php

namespace App\Models;

use Illuminate\Database\Eloquent\Factories\HasFactory;
use Illuminate\Database\Eloquent\Model;

class EnglishGame extends Model
{
    use HasFactory;

    protected $table = 'english_game';
    protected $fillable = ['userID', 'name', 'voted', 'status'];
    public $primaryKey = 'id';
    public $timestamps = true;

    public function employees()
    {
        return $this->belongsTo(Employees::class, 'userID');
    }
}
