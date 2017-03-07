using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUp : Pickable {
    public float Speed = 5f;
    public Power power;

    private Transform tf;

	void Start () {
        tf = transform;
	}
	
	void Update () {
        tf.Translate(Vector3.down * Speed * Time.deltaTime);
	}

    public Power GetPower()
    {
        return power;
    }
}

public enum Power
{
    Split,
    Explosive,
    Piercing,
    Homing,
    NONE
}
