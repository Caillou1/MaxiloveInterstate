using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour {
    public float ComboTime;
    public Text texte;

    private float StartTime;
    private int Money;
    private int Score;
    private int NotYetAddedScore;
    private int ScoreMultiplier;

    public static GameManager Instance;

    private void Start()
    {
        Instance = this;
        Time.timeScale = 1f;
        Score = 0;
        NotYetAddedScore = 0;
        ScoreMultiplier = 0;
        Money = 0;
        StartTime = Time.time;
    }

    private void Update()
    {
        //var txt = "Money : " + Money;
        var txt = "Score : " + Score + "\n";
        if(NotYetAddedScore>0) txt += "+" + NotYetAddedScore + "x" + ScoreMultiplier;
        texte.text = txt;
    }

    private void OnDestroy()
    {
        PlayerPrefs.SetInt("Money", Money);
    }

    public float GetGameDuration()
    {
        return Time.time - StartTime;
    }

    public int GetScore()
    {
        return Score + NotYetAddedScore*ScoreMultiplier;
    }

    public int GetMoney()
    {
        return Money;
    }

    public void AddScore(int s)
    {
        NotYetAddedScore += s;
        ScoreMultiplier++;
        texte.GetComponent<ScaleModulator>().TriggerSwell();
        StopAllCoroutines();
        StartCoroutine(DelayedAdd());
    }

    public void AddMoney(int m)
    {
        Money += m;
    }

    IEnumerator DelayedAdd()
    {
        yield return new WaitForSeconds(ComboTime);
        texte.GetComponent<ScaleModulator>().TriggerSwell();
        Score += NotYetAddedScore * ScoreMultiplier;

        if (Score >= SpawnManager.Instance.MinimumScoreBeforeMedium)
        {
            SpawnManager.Instance.SetSpawnMedium();
        }

        if (Score >= SpawnManager.Instance.MinimumScoreBeforeHard)
        {
            SpawnManager.Instance.SetSpawnHard();
        }

        NotYetAddedScore = 0;
        ScoreMultiplier = 0;
    }
}
