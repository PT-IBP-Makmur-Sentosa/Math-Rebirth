using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class PlayerData
{
    public int level;
    public string trait;

    public PlayerData(GlobalControl player)
    {
        level = player.LevelGet();
        trait = player.TraitGet();
    }
}
