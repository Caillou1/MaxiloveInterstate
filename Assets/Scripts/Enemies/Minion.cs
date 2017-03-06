using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Minion : SmallEnemy {
    private GameObject Parent;

    public void SetParent(GameObject p)
    {
        Parent = p;
    }

	void Update () {
        Move();
        Shot(false);
	}

    protected override void Move()
    {
        if (CanMove)
        {
            transform.position = Parent.transform.position + (transform.position - Parent.transform.position).normalized * 2;
            tf.RotateAround(Parent.transform.position, Vector3.back, 50 * Time.deltaTime);
            tf.rotation = Quaternion.Euler(Vector3.up);
        }
    }
}
