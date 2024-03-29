﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Drift : MonoBehaviour
{
    private float coolDown;
    private bool despawn;

    // Start is called before the first frame update
    void Start()
    {
        despawn = false;

        //aesthetic purposes only
        gameObject.GetComponent<Rigidbody2D>().angularVelocity = Random.Range(-30.0f, 30.0f);
    }

    // Update is called once per frame
    void Update()
    {
        if (despawn)
        {
            coolDown -= Time.deltaTime;

            if (coolDown <= 0)
            {
                Destroy(gameObject);
            }
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Ground")
        {
            coolDown = 3;
            despawn = true;
        }
    }
}
