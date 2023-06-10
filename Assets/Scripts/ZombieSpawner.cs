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
                // Cria um novo zumbi na localização do spawn
                Instantiate(zombiePrefab, spawnLocation.position, spawnLocation.rotation);

                // Espera o tempo de atraso antes de continuar o loop
                yield return new WaitForSeconds(spawnDelay);
            }

            // Aqui você pode querer adicionar um atraso maior entre cada "onda" de zumbis
            yield return new WaitForSeconds(5f);
        }
    }
}
