using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class BlackFade : MonoBehaviour {

	[SerializeField]
	bool fadeAtStart;

	[SerializeField]
	Image fadeImg;

	[SerializeField, Range(0,1)]
	float fadeTime;
	[SerializeField]
	AnimationCurve fadeStartCurve;
	[SerializeField]
	AnimationCurve fadeEndCurve;

	[SerializeField]
	Color fadeColour;

	float timer;

	Canvas cv;

	bool fadingOn;
	bool fadeAtEnd;

	int nextSceneIndex = -1;

	void Start(){
		cv = GetComponent<Canvas> ();
		cv.sortingOrder = -1;

	}

	void Update(){
		if (fadeAtStart) {
			FadeStart ();
		}

		if (fadeAtEnd) {
			FadeEnd ();
		}
	}


	void FadeStart(){
		if (!fadeImg.gameObject.activeInHierarchy) {
			fadeImg.gameObject.SetActive (true);
		}
		cv.sortingOrder = 10;

		timer += Time.deltaTime;

		float fadePercent = timer / fadeTime;
		fadePercent = Mathf.Clamp01 (fadePercent);

		float fadeAmount = fadeStartCurve.Evaluate (fadePercent);
		fadeImg.color = new Color (fadeColour.r, fadeColour.g, fadeColour.b, fadeAmount);

		if (fadePercent >= 1f) {
			fadeImg.gameObject.SetActive (false);
			timer = 0;
			fadeAtStart = false;
		}
	}

	public void NextScene(int nextIndex){
		fadeAtEnd = true;
		nextSceneIndex = nextIndex;
		if (!fadeImg.gameObject.activeInHierarchy) {
			fadeImg.gameObject.SetActive (true);
		}
	}

	void FadeEnd(){
		if (!fadeImg.gameObject.activeInHierarchy) {
			fadeImg.gameObject.SetActive (true);
		}
		cv.sortingOrder = 10;

		timer += Time.unscaledDeltaTime;

		float fadePercent = timer / fadeTime;
		fadePercent = Mathf.Clamp01 (fadePercent);

		float fadeAmount = fadeEndCurve.Evaluate (fadePercent);
		fadeImg.color = new Color (fadeColour.r, fadeColour.g, fadeColour.b, fadeAmount);

		if (fadePercent >= 1f) {
			timer = 0;
			fadeAtEnd = false;
			Time.timeScale = 1;
			SceneManager.LoadScene (nextSceneIndex);
		}
	}
}
