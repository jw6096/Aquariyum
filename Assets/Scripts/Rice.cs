using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rice : MonoBehaviour
{
    public float foodVal;

    private float coolDown;
    private bool despawn;

    // Start is called before the first frame update
    void Start()
    {
        foreach (GameObject fish in GameObject.FindGameObjectsWithTag("Fish"))
        {
            fish.SendMessage("addRice", gameObject);
        }

        despawn = false;
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

    private void OnDestroy()
    {
        foreach (GameObject fish in GameObject.FindGameObjectsWithTag("Fish"))
        {
            fish.SendMessage("removeRice", gameObject);
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        coolDown = 3;
        despawn = true;
    }
}
