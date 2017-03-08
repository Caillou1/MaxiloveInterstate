using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBullet : MonoBehaviour {
    public float Speed;
    public float LifeTime;

    private Transform tf;

	void Start () {
        tf = transform;
        Destroy(gameObject, LifeTime);
        var wave = SpawnManager.Instance.GetWave();
        if (wave > 45)
            Speed += (0.05f * ((float)wave - 45)); 
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