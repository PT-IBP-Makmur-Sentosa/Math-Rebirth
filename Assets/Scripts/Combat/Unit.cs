using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Unit : MonoBehaviour
{
    public string unitName;
    public int unitLevel = 1;
    public int prevLevel = 0;
    public BattleHUD playerHUD;

    public string trait;
    Dictionary<string, float[]> dict = new Dictionary<string, float[]>();

    public float Str;
    public float Agi;
    public float Int;

    public float Atk;
    public float Def;

    public float CRate;
    public float CDmg;

    public float ExtraTime;
    public float ExtraMult;


    public float maxHP;
    public float currentHP;

    public int maxStamina;
    public int currentStamina;
    Animator playerAnimator;

    public bool TakeDamage(float dmg)
    {
        float damageTakenMod = ((dmg) / (dmg + Def));

        if (damageTakenMod < 0.2f)
        {
            damageTakenMod = 0.2f;
        }

        float damageTaken = dmg * damageTakenMod;

        currentHP -= damageTaken;

        Debug.Log(dmg);
        playerHUD.battle_text.text = unitName + " got hit and lose " + damageTaken.ToString("0.0") + " HP. Current HP left: " + currentHP.ToString("0.0");
        Debug.Log(unitName + " got hit and lose " + damageTaken + " HP. Current HP left: " + currentHP);
        if (currentHP <= 0)
        {
            return true;
            //enemy died
        }
        else
        {
            return false;
            //enemy still alive
        }
    }

    public void Reset(int mode)
    {
        if (mode == 1)
        {
            currentStamina = maxStamina;
        }
        else if (mode == 0)
        {
            currentHP = maxHP;
            currentStamina = maxStamina;
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        trait = GameObject.Find("GlobalObject").GetComponent<GlobalControl>().TraitGet();
        if (gameObject.tag == "Player") unitLevel = GameObject.Find("GlobalObject").GetComponent<GlobalControl>().LevelGet();
        prevLevel = unitLevel;

        float[] mult;
        //                    Str,  Agi,  Int,  HP,    Atk,    Def,   CRr,    CDmg,   Mult
        mult = new float[9] { 5.0f, 2.0f, 2.0f, 40.0f, 100.0f, 10.0f, 0.125f, 400.0f, 200.0f };
        dict.Add("Strong Body", mult);

        mult = new float[9] { 2.0f, 5.0f, 2.0f, 30.0f, 80.0f, 25.0f, 0.25f, 500.0f, 200.0f };
        dict.Add("Agile Body", mult);

        mult = new float[9] { 2.0f, 2.0f, 5.0f, 30.0f, 80.0f, 10.0f, 0.125f, 400.0f, 100.0f };
        dict.Add("Enhanced Mind", mult);

        mult = new float[9] { 3.0f, 3.0f, 3.0f, 40.0f, 120.0f, 15.0f, 0.1875f, 500.0f, 200.0f };
        dict.Add("Average Joe", mult);

        LevelUp();
        PlayerData data = SaveSystem.LoadGame();
        if (gameObject.tag == "Player" && data != null) currentHP = GameObject.Find("GlobalObject").GetComponent<GlobalControl>().playerCurrentHP;
    }

    // Update is called once per frame
    void Update()
    {
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
            maxHP = Str * (8.0f + unitLevel / dict[trait][3]);
            if (trait == "Strong Body") maxHP = Str * (5.0f + unitLevel / dict[trait][3]);
            Atk = Str * (1.25f + unitLevel / dict[trait][4]);
            if (trait == "Strong Body") Atk = Str * (0.75f + unitLevel / dict[trait][4]);
            Def = Agi * (2.5f + unitLevel / dict[trait][5]);
            CRate = 5.0f + Agi * (dict[trait][6]);
            CDmg = 1.5f + Agi * (0.0025f * (1 + unitLevel / dict[trait][7]));
            ExtraMult = Int * (0.004f * (1 + unitLevel / dict[trait][8]));
            ExtraTime = Int * 0.02f;
        }
        // Area 1
        else if (gameObject.CompareTag("Skeleton"))
        {
            maxHP = unitLevel * (10.0f + unitLevel / 3f);
            Atk = unitLevel * (4.5f + unitLevel / 9f);
            Def = unitLevel * (2.0f + unitLevel / 4f);
            CRate = 5.0f;
            CDmg = 1.5f;
            ExtraMult = 0.0f;
            ExtraTime = 0.0f;
        }
        else if (gameObject.CompareTag("Shade"))
        {
            maxHP = unitLevel * (10.0f + unitLevel / 3f);
            Atk = unitLevel * (4.5f + unitLevel / 9f);
            Def = unitLevel * (2.0f + unitLevel / 4f);
            CRate = 5.0f;
            CDmg = 1.5f;
            ExtraMult = 0.0f;
            ExtraTime = 0.0f;
        }
        else if (gameObject.CompareTag("Bat"))
        {
            maxHP = unitLevel * (10.0f + unitLevel / 3f);
            Atk = unitLevel * (4.5f + unitLevel / 9f);
            Def = unitLevel * (2.0f + unitLevel / 4f);
            CRate = 5.0f;
            CDmg = 1.5f;
            ExtraMult = 0.0f;
            ExtraTime = 0.0f;
        }
        else if (gameObject.CompareTag("TrashCave"))
        {
            maxHP = unitLevel * (10.0f + unitLevel / 3f);
            Atk = unitLevel * (4.5f + unitLevel / 9f);
            Def = unitLevel * (2.0f + unitLevel / 4f);
            CRate = 5.0f;
            CDmg = 1.5f;
            ExtraMult = 0.0f;
            ExtraTime = 0.0f;
        }
        else if (gameObject.CompareTag("Zombie"))
        {
            maxHP = unitLevel * (10.0f + unitLevel / 3f);
            Atk = unitLevel * (4.5f + unitLevel / 9f);
            Def = unitLevel * (2.0f + unitLevel / 4f);
            CRate = 5.0f;
            CDmg = 1.5f;
            ExtraMult = 0.0f;
            ExtraTime = 0.0f;
        }
        else if (gameObject.CompareTag("Boss1"))
        {
            maxHP = unitLevel * (10.0f + unitLevel / 3f);
            Atk = unitLevel * (4.5f + unitLevel / 9f);
            Def = unitLevel * (2.0f + unitLevel / 4f);
            CRate = 5.0f;
            CDmg = 1.5f;
            ExtraMult = 0.0f;
            ExtraTime = 0.0f;
        }
        // Area 2
        else if (gameObject.CompareTag("Goblin"))
        {
            maxHP = unitLevel * (10.0f + unitLevel / 3f);
            Atk = unitLevel * (4.5f + unitLevel / 9f);
            Def = unitLevel * (2.0f + unitLevel / 4f);
            CRate = 5.0f;
            CDmg = 1.5f;
            ExtraMult = 0.0f;
            ExtraTime = 0.0f;
        }
        else if (gameObject.CompareTag("Mushroom"))
        {
            maxHP = unitLevel * (10.0f + unitLevel / 3f);
            Atk = unitLevel * (4.5f + unitLevel / 9f);
            Def = unitLevel * (2.0f + unitLevel / 4f);
            CRate = 5.0f;
            CDmg = 1.5f;
            ExtraMult = 0.0f;
            ExtraTime = 0.0f;
        }
        else if (gameObject.CompareTag("SlimeForest"))
        {
            maxHP = unitLevel * (10.0f + unitLevel / 3f);
            Atk = unitLevel * (4.5f + unitLevel / 9f);
            Def = unitLevel * (2.0f + unitLevel / 4f);
            CRate = 5.0f;
            CDmg = 1.5f;
            ExtraMult = 0.0f;
            ExtraTime = 0.0f;
        }
        else if (gameObject.CompareTag("Tooth"))
        {
            maxHP = unitLevel * (10.0f + unitLevel / 3f);
            Atk = unitLevel * (4.5f + unitLevel / 9f);
            Def = unitLevel * (2.0f + unitLevel / 4f);
            CRate = 5.0f;
            CDmg = 1.5f;
            ExtraMult = 0.0f;
            ExtraTime = 0.0f;
        }
        else if (gameObject.CompareTag("TrashForest"))
        {
            maxHP = unitLevel * (10.0f + unitLevel / 3f);
            Atk = unitLevel * (4.5f + unitLevel / 9f);
            Def = unitLevel * (2.0f + unitLevel / 4f);
            CRate = 5.0f;
            CDmg = 1.5f;
            ExtraMult = 0.0f;
            ExtraTime = 0.0f;
        }
        else if (gameObject.CompareTag("Boss2"))
        {
            maxHP = unitLevel * (10.0f + unitLevel / 3f);
            Atk = unitLevel * (4.5f + unitLevel / 9f);
            Def = unitLevel * (2.0f + unitLevel / 4f);
            CRate = 5.0f;
            CDmg = 1.5f;
            ExtraMult = 0.0f;
            ExtraTime = 0.0f;
        }
        // Area 3
        else if (gameObject.CompareTag("Eyeball"))
        {
            maxHP = unitLevel * (10.0f + unitLevel / 3f);
            Atk = unitLevel * (4.5f + unitLevel / 9f);
            Def = unitLevel * (2.0f + unitLevel / 4f);
            CRate = 5.0f;
            CDmg = 1.5f;
            ExtraMult = 0.0f;
            ExtraTime = 0.0f;
        }
        else if (gameObject.CompareTag("Fireworm"))
        {
            maxHP = unitLevel * (10.0f + unitLevel / 3f);
            Atk = unitLevel * (4.5f + unitLevel / 9f);
            Def = unitLevel * (2.0f + unitLevel / 4f);
            CRate = 5.0f;
            CDmg = 1.5f;
            ExtraMult = 0.0f;
            ExtraTime = 0.0f;
        }
        else if (gameObject.CompareTag("FlyEye"))
        {
            maxHP = unitLevel * (10.0f + unitLevel / 3f);
            Atk = unitLevel * (4.5f + unitLevel / 9f);
            Def = unitLevel * (2.0f + unitLevel / 4f);
            CRate = 5.0f;
            CDmg = 1.5f;
            ExtraMult = 0.0f;
            ExtraTime = 0.0f;
        }
        else if (gameObject.CompareTag("Demon"))
        {
            maxHP = unitLevel * (10.0f + unitLevel / 3f);
            Atk = unitLevel * (4.5f + unitLevel / 9f);
            Def = unitLevel * (2.0f + unitLevel / 4f);
            CRate = 5.0f;
            CDmg = 1.5f;
            ExtraMult = 0.0f;
            ExtraTime = 0.0f;
        }
        else if (gameObject.CompareTag("Imp"))
        {
            maxHP = unitLevel * (10.0f + unitLevel / 3f);
            Atk = unitLevel * (4.5f + unitLevel / 9f);
            Def = unitLevel * (2.0f + unitLevel / 4f);
            CRate = 5.0f;
            CDmg = 1.5f;
            ExtraMult = 0.0f;
            ExtraTime = 0.0f;
        }
        else if (gameObject.CompareTag("Boss3"))
        {
            maxHP = unitLevel * (10.0f + unitLevel / 3f);
            Atk = unitLevel * (4.5f + unitLevel / 9f);
            Def = unitLevel * (2.0f + unitLevel / 4f);
            CRate = 5.0f;
            CDmg = 1.5f;
            ExtraMult = 0.0f;
            ExtraTime = 0.0f;
        }

        currentHP = maxHP;
        prevLevel = unitLevel;
    }
}
