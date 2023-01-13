using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class spikes_hurt : MonoBehaviour
{
    // Start is called before the first frame update
    public string unitName;
    public Unit playerUnit;
    private bool TrapHit;
    public AudioSource hit;
    void Start()
    {
        // if (gameObject.tag == "Player") currentHP = GameObject.Find("GlobalObject").GetComponent<GlobalControl>().playerCurrentHP;
    }
    void Update()
    {
        
    }
    // Update is called once per frame
    
    void OnTriggerEnter2D(Collider2D other) { 
        print("test"); 
        if(other.CompareTag("Player")) {
            print("Another object has entered the trigger");  
            if (playerUnit.currentHP >= playerUnit.maxHP * 0.10f ) {
                TrapHit = true;
                StartCoroutine(CastDamage(0.05f));
            }
            
        }
    } 
    void OnTriggerExit2D(Collider2D other) { 
        if(other.CompareTag("Player")) {
            TrapHit = false;
        }
    } 
    IEnumerator CastDamage(float damage){
        while(TrapHit){
            playerUnit.currentHP -= playerUnit.maxHP * 0.05f ;
            hit.Play();
            yield return new WaitForSeconds(1.0f);
        }
        
    }

}
