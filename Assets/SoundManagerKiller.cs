using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManagerKiller : MonoBehaviour {
    public void Kill()
    {
        Destroy(GameObject.Find("SoundManager").gameObject);
    }
}
