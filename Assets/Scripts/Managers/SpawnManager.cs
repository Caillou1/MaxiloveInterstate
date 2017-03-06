using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    public GameObject[] EasyWaves;
    public GameObject[] MediumWaves;
    public float MediumSpawnChance;
    public GameObject[] HardWaves;
    public float HardSpawnChance;

    public float SpawnChanceAugmentation;

    private bool CanSpawnHard;
    private bool CanSpawnMedium;

    public int MinimumScoreBeforeMedium;
    public int MinimumScoreBeforeHard;
    
    private bool EnemiesJustSpawned;
    private Transform tf;
    private int wave;

    private List<GameObject> EnemiesOnScreen;

    public static SpawnManager Instance = null;

    private void Awake()
    {
        Instance = this;

        tf = transform;
        wave = 0;
        EnemiesOnScreen = new List<GameObject>();

        StartCoroutine(DelayedSpawn());
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

    void Spawn()
    {
        wave++;

        var x = Random.value;

        if (CanSpawnHard && x <= HardSpawnChance)
        {
            SoundManager.Instance.PlaySpawnTank();
            Instantiate(HardWaves[Random.Range(0, HardWaves.Length)], Vector3.zero, Quaternion.identity, tf);
            HardSpawnChance += SpawnChanceAugmentation;
        }
        else if (CanSpawnMedium && (1 - x) <= MediumSpawnChance)
        {
            SoundManager.Instance.PlaySpawnVoiture();
            Instantiate(MediumWaves[Random.Range(0, MediumWaves.Length)], Vector3.zero, Quaternion.identity, tf);
            MediumSpawnChance += SpawnChanceAugmentation;
        }
        else
        {
            SoundManager.Instance.PlaySpawnMoto();
            Instantiate(EasyWaves[Random.Range(0, EasyWaves.Length)], Vector3.zero, Quaternion.identity, tf);
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
