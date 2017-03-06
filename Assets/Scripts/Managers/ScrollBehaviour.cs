using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScrollBehaviour : MonoBehaviour {
    public float Speed = 10f;

    private Transform tf;

	void Start () {
        tf = transform;
	}
	
	void Update () {
        tf.Translate(Vector3.down * Time.deltaTime * Speed);
	}
}
