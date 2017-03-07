using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class StartGame : MonoBehaviour {

	[SerializeField, Range(0, 1f)]
	float fadeTime;
	[SerializeField]
	AnimationCurve fadeCurve;

	float timer;

	[SerializeField]
	Image blackScreen;

	bool playOn;

	public void Play(){
		playOn = true;
	}

	void Update(){
		if (playOn) {
			TransitionAndStart ();
		}
	}

	void TransitionAndStart(){
		if (!blackScreen.gameObject.activeInHierarchy) {
			blackScreen.gameObject.SetActive (true);
		}

		timer += Time.deltaTime;

		float fadePercent = timer / fadeTime;
		fadePercent = Mathf.Clamp01 (fadePercent);
		float fadeValue = fadeCurve.Evaluate (fadePercent);
		blackScreen.color = new Color (0, 0, 0, fadeValue);

		if (fadePercent >= 1) {
			int currentSceneIndex = SceneManager.GetActiveScene ().buildIndex;
			SceneManager.LoadScene (currentSceneIndex + 1);
		}
	}
}
