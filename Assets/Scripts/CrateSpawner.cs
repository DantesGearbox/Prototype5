using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CrateSpawner : MonoBehaviour
{
    public GameObject prefab;

    public float interval;
    public float amountOfCrates;

    private BoxCollider spawnArea;

    private float startTimestamp;

    // Use this for initialization
    void Start()
    {
        spawnArea = GetComponent<BoxCollider>();

        Invoke("SpawnCrates", 0);
    }

    // Update is called once per frame
    void Update()
    {
        if (Time.time - startTimestamp > interval)
        {
            SpawnCrates();

            startTimestamp = Time.time;
        }
    }

    void SpawnCrates()
    {
        for (int i = 0; i < amountOfCrates; i++)
        {
            Vector3 pos = new Vector3(Random.Range(spawnArea.bounds.min.x, spawnArea.bounds.max.x), transform.position.y + 0.5f, Random.Range(spawnArea.bounds.min.z, spawnArea.bounds.max.z));
            Instantiate(prefab, pos, Quaternion.identity);
        }
    }
}
