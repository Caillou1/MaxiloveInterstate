using System.Collections;
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
    public Texture PoliceTex;

    private bool CanBeHurt;
    private int MaxLifePoints;
    private bool isFiring;
    private Camera cam;
    private Vector2 pos;
    private Transform tf;
    private GameOverUI gameOverUI;
    private PropertyManager propM;
    private GameManager gameManager;
    private bool CanMove = true;
    private CameraShake camShake;
    private bool CanScaleUp;
    private PostEffect postEffect;

    //Particles
    private ParticleSystem Trail;
    private LineRenderer RainbowTrail;
    private ParticleSystem Hit;
    private ParticleSystem Destruction;
    private SkinnedMeshRenderer BusTex;

    private float ScreenSup;

    public RectTransform WeaponUI;
    public RectTransform PauseUI;

    public bool IsPaused = false;
    public bool ShowFPS = true;

    public Image[] Lives;
    private Text FPSText;

    public static PlayerController Instance;

    public AudioHighPassFilter highpass;
    private AudioHighPassFilter highpassVan;

    private EventSystem eventSystem;
    private bool HasLostLife;

    void Start () {
        eventSystem = GameObject.Find("EventSystem").GetComponent<EventSystem>();
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
        camShake = GameObject.Find("CameraContainer").GetComponent<CameraShake>();

        Trail = tf.FindChild("Trail").GetComponent<ParticleSystem>();
        RainbowTrail = tf.FindChild("TrailRB").GetComponent<LineRenderer>();
        Hit = tf.FindChild("Hit").GetComponent<ParticleSystem>();
        Destruction = tf.FindChild("Destruction").GetComponent<ParticleSystem>();
        BusTex = tf.FindChild("Mesh").transform.FindChild("Sk").GetComponent<SkinnedMeshRenderer>();
        FPSText = GameObject.Find("_FPSTEXT_").GetComponent<Text>();
        highpassVan = GetComponent<AudioHighPassFilter>();
        postEffect = cam.GetComponent<PostEffect>();
        HasLostLife = false;

        StartCoroutine(WaitForAchievement());
        StartCoroutine(WaitForLowFPS());
    }

    IEnumerator WaitForAchievement()
    {
        yield return new WaitForSeconds(1);
        yield return new WaitUntil(() => GameManager.Instance.GetScore()>= 5000);
        if (!HasLostLife)
        {
            AchievementManager.Instance.UnlockAchievement(3);
        }
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
        if(Time.timeScale != 1)
        {
            postEffect.hue = Mathf.Cos(Time.time*2);
        } else
        {
            postEffect.hue = 0;
        }

        if(ShowFPS)
        {
            FPSText.text = "FPS : "+(1.0f / Time.smoothDeltaTime);
        }

        if (CanMove && Input.touches.Length > 0 && !eventSystem.IsPointerOverGameObject()) //Input.touches[0].position.y > ScreenSup && Input.touches[0].position.y < ScreenInf) 
        {
            if (!isFiring)
            {
                ScreenSup /= 2;
                propM.TriggerSwell(false);
                isFiring = true;
                if (!IsPaused && Ralenti)
                {
                    Time.timeScale = 1; // DOVirtual.Float(Time.timeScale, 1f, 1f, (float t) => Time.timeScale = t);
                    //SoundManager.Instance.SetPitch();
                    DOVirtual.Float(highpass.cutoffFrequency, 10, .5f, (float x) => { highpass.cutoffFrequency = x; highpassVan.cutoffFrequency = x; });
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
        } else if (CanMove && Input.GetMouseButton(0) && !eventSystem.IsPointerOverGameObject()) //Input.mousePosition.y > ScreenSup && Input.mousePosition.y < ScreenInf)
        {
            if(!isFiring)
            {
                ScreenSup /= 2;
                propM.TriggerSwell(false);
                isFiring = true;
                if (!IsPaused && Ralenti)
                {
                    Time.timeScale = 1; // DOVirtual.Float(Time.timeScale, 1f, 1f, (float t) => Time.timeScale = t);
                    //SoundManager.Instance.SetPitch();
                    DOVirtual.Float(highpass.cutoffFrequency, 10, .5f, (float x) => { highpass.cutoffFrequency = x; highpassVan.cutoffFrequency = x; });
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
                ScreenSup *= 2;
                propM.TriggerSwell(true);
                isFiring = false;
                if (!IsPaused && Ralenti)
                {
                    Time.timeScale = ValeurRalenti;
                    DOVirtual.Float(highpass.cutoffFrequency, 5000, .5f, (float x) => { highpass.cutoffFrequency = x; highpassVan.cutoffFrequency = x; });
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
        if (CanScaleUp)
        {
            Time.timeScale = 1; DOVirtual.Float(highpass.cutoffFrequency, 10, .5f, (float x) => { highpass.cutoffFrequency = x; highpassVan.cutoffFrequency = x; });
        }
        CanScaleUp = true;
    }

    void SpawnBullet()
    {
        Instantiate(Bullet, tf.position - new Vector3(0, 0, 0.1f), Quaternion.identity);
        SoundManager.Instance.PlayShoot();
        AmplitudeData.Instance.BulletsFired++;
    }

    public void DoDamage(int damage)
    {
        if(CanBeHurt && LifePoints>0)
        {
            Hit.Play(true);
            LifePoints -= damage;
            HasLostLife = true;

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
        if(GameManager.Instance.GetScore() >= 7000)
        {
            AchievementManager.Instance.UnlockAchievement(2);
        }

        BusTex.material.mainTexture = PoliceTex;
        Ralenti = false;
        Destruction.Play(true);
        Trail.Stop(true);
        RainbowTrail.enabled = false;
        CanMove = false;
        //Render.enabled = false;
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
        //propM.DisableToggles(AmmoProperties.Cooldown);
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
        } else if(other.CompareTag("PowerUp"))
        {
            var pu = other.GetComponent<PowerUp>();
            if(pu!=null)
            {
                propM.AddProperty(pu.GetPower());
                Destroy(other.gameObject);
            }
        }
    }
}