<?php

namespace App\Models;

class OpenTalk extends BaseModel
{
    protected $table = 'opentalk';
    protected $fillable = ['topicName', 'rewardId', 'userID'];
    public $primaryKey= 'id';
    public $timestamps = true;

    public function employees()
    {
        return $this->belongsTo(Employees::class, 'userID');
    }

    public function rewards()
    {
        return $this->belongsTo(Rewards::class, 'rewardId');
    }
}
