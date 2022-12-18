using Inventory.Model;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GlobalControl : MonoBehaviour
{
    public static GlobalControl Instance;
    public bool inCombat = false;
    string playerTrait = "Strong Body";
    string curScene;
    public float playerCurrentHP = 0;
    public int playerCurrency = 0;
    int playerLevel = 1;
    float musicVol= 0.5f;
    float soundVol;

    [SerializeField] bool trigger = false;
    [SerializeField] bool save = false;
    [SerializeField] InventorySO InventSO = null;

    static Dictionary<string, ItemSO> allItemCodes = new Dictionary<string, ItemSO>();

    string[] playerInventory;
    Dictionary<int, InventoryItem> InventoryDict;
    public int[] stageList = new int[15] { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 };

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

        ItemSO[] allItems = Resources.LoadAll<ItemSO>("");

        foreach (ItemSO i in allItems)
        {
            allItemCodes[i.name] = i;
        }
    }

    private void Update()
    {
        if (trigger)
        {
            trigger = false;
            Invoke("printOut", 0.0f);
        }

        if (save)
        {
            save = false;
            SaveGame();
        }
    }

    public void SetVolume(float vol)
    {
        musicVol = vol;
        foreach (AudioSource audios in GameObject.FindObjectsOfType<AudioSource>())
        {
            if(audios.tag == "BGM") audios.volume = musicVol;
        }
    }

    public void SetSFX(float vol)
    {
        soundVol = vol;
        foreach (AudioSource audios in GameObject.FindObjectsOfType<AudioSource>())
        {
            if (audios.tag != "BGM") audios.volume = soundVol;
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

    public int CurrencyGet()
    {
        return playerCurrency;
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

    public void StageFinish(int idx)
    {
        stageList[idx] = 1;
    }
}
