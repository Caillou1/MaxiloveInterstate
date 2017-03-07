using UnityEngine;
using UnityEngine.UI;

public class AchievementUI : MonoBehaviour {

	[SerializeField]
	Image blackScreen;

	[SerializeField] 
	Image achvBackground;

	[SerializeField]
	AnimationCurve fadeCurve;
	[SerializeField, Range(0,5)]
	float fadeTime;
	[SerializeField]
	AnimationCurve popupCurve;
	[SerializeField, Range(0,5)]
	float popupTime;

	float timer;

	bool transitionOn;

	void Start(){
		if (blackScreen.gameObject.activeInHierarchy) {
			blackScreen.gameObject.SetActive (false);
		}
		if (achvBackground.gameObject.activeInHierarchy) {
			achvBackground.gameObject.SetActive (false);
		}
	}


	public void DisplayAchievements(){
		transitionOn = true;


	}

	void Update(){
		if (transitionOn) {
			Transition ();
		}
	}


	void Transition(){
		if (!blackScreen.gameObject.activeInHierarchy) {
			blackScreen.gameObject.SetActive (true);
		}
		if (!achvBackground.gameObject.activeInHierarchy) {
			achvBackground.gameObject.SetActive (true);
		}

		timer += Time.deltaTime;

		float fadePercent = timer / fadeTime;
		fadePercent = Mathf.Clamp01 (fadePercent);
		float fadeAmount = fadeCurve.Evaluate (fadePercent);
		Color fadeColour = new Color (0, 0, 0, fadeAmount);
		blackScreen.color = fadeColour;

		float popupPercent = timer / popupTime;
		popupPercent = Mathf.Clamp01 (fadePercent);
		float popupAmount = popupCurve.Evaluate (popupPercent);
		achvBackground.transform.localScale = Vector3.one * popupAmount;

		if (fadePercent >= 1 && popupPercent >= 1) {
			transitionOn = false;
			timer = 0;
		}

	}

	public void ExitAchievements(){
		achvBackground.gameObject.SetActive (false);
		blackScreen.gameObject.SetActive (false);
	}
}
