using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseManager : MonoBehaviour
{
    private Camera camera;

    // Start is called before the first frame update
    void Start()
    {
        Camera camera = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Camera>();
       float xScale = camera.orthographicSize * 2 * Screen.width / Screen.height;

        gameObject.transform.position = new Vector3(0.0f, -camera.ViewportToWorldPoint(new Vector3(1, 1)).y, 1.0f);
        gameObject.transform.localScale = new Vector3((Screen.width / xScale) * 2.0f, 25, 1.0f);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
