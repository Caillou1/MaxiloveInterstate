using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScrollBehaviour : MonoBehaviour {
    public static float Speed = 10f;

    private bool CanMove = true;
    private Transform tf;

	void Start () {
        tf = transform;
	}

    public void SetCanMove(bool b)
    {
        CanMove = b;
    }
	
	void Update () {
        if (CanMove)
        {
            tf.Translate(Vector3.down * Time.deltaTime * Speed);
        }
	}
}
