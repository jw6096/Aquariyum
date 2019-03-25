using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Coin : MonoBehaviour
{
    CircleCollider2D cCollider;

    // Start is called before the first frame update
    void Start()
    {
        cCollider = GetComponent<CircleCollider2D>();
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetMouseButtonDown(0))
        {
            Vector3 mousePos = GameManager.instance.MainCamera.ScreenToWorldPoint(Input.mousePosition);
            if(cCollider.OverlapPoint(new Vector2(mousePos.x, mousePos.y)))
            {
                PickupCoin();
            }
        }
    }

    public void PickupCoin()
    {
        GameManager.instance.Coins++;
        Debug.Log(GameManager.instance.Coins);
        Destroy(this.gameObject);
    }
}