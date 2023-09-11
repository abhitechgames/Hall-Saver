using System.Collections;
using UnityEngine;

public class WaveCreator : MonoBehaviour
{
    [Range(0f, 5f)]
    [SerializeField] private float startSpawnRate = 1f;
    private float currentSpawnRate;

    [SerializeField] private GameObject[] zombiePrefabs;

    [SerializeField] private int currentRangeOfZombies = 0;
    [SerializeField] private Transform[] spawnPos;

    private void Start()
    {
        currentSpawnRate = startSpawnRate;

        StartCoroutine(IncreaseZombieRangeToOne());
        StartCoroutine(DecreaseSpawnRate());
    }

    private void Update()
    {
        if (currentSpawnRate < 0f)
        {
            Instantiate(zombiePrefabs[Random.Range(0, currentRangeOfZombies)], spawnPos[Random.Range(0, spawnPos.Length)].position, Quaternion.identity);
            currentSpawnRate = startSpawnRate;
        }
        else
        {
            currentSpawnRate -= Time.deltaTime;
        }
    }

    public void DecreaseSpawnRate(float decreaseAmount)
    {
        startSpawnRate -= decreaseAmount;
    }

    IEnumerator IncreaseZombieRangeToOne()
    {
        yield return new WaitForSeconds(15f);

        currentRangeOfZombies++;

        StartCoroutine(IncreaseZombieRangeToTwo());
    }

    IEnumerator IncreaseZombieRangeToTwo()
    {
        yield return new WaitForSeconds(15f);

        currentRangeOfZombies++;
    }

    IEnumerator DecreaseSpawnRate()
    {
        yield return new WaitForSeconds(8f);

        startSpawnRate -= 0.25f;

        if (startSpawnRate < .25f)
            startSpawnRate = 0.25f;

        StartCoroutine(DecreaseSpawnRate());
    }
}
