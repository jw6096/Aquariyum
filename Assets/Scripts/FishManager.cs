using System.Collections;
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
    public GameObject deadFish;

    private float hunger;
    private FishState fishState;

    private SpriteRenderer image;
    private Rigidbody2D rigidbody2D;

    void Start()
    {
        hunger = 60;
        fishState = FishState.Full;

        image = gameObject.GetComponent<SpriteRenderer>();
        rigidbody2D = gameObject.GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        //depending on state and availability of food, seek food/idleswim?
        if (true) //assuming no food
        {
            rigidbody2D.velocity += new Vector2(Random.Range(-0.25f, 0.25f), Random.Range(-0.25f, 0.25f));
        }

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
                        image.sprite = fishNorm;
                    }
                }
                break;

            case FishState.Norm:
                if (hunger > 40)
                {
                    if (hunger <= 40)
                    {
                        fishState = FishState.Full;
                        image.sprite = fishFull;
                    }
                }
                else if(hunger <= 20)
                {
                    fishState = FishState.Sick;
                    image.sprite = fishSick;
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
        Instantiate(deadFish, transform.position, Quaternion.identity);

        Destroy(gameObject);
    }
}
