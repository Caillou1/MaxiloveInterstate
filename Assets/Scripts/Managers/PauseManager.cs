using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PauseManager : MonoBehaviour {


	[SerializeField, Range(0, 1f)]
	float fadeTime;
	[SerializeField]
	AnimationCurve fadeCurve;

	[SerializeField, Range(0, 1f)]
	float popupTime;
	[SerializeField]
	AnimationCurve popupCurve;

	[SerializeField, Range(0, 1f)]
	float resumeSmoothingTime;
	[SerializeField]
	AnimationCurve resumeCurve;

	float timer;

	[SerializeField]
	Image blackScreen;

	[SerializeField]
	Image pauseImage;

	bool pauseEnabled;
	bool smoothingEnabled;



	void Update(){
		if (pauseEnabled) {
			PauseGame ();
		}
		if (smoothingEnabled) {
			SmoothTimescale ();
		}
	}

	public void TriggerPause(){
		pauseEnabled = true;
        AmplitudeHelper.Instance.LogEvent("Player clicked Pause");
        PlayerController.Instance.StopScaleUp();
        Time.timeScale = 0;
	}

	void PauseGame(){
        PlayerController.Instance.IsPaused = true;
		if (!blackScreen.gameObject.activeInHierarchy) {
			blackScreen.gameObject.SetActive (true);
		}
		if (!pauseImage.gameObject.activeInHierarchy) {
			pauseImage.gameObject.SetActive (true);
		}

		timer += Time.unscaledDeltaTime;
			
		float fadePercent = timer / fadeTime;
		fadePercent = Mathf.Clamp01 (fadePercent);
		float fadeValue = fadeCurve.Evaluate (fadePercent);
		blackScreen.color = new Color (0, 0, 0, fadeValue);

		float popupPercent = timer / popupTime;
		popupPercent = Mathf.Clamp01 (popupPercent);
		float popupValue = popupCurve.Evaluate (popupPercent);
		pauseImage.rectTransform.localScale = Vector3.one * popupValue;


		if (popupPercent == 1 && fadePercent == 1) {
			pauseEnabled = false;
			timer = 0;
		}

	}

	public void UnpauseGame(){
		pauseImage.gameObject.SetActive (false);
		blackScreen.gameObject.SetActive (false);
		smoothingEnabled = true;
	}

	void SmoothTimescale(){
		timer += Time.unscaledDeltaTime;
		float smoothPercent = timer / resumeSmoothingTime;
		smoothPercent = Mathf.Clamp01 (smoothPercent);
		Time.timeScale = resumeCurve.Evaluate (smoothPercent);

		if (smoothPercent == 1) {
			smoothingEnabled = false; 
			timer = 0;
		}

	}

	public void QuitGame(){
        AmplitudeHelper.Instance.LogEvent("Player quitted");
		Application.Quit();
	}

	public void ReloadScene(){
        AmplitudeHelper.Instance.LogEvent("Player clicked Retry");
		//Scene currentScene = SceneManager.GetActiveScene ();
		//SceneManager.LoadScene(currentScene.buildIndex);
	}
}
