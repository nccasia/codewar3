<?php

namespace App\Enums;

use BenSampo\Enum\Enum;

/**
 * @method static static OptionOne()
 * @method static static OptionTwo()
 * @method static static OptionThree()
 */
final class OfferStatus extends Enum
{
    const HOT = "hot";
    const BORN = "born";
    const ACTIVE = "active";
    const BACKBURNER = "backburner";
    const EXPIRED = "expired";
    const REJECTED = "rejected";
}
