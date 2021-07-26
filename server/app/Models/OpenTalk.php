<?php

namespace App\Models;

use Illuminate\Database\Eloquent\Factories\HasFactory;
use Illuminate\Database\Eloquent\Model;

class Opentalk extends Model
{
    use HasFactory;

    protected $table = 'opentalk';
    protected $fillable = ['topicName', 'rewardID', 'userID'];
    public $primaryKey = 'id';
    public $timestamps = true;

    public function employees()
    {
        return $this->belongsTo(Employees::class, 'userID');
    }

    public function rewards()
    {
        return $this->belongsTo(Rewards::class, 'rewardID');
    }
}
