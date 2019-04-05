using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    public GameObject bronze;
    public GameObject silver;
    public GameObject gold;
    Camera mainCamera;
    int coins = 100;      //Number of coins
    int numberFish; //Number of owned fish
    float spawnTimer;
    public GameObject rice;
    public List<GameObject> items = new List<GameObject>();
    public List<int> prices = new List<int>();
    private int itemSlotNumber = 0;
    [HideInInspector] public ushort value;

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
        if (Input.GetMouseButtonDown(0))
        {
            Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            RaycastHit2D hit = Physics2D.Raycast(new Vector2(mousePos.x, mousePos.y), Vector2.zero, 0);
            if (!hit)
            {
                // TODO: Set up correct way to raycast for UI with another script
                if (mousePos.y < 3)
                {
                    if (coins >= 1)
                    {
                        GameObject temp = Instantiate(rice, new Vector2(mousePos.x, mousePos.y), Quaternion.identity);
                        coins -= 1;
                        GetComponent<UIManager>().UpdateCoins(coins);
                    }
                }
            }
            else
            {
                if (hit.transform.gameObject.tag == "Coin")
                {
                    coins += hit.transform.gameObject.GetComponent<Coin>().value;
                    Destroy(hit.transform.gameObject);
                    GetComponent<UIManager>().UpdateCoins(coins);
                }
            }
            //temp.SendMessage("splash", null, SendMessageOptions.DontRequireReceiver);

            /*
            if (temp.tag == "Fish")
            {
                temp.GetComponent<FishManager>().splashIn = true;
            }
            */
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
        int randVal = Random.Range(0, 100);

        if (randVal < 70)
        {
            randVal = 0;
        }
        else if (randVal < 90)
        {
            randVal = 1;
        }
        else
        {
            randVal = 2;
        }

        switch (randVal)
        {
            case 0:
                value = 1;
                Instantiate(bronze, position, Quaternion.identity);
                break;
            case 1:
                value = 5;
                Instantiate(silver, position, Quaternion.identity);
                break;
            case 2:
                value = 10;
                Instantiate(gold, position, Quaternion.identity);
                break;
            default:
                value = 1;
                break;
        }
    }

    public void SpawnCoin(Vector3 position, Transform parentTransform)
    {
        int randVal = Random.Range(0, 100);

        if (randVal < 70)
        {
            randVal = 0;
        }
        else if (randVal < 90)
        {
            randVal = 1;
        }
        else
        {
            randVal = 2;
        }

        switch (randVal)
        {
            case 0:
                value = 1;
                Instantiate(bronze, position, Quaternion.identity);
                break;
            case 1:
                value = 5;
                Instantiate(silver, position, Quaternion.identity);
                break;
            case 2:
                value = 10;
                Instantiate(gold, position, Quaternion.identity);
                break;
            default:
                value = 1;
                break;
        }
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
                //Debug.Log(items[slotNumber].tag);
                Instantiate(items[slotNumber], new Vector2(0, 0), Quaternion.identity).SendMessage("splash", null, SendMessageOptions.DontRequireReceiver);
                coins -= prices[slotNumber];
            }
        }
        GetComponent<UIManager>().UpdateCoins(coins);
    }
}
