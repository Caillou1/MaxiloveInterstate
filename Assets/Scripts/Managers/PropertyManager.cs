using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;


public class PropertyManager : MonoBehaviour
{
    private RectTransform[] rectToggles;
    public float[] Cooldowns;

    public Toggle[] toggles;
    private WeaponCooldown[] wcToggles;

    public Text[] Numbers;

    private bool[] isUp;

    private void Start()
    {
        rectToggles = new RectTransform[toggles.Length];
        isUp = new bool[4];
        wcToggles = new WeaponCooldown[toggles.Length];
        for(int i=0; i<wcToggles.Length; i++)
        {
            wcToggles[i] = toggles[i].GetComponent<WeaponCooldown>();
            rectToggles[i] = toggles[i].GetComponent<RectTransform>();
            isUp[i] = false;
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

    public void ScaleToggle(float scale)
    {
        if(AmmoProperties.SplitNb > 0 || rectToggles[0].localScale.x > .5f)
        {
            DOVirtual.Float(rectToggles[0].localScale.y, scale, .25f * Time.timeScale, (float y) => rectToggles[0].localScale = new Vector3(y, y, 0));
        }

        if(AmmoProperties.ExplosiveNb > 0 || rectToggles[1].localScale.x > .5f)
        {
            DOVirtual.Float(rectToggles[1].localScale.y, scale, .25f * Time.timeScale, (float y) => rectToggles[1].localScale = new Vector3(y, y, 0));
        }

        if(AmmoProperties.PiercingNb > 0 || rectToggles[2].localScale.x > .5f)
        {
            DOVirtual.Float(rectToggles[2].localScale.y, scale, .25f * Time.timeScale, (float y) => rectToggles[2].localScale = new Vector3(y, y, 0));
        }

        if(AmmoProperties.HomingNb > 0 || rectToggles[3].localScale.x > .5f)
        {
            DOVirtual.Float(rectToggles[3].localScale.y, scale, .25f * Time.timeScale, (float y) => rectToggles[3].localScale = new Vector3(y, y, 0));
        }
    }

    public void MoveToggle(float move)
    {
        if (AmmoProperties.SplitNb > 0 || rectToggles[0].anchoredPosition.y > -70)
        {
            DOVirtual.Float(rectToggles[0].anchoredPosition.y, move, .25f * Time.timeScale, (float y) => rectToggles[0].anchoredPosition = new Vector3(rectToggles[0].anchoredPosition.x, y, 0));
        }

        if (AmmoProperties.ExplosiveNb > 0 || rectToggles[1].anchoredPosition.y > -70)
        {
            DOVirtual.Float(rectToggles[1].anchoredPosition.y, move, .25f * Time.timeScale, (float y) => rectToggles[1].anchoredPosition = new Vector3(rectToggles[1].anchoredPosition.x, y, 0));
        }

        if (AmmoProperties.PiercingNb > 0 || rectToggles[2].anchoredPosition.y > -70)
        {
            DOVirtual.Float(rectToggles[2].anchoredPosition.y, move, .25f * Time.timeScale, (float y) => rectToggles[2].anchoredPosition = new Vector3(rectToggles[2].anchoredPosition.x, y, 0));
        }

        if (AmmoProperties.HomingNb > 0 || rectToggles[3].anchoredPosition.y > -70)
        {
            DOVirtual.Float(rectToggles[3].anchoredPosition.y, move, .25f * Time.timeScale, (float y) => rectToggles[3].anchoredPosition = new Vector3(rectToggles[3].anchoredPosition.x, y, 0));
        }
    }

    public void TriggerSwell(bool trigger)
    {
        if (trigger)
        {
            MoveToggle(0f);
            ScaleToggle(1f);
        }
        else
        {
            MoveToggle(-70f);
            ScaleToggle(.5f);
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
