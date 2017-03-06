using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScrollManager : MonoBehaviour {
    public GameObject[] Chunks;

    public GameObject LastSpawnedChunk;
    public GameObject MiddleSpawnedChunk;
    public GameObject FirstSpawnedChunk;

    public float ChunkSize = 8;

    private List<GameObject> SpawnedChunks;
    private List<GameObject> ActiveChunks;

    private Transform tf;

    private void Start()
    {
        tf = transform;
        SpawnedChunks = new List<GameObject>();
        ActiveChunks = new List<GameObject>();

        foreach (var chunk in Chunks)
        {
            SpawnedChunks.Add(Instantiate(chunk, new Vector3(100, 100, 100), Quaternion.Euler(0, -180, 0), tf));
        }

        ActiveChunks.Add(LastSpawnedChunk);
        ActiveChunks.Add(MiddleSpawnedChunk);
        ActiveChunks.Add(FirstSpawnedChunk);

        StartCoroutine(SpawnChunk());
    }

    IEnumerator SpawnChunk()
    {
        yield return new WaitUntil(() => FirstSpawnedChunk.transform.position.y >= ChunkSize);
        FirstSpawnedChunk.transform.position = new Vector3(100, 100, 100);
        FirstSpawnedChunk.GetComponent<ScrollBehaviour>().SetCanMove(false);
        SpawnedChunks.Add(FirstSpawnedChunk);
        ActiveChunks.Remove(FirstSpawnedChunk);

        var m_tmp = MiddleSpawnedChunk;
        var l_tmp = LastSpawnedChunk;

        LastSpawnedChunk = SpawnedChunks[Random.Range(0, SpawnedChunks.Count)].gameObject;
        LastSpawnedChunk.transform.position = new Vector3(0, l_tmp.transform.position.y - 8, 0);
        LastSpawnedChunk.GetComponent<ScrollBehaviour>().SetCanMove(true);

        ActiveChunks.Add(LastSpawnedChunk);
        SpawnedChunks.Remove(LastSpawnedChunk);

        MiddleSpawnedChunk = l_tmp;
        FirstSpawnedChunk = m_tmp;
        StartCoroutine(SpawnChunk());
    }
}
