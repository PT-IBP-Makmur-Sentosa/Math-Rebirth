using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GlobalControl : MonoBehaviour
{
    public static GlobalControl Instance;
    string playerTrait = "Strong Body";
    int playerLevel = 1;
    [SerializeField] bool trigger = false;
    [SerializeField] bool save = false;

    public bool inCombat = false;

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

        if (save)
        {
            save = false;
            SaveGame();
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

    void printOut()
    {
        print(playerTrait);
        print(playerLevel);
    }

    public void SaveGame()
    {
        print("Saving game files...");
        playerLevel = GameObject.Find("Combat Overlay/Combat_UI/Player").GetComponent<Unit>().unitLevel;
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
        }
        else print("No saved files! New game...");

        return data;
    }
}
