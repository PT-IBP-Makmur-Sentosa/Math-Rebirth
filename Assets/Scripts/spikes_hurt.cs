using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class spikes_hurt : MonoBehaviour
{
    // Start is called before the first frame update
    public string unitName;
    public Unit playerUnit;

    void Start()
    {
        // if (gameObject.tag == "Player") currentHP = GameObject.Find("GlobalObject").GetComponent<GlobalControl>().playerCurrentHP;
    }

    // Update is called once per frame
    
    void OnTriggerEnter2D(Collider2D other) { 
        print("test"); 
        if(other.CompareTag("Player")) {
            print("Another object has entered the trigger");  
            if (playerUnit.currentHP >= playerUnit.maxHP * 0.10f ) {
                playerUnit.currentHP -= playerUnit.maxHP * 0.05f ;
            }
            
        }
    } 
}
