using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FishManager : MonoBehaviour
{
    public enum FishState
    {
        Full,
        Norm,
        Sick
    }

    // Start is called before the first frame update

    public float interval;
    public Sprite fishFull;
    public Sprite fishNorm;
    public Sprite fishSick;
    public GameObject deadFish;

    private float hunger;
    private FishState fishState;

    private float x;
    private float y;
    private Camera camera;
    private bool flip;

    private SpriteRenderer image;
    private Rigidbody2D rigidbody2D;
    private List<GameObject> riceList = new List<GameObject>();
    private GameObject closestRice;

    void Start()
    {
        hunger = 2.5f * interval;
        fishState = FishState.Full;

        Camera camera = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Camera>();
        x = camera.ViewportToWorldPoint(new Vector3(1, 1)).x - 1.75f;
        y = camera.ViewportToWorldPoint(new Vector3(1, 1)).y - 1.5f;

        image = gameObject.GetComponent<SpriteRenderer>();
        rigidbody2D = gameObject.GetComponent<Rigidbody2D>();

        riceList.AddRange(GameObject.FindGameObjectsWithTag("Rice"));
    }

    // Update is called once per frame
    void Update()
    {
        //avoid edges
        avoidEdge();

        //depending on state and availability of food, seek food/idleswim?
        if (fishState != FishState.Full && riceList.Count > 0) //assuming food & hungry
        {
            if (closestRice == null)
            {
                assignClosestRice();
            }
            else
            {
                Vector2 direction = new Vector2(closestRice.transform.position.x - gameObject.transform.position.x, closestRice.transform.position.y - gameObject.transform.position.y).normalized;

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
            rigidbody2D.AddForce(new Vector2(Random.Range(-2f, 2f), Random.Range(-2f, 2f)));
        }

        hunger -= Time.deltaTime;
        checkState();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Rice" && fishState != FishState.Full)
        {
            hunger += collision.gameObject.GetComponent<Rice>().foodVal;

            rigidbody2D.velocity /= 5.0f;

            Destroy(collision.gameObject);
        }
    }

    public void checkState()
    {
        switch (fishState)
        {
            case FishState.Full:
                if (hunger < interval * 3.0f)
                {
                    if (hunger <= interval * 2.0f)
                    {
                        fishState = FishState.Norm;
                        image.sprite = fishNorm;
                    }
                }
                break;

            case FishState.Norm:
                if (hunger > interval * 2.0f)
                {
                    fishState = FishState.Full;
                    image.sprite = fishFull;
                }
                else if (hunger <= interval * 1.0f)
                {
                    fishState = FishState.Sick;
                    image.sprite = fishSick;
                }
                break;

            case FishState.Sick:
                if (hunger > interval * 2.0f)
                {
                    if (hunger <= interval * 1.0f)
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

    public void addRice(GameObject rice)
    {
        if (!riceList.Contains(rice))
        {
            riceList.Add(rice);

            assignClosestRice();
        }
    }

    public void removeRice(GameObject rice)
    {
        riceList.Remove(rice);
    }

    private void avoidEdge()
    {
        //avoid y edge
        if (gameObject.transform.position.y > y)
        {
            rigidbody2D.AddForce(new Vector2(0.0f, -1.0f));
        }
        else if (gameObject.transform.position.y < -y)
        {
            rigidbody2D.AddForce(new Vector2(0.0f, 1.0f));
        }

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

    private void assignClosestRice()
    {
        float mag = 100;

        foreach (GameObject rice in riceList)
        {
            if (Vector3.Distance(gameObject.transform.position, rice.transform.position) < mag)
            {
                closestRice = rice;

                mag = Vector3.Distance(gameObject.transform.position, rice.transform.position);
            }
        }
    }

    private void die()
    {
        Instantiate(deadFish, transform.position, Quaternion.identity);

        Destroy(gameObject);
    }
}
