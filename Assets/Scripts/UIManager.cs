﻿using System.Collections;
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
        if(isSlidingIn)
        {
            RectTransform rt = buyMenu.GetComponent<RectTransform>();
            rt.anchoredPosition = new Vector2(0, rt.anchoredPosition.y - 1);
            if (rt.anchoredPosition.y <= -65)
            {
                rt.anchoredPosition = new Vector2(0, -65);
                isSlidingIn = false;
                isStoreVisable = true;
                for (int i = 0; i < storeButtons.Length; i++)
                {
                    if (i == gm.seaweedSlotNumber)
                    {
                        if (gm.numSeaweed != 2)
                        {
                            storeButtons[i].interactable = true;
                        }
                    }
                    else
                    {
                        storeButtons[i].interactable = true;
                    }
                }
                slider.GetComponent<ScrollRect>().enabled = true;
                buyButton.interactable = true;
            }
        }
        if(isSlidingOut)
        {
            storeButtons[0].GetComponent<Image>().color = Color.white;
            GetComponent<GameManager>().isBuyingRice = false;
            RectTransform rt = buyMenu.GetComponent<RectTransform>();
            rt.anchoredPosition = new Vector2(0, rt.anchoredPosition.y + 1);
            if (rt.anchoredPosition.y >= 0)
            {
                rt.anchoredPosition = new Vector2(0, 0);
                isSlidingOut = false;
                isStoreVisable = false;
                buyButton.interactable = true;
            }
        }
        UpdateCoins(gm.Coins);
    }

    public void UpdateCoins(int amount)
    {
        coins.text = "Coins: " + amount.ToString();
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
