<?php

namespace App\Models;

use Illuminate\Database\Eloquent\Factories\HasFactory;
use Illuminate\Database\Eloquent\Model;

class Employees extends Model
{
    use HasFactory;

    protected $table = 'employees';
    protected $fillable = ['fullName', 'phone', 'image', 'funnyImage'];
    public $primaryKey = 'id';
    public $timestamps = true;

    public function opentalk()
    {
        return $this->hasMany(Opentalk::class, 'userID');
    }

    public function englishGame()
    {
        return $this->hasMany(EnglishGame::class, 'userID');
    }
}
