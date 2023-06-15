using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZombieSpawner : MonoBehaviour
{
    public GameObject zombiePrefab;
    public Transform spawnLocation;
    public int numberOfZombies = 2;
    public float spawnDelay = 2.0f;

    private void Start()
    {
        StartCoroutine(SpawnZombies());
    }

    IEnumerator SpawnZombies()
    {
        while (true)
        {
            for (int i = 0; i < numberOfZombies; i++)
            {
                Instantiate(zombiePrefab, spawnLocation.position, spawnLocation.rotation);

                yield return new WaitForSeconds(spawnDelay);
            }

            yield return new WaitForSeconds(5f);
        }
    }
}
