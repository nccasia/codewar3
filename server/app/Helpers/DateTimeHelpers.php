<?php

namespace App\Helpers;
use Carbon\Carbon;

/**
 * @method static static OptionOne()
 * @method static static OptionTwo()
 * @method static static OptionThree()
 */
class DateTimeHelpers
{
    // public static function now(){
	// 	return Carbon::now();
	// }
    public static function getFirstDayOfCurrentYear() {
        $now = Carbon::now();
        return Carbon::create($now->year)->toDateTimeString();
    }

    public static function lastDayOfCurrentYear() {
        $now = Carbon::now();
        return Carbon::create($now->year, 12, 31)->toDateTimeString();
    }

    public static function now() {
        return Carbon::now()->toDateTimeString();
    }
}
