using UnityEngine;
using UnityEngine.UI;

public class Tuto_UI : MonoBehaviour {


	Canvas tutoCanvas;

	[SerializeField]
	GameObject slideContainer;
	/*
	[SerializeField, Range(0, 1f)]
	float fadeTime;
	[SerializeField]
	AnimationCurve fadeCurve;
	*/
	/*
	[SerializeField]
	Image blackScreen;
	*/

	float timer;

	bool blackScreenOn;

	[SerializeField, Range(0, 1f)]
	float popupTime;
	[SerializeField]
	AnimationCurve popupCurve;

	[SerializeField]
	Image tutoImage;

	void Start(){
		tutoCanvas = GetComponent<Canvas> ();
		tutoCanvas.sortingOrder = -10;
	}


	public void DisplayTutorial(){
        AmplitudeHelper.Instance.LogEvent("Checked the tutorial !");
        //Time.timeScale = 0;
		tutoCanvas.sortingOrder = 5;

		slideContainer.SetActive (true);
		//blackScreen.color = Color.clear;
		blackScreenOn = true;
	}

	public void CloseTutorial(){
		tutoCanvas.sortingOrder = -10;
		slideContainer.SetActive (false);
	}

	void Update(){
		if (blackScreenOn) {
			ActivateBlackScreen ();
		}

		/*
		if (Input.GetKeyDown (KeyCode.V)) {
			DisplayTutorial ();
		}
		*/



	}

	void ActivateBlackScreen(){
		/*
		if (!blackScreen.gameObject.activeInHierarchy) {
			blackScreen.gameObject.SetActive (true);
		}
		*/
		if (!tutoImage.gameObject.activeInHierarchy) {
			tutoImage.gameObject.SetActive (true);
		}

		timer += Time.unscaledDeltaTime;

		/*
		float fadePercent = timer / fadeTime;
		fadePercent = Mathf.Clamp01 (fadePercent);
		float fadeValue = fadeCurve.Evaluate (fadePercent);
		blackScreen.color = new Color (0, 0, 0, fadeValue);
		*/

		float popupPercent = timer / popupTime;
		popupPercent = Mathf.Clamp01 (popupPercent);
		float popupValue = popupCurve.Evaluate (popupPercent);
		tutoImage.rectTransform.localScale = Vector3.one * popupValue;

		if (popupPercent >= 1) {
			blackScreenOn = false;
			timer = 0;
		}
	}

}
