using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    public GameObject obstaclePrefab;
    public GameObject coinPrefab; 
    private Vector3 spawnPos;

    void Start()
    {
        spawnPos = new Vector3(25, 0, 4);
        InvokeRepeating("SpawnObstacleWithCoin", 4f, 4f);
    }
    void Update()
    {
        if (coinPrefab == null)
        {
            Debug.Log("Coin prefab is not assigned!");
        }
    }


    void SpawnObstacleWithCoin()
    {
        GameObject obstacle = Instantiate(obstaclePrefab, spawnPos, Quaternion.identity);
        Destroy(obstacle, 5f);

        if (coinPrefab != null)
        {
            Vector3 coinPos = spawnPos + new Vector3(0, 4f, 0);
            GameObject coin = Instantiate(coinPrefab, coinPos, Quaternion.identity);

            coin.transform.parent = obstacle.transform;
        }
    }

}
