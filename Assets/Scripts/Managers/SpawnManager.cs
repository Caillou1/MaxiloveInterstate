using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    public GameObject[] TutorialWaves;
    private int TutoWave;

    public GameObject[] EasyWaves;
    public GameObject[] MediumWaves;
    public float MediumSpawnChance;
    public GameObject[] HardWaves;
    public float HardSpawnChance;
    public GameObject[] VeryHardWaves;
    public float VeryHardSpawnChance;

    public float SpawnChanceAugmentation;

    private bool CanSpawnVeryHard;
    private bool CanSpawnHard;
    private bool CanSpawnMedium;

    public int MinimumScoreBeforeMedium;
    public int MinimumScoreBeforeHard;
    public int MinimumScoreBeforeVeryHard;
    
    private bool EnemiesJustSpawned;
    private Transform tf;
    private int wave;

    private List<GameObject> EnemiesOnScreen;

    private List<int> NotSeenEasyWaves;
    private List<int> NotSeenMediumWaves;
    private List<int> NotSeenHardWaves;
    private List<int> NotSeenVeryHardWaves;

    public static SpawnManager Instance = null;

    private void Awake()
    {
        Instance = this;
        TutoWave = 0;

        NotSeenEasyWaves = new List<int>();
        NotSeenMediumWaves = new List<int>();
        NotSeenHardWaves = new List<int>();
        NotSeenVeryHardWaves = new List<int>();

        CanSpawnHard = false;
        CanSpawnMedium = false;
        CanSpawnVeryHard = false;

        ResetAllNotSeenWaves();

        tf = transform;
        wave = 0;
        EnemiesOnScreen = new List<GameObject>();

        StartCoroutine(DelayedSpawn());
    }

    public void ResetAllNotSeenWaves()
    {
        ResetNotSeenEasyWaves();
        ResetNotSeenMediumWaves();
        ResetNotSeenHardWaves();
        ResetNotSeenVeryHardWaves();
    }

    public void ResetNotSeenEasyWaves()
    {
        NotSeenEasyWaves.Clear();

        for (int i = 0; i < EasyWaves.Length; i++)
            NotSeenEasyWaves.Add(i);
    }

    public void ResetNotSeenMediumWaves()
    {
        NotSeenMediumWaves.Clear();

        for (int i = 0; i < MediumWaves.Length; i++)
            NotSeenMediumWaves.Add(i);
    }

    public void ResetNotSeenHardWaves()
    {
        NotSeenHardWaves.Clear();

        for (int i = 0; i < HardWaves.Length; i++)
            NotSeenHardWaves.Add(i);
    }

    public void ResetNotSeenVeryHardWaves()
    {
        NotSeenVeryHardWaves.Clear();

        for (int i = 0; i < VeryHardWaves.Length; i++)
            NotSeenVeryHardWaves.Add(i);
    }

    public void Register(GameObject obj)
    {
        EnemiesOnScreen.Add(obj);
    }

    public void Unregister(GameObject obj)
    {
        EnemiesOnScreen.Remove(obj);
    }

    public void SetSpawnMedium()
    {
        CanSpawnMedium = true;
    }

    public void SetSpawnHard()
    {
        CanSpawnHard = true;
    }

    public void SetSpawnVeryHard()
    {
        CanSpawnVeryHard = true;
    }

    void Spawn()
    {
        if (TutoWave == TutorialWaves.Length)
        {
            wave++;

            var x = Random.value;

            if(CanSpawnVeryHard && (1-x) <= VeryHardSpawnChance)
            {
                SoundManager.Instance.PlaySpawnTank();
                if (NotSeenVeryHardWaves.Count == 0)
                    ResetNotSeenHardWaves();
                int y = NotSeenVeryHardWaves[Random.Range(0, NotSeenVeryHardWaves.Count)];
                NotSeenVeryHardWaves.Remove(y);
                Instantiate(VeryHardWaves[y], Vector3.zero, Quaternion.identity, tf);
                VeryHardSpawnChance += SpawnChanceAugmentation;
            }
            else if (CanSpawnHard && x <= HardSpawnChance)
            {
                SoundManager.Instance.PlaySpawnTank();
                if (NotSeenHardWaves.Count == 0)
                    ResetNotSeenHardWaves();
                int y = NotSeenHardWaves[Random.Range(0, NotSeenHardWaves.Count)];
                NotSeenHardWaves.Remove(y);
                Instantiate(HardWaves[y], Vector3.zero, Quaternion.identity, tf);
                HardSpawnChance += SpawnChanceAugmentation;
            }
            else if (CanSpawnMedium && (1 - x) <= MediumSpawnChance)
            {
                SoundManager.Instance.PlaySpawnVoiture();
                if (NotSeenMediumWaves.Count == 0)
                    ResetNotSeenMediumWaves();
                int y = NotSeenMediumWaves[Random.Range(0, NotSeenMediumWaves.Count)];
                NotSeenMediumWaves.Remove(y);
                Instantiate(MediumWaves[y], Vector3.zero, Quaternion.identity, tf);
                MediumSpawnChance += SpawnChanceAugmentation;
            }
            else
            {
                SoundManager.Instance.PlaySpawnMoto();
                if (NotSeenEasyWaves.Count == 0)
                    ResetNotSeenEasyWaves();
                int y = NotSeenEasyWaves[Random.Range(0, NotSeenEasyWaves.Count)];
                NotSeenEasyWaves.Remove(y);
                Instantiate(EasyWaves[y], Vector3.zero, Quaternion.identity, tf);
            }
        } else
        {
            Instantiate(TutorialWaves[TutoWave], Vector3.zero, Quaternion.identity, tf);
            TutoWave++;
        }

        StartCoroutine(WaitForEmptyScreen());
    }

    IEnumerator WaitForEmptyScreen()
    {
        yield return new WaitUntil(() => EnemiesOnScreen.Count != 0);
        yield return new WaitUntil(() => EnemiesOnScreen.Count == 0);
        yield return new WaitForSeconds(1);
        Spawn();
    }

    IEnumerator DelayedSpawn()
    {
        yield return new WaitForSeconds(2);
        Spawn();
    }
}
