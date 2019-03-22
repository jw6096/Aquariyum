﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FishManager : MonoBehaviour
{
    private enum FishState
    {
        Full,
        Norm,
        Sick
    }

    // Start is called before the first frame update
    public Sprite fishFull;
    public Sprite fishNorm;
    public Sprite fishSick;

    private float hunger;
    private FishState fishState;

    void Start()
    {
        hunger = 60;
        fishState = FishState.Full;
    }

    // Update is called once per frame
    void Update()
    {
        //depending on state and availability of food, seek food/idleswim?

        hunger -= Time.deltaTime;
        checkState();
    }

    public void checkState()
    {
        switch (fishState)
        {
            case FishState.Full:
                if (hunger < 60)
                {
                    if (hunger <= 40)
                    {
                        fishState = FishState.Norm;
                        gameObject.GetComponent<SpriteRenderer>().sprite = fishNorm;
                    }
                }
                break;

            case FishState.Norm:
                if (hunger > 40)
                {
                    if (hunger <= 40)
                    {
                        fishState = FishState.Full;
                        gameObject.GetComponent<SpriteRenderer>().sprite = fishFull;
                    }
                }
                else if(hunger <= 20)
                {
                    fishState = FishState.Sick;
                    gameObject.GetComponent<SpriteRenderer>().sprite = fishSick;
                }
                break;

            case FishState.Sick:
                if (hunger > 40)
                {
                    if (hunger <= 20)
                    {
                        fishState = FishState.Norm;
                        gameObject.GetComponent<SpriteRenderer>().sprite = fishNorm;
                    }
                }
                else if (hunger <= 0)
                {
                    die();
                }
                break;

            default:
                die();
                break;

        }
    }

    public void die()
    {
        Destroy(gameObject);
        //summon dead fish item
    }
}