using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class WeaponCooldown : MonoBehaviour {

	Toggle toggle;

	Image img;

    public Sprite Available;
    public Sprite AvailablePressed;

    public Sprite Toggled;
    public Sprite ToggledPressed;

    private Sprite Disabled;
    private Sprite DisabledPressed;

    private bool isDisabled = true;
    private bool isAvailable = false;
    private bool isToggled = false;

    private ScaleModulator scaleMod;

    public Power power;

    void Awake(){
        scaleMod = GetComponentInChildren<ScaleModulator>();
		toggle = GetComponent<Toggle>();
		img = GetComponent<Image>();

        Disabled = img.sprite;
        DisabledPressed = toggle.spriteState.pressedSprite;
	}

    public void Disable()
    {
        toggle.interactable = false;
        isDisabled = true;
        isAvailable = false;
        isToggled = false;
        UpdateSprite();
    }

    public void Enable()
    {
        toggle.interactable = true;
        isDisabled = false;
        isAvailable = true;
        isToggled = false;
        UpdateSprite();
    }

    public void Toggle()
    {
        toggle.interactable = true;
        isDisabled = false;
        isAvailable = false;
        isToggled = true;
        UpdateSprite();
        ActivateCooldown(10);
    }

    public void UpdateSprite()
    {
        if(isDisabled)
        {
            var state = toggle.spriteState;
            state.pressedSprite = DisabledPressed;
            state.disabledSprite = Disabled;
            img.sprite = Disabled;
            toggle.spriteState = state;
        } else if(isAvailable)
        {
            var state = toggle.spriteState;
            state.pressedSprite = AvailablePressed;
            state.disabledSprite = Available;
            img.sprite = Available;
            toggle.spriteState = state;
        } else if(isToggled)
        {
            var state = toggle.spriteState;
            state.pressedSprite = ToggledPressed;
            state.disabledSprite = Toggled;
            img.sprite = Toggled;
            toggle.spriteState = state;
        }
    }

	public void ActivateCooldown(float time){
		
		StartCoroutine (StartCooldown (time));
	}

	IEnumerator StartCooldown(float time){
        toggle.interactable = false;
        scaleMod.TriggerSwell(time);
		yield return new WaitForSeconds (time);
        int x = 0;
        switch (power)
        {
            case Power.Explosive:
                x = AmmoProperties.ExplosiveNb;
                AmmoProperties.Explosive = false;
                break;
            case Power.Homing:
                x = AmmoProperties.HomingNb;
                AmmoProperties.Homing = false;
                break;
            case Power.Piercing:
                x = AmmoProperties.PiercingNb;
                AmmoProperties.Piercing = false;
                break;
            case Power.Split:
                x = AmmoProperties.SplitNb;
                AmmoProperties.Split = false;
                break;
        }

        if (x == 0)
        {
            Disable();
        }
        else
        {
            Enable();
        }
    }
}
