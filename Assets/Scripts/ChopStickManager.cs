using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChopStickManager : MonoBehaviour
{
    private GameObject fish;
    private GameObject sushi;
    private GameObject prey;

    private List<GameObject> preyFish;

    public float health;

    // Start is called before the first frame update
    void Start()
    {
        for (int i = 0; i < fish.transform.childCount; i++)
        {
            preyFish.Add(fish.transform.GetChild(i).gameObject);
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (health <= 0)
        {
            if (sushi != null)
            {
                sushi.transform.parent = fish.transform;
            }

            //chopsticks float up, die when above screen. 
        }
        else
        {
            if (sushi != null)
            {
                //float upwards, destroy sushi when above screen
            }

            //seek nearest fish, grab and parent, set parented fish to sushi
        }
    }

    public void OnMouseDown()
    {
        health--;

        if (sushi != null)
        {
            sushi.transform.parent = fish.transform;
        }
    }
}
