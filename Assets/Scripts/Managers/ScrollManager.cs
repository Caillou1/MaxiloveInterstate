using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScrollManager : MonoBehaviour {
    public GameObject[] Chunks;

    public GameObject LastSpawnedChunk;
    public GameObject MiddleSpawnedChunk;
    public GameObject FirstSpawnedChunk;

    private Transform tf;

    private void Start()
    {
        tf = transform;
        StartCoroutine(SpawnChunk());
    }

    IEnumerator SpawnChunk()
    {
        yield return new WaitUntil(() => FirstSpawnedChunk.transform.position.y <= -16);
        Destroy(FirstSpawnedChunk);
        var m_tmp = MiddleSpawnedChunk;
        var l_tmp = LastSpawnedChunk;
        LastSpawnedChunk = Instantiate(Chunks[Random.Range(0, Chunks.Length)], new Vector3(0, l_tmp.transform.position.y, 0) + new Vector3(0, 8, 0), Quaternion.Euler(0,-180,0), tf);
        MiddleSpawnedChunk = l_tmp;
        FirstSpawnedChunk = m_tmp;
        StartCoroutine(SpawnChunk());
    }
}
