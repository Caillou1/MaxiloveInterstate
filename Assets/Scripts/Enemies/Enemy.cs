using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Enemy : MonoBehaviour {
    public float LifePoints;
    public float MovementSpeed;
    public float CollisionDamage = 1;
    public int Score;
    public int Money;
    public float DropLifeChance;
    public float FireRate = .001f;
    public GameObject Life;
    public GameObject Bullet;
    public Texture PeaceTexture;
    public float MinimumTimeBetweenShots = 1f;
    public float MaximumTimeBetweenShots = 3f;

    protected Material[] InitialsMaterial;
    protected GameManager gameManager;
    protected List<Vector3> Waypoints;
    protected Transform tf;
    protected Rigidbody rb;
    protected bool CanMove;
    protected int WaypointTarget;
    protected bool CanBeHurt;
    protected bool CanFire = true;

    //Variations
    public bool isShielded;
    public float ShieldLife;
    public bool isArmored;
    public bool hasMinions;
    public GameObject Minion;

    //Particles
    private ParticleSystem Trail;
    private ParticleSystem TrailPeace;
    private ParticleSystem Hit;
    private ParticleSystem Pacification;

    void Start () {
        Init();
	}

    protected void Init()
    {
        SpawnManager.Instance.Register(gameObject);
        CanMove = false;
        CanBeHurt = false;
        tf = transform;
        rb = GetComponent<Rigidbody>();

        WaypointTarget = 0;
        StartCoroutine(MoveToScreen());
        StartCoroutine(DecreaseScore());
        gameManager = GameObject.Find("Manager").GetComponent<GameManager>();

        Trail = tf.FindChild("Trail").GetComponent<ParticleSystem>();
        TrailPeace = tf.FindChild("TrailPeace").GetComponent<ParticleSystem>();
        Hit = tf.FindChild("Hit").GetComponent<ParticleSystem>();
        Pacification = tf.FindChild("Peace").GetComponent<ParticleSystem>();

        var tmp = tf.GetComponentsInChildren<MeshRenderer>();
        InitialsMaterial = new Material[tmp.Length];
        for(int i = 0; i<tmp.Length; i++)
        {
            InitialsMaterial[i] = tmp[i].material;
        }

        StartCoroutine(Fire());
    }

    public bool IsDead()
    {
        return LifePoints <= 0;
    }

    protected void InitMinions()
    {
        /*if (hasMinions)
        {
            var o = Instantiate(Minion, tf.position + new Vector3(1, -1), Quaternion.identity);
            o.GetComponent<Minion>().SetParent(gameObject);
            o = Instantiate(Minion, tf.position + new Vector3(1, 1), Quaternion.identity);
            o.GetComponent<Minion>().SetParent(gameObject);
            o = Instantiate(Minion, tf.position + new Vector3(-1, 1), Quaternion.identity);
            o.GetComponent<Minion>().SetParent(gameObject);
            o = Instantiate(Minion, tf.position + new Vector3(-1, -1), Quaternion.identity);
            o.GetComponent<Minion>().SetParent(gameObject);
        }*/
    }

    virtual protected void Move()
    {
        if (CanMove)
        {
            if (Waypoints.Count > 0)
            {
                tf.Translate((Waypoints[WaypointTarget] - tf.position).normalized * MovementSpeed * Time.deltaTime);
                rb.velocity = ((Waypoints[WaypointTarget] - tf.position).normalized * MovementSpeed);
            }
        }
    }

    protected void Peace()
    {
        StopAllCoroutines();
        foreach (var im in InitialsMaterial)
        {
            im.mainTexture = PeaceTexture;
        }
        CanFire = false;
        CanBeHurt = false;
        Trail.Stop(true);
        TrailPeace.Play(true);
        Pacification.Play(true);
        Destroy(Pacification.gameObject, 5);
        Pacification.transform.parent = null;
        Pacification.GetComponent<Rigidbody>().velocity = Vector3.down * 10;

        gameManager.AddScore(Score);
        gameManager.AddMoney(Money);
        SoundManager.Instance.PlayPickMoney();

        if((this as SmallEnemy) != null)
        {
            SoundManager.Instance.PlayPeaceMoto();
        } else if((this as MediumEnemy) != null)
        {
            SoundManager.Instance.PlayPeaceVoiture();
        } else if((this as BigEnemy) != null)
        {
            SoundManager.Instance.PlayPeaceTank();
        } else
        {
            Debug.Log("Enemy is not Small, Medium or Big.");
        }

        DropLife();
        CanMove = false;
        StartCoroutine(MoveOffScreen());
    }

    protected void DropLife()
    {
        if(Random.value <= DropLifeChance)
        {
            Instantiate(Life, tf.position - new Vector3(0, 0, 0.1f), Quaternion.identity);
        }
    }

    protected void InitWaypoints()
    {
        Waypoints = new List<Vector3>();

        List<GameObject> WaypointsToDestroy = new List<GameObject>();

        for (int i = 0; i < tf.childCount; i++)
        {
            var child = tf.GetChild(i);
            if (child.CompareTag("Waypoint"))
            {
                Waypoints.Add(child.position);
                WaypointsToDestroy.Add(child.gameObject);
            }
        }

        foreach (var w in WaypointsToDestroy)
        {
            Destroy(w);
        }
    }

    public void LeaveBomb(float time, float radius, float damage)
    {
        StartCoroutine(BombCountdown(time, radius, damage));
    }

    public void DoDamage(float damage, bool ignoreShield)
    {
        if (CanBeHurt)
        {
            Hit.Play();
            if (isShielded && !ignoreShield)
            {
                ShieldLife -= damage;
                if (ShieldLife <= 0)
                {
                    isShielded = false;
                }
            }
            else
            {
                if (LifePoints > 0)
                {
                    if (isArmored)
                    {
                        damage /= 2;
                    }
                    LifePoints -= damage;
                    if (LifePoints <= 0)
                        Peace();
                }
            }
        }
    }

    protected abstract void Shot(bool ignoreRandom);

    protected IEnumerator FirstShot()
    {
        yield return new WaitForSeconds(.1f);
        Shot(true);
    }

    protected IEnumerator Fire()
    {
        yield return new WaitForSeconds(Random.Range(MinimumTimeBetweenShots, MaximumTimeBetweenShots));
        Shot(false);
        StartCoroutine(Fire());
    }

    IEnumerator DecreaseScore()
    {
        yield return new WaitForSeconds(.1f);
        if (Score > 1)
        {
            Score--;
            StartCoroutine(DecreaseScore());
        }
    }

    IEnumerator BombCountdown(float time, float radius, float damage)
    {
        yield return new WaitForSeconds(time);
        SoundManager.Instance.PlayExplosion();
        DoDamage(damage, false);
        isArmored = false;
        var around = Physics.OverlapSphere(tf.position, radius);
        foreach(var e in around)
        {
            if(e.CompareTag("Enemy"))
            {
                e.GetComponent<Enemy>().DoDamage(damage * (1 - (Vector3.Distance(e.transform.position, tf.position)/radius)), false);
            }
        }
    }

    protected IEnumerator ChangeWaypoint()
    {
        yield return new WaitUntil(() => Vector3.SqrMagnitude(Waypoints[WaypointTarget] - tf.position) < 0.1f);
        WaypointTarget = (WaypointTarget + 1) % Waypoints.Count;
        StartCoroutine(ChangeWaypoint());
    }

    IEnumerator CheckOnScreen()
    {
        yield return new WaitUntil(() => (tf.position.y > 10 || tf.position.y < -2) || (tf.position.x < -5 || tf.position.x > 5));
        Peace();
    }

    protected IEnumerator MoveToScreen()
    {
        rb.velocity = Vector3.zero;
        if (tf.position.y > 6)
            rb.velocity += Vector3.down;
        if (tf.position.x < -3)
            rb.velocity += Vector3.right;
        if (tf.position.x > 3)
            rb.velocity += Vector3.left;


        rb.velocity *= MovementSpeed;

        yield return new WaitForEndOfFrame();
        if (tf.position.y > 6 || (tf.position.x <= -3 || tf.position.x >= 3))
            StartCoroutine(MoveToScreen());
        else
        {
            InitWaypoints();
            if(Waypoints.Count != 0) StartCoroutine(ChangeWaypoint());
            rb.velocity = Vector3.zero;
            CanMove = true;
            CanBeHurt = true;
            StartCoroutine(CheckOnScreen());
        }
    }

    IEnumerator MoveOffScreen()
    {
        rb.velocity = Vector3.up * MovementSpeed * 2;
        yield return new WaitForEndOfFrame();
        if (tf.position.y < 10)
            StartCoroutine(MoveOffScreen());
        else
            Destroy(gameObject);
    }

    private void OnTriggerEnter(Collider other)
    {
        if(!IsDead() && other.CompareTag("Player"))
        {
            if (CollisionDamage != 0)
            {
                other.GetComponent<PlayerController>().DoDamage((int)CollisionDamage);
                Peace();
            }
        }
    }

    private void OnDestroy()
    {
        StopAllCoroutines();
        AmplitudeData.Instance.EnemiesKilled++;
        SpawnManager.Instance.Unregister(gameObject);
    }
}
