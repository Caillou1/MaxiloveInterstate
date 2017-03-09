using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomSpriteColor : MonoBehaviour {

    public Gradient colors;
    public float speed;

	// Use this for initialization
	void Start ()
    {
        GetComponent<SpriteRenderer>().color = colors.Evaluate(Mathf.Abs((Mathf.Sin(Time.time *speed)+1)/2));
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
