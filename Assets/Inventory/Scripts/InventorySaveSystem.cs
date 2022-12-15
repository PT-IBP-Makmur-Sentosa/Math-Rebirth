using System.IO; //For reading and writing
using System.Collections.Generic;
using UnityEngine;
using Inventory.Model;
using System;


public class InventorySaveSystem : MonoBehaviour
{
    [SerializeField] private InventorySO inventoryToSave = null;

    private static Dictionary<int, ItemSO> allItemCodes = new Dictionary<int, ItemSO>();

    private static int HashItem(ItemSO item) => Animator.StringToHash(item.name);

    const char SPLIT_CHAR = '_';

    private static string FILE_PATH = "NULL!";

    private void Awake()
    {
        FILE_PATH = Application.persistentDataPath + "/Inventory.txt";
        print(FILE_PATH);
        CreateItemDictionary();
    }

    private void OnDisable()
    {
        SaveInventory();
    }

    private void CreateItemDictionary()
    {
        ItemSO[] allItems = Resources.FindObjectsOfTypeAll<ItemSO>();

        foreach (ItemSO i in allItems)
        {
            int key = HashItem(i);

            if (!allItemCodes.ContainsKey(key))
                allItemCodes.Add(key, i);
        }
    }

    public void SaveInventory()
    {
        using (StreamWriter sw = new StreamWriter(FILE_PATH))
        {
            foreach (KeyValuePair<int, InventoryItem> kvp in inventoryToSave.GetCurrentInventoryState())
            {
                ItemSO item = kvp.Value.item;
                int count = kvp.Value.quantity;

                string itemID = HashItem(item).ToString();

                sw.WriteLine(itemID + SPLIT_CHAR + count);
            }
        }
    }

    public bool InventorySaveExists()
    {
        if (!File.Exists(FILE_PATH))
        {
            Debug.LogWarning("The file you're trying to access doesn't exist. (Try saving an inventory first).");
            return false;
        }
        return true;
    }

    //Delete all items in the inventory. Will be irreversable. Could just create a new file (ie. Change the name of the old save file and create a new one)
    public void ClearInventorySaveFile()
    {
        if (!InventorySaveExists())
            return;

        File.WriteAllText(FILE_PATH, String.Empty);
    }

    public List<InventoryItem> LoadInventorySave()
    {
        List<InventoryItem> inventory = new List<InventoryItem>();
        if (!InventorySaveExists())
            return inventory;

        string line = "";

        using (StreamReader sr = new StreamReader(FILE_PATH))
        {
            while ((line = sr.ReadLine()) != null)
            {
                int key = int.Parse(line.Split(SPLIT_CHAR)[0]);
                ItemSO item = allItemCodes[key];
                int count = int.Parse(line.Split(SPLIT_CHAR)[1]);
                InventoryItem inventoryItem = new InventoryItem
                {
                    item = item,
                    quantity = count
                };

                inventory.Add(inventoryItem);
            }
        }

        return inventory;
    }
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.S))
        {
            print("Saving inventory");
            SaveInventory();
        }
    }
}