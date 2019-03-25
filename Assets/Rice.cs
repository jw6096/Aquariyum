using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rice : MonoBehaviour
{
    public float foodVal;

    // Start is called before the first frame update
    void Start()
    {
        foreach (GameObject fish in GameObject.FindGameObjectsWithTag("Fish"))
        {
            fish.SendMessage("addRice", gameObject);
        }
    }

    // Update is called once per frame
    void Update()
    {
        gameObject.transform.position -= new Vector3(0.0f, 0.25f, 0.0f) * Time.deltaTime;
    }

    private void OnDestroy()
    {
        foreach (GameObject fish in GameObject.FindGameObjectsWithTag("Fish"))
        {
            fish.SendMessage("removeRice", gameObject);
        }
    }
}
