using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ColorModulator : MonoBehaviour {

    Image mImage;
    public Gradient colors;
    public float speed;

    float t;
	// Use this for initialization
	void Start ()
    {
        mImage = GetComponent<Image>();
	}
	
	// Update is called once per frame
	void Update ()
    {
        t+=speed;
        mImage.color = colors.Evaluate((Mathf.Sin(t)+1)/2);	
	}
}
