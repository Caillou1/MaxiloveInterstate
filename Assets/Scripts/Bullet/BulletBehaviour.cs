using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class BulletBehaviour : MonoBehaviour
{
    public float MovementSpeed = 10;
    public float Damage = 5;
    public int TouchedEnemiesBeforeDestroy = 3;
    public int NumberOfFragment = 5;
    public float BombRadius = 1;
    public float BombTime = 1;
    public float BombDamage = 5;
    public float LifeTime = 2;
    public float HomingRadius = .5f;

    private bool hasNoTarget;
    private bool isFragment = false;
    private Transform tf;
    private int TouchedEnemies;

    private bool Split = false;
    private bool Homing = false;
    private bool Explosive = false;
    private bool Piercing = false;
    private bool isSecondFragment = false;

    private void Awake()
    {
        tf = transform;
    }

    void Start()
    {
        tf = transform;
        Destroy(gameObject, LifeTime);
        TouchedEnemies = 0;
        hasNoTarget = true;
        SetProperties();
        if(Homing) StartCoroutine(DelayedHoming());
        if(!isFragment && !isSecondFragment)
        {
            if (Split)
                AmplitudeData.Instance.BulletsFiredWithSplit++;
            if (Explosive)
                AmplitudeData.Instance.BulletsFiredWithExplosive++;
            if (Piercing)
                AmplitudeData.Instance.BulletsFiredWithPiercing++;
            if (Homing)
                AmplitudeData.Instance.BulletsFiredWithHoming++;
        }
    }

    void Update()
    {
        tf.Translate(Vector3.up * MovementSpeed * Time.deltaTime);
    }

    public void StopHomingCoroutine()
    {
        StopCoroutine(StartHoming());
    }

    public void SetDamage(float dmg)
    {
        Damage = dmg;
    }

    public void SetProperties()
    {
        if (!AmmoProperties.SplitDisabled) Split = AmmoProperties.Split;
        if (!AmmoProperties.HomingDisabled) Homing = AmmoProperties.Homing;
        if (!AmmoProperties.ExplosiveDisabled) Explosive = AmmoProperties.Explosive;
        if (!AmmoProperties.PiercingDisabled) Piercing = AmmoProperties.Piercing;
    }

    public void SetFragment(bool b)
    {
        isFragment = b;
    }

    public void SetSecondFragment(bool b)
    {
        isSecondFragment = b;
    }

    public void IgnoreCollision(Collider obj)
    {
        Physics.IgnoreCollision(obj, GetComponent<Collider>());
    }

    IEnumerator DelayedHoming()
    {
        yield return new WaitForSeconds(.3f);
        StartCoroutine(StartHoming());
    }

    IEnumerator StartHoming()
    {
        var potentialTargets = Physics.OverlapSphere(tf.position, HomingRadius);
        foreach(var target in potentialTargets) {
            if (target.CompareTag("Enemy"))
            {
                SoundManager.Instance.PlayHoming();
                Vector3 diff = target.transform.position - transform.position;
                diff.Normalize();

                float rot_z = Mathf.Atan2(diff.y, diff.x) * Mathf.Rad2Deg;
                transform.rotation = Quaternion.Euler(0f, 0f, rot_z - 90);

                //SMOOTH DOVirtual.Float(tf.rotation.z, rot_z, .2f, (float rot) => transform.rotation = Quaternion.Euler(0f, 0f, rot - 90));

                hasNoTarget = false;
                break;
            }
        }
        yield return new WaitForSeconds(.1f);
        if(hasNoTarget)
            StartCoroutine(StartHoming());
    }

    private void OnTriggerEnter(Collider collision)
    {
        if (collision.CompareTag("Enemy"))
        {
            var enemy = collision.gameObject.GetComponent<Enemy>();
            if (!enemy.IsDead())
            {
                SoundManager.Instance.PlayHit();
                TouchedEnemies++;
                AmplitudeData.Instance.Hits++;

                float dmg = Damage;

                if (Explosive)
                {
                    enemy.LeaveBomb(BombTime, BombRadius, BombDamage);
                    SoundManager.Instance.PlayBip();
                }

                if (Split && !isSecondFragment)
                {
                    SoundManager.Instance.PlaySplit();

                    for (int i = 0; i < NumberOfFragment; i++)
                    {
                        var bb = Instantiate(gameObject, tf.position, Quaternion.Euler(0, 0, Random.Range(-90, 90))).GetComponent<BulletBehaviour>();
                        bb.SetFragment(true);
                        bb.StopHomingCoroutine();
                        if (isFragment)
                            bb.SetSecondFragment(true);
                        bb.IgnoreCollision(collision);
                        if (Piercing)
                        {
                            bb.SetDamage(dmg / 2);
                        }
                    }
                    Destroy(gameObject);
                }
                if (Split)
                {
                    dmg /= 2;
                }

                collision.GetComponent<Enemy>().DoDamage(dmg, Piercing);

                if (!Piercing || TouchedEnemies >= TouchedEnemiesBeforeDestroy || enemy.isArmored)
                {
                    Destroy(gameObject);
                }
                else
                {
                    SoundManager.Instance.PlayPierce();
                }

                if(Piercing && Split && Homing && Explosive && enemy.IsDead())
                {
                    AchievementManager.Instance.UnlockAchievement(0);
                }
            }
        } 
    }
}