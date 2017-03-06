using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BigEnemy : Enemy
{
    public float AngleOfWave;
    public float NumberOfBulletsPerWave;
    public float NumberOfShotWaves;
    public float DelayBetweenEachShotWaves;

    protected override void Shot(bool ignoreRandom)
    {
        if (CanFire)
        {
            float time = 0;
            for (int i = 0; i < NumberOfShotWaves; i++)
            {
                StartCoroutine(DelayedShot(time));
                time += DelayBetweenEachShotWaves;
            }
        }
    }

    void Start()
    {
        Init();
        hasMinions = (UnityEngine.Random.value <= .1f) ? true : false;
        InitMinions();
        StartCoroutine(FirstShot());
    }

    void FixedUpdate()
    {
        Move();
    }

    IEnumerator DelayedShot(float time)
    {
        yield return new WaitForSeconds(time);

        SoundManager.Instance.PlayTirTank();
        float minDeg = 90 + (180 - AngleOfWave) / 2;
        float maxDeg = 270 - (180 - AngleOfWave) / 2;
        float deg = 0;

        if (NumberOfBulletsPerWave == 1)
        {
            deg = AngleOfWave / 2;
            minDeg += deg;
        } else
        {
            deg = AngleOfWave / (NumberOfBulletsPerWave - 1);
        }

        for (float degree = minDeg; degree <= maxDeg; degree += deg)
        {
            Instantiate(Bullet, tf.position - new Vector3(0, 0, 0.05f), Quaternion.Euler(0, 0, degree));
        }
    }
}