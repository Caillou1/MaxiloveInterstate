using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AchievementManager : MonoBehaviour {
    public static AchievementManager Instance = null;

    private void Awake()
    {
        if(Instance == null)
        {
            Instance = this;
        } else if(Instance != this)
        {
            Destroy(gameObject);
        }

        DontDestroyOnLoad(gameObject);
    }

    public void UnlockAchievement(int i)
    {
        if (PlayerPrefs.GetInt("Ach" + i, 0) == 0)
        {
            Debug.Log("Unlocked " + i + " !");
            PlayerPrefs.SetInt("Ach" + i, 1);
            AmplitudeHelper.Instance.LogEvent("Achievement " + i + " unlocked");
        }
    }
}
