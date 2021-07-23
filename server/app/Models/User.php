<?php

namespace App\Models;

use App\Models\Role;
use App\Models\BussinessUnit;
use Hash;
use Illuminate\Auth\Authenticatable;
use Illuminate\Auth\Passwords\CanResetPassword;
use Illuminate\Contracts\Auth\Access\Authorizable as AuthorizableContract;
use Illuminate\Contracts\Auth\Authenticatable as AuthenticatableContract;
use Illuminate\Contracts\Auth\CanResetPassword as CanResetPasswordContract;
use Illuminate\Foundation\Auth\Access\Authorizable;
use Illuminate\Notifications\Notifiable;
use Tymon\JWTAuth\Contracts\JWTSubject;
use Illuminate\Database\Eloquent\SoftDeletes;

class User extends BaseModel implements
    AuthenticatableContract,
    AuthorizableContract,
    CanResetPasswordContract,
    JWTSubject
{
    use Authenticatable, Authorizable, CanResetPassword, Notifiable,SoftDeletes;
    /**
     * @var int Auto increments integer key
     */
    public $primaryKey = 'user_id';

    /**
     * @var array Relations to load implicitly by Restful controllers
     */
    public static $localWith = ['primaryRole', 'roles'];

    /**
     * The attributes that are mass assignable.
     *
     * @var array
     */
    protected $fillable = [
        'user_name', 'first_name', 'second_name', 'email', 'password', 'primary_role', 'birthday', 'gender', 'phone_number', 'profile_picture', 'business_unit_id', 'linked_in_url', 'is_active'
    ];

    /**
     * The attributes that should be hidden for arrays and API output
     *
     * @var array
     */
    protected $hidden = [
        'password', 'remember_token', 'email_verified_at', 'primary_role',
    ];

    /**
     * The attributes that should be cast to native types.
     *
     * @var array
     */
    protected $casts = [
        'email_verified_at' => 'datetime',
    ];

    /**
     * Model's boot function
     */
    public static function boot()
    {
        parent::boot();

        static::saving(function (self $user) {
            // Hash user password, if not already hashed
            if (Hash::needsRehash($user->password)) {
                $user->password = Hash::make($user->password);
            }
        });
    }

    /**
     * Return the validation rules for this model
     *
     * @return array Rules
     */
    public function getValidationRules()
    {
        return [
            'email' => 'email|max:255|unique:users',
            'password' => 'required|min:6',
        ];
    }

    /**
     * User's primary role
     *
     * @return \Illuminate\Database\Eloquent\Relations\belongsTo
     */
    public function primaryRole()
    {
        return $this->belongsTo(Role::class, 'primary_role');
    }

    /**
     * User's secondary roles
     *
     * @return \Illuminate\Database\Eloquent\Relations\belongsToMany
     */
    public function roles()
    {
        return $this->belongsToMany(Role::class, 'user_roles', 'user_id', 'role_id');
    }

    public function bussinessUnit()
    {
        return $this->belongsTo(BussinessUnit::class, 'business_unit_id');
    }
    public function userStar()
    {
        return $this->belongsToMany(Offer::class, 'user_stars', 'user_id', 'offer_id');
    }

    public function userKnows()
    {
        return $this->belongsToMany(Contact::class, 'user_knows', 'user_id', 'contact_id');
    }

    public function ownerContacts()
    {
        return $this->hasMany(Contact::class, 'owner_user_id', 'user_id');
    }

    public function offerAuthors()
    {
        return $this->belongsToMany(Offer::class, 'offer_authors', 'user_id', 'offer_id');
    }

    public function activityOffers() {
        return $this->hasMany(Offer::class,'creator_id', 'user_id')
            ->where('offers.status','=',3)
            ->orderBy('created_at','desc');
    }
    public function hotOffers() {
        return $this->hasMany(Offer::class,'creator_id', 'user_id')
            ->where('offers.status','=',2)
            ->orderBy('created_at','desc');
    }

    public function notifications()
    {
        return $this->hasMany(Notification::class, 'user_id','user_id')
            ->orderBy('created_at','desc');
    }

    public function heartBeats()
    {
        return $this->belongsToMany(HeartBeat::class, 'heart_beat_users', 'user_id', 'heart_beat_id');
    }

    /**
     * Get all user's roles
     */
    public function getRoles()
    {
        $allRoles = array_merge(
            [
                $this->primaryRole->name,
            ],
            $this->roles->pluck('name')->toArray()
        );

        return $allRoles;
    }

    /**
     * Is this user an admin?
     *
     * @return bool
     */
    public function isAdmin()
    {
        return $this->primaryRole->name == Role::ROLE_ADMIN;
    }

    /**
     * For Authentication
     * Get the identifier that will be stored in the subject claim of the JWT.
     *
     * @return mixed
     */
    public function getJWTIdentifier()
    {
        return $this->getKey();
    }

    /**
     * For Authentication
     * Return a key value array, containing any custom claims to be added to the JWT.
     *
     * @return array
     */
    public function getJWTCustomClaims()
    {
        return [
            'user' => [
                'id' => $this->getKey(),
                'name' => $this->user_name,
                'roleId' => $this->primaryRole->name,
            ],
        ];
    }

    /**
     * Get the name of the unique identifier for the user.
     *
     * @return string
     */
    public function getAuthIdentifierName()
    {
        return $this->getKeyName();
    }
}
