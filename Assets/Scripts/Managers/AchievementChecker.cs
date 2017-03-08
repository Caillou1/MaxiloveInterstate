using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AchievementChecker : MonoBehaviour {
    public GameObject[] AchievementsChecked;

	public void Check()
    {
        for(int i=0; i<AchievementsChecked.Length; i++)
        {
            if(PlayerPrefs.GetInt("Ach"+i, 0) == 1)
            {
                AchievementsChecked[i].SetActive(true);
            }
        }
    }
}
