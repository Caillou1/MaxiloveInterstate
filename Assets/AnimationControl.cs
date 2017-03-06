using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationControl : MonoBehaviour {

    public float precision;

    Animator mAnimator;
    public Transform parentTransform;

    Vector3 lastPos;

	// Use this for initialization
	void Start ()
    {
        mAnimator = GetComponent<Animator>();
        parentTransform = GetComponentsInParent<Transform>()[1];	
	}
	
	// Update is called once per frame
	void FixedUpdate ()
    {
        if (Mathf.Abs(parentTransform.position.x - lastPos.x) <= precision)
            mAnimator.SetFloat("Velocity", 0);
        else if (parentTransform.position.x - lastPos.x > 0)
            mAnimator.SetFloat("Velocity", 1);
        else if (parentTransform.position.x - lastPos.x < 0)
            mAnimator.SetFloat("Velocity", -1);

        lastPos = parentTransform.position;
         
    }
}
