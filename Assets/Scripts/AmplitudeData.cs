using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AmplitudeData : MonoBehaviour {
    [Header("Amplitude")]
    public string APIKEY;

    [Header("Game Data")]
    public int LifePicked;
    public int LifeLost;
    public float GameDuration;
    public float EnemiesKilled;
    private int Score;
    public int BulletsFired;
    public int BulletsFiredWithSplit;
    public int BulletsFiredWithExplosive;
    public int BulletsFiredWithPiercing;
    public int BulletsFiredWithHoming;
    public string MostUsedProperty;
    public int Hits;
    public int NumberOfTouchesOnWeaponUI;

    [Header("Session Data")]
    public float SessionDuration;
    public float AverageGameDuration;
    public float TotalGameDuration;
    public int GamesNumber = 0;
    public int GamesInARow = 0;

    public static AmplitudeData Instance = null;

    private float StartTime;

    private void ResetData()
    {
        LifePicked = 0;
        LifeLost = 0;
        GameDuration = 0;
        EnemiesKilled = 0;
        Score = 0;
        BulletsFired = 0;
        BulletsFiredWithExplosive = 0;
        BulletsFiredWithHoming = 0;
        BulletsFiredWithPiercing = 0;
        BulletsFiredWithSplit = 0;
        MostUsedProperty = "";
        Hits = 0;
        NumberOfTouchesOnWeaponUI = 0;
    }

    private void InitAmplitude()
    {
        AmplitudeHelper.AppId = APIKEY;
        AmplitudeHelper.Instance.FillCustomProperties += FillTrackingProperties;
        AmplitudeHelper.Instance.StartSession();
        StartTime = Time.time;
    }

    void FillTrackingProperties(Dictionary<string, object> properties)
    {
        properties["money"] = PlayerPrefs.GetInt("Money", 0);
    }

    private void Awake()
    {
#if UNITY_STANDALONE
        Screen.SetResolution(480,800, false);
#endif

        if (Instance == null)
        {
            Instance = this;
            ResetData();
            AverageGameDuration = 0;
            TotalGameDuration = 0;
            InitAmplitude();
        }
        else if (Instance != this)
        {
            Destroy(gameObject);
        }

        GamesNumber = PlayerPrefs.GetInt("TotalGames", 0);

        DontDestroyOnLoad(gameObject);
    }

    public void GameFinished()
    {
        GamesInARow++;
        GamesNumber++;

        Score = GameManager.Instance.GetScore();
        GameDuration = GameManager.Instance.GetGameDuration();
        CalculateMostUsedProperty();

        var GameFinisehdProperties = new Dictionary<string, object>()
        {
            { "Score" , Score},
            { "Duration" , GameDuration},
            { "Number of enemies killed" , EnemiesKilled },
            { "Bullets fired" , BulletsFired },
            { "Bullets hit" , Hits },
            { "Most used property" , MostUsedProperty },
            { "Life picked" , LifePicked },
            { "Life lost" , LifeLost },
            { "Number of touches on Weapon UI" , NumberOfTouchesOnWeaponUI }
        };

        TotalGameDuration += GameDuration;
        AverageGameDuration = TotalGameDuration / GamesInARow;

        SaveScore();
        CheckGamesInARow();
        CheckGamesTotal();

        AmplitudeHelper.Instance.LogEvent("Game Finished", GameFinisehdProperties);

        ResetData();
    }

    void SaveScore()
    {
        int Best = PlayerPrefs.GetInt("BestScore", 0);
        if(Score > Best)
        {
            PlayerPrefs.SetInt("BestScore", Score);
            AmplitudeHelper.Instance.LogEvent("New Best Score : " + Score);
        }
    }

    void CheckGamesInARow()
    {
        if(GamesInARow % 10 == 0)
        {
            AmplitudeHelper.Instance.LogEvent(GamesInARow + " games in a row !");
        }
    }

    void CheckGamesTotal()
    {
        if(GamesNumber % 10 == 0)
        {
            AmplitudeHelper.Instance.LogEvent(GamesNumber + " games in total !");
        }
    }

    void CalculateMostUsedProperty()
    {
        int[] tab = new int[4];
        tab[0] = BulletsFiredWithExplosive;
        tab[1] = BulletsFiredWithHoming;
        tab[2] = BulletsFiredWithPiercing;
        tab[3] = BulletsFiredWithSplit;

        int max = Mathf.Max(tab);

        if(max == BulletsFiredWithExplosive)
        {
            MostUsedProperty = "Explosive";
        } else if(max == BulletsFiredWithHoming)
        {
            MostUsedProperty = "Homing";
        }
        else if(max == BulletsFiredWithPiercing)
        {
            MostUsedProperty = "Piercing";
        } else
        {
            MostUsedProperty = "Split";
        }
    }

    private void OnApplicationQuit()
    {
        PlayerPrefs.SetInt("TotalGames", GamesNumber);

        SessionDuration = Time.time - StartTime;

        var QuitProperties = new Dictionary<string, object>() {
            { "Games played during session" , GamesInARow },
            { "Total games played" , GamesNumber },
            { "Session duration" , SessionDuration },
            { "Average game duration" , AverageGameDuration }
        };

        AmplitudeHelper.Instance.LogEvent("Game quitted", QuitProperties);
        AmplitudeHelper.Instance.EndSession();
    }
}