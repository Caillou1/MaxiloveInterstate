using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TutorialManager : MonoBehaviour {
    public string[] Tutos;
    public Text texte;

    private ScaleModulator scaleMod;

	void Start () {
		if(true)//PlayerPrefs.GetInt("Tutorial", 0) == 0)
        {
            scaleMod = texte.GetComponent<ScaleModulator>();
            StartCoroutine(StartTuto());
        } else
        {
            SpawnManager.Instance.StartGame();
        }
	}

    IEnumerator StartTuto()
    {
        texte.text = "";
        yield return new WaitForSeconds(2f);
        scaleMod.TriggerSwell();
        yield return new WaitForSeconds(.1f);
        texte.text = Tutos[0];

        yield return new WaitUntil(() => Input.touches.Length>0 || Input.GetMouseButton(0));
        texte.text = "";
        yield return new WaitForSeconds(1f);
        scaleMod.TriggerSwell();
        yield return new WaitForSeconds(.1f);
        texte.text = Tutos[1];

        yield return new WaitForSeconds(1);
        SpawnManager.Instance.LaunchWaveTuto(0);

        yield return new WaitForSeconds(1);
        yield return new WaitUntil(() => SpawnManager.Instance.EnemiesAreOnScreen() == false);
        SpawnManager.Instance.LaunchWaveTuto(1);
        texte.text = "";

        yield return new WaitForSeconds(1);
        yield return new WaitUntil(() => SpawnManager.Instance.EnemiesAreOnScreen() == false);
        scaleMod.TriggerSwell();
        yield return new WaitForSeconds(.1f);
        texte.text = Tutos[2];
        yield return new WaitForSeconds(3);
        SpawnManager.Instance.LaunchWaveTuto(2);
        texte.text = "";

        yield return new WaitForSeconds(1);
        yield return new WaitUntil(() => SpawnManager.Instance.EnemiesAreOnScreen() == false);

        scaleMod.TriggerSwell();
        yield return new WaitForSeconds(.1f);
        texte.text = Tutos[3];

        yield return new WaitForSeconds(3);
        texte.text = "";
        SpawnManager.Instance.LaunchWaveTuto(3);

        yield return new WaitForSeconds(1f);
        yield return new WaitUntil(() => SpawnManager.Instance.EnemiesAreOnScreen() == false);
        yield return new WaitForSeconds(1);

        scaleMod.TriggerSwell();
        yield return new WaitForSeconds(.1f);
        texte.text = "Good luck !";

        PlayerPrefs.SetInt("Tutorial", 1);
        SpawnManager.Instance.StartGame();

        yield return new WaitForSeconds(3f);
        texte.text = "";
    }
}