using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class OpacityFromParent : MonoBehaviour {

    public Image[] images;

	// Use this for initialization
	void Start ()
    {
        images = GetComponentsInParent<Image>();	
	}
	
	// Update is called once per frame
	void Update ()
    {
        images[0].color = new Color(images[0].color.r, images[0].color.g, images[0].color.b, images[1].color.a);
	}
}
