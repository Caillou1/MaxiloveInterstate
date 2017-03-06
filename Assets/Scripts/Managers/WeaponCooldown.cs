using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class WeaponCooldown : MonoBehaviour {

	Toggle toggle;

	Image img;
    public Sprite Toggled;
    public Sprite ToggledPressed;

    Sprite Initial;
    Sprite InitialPressed;

    void Start(){
		toggle = GetComponent<Toggle> ();
		img = GetComponent<Image> ();

        Initial = img.sprite;
        InitialPressed = toggle.spriteState.pressedSprite;
	}

	void Update(){
		if (toggle.isOn) {
            img.sprite = Toggled;
            var t = toggle.spriteState;
            t.pressedSprite = ToggledPressed;
            toggle.spriteState = t;
        } else {
			img.sprite = Initial;
            var t = toggle.spriteState;
            t.pressedSprite = InitialPressed;
            toggle.spriteState = t;
        }
	}

	public void ActivateCooldown(float time){
		
		StartCoroutine (StartCooldown (time));
	}

	IEnumerator StartCooldown(float time){
		yield return new WaitForSeconds (time);
		toggle.interactable = true;
		toggle.isOn = true;
	}
}
