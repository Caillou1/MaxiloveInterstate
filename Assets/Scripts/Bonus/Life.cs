using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Life : Pickable {
    public int LifePoint;
    public float Speed;

    public float BulletDestructionRadius;

    private Transform tf;

    private void Start()
    {
        tf = transform;
        var around = Physics.OverlapSphere(tf.position, BulletDestructionRadius);
        foreach(var a in around)
        {
            if(a.CompareTag("EnemyBullet"))
            {
                Destroy(a.gameObject);
            }
        }
    }

    private void Update()
    {
        tf.Translate(Vector3.down * Speed * Time.deltaTime);
    }
}
