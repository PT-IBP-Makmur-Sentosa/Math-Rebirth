using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwitchStage : MonoBehaviour
{
    // Start is called before the first frame update
    private bool istrigger = false;
    public GameObject map;

    Vector3 pos;
    
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(istrigger)
        {
            map.SetActive(true);
        }
    }
    private void OnTriggerEnter2D(Collider2D coll)
    {
        
        if(coll.CompareTag("Player"))
        {
            istrigger = true;
            
        }   
    }
    private void OnTriggerExit2D(Collider2D coll)
    {
        if(coll.CompareTag("Player"))
        {
            istrigger = false;
        } 
    }
    
}
