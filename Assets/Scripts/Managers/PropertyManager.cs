using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class PropertyManager : MonoBehaviour
{
    public float[] SplitFragments;
    public float[] ExplosiveRadius;
    public float[] PiercingNumber;
    public float[] HomingRadius;

    private static int SplitLevel;
    private static int ExplosiveLevel;
    private static int PiercingLevel;
    private static int HomingLevel;

    public float[] Cooldowns;

    public Toggle[] toggles;

    private void Awake()
    {
        AmmoProperties.InitUpgrades(Cooldowns[0]);
    }

    public void UpgradeSplit()
    {
        Debug.Log("Not implemented");
    }

    public void UpgradeExplosive()
    {
        Debug.Log("Not implemented");
    }

    public void UpgradePiercing()
    {
        Debug.Log("Not implemented");
    }

    public void UpgradeHoming()
    {
        Debug.Log("Not implemented");
    }

    public void EnableSplit(bool b) {
        AmmoProperties.Split = b;
    }

    public void EnableExplosive(bool b) {
        AmmoProperties.Explosive = b;
    }

    public void EnableHoming(bool b) {
        AmmoProperties.Homing = b;
    }

    public void EnablePiercing(bool b) {
        AmmoProperties.Piercing = b;
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
        AmmoProperties.Cooldown = Cooldowns[AmmoProperties.ActiveProperties];
    }

    public void DisableToggles(float time)
    {
        StopAllCoroutines();

        if (AmmoProperties.Split)
        {
            toggles[0].interactable = false;
            toggles[0].transform.GetChild(0).GetComponent<ScaleModulator>().TriggerSwell(AmmoProperties.Cooldown);
        }
        if (AmmoProperties.Explosive)
        {
            toggles[1].interactable = false;
            toggles[1].transform.GetChild(0).GetComponent<ScaleModulator>().TriggerSwell(AmmoProperties.Cooldown);
        }
        if (AmmoProperties.Piercing)
        {
            toggles[2].interactable = false;
            toggles[2].transform.GetChild(0).GetComponent<ScaleModulator>().TriggerSwell(AmmoProperties.Cooldown);
        }
        if (AmmoProperties.Homing)
        {
            toggles[3].interactable = false;
            toggles[3].transform.GetChild(0).GetComponent<ScaleModulator>().TriggerSwell(AmmoProperties.Cooldown);
        }

        StartCoroutine(Cooldown(time));
    }

    IEnumerator Cooldown(float time)
    {
        yield return new WaitForSeconds(time);
        foreach(var t in toggles) t.interactable = true;
    }
}
