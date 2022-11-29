using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Unit : MonoBehaviour
{
    public string unitName;
    public int unitLevel;
    public float damage;
    public float maxHP;
    public float currentHP;
    public int maxStamina;
    public int currentStamina;
    private Animator playerAnimator;

    public bool TakeDamage(float dmg)
    {
        currentHP -= dmg;
        if(currentHP <=0)
        {
            return true;
            //enemy died
        }
        else{
            return false;
            //enemy still alive
        }
    }

    public void Reset(int mode){
        if(mode == 1){
            currentStamina = maxStamina;
        }
        else if (mode == 0){
            currentHP = maxHP;
            currentStamina = maxStamina;
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}