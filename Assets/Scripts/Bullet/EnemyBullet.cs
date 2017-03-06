using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBullet : MonoBehaviour {
    public float Speed = 10;
    public float LifeTime;

    private Transform tf;

	void Start () {
        tf = transform;
        Destroy(gameObject, LifeTime);
	}
	
	void Update () {
        tf.Translate(Vector3.up * Time.deltaTime * Speed);
	}

    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Player"))
        {
            other.GetComponent<PlayerController>().DoDamage(1);
            Destroy(gameObject);
        }
    }
}