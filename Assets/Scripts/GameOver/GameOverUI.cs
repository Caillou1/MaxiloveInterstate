using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameOverUI : MonoBehaviour {

	[SerializeField, Range(0, 1f)]
	float fadeTime;
	[SerializeField]
	AnimationCurve fadeCurve;

	[SerializeField, Range(0, 1f)]
	float popupTime;
	[SerializeField]
	AnimationCurve popupCurve;

	[SerializeField]
	AnimationCurve scoreCurve;

	[SerializeField]
	Image blackScreen;

	[SerializeField]
	Image GOImage;

	[SerializeField]
	Text scoreTxt;

    public Text inGameScore;

	[HideInInspector]
	public bool isGameOver;

	float timer;

	void Update(){
		/*
		//when game is over, change isGameOver to true
		if (Input.GetKeyDown (KeyCode.B)) {
			isGameOver = true;
		}
		*/


		if (isGameOver) {
			TriggerGameOver ();
		}
	}


	void TriggerGameOver(){
		if (!blackScreen.gameObject.activeInHierarchy) {
			blackScreen.gameObject.SetActive (true);
		}
		if (!GOImage.gameObject.activeInHierarchy) {
			GOImage.gameObject.SetActive (true);
		}
		if (!scoreTxt.gameObject.activeInHierarchy) {
			scoreTxt.gameObject.SetActive (true);
		}
		if (Time.timeScale != 0) {
			Time.timeScale = 0;
		}

		timer += Time.unscaledDeltaTime;

        scoreTxt.text = "Score\n" + GameManager.Instance.GetScore();
        inGameScore.enabled = false;

		float fadePercent = timer / fadeTime;
		fadePercent = Mathf.Clamp01 (fadePercent);
		float fadeValue = fadeCurve.Evaluate (fadePercent);
		float scoreValue = scoreCurve.Evaluate (fadePercent);
		blackScreen.color = new Color (0, 0, 0, fadeValue);
		scoreTxt.color = new Color (1, 1, 1, scoreValue);

		float popupPercent = timer / popupTime;
		popupPercent = Mathf.Clamp01 (popupPercent);
		float popupValue = popupCurve.Evaluate (popupPercent);
		GOImage.rectTransform.localScale = Vector3.one * popupValue;

		if (popupPercent == 1 && fadePercent == 1) {
			isGameOver = false;
			timer = 0;
		}
	}

	public void QuitGame(){
		Application.Quit();
	}

	public void ReloadScene(){
		Scene currentScene = SceneManager.GetActiveScene ();
		SceneManager.LoadScene(currentScene.buildIndex);
	}
}
