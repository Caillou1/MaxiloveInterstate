using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SmallEnemy : Enemy {
    protected override void Shot(bool ignoreRandom)
    {
        if(CanFire && tf.position.y > .5f)
        {
            Vector3 diff = tf.position - GameObject.FindGameObjectWithTag("Player").transform.position;
            diff.Normalize();

            float rot_z = Mathf.Atan2(diff.y, diff.x) * Mathf.Rad2Deg;
            Instantiate(Bullet, tf.position - new Vector3(0, 0, 0.05f), Quaternion.Euler(0f, 0f, rot_z + 90));
            SoundManager.Instance.PlayTirMoto();
        }
    }
	
	void FixedUpdate () {
        Move();
	}
}