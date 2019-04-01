using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Seaweed : MonoBehaviour
{
    public float minSpawnTime;
    public float maxSpawnTime;

    float coinTimer;

    // Start is called before the first frame update
    void Start()
    {
        coinTimer = Random.Range(minSpawnTime, maxSpawnTime);
    }

    // Update is called once per frame
    void Update()
    {
        coinTimer -= Time.deltaTime;

        if(coinTimer <= 0)
        {
            coinTimer = Random.Range(minSpawnTime, maxSpawnTime);

            GameManager.instance.SpawnCoin(transform.position + Vector3.up);
        }
    }
}
