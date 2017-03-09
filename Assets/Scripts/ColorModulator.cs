using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

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
        t+=speed*Time.unscaledDeltaTime;
        mImage.color = colors.Evaluate((Mathf.Sin(t)+1)/2);	
	}

    public void Appear(float time)
    {
        StartCoroutine(Disappear(time));
    }

    IEnumerator Disappear(float time)
    {
        mImage.color = Color.clear;
        mImage.enabled = true;
        DOVirtual.Float(0, 1, 1, (float x) => mImage.color = new Color(mImage.color.r, mImage.color.g, mImage.color.b, x));
        yield return new WaitForSeconds(time);
        DOVirtual.Float(1, 0, 1, (float x) => mImage.color = new Color(mImage.color.r, mImage.color.g, mImage.color.b, x));
        yield return new WaitForSeconds(1);
        mImage.enabled = false;
    }
}
