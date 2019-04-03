using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Seaweed : MonoBehaviour
{
    float minSpawnTime = 5.0f;
    float maxSpawnTime = 10.0f;

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

            //Spawn random distance away
            Vector3 spawnPos = new Vector3(transform.position.x + Random.Range(-3.0f, 3.0f), transform.position.y + Random.Range(1.0f, 3.0f), transform.position.z);
            GameManager.instance.SpawnCoin(spawnPos);
        }
    }

    public void SetSpawnTimers(float min, float max)
    {
        minSpawnTime = Mathf.Abs(min);

        if(max >= min)
        {
            maxSpawnTime = max;
        }
        else
        {
            maxSpawnTime = min + 1.0f;
        }
    }
}
