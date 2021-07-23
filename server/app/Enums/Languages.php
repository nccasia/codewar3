<?php

namespace App\Enums;

use BenSampo\Enum\Enum;

/**
 * @method static static OptionOne()
 * @method static static OptionTwo()
 * @method static static OptionThree()
 */
final class Languages extends Enum
{
    const EN = "English";
    const GE = "German";
    const VI = "Vietnamese";
    const JA = "Japanese";
}
