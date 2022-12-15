using Inventory.Model;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class PlayerData
{
    public int level;
    public int currency;
    public float curHP;
    public string trait;
    public string scene;
    public string[] inventory;

    public PlayerData(GlobalControl player)
    {
        level = player.LevelGet();
        trait = player.TraitGet();
        scene = player.SceneGet();
        curHP = player.playerCurrentHP;
        currency = player.playerCurrency;
        inventory = player.InventoryGet();
    }
}
