using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    // UI elements to be assigned in editor
    public Text coins;
    public Button buyButton;
    public GameObject buyMenu;
    public Button[] storeButtons;
    public GameObject slider;
    private GameManager gm;
    private bool isStoreVisable = false; // True if the store is already displayed or not
    private bool isSlidingIn = false;
    private bool isSlidingOut = false;
    void Start()
    {
        gm = GetComponent<GameManager>();
        UpdateCoins(gm.Coins);
    }
    public void FixedUpdate()
    {
        // TODO: Update to Rect transform so things scale properly
        if(isSlidingIn)
        {
            RectTransform rt = buyMenu.GetComponent<RectTransform>();
            rt.anchoredPosition = new Vector2(0, rt.anchoredPosition.y - 1);
            if (rt.anchoredPosition.y <= -300)
            {
                rt.anchoredPosition = new Vector2(0, -300);
                isSlidingIn = false;
                isStoreVisable = true;
                foreach (Button button in storeButtons)
                {
                    button.interactable = true;
                }
                slider.GetComponent<ScrollRect>().enabled = true;
                buyButton.interactable = true;
            }
        }
        if(isSlidingOut)
        {
            RectTransform rt = buyMenu.GetComponent<RectTransform>();
            rt.anchoredPosition = new Vector2(0, rt.anchoredPosition.y + 1);
            if (rt.anchoredPosition.y >= -250)
            {
                rt.anchoredPosition = new Vector2(0, -250);
                isSlidingOut = false;
                isStoreVisable = false;
                buyButton.interactable = true;
            }
        }

        UpdateCoins(gm.Coins);
    }

    public void UpdateCoins(int amount)
    {
        coins.text = amount.ToString();
    }

    public void SlideStore()
    {
        if (isSlidingIn == false && isSlidingOut == false)
        {
            buyButton.interactable = false;
            if (isStoreVisable)
            {
                isSlidingIn = false;
                isSlidingOut = true;
                foreach (Button button in storeButtons)
                {
                    button.interactable = false;
                }
                slider.GetComponent<ScrollRect>().enabled = false;
            }
            else
            {
                isSlidingIn = true;
                isSlidingOut = false;
            }
        }
    }
}
