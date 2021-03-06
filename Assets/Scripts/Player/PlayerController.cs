﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using UnityEngine.EventSystems;

public class PlayerController : MonoBehaviour {
    public int LifePoints;
    public float SlideSpeed = 0.5f;
    public float Cadence = 5;
    public GameObject Bullet;
    public float InvulnerabilityTime;
    public bool Ralenti;
    public float ValeurRalenti;

    private bool CanBeHurt;
    private int MaxLifePoints;
    private bool isFiring;
    private Camera cam;
    private Vector2 pos;
    private Transform tf;
    private GameOverUI gameOverUI;
    private PropertyManager propM;
    private GameManager gameManager;
    private MeshRenderer Render;
    private bool CanMove = true;
    private CameraShake camShake;
    private bool CanScaleUp;

    //Particles
    private ParticleSystem Trail;
    private LineRenderer RainbowTrail;
    private ParticleSystem Hit;
    private ParticleSystem Destruction;

    private float ScreenSup;
    private float ScreenInf;

    public RectTransform WeaponUI;
    public RectTransform PauseUI;

    public bool IsPaused = false;

    public Image[] Lives;

    public static PlayerController Instance;

    void Start () {
        CanScaleUp = true;
        Instance = this;
        cam = GameObject.Find("CameraContainer").transform.GetChild(0).GetComponent<Camera>();
        tf = transform;
        pos = new Vector2(tf.position.x, tf.position.y);
        isFiring = false;
        MaxLifePoints = LifePoints;
        StartCoroutine(Fire());
        gameOverUI = GameObject.Find("GameOver").GetComponent<GameOverUI>();
        propM = GameObject.Find("Manager").GetComponent<PropertyManager>();
        gameManager = GameObject.Find("Manager").GetComponent<GameManager>();
        CanBeHurt = true;
        Render = tf.FindChild("Mesh").GetComponent<MeshRenderer>();
        camShake = GameObject.Find("CameraContainer").GetComponent<CameraShake>();

        Trail = tf.FindChild("Trail").GetComponent<ParticleSystem>();
        RainbowTrail = tf.FindChild("TrailRB").GetComponent<LineRenderer>();
        Hit = tf.FindChild("Hit").GetComponent<ParticleSystem>();
        Destruction = tf.FindChild("Destruction").GetComponent<ParticleSystem>();
        var par = WeaponUI.parent.GetComponent<Canvas>();
        ScreenSup = WeaponUI.rect.height * par.scaleFactor;
        ScreenInf = Screen.height - PauseUI.rect.height * par.scaleFactor - 50;

        StartCoroutine(WaitForLowFPS());
    }

    IEnumerator WaitForLowFPS()
    {
        yield return new WaitForSeconds(10);
        yield return new WaitUntil(() => (1.0f / Time.smoothDeltaTime) < 25);

        var prop = new Dictionary<string, object>()
        {
            { "FPS", (1.0f / Time.smoothDeltaTime) },
            { "Processor Count", SystemInfo.processorCount },
            { "Processor Frequency", SystemInfo.processorFrequency },
            { "Graphics Memory Size", SystemInfo.graphicsMemorySize },
            { "Memory Size", SystemInfo.systemMemorySize }
        };

        AmplitudeHelper.Instance.LogEvent("Low FPS", prop);

        StartCoroutine(WaitForLowFPS());
    }

    void Update ()
    {
        if (CanMove && Input.touches.Length > 0 && Input.touches[0].position.y > ScreenSup && Input.touches[0].position.y < ScreenInf) 
        {
            if (!isFiring) 
            {
                isFiring = true;
                if (!IsPaused && Ralenti)
                {
                    Time.timeScale = 1; // DOVirtual.Float(Time.timeScale, 1f, 1f, (float t) => Time.timeScale = t);
                    //SoundManager.Instance.SetPitch();
                }
            }
            float from = pos.x;
            float to = cam.ScreenToWorldPoint(new Vector3(Input.touches[0].position.x, tf.position.y, 10)).x;
            if (to > 2.5f) to = 2.5f;
            else if (to < -2.5f) to = -2.5f;
            DOVirtual.Float(from, to, SlideSpeed, (float x) =>
            {
                if (tf != null) pos = new Vector3(x, tf.position.y, tf.position.z);
            });
        } else if (CanMove && Input.GetMouseButton(0) && Input.mousePosition.y > ScreenSup && Input.mousePosition.y < ScreenInf)
        {
            if(!isFiring)
            {
                isFiring = true;
                if (!IsPaused && Ralenti)
                {
                    Time.timeScale = 1; // DOVirtual.Float(Time.timeScale, 1f, 1f, (float t) => Time.timeScale = t);
                    //SoundManager.Instance.SetPitch();
                }
            }
            float from = pos.x;
            float to = cam.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, tf.position.y, 10)).x;
            if (to > 2.5f) to = 2.5f;
            else if (to < -2.5f) to = -2.5f;
            DOVirtual.Float(from, to, SlideSpeed, (float x) =>
              {
                  if (tf != null) pos = new Vector3(x, tf.position.y, tf.position.z);
              });
        } else {
            if (isFiring)
            {
                isFiring = false;
                if (!IsPaused && Ralenti)
                {
                    Time.timeScale = ValeurRalenti;
                    //SoundManager.Instance.SetPitch();
                    StartCoroutine(TriggerScaleUp());
                }
            }
        }

        tf.position = pos;
    }

    public void StopScaleUp()
    {
        CanScaleUp = false;
        StopCoroutine(TriggerScaleUp());
    }

    IEnumerator TriggerScaleUp()
    {
        yield return new WaitForSecondsRealtime(2);
        if(CanScaleUp)
            Time.timeScale = 1;
        CanScaleUp = true;
    }

    void SpawnBullet()
    {
        Instantiate(Bullet, tf.position - new Vector3(0, 0, 0.04f), Quaternion.identity);
        SoundManager.Instance.PlayShoot();
        AmplitudeData.Instance.BulletsFired++;
    }

    public void DoDamage(int damage)
    {
        if(CanBeHurt && LifePoints>0)
        {
            Hit.Play(true);
            LifePoints -= damage;

            Lives[LifePoints].enabled = false;

            SoundManager.Instance.PlayLoseLife();
            //Handheld.Vibrate();
            StartCoroutine(Invulnerability());
            camShake.StartShake();
            AmplitudeData.Instance.LifeLost++;
            if (LifePoints<=0)
            {
                StartCoroutine(GameOver());
            }
        }
    }

    IEnumerator GameOver()
    {
        Ralenti = false;
        Destruction.Play(true);
        Trail.Stop(true);
        RainbowTrail.enabled = false;
        CanMove = false;
        Render.enabled = false;
        SoundManager.Instance.PlayDeath();
        PlayerPrefs.SetInt("BestScore", Mathf.Max(gameManager.GetScore(), PlayerPrefs.GetInt("BestScore", 0)));
        PlayerPrefs.SetInt("Money", PlayerPrefs.GetInt("Money", 0) + gameManager.GetMoney());
        AmplitudeData.Instance.GameFinished();
        yield return new WaitForSeconds(2f);
        gameOverUI.isGameOver = true;
    }

    IEnumerator Invulnerability()
    {
        CanBeHurt = false;
        yield return new WaitForSeconds(InvulnerabilityTime);
        CanBeHurt = true;
    }

    IEnumerator Fire()
    {
        yield return new WaitUntil(() => isFiring);
        SpawnBullet();
        propM.DisableToggles(AmmoProperties.Cooldown);
        yield return new WaitForSeconds(AmmoProperties.Cooldown);
        StartCoroutine(Fire());
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Pickable"))
        {
            var o = other.GetComponent<Life>();
            if(o != null)
            {
                StartCoroutine(Invulnerability());
                SoundManager.Instance.PlayPickLife();
                AmplitudeData.Instance.LifePicked++;
                LifePoints = Mathf.Min(LifePoints + o.LifePoint, MaxLifePoints);
                Lives[LifePoints-1].enabled = true;
                Destroy(other.gameObject);
            }
        }
    }
}