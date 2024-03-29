﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BottomFeederManager : MonoBehaviour
{
    public enum FishState
    {
        Full,
        Norm,
        Sick,
        Dead
    }

    // Start is called before the first frame update
    public float ageUpAt;
    public float feedingInterval;
    public string[] consumables;
    public Sprite[] sprite;
    public GameObject nextStage;

    private float age = 0;
    private float coolDown = 1;
    private float hunger = 0;
    private FishState fishState;

    private float x;
    private Camera camera;
    private bool flip;
    private bool nabbed = false;

    private GameObject fishManager;
    private SpriteRenderer image;
    private Rigidbody2D rigidbody2D;
    private List<GameObject> foodList = new List<GameObject>();
    private List<GameObject> coinList = new List<GameObject>();
    private GameObject closestFood;
    private GameObject closestCoin;

    void Start()
    {
        fishManager = GameObject.FindGameObjectWithTag("FishManager");
        gameObject.transform.parent = fishManager.transform;
        
        if (hunger == 0)
        {
            hunger = 2.5f * feedingInterval;
        }
        fishState = FishState.Full;

        Camera camera = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Camera>();
        x = camera.ViewportToWorldPoint(new Vector3(1, 1)).x - gameObject.GetComponent<BoxCollider2D>().size.x / 1.5f;

        image = gameObject.GetComponent<SpriteRenderer>();
        rigidbody2D = gameObject.GetComponent<Rigidbody2D>();

        foreach (string consumable in consumables)
        {            
            foodList.AddRange(GameObject.FindGameObjectsWithTag(consumable));
        }

        coinList.AddRange(GameObject.FindGameObjectsWithTag("Coin"));
    }

    // Update is called once per frame
    void Update()
    {
        if (nabbed)
        {
            return;
        }

        if (fishState != FishState.Dead)
        {
            //avoid edges
            avoidEdge();

            //depending on state and availability of food, seek food/idleswim?
            if (fishState != FishState.Full && foodList.Count > 0) //assuming food & hungry
            {
                if (closestFood == null)
                {
                    assignClosestFood();
                }
                else
                {
                    Vector2 direction = new Vector2(closestFood.transform.position.x - gameObject.transform.position.x, 0).normalized;

                    rigidbody2D.AddForce(direction);

                    rigidbody2D.velocity += direction;
                }

                if (rigidbody2D.velocity.magnitude > 2.0f)
                {
                    rigidbody2D.velocity = rigidbody2D.velocity.normalized * 2.0f;
                }
            }
            else if(coinList.Count > 0) //assuming coins are present
            {
                if (closestCoin == null)
                {
                    assignClosestCoin();
                }
                else
                {
                    Vector2 direction = new Vector2(closestCoin.transform.position.x - gameObject.transform.position.x, 0).normalized;

                    rigidbody2D.AddForce(direction);

                    rigidbody2D.velocity += direction;
                }

                if (rigidbody2D.velocity.magnitude > 2.0f)
                {
                    rigidbody2D.velocity = rigidbody2D.velocity.normalized * 2.0f;
                }
            }
            else
            {
                rigidbody2D.AddForce(new Vector2(Random.Range(-2f, 2f), 0));
            }

            hunger -= Time.deltaTime;
            age += Time.deltaTime;
            checkState();
        }
        else
        {
            coolDown -= Time.deltaTime;
            image.color = new Color(1, 1, 1, coolDown);

            gameObject.transform.position -= new Vector3(0, 0.05f, 0);

            if (coolDown <= 0)
            {
                Destroy(gameObject);
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Coin")
        {
            collision.SendMessage("PickupCoin", null, SendMessageOptions.DontRequireReceiver);
        }

        foreach (string consumable in consumables)
        {
            if (collision.tag == consumable && fishState != FishState.Full)
            {
                hunger += collision.gameObject.GetComponent<Food>().foodVal;

                rigidbody2D.velocity /= 5.0f;

                Destroy(collision.gameObject);
            }
        }
    }

    public void setHunger(float assgn)
    {
        hunger = assgn;
    }

    public void grab()
    {
        nabbed = true;

        rigidbody2D.simulated = false;
    }

    public void release()
    {
        nabbed = false;

        rigidbody2D.simulated = true;
    }

    public void addFood(GameObject food)
    {
        if (System.Array.IndexOf(consumables, food.tag) != -1 && !foodList.Contains(food))
        {
            foodList.Add(food);

            assignClosestFood();
        }
    }

    public void removeFood(GameObject food)
    {
        if (foodList.Contains(food))
        {
            foodList.Remove(food);

            assignClosestFood();
        }
    }

    public void addCoin(GameObject coin)
    {
        if (!coinList.Contains(coin))
        {
            coinList.Add(coin);

            assignClosestCoin();
        }
    }

    public void removeCoin(GameObject coin)
    {
        if (coinList.Contains(coin))
        {
            coinList.Remove(coin);

            assignClosestCoin();
        }
    }

    private void checkState()
    {
        switch (fishState)
        {
            case FishState.Full:
                if (hunger <= feedingInterval * 1.0f)
                {
                    fishState = FishState.Sick;
                    image.sprite = sprite[2];
                }
                else if (hunger <= feedingInterval * 2.0f)
                {
                    fishState = FishState.Norm;
                    image.sprite = sprite[1];
                }
                break;

            case FishState.Norm:
                if (hunger > feedingInterval * 2.0f)
                {
                    fishState = FishState.Full;
                    image.sprite = sprite[0];
                }
                else if (hunger <= feedingInterval * 1.0f)
                {
                    fishState = FishState.Sick;
                    image.sprite = sprite[2];
                }
                break;

            case FishState.Sick:
                if (hunger > feedingInterval * 1.0f)
                {

                    if (hunger > feedingInterval * 2.0f)
                    {
                        fishState = FishState.Full;
                        image.sprite = sprite[0];
                    }
                    else if (hunger > feedingInterval * 1.0f)
                    {
                        fishState = FishState.Norm;
                        image.sprite = sprite[1];
                    }
                }
                else if (hunger <= 0)
                {
                    fishState = FishState.Dead;
                    image.sprite = sprite[3];
                }
                break;

            default:
                fishState = FishState.Dead;
                image.sprite = sprite[3];
                break;
        }

        if (age >= ageUpAt)
        {
            if (nextStage)
            {
                FishManager temp = Instantiate(nextStage, gameObject.transform.position, Quaternion.identity).GetComponent<FishManager>();

                temp.setHunger(hunger);

                Destroy(gameObject);
            }
            else
            {
                fishState = FishState.Dead;
                image.sprite = sprite[3];
            }
        }
    }

    private void avoidEdge()
    {
        //avoid x edge
        if (gameObject.transform.position.x > x)
        {
            rigidbody2D.AddForce(new Vector2(-1.0f, 0.0f));
        }
        else if (gameObject.transform.position.x < -x)
        {
            rigidbody2D.AddForce(new Vector2(1.0f, 0.0f));
        }

        if (rigidbody2D.velocity.magnitude > 1.5f)
        {
            rigidbody2D.velocity = rigidbody2D.velocity.normalized * 1.5f;
        }

        if (rigidbody2D.velocity.x < 0 && flip)
        {
            image.flipX = false;
            flip = false;
        }
        else if (rigidbody2D.velocity.x > 0 && !flip)
        {
            image.flipX = true;
            flip = true;
        }
    }

    private void assignClosestFood()
    {
        float mag = 100;

        foreach (GameObject food in foodList)
        {
            if (Vector3.Distance(gameObject.transform.position, food.transform.position) < mag)
            {
                closestFood = food;

                mag = Vector3.Distance(gameObject.transform.position, food.transform.position);
            }
        }

        if (closestFood.GetComponent<BoxCollider2D>().IsTouching(gameObject.GetComponent<BoxCollider2D>())) {

            hunger += closestFood.GetComponent<Food>().foodVal;

            rigidbody2D.velocity /= 5.0f;

            removeFood(closestFood);

            Destroy(closestFood);
        }
    }

    private void assignClosestCoin()
    {
        float mag = 100;

        if (coinList.Count > 0)
        {
            foreach (GameObject coin in coinList)
            {
                if (Vector3.Distance(gameObject.transform.position, coin.transform.position) < mag)
                {
                    closestCoin = coin;

                    mag = Vector3.Distance(gameObject.transform.position, coin.transform.position);
                }
            }

            if (closestCoin.GetComponent<CircleCollider2D>().IsTouching(gameObject.GetComponent<BoxCollider2D>()))
            {
                closestCoin.SendMessage("PickupCoin", null, SendMessageOptions.DontRequireReceiver);
            }
        }
        else
        {
            closestCoin = null;
        }
    }
}
