using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GlobalControl : MonoBehaviour
{
    public static GlobalControl Instance;
    string playerTrait;
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

    void printOut()
    {
        print(playerTrait);
    }
}