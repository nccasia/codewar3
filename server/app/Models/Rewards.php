<?php

namespace App\Models;

use Illuminate\Database\Eloquent\Factories\HasFactory;
use Illuminate\Database\Eloquent\Model;

class Rewards extends Model
{
    use HasFactory;

    protected $table = 'rewards';
    protected $fillable = ['name', 'image'];
    public $primaryKey = 'id';
    public $timestamps = true;

    public function opentalk()
    {
        return $this->hasMany(Opentalk::class, 'rewardID', 'id');
    }
}
