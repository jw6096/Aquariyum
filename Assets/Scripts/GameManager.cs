using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    public GameObject coinPrefab;
    Camera mainCamera;
    int coins = 99999;      //Number of coins
    int numberFish; //Number of owned fish
    private bool isBuying = false;
    public List<GameObject> items = new List<GameObject>();
    public List<int> prices = new List<int>();
    private int itemSlotNumber = 0;

    public int Coins
    {
        get { return coins; }
        set
        {
            if(value >= 0)
            {
                coins = value;
            }
        }
    }

    public Camera MainCamera
    {
        get { return mainCamera; }
    }

    private void Awake()
    {
        //Make sure only one instance exists, singleton
        if (GameManager.instance == null)
        {
            GameManager.instance = this;
        }
        else if (GameManager.instance != this)
        {
            Destroy(this.gameObject);
        }

        mainCamera = Camera.main;

        DontDestroyOnLoad(this);
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (isBuying)
        {
            if (Input.GetMouseButtonDown(0))
            {
                Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                Instantiate(items[itemSlotNumber], new Vector2(mousePos.x, mousePos.y), Quaternion.identity);
                coins -= prices[itemSlotNumber];
                isBuying = false;
            }
        }
    }

    public void SpawnCoin(Vector2 position)
    {
        Instantiate(coinPrefab, new Vector3(position.x, position.y, 0.0f), Quaternion.identity);
    }

    public void SpawnCoin(Vector3 position, Transform parentTransform)
    {
        Instantiate(coinPrefab, new Vector3(position.x, position.y, 0.0f), Quaternion.identity, parentTransform);
    }

    public void QuitGame()
    {
        Application.Quit();
    }

    public void BuyItem(int slotNumber)
    {
        if (items[itemSlotNumber] != null)
        {
            if (coins - prices[slotNumber] > 0)
            {
                isBuying = true;
                itemSlotNumber = slotNumber;
            }
        }
    }
}
