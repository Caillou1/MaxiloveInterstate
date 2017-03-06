using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class gethighscore : MonoBehaviour {
	void Start () {
        GetComponent<Text>().text += PlayerPrefs.GetInt("BestScore", 0);
	}
}
