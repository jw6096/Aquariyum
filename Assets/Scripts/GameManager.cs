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
    float spawnTimer;
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
        spawnTimer = Random.Range(0.3f, 4.0f);
    }

    // Update is called once per frame
    void Update()
    {
        if (isBuying)
        {
            if (Input.GetMouseButtonDown(0))
            {
                Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);

                GameObject temp = Instantiate(items[itemSlotNumber], new Vector2(mousePos.x, mousePos.y), Quaternion.identity);
                //temp.SendMessage("splash", null, SendMessageOptions.DontRequireReceiver);
                
                /*
                if (temp.tag == "Fish")
                {
                    temp.GetComponent<FishManager>().splashIn = true;
                }
                */

                coins -= prices[itemSlotNumber];
                isBuying = false;
            }
        }

        //Temp until coin spawner stuff is in
        spawnTimer -= Time.deltaTime;
        if(spawnTimer <= 0.0f)
        {
            SpawnCoin(new Vector3(Random.Range(-7, 7), 2.0f, 0.0f));
            spawnTimer = Random.Range(0.3f, 4.0f);
        }
    }

    public void SpawnCoin(Vector3 position)
    {
        Instantiate(coinPrefab, position, Quaternion.identity);
    }

    public void SpawnCoin(Vector3 position, Transform parentTransform)
    {
        Instantiate(coinPrefab, position, Quaternion.identity, parentTransform);
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
                if (items[slotNumber].tag != "Rice")
                {
                    //Debug.Log(items[slotNumber].tag);
                    Instantiate(items[slotNumber], new Vector2(0, 0), Quaternion.identity).SendMessage("splash", null, SendMessageOptions.DontRequireReceiver);
                }
                else
                {
                    isBuying = true;
                    itemSlotNumber = slotNumber;
                }
            }
        }
    }
}
