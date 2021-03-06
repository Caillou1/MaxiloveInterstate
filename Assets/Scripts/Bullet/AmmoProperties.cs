﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class AmmoProperties {
    public static bool Split;
    public static bool Explosive;
    public static bool Piercing;
    public static bool Homing;

    public static bool SplitDisabled;
    public static bool ExplosiveDisabled;
    public static bool PiercingDisabled;
    public static bool HomingDisabled;

    public static float Cooldown;

    public static int ActiveProperties;

    public static bool CanShot
    {
        get
        {
            return !(SplitDisabled && ExplosiveDisabled && PiercingDisabled && HomingDisabled);
        }
    }

    public static void InitUpgrades(float defaultCooldown)
    {
        ActiveProperties = 0;

        Split = false;
        Explosive = false;
        Piercing = false;
        Homing = false;

        SplitDisabled = false;
        ExplosiveDisabled = false;
        PiercingDisabled = false;
        HomingDisabled = false;

        Cooldown = defaultCooldown;
    }
}
