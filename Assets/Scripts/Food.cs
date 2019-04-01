using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Food : MonoBehaviour
{
    public float foodVal;
    private GameObject fish;

    // Start is called before the first frame update
    void Start()
    {
        fish = GameObject.FindGameObjectWithTag("FishManager");

        fish.BroadcastMessage("addFood", gameObject, SendMessageOptions.DontRequireReceiver);
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void OnDestroy()
    {
        fish.BroadcastMessage("removeFood", gameObject);
    }
}
