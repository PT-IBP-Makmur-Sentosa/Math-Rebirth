using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GlobalControl : MonoBehaviour
{
    public static GlobalControl Instance;
    public bool inCombat = false;
    string playerTrait = "Strong Body";
    string curScene;
    public float playerCurrentHP = 0;
    public int playerCurrency = 0;
    int playerLevel = 1;

    [SerializeField] bool trigger = false;

    void Awake()
    {
        if (Instance == null)
        {
            DontDestroyOnLoad(gameObject);
            Instance = this;
        }
        else if (Instance != this)
        {
            Destroy(gameObject);
        }
    }

    private void Update()
    {
        if (trigger)
        {
            trigger = false;
            Invoke("printOut", 0.0f);
        }

    }

    public void TraitSet(string trait)
    {
        playerTrait = trait;
    }

    public string TraitGet()
    {
        return playerTrait;
    }

    public void LevelSet(int lvl)
    {
        playerLevel = lvl;
    }

    public int LevelGet()
    {
        return playerLevel;
    }

    public string SceneGet()
    {
        return curScene;
    }

    public string[] InventoryGet()
    {
        return playerInventory;
    }

    public Dictionary<int, InventoryItem> InventoryDictGet()
    {
        return InventoryDict;
    }

    void printOut()
    {
        print(playerTrait);
        print(playerLevel);
        print(curScene);
        print(playerCurrency);
    }

    public void SaveGame()
    {
        print("Saving game files...");
        Scene scene = SceneManager.GetActiveScene();
        curScene = scene.name;


        InventoryDict = InventSO.GetCurrentInventoryState();
        playerInventory = new string[InventoryDict.Count];
        int i = 0;
        foreach (KeyValuePair<int, InventoryItem> kvp in InventoryDict)
        {
            ItemSO item = kvp.Value.item;
            int count = kvp.Value.quantity;

            playerInventory[i] = item + "_" + count;
            i++;
        }

        playerLevel = GameObject.Find("Combat Overlay/Combat_UI/Player").GetComponent<Unit>().unitLevel;
        playerCurrentHP = GameObject.Find("Combat Overlay/Combat_UI/Player").GetComponent<Unit>().currentHP;
        SaveSystem.SaveGame(this);
    }

    public PlayerData LoadGame()
    {
        PlayerData data = SaveSystem.LoadGame();
        if (data != null)
        {
            print("Loading saved files...");
            playerTrait = data.trait;
            playerLevel = data.level;
            curScene = data.scene;
            playerCurrentHP = data.curHP;
            playerInventory = data.inventory;
            playerCurrency = data.currency;
            InventoryDict = new Dictionary<int, InventoryItem>();

            int i = 0;
            foreach (string items in playerInventory)
            {
                string key = items.Split("_")[0];
                key = key.Split(" ")[0];
                ItemSO item = allItemCodes[key];
                int count = int.Parse(items.Split("_")[1]);
                InventoryItem inventoryItem = new InventoryItem
                {
                    item = item,
                    quantity = count
                };

                InventoryDict[i] = inventoryItem;
                i++;
            }
        }
        else print("No saved files! New game...");

        return data;
    }
}
