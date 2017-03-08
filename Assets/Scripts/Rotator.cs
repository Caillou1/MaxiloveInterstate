using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Rotator : MonoBehaviour {

    public Vector3 angle;
    public float speed;

    public float minSpeed;
    public float maxSpeed;

    public bool randomSpeed;
    public bool UI;
    RectTransform rectTrans;
    float defSpeed;

	// Use this for initialization
	void Start ()
    {
        if (randomSpeed)
            defSpeed = Random.Range(minSpeed, maxSpeed);
        else
            defSpeed = speed;
        if (UI)
            rectTrans = GetComponent<RectTransform>();	
	}
	
	// Update is called once per frame
	void Update ()
    {
        if (UI)
            rectTrans.Rotate(angle * defSpeed * Time.deltaTime);
        else
            transform.Rotate(angle * defSpeed * Time.deltaTime);	
	}
}
