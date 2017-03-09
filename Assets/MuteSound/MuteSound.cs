using UnityEngine.UI;
using UnityEngine;

public class MuteSound : MonoBehaviour {

	bool soundMuted;

	[SerializeField]
	Image imgOn;

	[SerializeField]
	Image imgOff;

	//AudioListener audioLis;


	void Start(){
		//audioLis = Camera.main.GetComponent<AudioListener> ();
        if(AudioListener.volume == 0)
        {
            imgOff.gameObject.SetActive(true);
            imgOn.gameObject.SetActive(false);
            soundMuted = true;
        } else
        {

            imgOff.gameObject.SetActive(false);
            imgOn.gameObject.SetActive(true);
            soundMuted = false;
        }
	}

	public void ToggleMute(){
		if (!soundMuted) {
			AudioListener.volume = 0;
			//audioLis.enabled = false;
			imgOn.gameObject.SetActive (false);
			imgOff.gameObject.SetActive (true);
			soundMuted = true;
		} else {
			AudioListener.volume = 1;
			//audioLis.enabled = true;
			imgOn.gameObject.SetActive (true);
			imgOff.gameObject.SetActive (false);
			soundMuted = false;
		}

		//Debug.Log (audioLis.enabled);
		//Debug.Log (AudioListener.volume);
	}
}
