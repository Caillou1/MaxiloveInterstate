using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MediumEnemy : Enemy
{
    public float AngleOfWave = 30;
    public float NumberOfBulletsPerWave;

    protected override void Shot(bool ignoreRandom)
    {
        if (CanFire) {
            SoundManager.Instance.PlayTirVoiture();
            float minDeg = 90 + (180 - AngleOfWave) / 2;
            float maxDeg = 270 - (180 - AngleOfWave) / 2;
            float deg = 0;

            if (NumberOfBulletsPerWave <=1)
            {
                if(NumberOfBulletsPerWave <1)
                {
                    Debug.LogError("NumberOfBulletsPerWave can't be lower than 1.");
                }
                deg = AngleOfWave / 2;
                minDeg += deg;
            }
            else
            {
                deg = AngleOfWave / (NumberOfBulletsPerWave - 1);
            }

            for (float degree = minDeg; degree <= maxDeg; degree += deg)
            {
                Instantiate(Bullet, tf.position - new Vector3(0, 0, 0.05f), Quaternion.Euler(0, 0, degree));
            }
        }
    }

    void Start()
    {
        Init();
        hasMinions = (UnityEngine.Random.value <= .1f) ? true : false;
        InitMinions();
    }

    void FixedUpdate()
    {
        Move();
    }
}
