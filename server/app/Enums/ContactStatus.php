<?php

namespace App\Enums;

use BenSampo\Enum\Enum;

/**
 * @method static static OptionOne()
 * @method static static OptionTwo()
 * @method static static OptionThree()
 */
final class ContactStatus extends Enum
{
    const ACTIVE = "active";
    const HOT = "hot";
    const PROSPECT = "prospect";
    const YUKON = "yukon";
    const SLEEPER = 'sleeper';
}
