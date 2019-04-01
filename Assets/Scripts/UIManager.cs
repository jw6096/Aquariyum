using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    // UI elements to be assigned in editor
    public Text coins;
    public GameObject buyMenu;
    private GameManager gm;
    private bool isStoreVisable = false; // True if the store is already displayed or not
    private bool isSlidingIn = false;
    private bool isSlidingOut = false;
    void Start()
    {
        gm = GetComponent<GameManager>();
    }

    public void FixedUpdate()
    {
        // TODO: Update to Rect transform so things scale properly
        if(isSlidingIn)
        {
            buyMenu.transform.position = new Vector3(buyMenu.transform.position.x, buyMenu.transform.position.y - 1, buyMenu.transform.position.z);
            if (buyMenu.transform.position.y <= 310)
            {
                buyMenu.transform.position = new Vector3(buyMenu.transform.position.x, 310f, buyMenu.transform.position.z);
                isSlidingIn = false;
                isStoreVisable = true;
            }
        }
        if(isSlidingOut)
        {
            buyMenu.transform.position = new Vector3(buyMenu.transform.position.x, buyMenu.transform.position.y + 1, buyMenu.transform.position.z);
            if (buyMenu.transform.position.y >= 455.5)
            {
                buyMenu.transform.position = new Vector3(buyMenu.transform.position.x, 455.5f, buyMenu.transform.position.z);
                isSlidingOut = false;
                isStoreVisable = false;
            }
        }
    }

    public void UpdateCoins(int amount)
    {
        coins.text = amount.ToString();
    }

    public void SlideStore()
    {
        if (isSlidingIn == false && isSlidingOut == false)
        {
            if (isStoreVisable)
            {
                isSlidingIn = false;
                isSlidingOut = true;
            }
            else
            {
                isSlidingIn = true;
                isSlidingOut = false;
            }
        }
    }
}
