using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeadFish : MonoBehaviour
{
    private float coolDown = 1;
    private SpriteRenderer image;

    // Start is called before the first frame update
    void Start()
    {
        image = gameObject.GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
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
