using System.Collections;
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

    public static int SplitNb;
    public static int ExplosiveNb;
    public static int PiercingNb;
    public static int HomingNb;

    public static float Cooldown;

    public static int ActiveProperties;

    public static void AddProperty(Power power)
    {
        switch (power)
        {
            case Power.Explosive:
                AmmoProperties.ExplosiveNb++;
                break;
            case Power.Homing:
                AmmoProperties.HomingNb++;
                break;
            case Power.Piercing:
                AmmoProperties.PiercingNb++;
                break;
            case Power.Split:
                AmmoProperties.SplitNb++;
                break;
        }
    }

    public static void UseProperty(Power power)
    {
        switch (power)
        {
            case Power.Explosive:
                AmmoProperties.ExplosiveNb--;
                break;
            case Power.Homing:
                AmmoProperties.HomingNb--;
                break;
            case Power.Piercing:
                AmmoProperties.PiercingNb--;
                break;
            case Power.Split:
                AmmoProperties.SplitNb--;
                break;
        }
    }

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

        SplitNb = 0;
        ExplosiveNb = 0;
        PiercingNb = 0;
        HomingNb = 0;

        Cooldown = defaultCooldown;
    }
}
