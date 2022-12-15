using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Unit : MonoBehaviour
{
    public string unitName;
    public int unitLevel;

    string trait;
    Dictionary<string, float[]> dict = new Dictionary<string, float[]>();

    float Str;
    float Agi;
    float Int;

    public float damage;
    float Def;

    float CRate;

    float ExtraTime;
    float ExtraMult;
    

    public float maxHP;
    public float currentHP;

    public int maxStamina;
    public int currentStamina;
    Animator playerAnimator;

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
        trait = GameObject.Find("GlobalObject").GetComponent<GlobalControl>().TraitGet();

        float[] mult = new float[3] { 5.0f, 2.0f, 2.0f };
        dict.Add("Strong Body", mult);

        mult = new float[3] { 2.0f, 5.0f, 2.0f };
        dict.Add("Agile Body", mult);

        mult = new float[3] { 2.0f, 2.0f, 5.0f };
        dict.Add("Enhanced Mind", mult);

        mult = new float[3] { 3.0f, 3.0f, 3.0f };
        dict.Add("Average Joe", mult);
    }

    // Update is called once per frame
    void Update()
    {
        Str = dict[trait][0] * unitLevel;
        Agi = dict[trait][1] * unitLevel;
        Int = dict[trait][2] * unitLevel;

        maxHP = Str * 20.0f;
        damage = Str * 1.0f;
        Def = Agi * 7.0f;
        if (gameObject.CompareTag("Player"))
        {
            CRate = Agi * 0.12f;
        }
        else CRate = 0.0f;
        ExtraMult = Int * 0.02f;
        ExtraTime = Int * 0.4f;

    }
}
