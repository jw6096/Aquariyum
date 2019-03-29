using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Coin : MonoBehaviour
{
    //CircleCollider2D cCollider;

    //private float coolDown;
    //private bool despawn;

    // Start is called before the first frame update
    void Start()
    {
        //cCollider = GetComponent<CircleCollider2D>();

        //despawn = false;
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

    public void OnMouseDown()
    {
        PickupCoin();
    }

    public void PickupCoin()
    {
        GameManager.instance.Coins++;
        Debug.Log(GameManager.instance.Coins);
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
}