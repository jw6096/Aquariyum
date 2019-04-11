using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChopStickManager : MonoBehaviour
{
    private GameObject fish;
    private GameObject sushi = null;
    private GameObject prey;

    private float coolDown = 1;
    private bool dead = false;
    private List<GameObject> preyFish = new List<GameObject>();

    private Rigidbody2D rigidbody2D;

    public float health = 10;

    // Start is called before the first frame update
    void Start()
    {
        rigidbody2D = gameObject.GetComponent<Rigidbody2D>();
        fish = GameObject.FindGameObjectWithTag("FishManager");
    }

    // Update is called once per frame
    void Update()
    {
        coolDown -= Time.deltaTime;

        if (health <= 0)
        {
            dead = true;

            if (sushi != null)
            {
                sushi.transform.parent = fish.transform;

                sushi.SendMessage("release");

                sushi = null;

                coolDown = 1;
            }

            if (gameObject.transform.position.y < 8)
            {
                rigidbody2D.AddForce(new Vector2(0, 1));
            }
            else
            {
                Destroy(gameObject);
            }
        }
        else
        {
            if (sushi != null)
            {
                if (gameObject.transform.position.y < 8)
                {
                    //Debug.Log("lifting fish");

                    rigidbody2D.AddForce(new Vector2(0, 2));
                }
                else
                {
                    Destroy(sushi);

                    rigidbody2D.velocity = new Vector2(0, 0);

                    sushi = null;

                    Destroy(gameObject);
                }
            }
            else
            {
                if (prey == null)
                {
                    assignClosestFood();
                }
                else
                {
                    Vector2 direction = new Vector2(prey.transform.position.x - (gameObject.transform.position.x + 0.5f), prey.transform.position.y - (gameObject.transform.position.y + 0.3f)).normalized;

                    rigidbody2D.AddForce(direction);

                    rigidbody2D.velocity += direction;
                }
            }
        }

        rigidbody2D.velocity = rigidbody2D.velocity.normalized * 2.5f;
    }

    public void OnMouseDown()
    {
        Debug.Log("Ouch!");

        health--;

        rigidbody2D.velocity = new Vector2(0, 0);

        if (sushi != null)
        {
            sushi.transform.parent = fish.transform;

            sushi.SendMessage("release");

            sushi = null;

            coolDown = 1;
        }
    }

    private void assignClosestFood()
    {
        float mag = 100;

        preyFish.Clear();

        for (int i = 0; i < fish.transform.childCount; i++)
        {
            preyFish.Add(fish.transform.GetChild(i).gameObject);
        }

        foreach (GameObject food in preyFish)
        {
            if (Vector3.Distance(gameObject.transform.position, food.transform.position) < mag)
            {
                prey = food;

                mag = Vector3.Distance(gameObject.transform.position, food.transform.position);
            }
        }

        if (prey == null)
        {
            return;
        }

        if (prey.GetComponent<BoxCollider2D>().IsTouching(gameObject.GetComponent<CircleCollider2D>()) && sushi == null && coolDown <= 0 && !dead)
        {
            sushi = prey;
            prey = null;

            sushi.transform.parent = gameObject.transform;
            sushi.transform.localPosition = new Vector3(0.5f, 0.3f, 0.0f);

            sushi.SendMessage("grab");

            //Debug.Log(sushi);
        }
    }

    public void OnTriggerEnter2D(Collider2D collision)
    {
        //Debug.Log(sushi);

        if (collision.transform.parent == fish.transform && sushi == null && coolDown <= 0 && collision.IsTouching(gameObject.GetComponent<CircleCollider2D>()) && !dead)
        {
            sushi = collision.gameObject;
            prey = null;

            sushi.transform.parent = gameObject.transform;
            sushi.transform.localPosition = new Vector3(0.5f, 0.3f, 0.0f);

            sushi.SendMessage("grab");

            //Debug.Log(sushi);
        }
    }
}
