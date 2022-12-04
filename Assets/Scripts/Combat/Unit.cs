using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Unit : MonoBehaviour
{
    public string unitName;
    public int unitLevel;
    public int prevLevel;

    string trait;
    Dictionary<string, float[]> dict = new Dictionary<string, float[]>();

    float Str;
    float Agi;
    float Int;

    public float Atk;
    public float Def;

    public float CRate;

    public float ExtraTime;
    public float ExtraMult;
    

    public float maxHP;
    public float currentHP;

    public int maxStamina;
    public int currentStamina;
    Animator playerAnimator;

    public bool TakeDamage(float dmg)
    {
        float damageTakenMod = (dmg / (dmg + Def));

        if (damageTakenMod < 0.3f)
        {
            damageTakenMod = 0.5f;
        }

        float damageTaken = dmg * damageTakenMod;
        
        currentHP -= damageTaken;

        Debug.Log(dmg);
        Debug.Log(unitName + " got hit and lose " + damageTaken + " HP. Current HP left: " + currentHP);
        if (currentHP <=0)
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
        prevLevel = unitLevel;

        float[] mult = new float[3] { 5.0f, 2.0f, 2.0f };
        dict.Add("Strong Body", mult);

        mult = new float[3] { 2.0f, 5.0f, 2.0f };
        dict.Add("Agile Body", mult);

        mult = new float[3] { 2.0f, 2.0f, 5.0f };
        dict.Add("Enhanced Mind", mult);

        mult = new float[3] { 3.0f, 3.0f, 3.0f };
        dict.Add("Average Joe", mult);

        LevelUp();
    }

    // Update is called once per frame
    void Update()
    {
        trait = GameObject.Find("GlobalObject").GetComponent<GlobalControl>().TraitGet();

        Str = dict[trait][0] * unitLevel;
        Agi = dict[trait][1] * unitLevel;
        Int = dict[trait][2] * unitLevel;

        if (gameObject.CompareTag("Player"))
        {
            maxHP = Str * 20.0f;
            Atk = Str * 3.0f;
            Def = Agi * 7.0f;
            CRate = Agi * 0.12f;
            ExtraMult = Int * 0.004f;
            ExtraTime = Int * 0.02f;
        }
        else if (gameObject.CompareTag("enemy"))
        {
            maxHP = unitLevel * 30.0f;
            Atk = unitLevel * 14.0f;
            Def = unitLevel * 7.0f;
            CRate = 0.0f;
            ExtraMult = 0.0f;
            ExtraTime = 0.0f;
        }

        if (prevLevel != unitLevel) LevelUp();
    }

    public void LevelUp()
    {
        print(gameObject.tag + " Level up! Now Level: " + unitLevel);

        Str = dict[trait][0] * unitLevel;
        Agi = dict[trait][1] * unitLevel;
        Int = dict[trait][2] * unitLevel;

        if (gameObject.CompareTag("Player"))
        {
            maxHP = Str * 20.0f;
            Atk = Str * 3.0f;
            Def = Agi * 7.0f;
            CRate = Agi * 0.12f;
            ExtraMult = Int * 0.004f;
            ExtraTime = Int * 0.02f;
        }
        else if (gameObject.CompareTag("enemy"))
        {
            maxHP = unitLevel * 30.0f;
            Atk = unitLevel * 14.0f;
            Def = unitLevel * 7.0f;
            CRate = 0.0f;
            ExtraMult = 0.0f;
            ExtraTime = 0.0f;
        }

        currentHP = maxHP;
        prevLevel = unitLevel;
    }
}
