using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SplashIn : MonoBehaviour
{
    private Rigidbody2D rigidbody2D;

    public GameObject value;

    // Start is called before the first frame update
    void Start()
    {        
        Camera camera = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Camera>();
        rigidbody2D = gameObject.GetComponent<Rigidbody2D>();

        float x = camera.ViewportToWorldPoint(new Vector3(1, 1)).x - gameObject.GetComponent<BoxCollider2D>().size.x / 1.5f;

        gameObject.transform.position = new Vector3(Random.Range(-x, x), 8.0f, gameObject.transform.position.z);
        rigidbody2D.AddForce(new Vector2(0.0f, -500.0f));
        //Debug.Log("Begin Splash");
    }

    // Update is called once per frame
    void Update()
    {
        if (gameObject.transform.position.y <= -1.0f)
        {
            Replace();

            //Debug.Log("End Splash");
        }
        else
        {

        }
    }

    private void Replace()
    {
        Instantiate(value, gameObject.transform.position, Quaternion.identity);

        Destroy(gameObject);
    }
}
