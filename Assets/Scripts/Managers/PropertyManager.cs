using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class PropertyManager : MonoBehaviour
{
    public float[] Cooldowns;

    public Toggle[] toggles;
    private WeaponCooldown[] wcToggles;

    public Text[] Numbers;

    private void Awake()
    {
        wcToggles = new WeaponCooldown[toggles.Length];
        for(int i=0; i<wcToggles.Length; i++)
        {
            wcToggles[i] = toggles[i].GetComponent<WeaponCooldown>();
        }
        AmmoProperties.InitUpgrades(Cooldowns[0]);
        UpdateToggles();
    }

    public void AddProperty(Power power)
    {
        AmmoProperties.AddProperty(power);
        UpdateToggles();
    }

    public void EnableSplit(bool b) {
        AmmoProperties.Split = b;
        AmmoProperties.UseProperty(Power.Split);
        UpdateToggles();
    }

    public void EnableExplosive(bool b) {
        AmmoProperties.Explosive = b;
        AmmoProperties.UseProperty(Power.Explosive);
        UpdateToggles();
    }

    public void EnableHoming(bool b) {
        AmmoProperties.Homing = b;
        AmmoProperties.UseProperty(Power.Homing);
        UpdateToggles();
    }

    public void EnablePiercing(bool b) {
        AmmoProperties.Piercing = b;
        AmmoProperties.UseProperty(Power.Piercing);
        UpdateToggles();
    }

    private void UpdateToggles()
    {
        //Numbers[0].text = ""+AmmoProperties.SplitNb;
        //Numbers[1].text = ""+AmmoProperties.ExplosiveNb;
        //Numbers[2].text = ""+AmmoProperties.PiercingNb;
        //Numbers[3].text = ""+AmmoProperties.HomingNb;

        if (AmmoProperties.SplitNb > 0)
        {
            wcToggles[0].Enable();
        }
        else
        {
            wcToggles[0].Disable();
        }

        if (AmmoProperties.ExplosiveNb > 0)
        {
            wcToggles[1].Enable();
        }
        else
        {
            wcToggles[1].Disable();
        }

        if (AmmoProperties.PiercingNb > 0)
        {
            wcToggles[2].Enable();
        }
        else
        {
            wcToggles[2].Disable();
        }

        if (AmmoProperties.HomingNb > 0)
        {
            wcToggles[3].Enable();
        }
        else
        {
            wcToggles[3].Disable();
        }
    }

    public void ChangeActiveProperty(bool b)
    {
        AmplitudeData.Instance.NumberOfTouchesOnWeaponUI++;
        if (b)
        {
            AmmoProperties.ActiveProperties++;
            SoundManager.Instance.PlayAccept();
        }
        else
        {
            AmmoProperties.ActiveProperties--;
            SoundManager.Instance.PlayCancel();
        }
        //AmmoProperties.Cooldown = Cooldowns[AmmoProperties.ActiveProperties];
    }

    public void DisableToggles(float time)
    {
        StopAllCoroutines();

        if (AmmoProperties.Split)
        {
            toggles[0].interactable = false;
        }
        if (AmmoProperties.Explosive)
        {
            toggles[1].interactable = false;
        }
        if (AmmoProperties.Piercing)
        {
            toggles[2].interactable = false;
        }
        if (AmmoProperties.Homing)
        {
            toggles[3].interactable = false;
        }

        StartCoroutine(Cooldown(time));
    }

    IEnumerator Cooldown(float time)
    {
        yield return new WaitForSeconds(time);
        foreach(var t in toggles) t.interactable = true;
    }
}
