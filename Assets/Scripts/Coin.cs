using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Coin : MonoBehaviour
{
    //CircleCollider2D cCollider;

    //private float coolDown;
    //private bool despawn;
    public GameManager gm;
    public int value = 1;
    private GameObject fish;

    // Start is called before the first frame update
    void Start()
    {
        gm = GameObject.FindGameObjectWithTag("Game Manager").GetComponent<GameManager>();
        //cCollider = GetComponent<CircleCollider2D>();

        //despawn = false;

        //Random weighted coin value spawning until coin spawners are in
        /*SpriteRenderer coinSprite = GetComponentInChildren<SpriteRenderer>();
        int randVal = Random.Range(0, 100);

        if(randVal < 70)
        {
            randVal = 0;
        }
        else if(randVal < 90)
        {
            randVal = 1;
        }
        else
        {
            randVal = 2;
        }

        switch(randVal)
        {
            case 0:
                value = 1;
                break;
            case 1:
                value = 5;
                //coinSprite.color = new Color(1.0f, 1.0f, 0.0f, 1.0f);   //yellow
                break;
            case 2:
                value = 10;
                //coinSprite.color = new Color(0.0f, 1.0f, 1.0f, 1.0f);   //light blue
                break;
            default:
                value = 1;
                break;
        }*/

        fish = GameObject.FindGameObjectWithTag("FishManager");

        fish.BroadcastMessage("addCoin", gameObject, SendMessageOptions.DontRequireReceiver);
    }

    // Update is called once per frame
    void Update()
    {
        /*
        if (despawn)
        {
            coolDown -= Time.deltaTime;

            if (coolDown <= 0)
            {
                Destroy(gameObject);
            }
        }
        */
    }

    public void OnMouseOver()
    {
        PickupCoin();
    }

    public void PickupCoin()
    {
        GameManager.instance.Coins += gm.value;
        Destroy(this.gameObject);
    }

    /*
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Ground")
        {
            coolDown = 3;
            despawn = true;
        }
    }
    */

    private void OnDestroy()
    {
        fish.BroadcastMessage("removeCoin", gameObject, SendMessageOptions.DontRequireReceiver);
    }
}