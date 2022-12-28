using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class StatsManager : MonoBehaviour
{
    [SerializeField] Sprite[] characterSprites;
    [SerializeField] Unit playerUnit;
    [SerializeField] Image playerSprites;
    [SerializeField] TextMeshProUGUI Trait;
    [SerializeField] TextMeshProUGUI Level;

    [SerializeField] TextMeshProUGUI Str;
    [SerializeField] TextMeshProUGUI Agi;
    [SerializeField] TextMeshProUGUI Int;

    [SerializeField] TextMeshProUGUI Atk;
    [SerializeField] TextMeshProUGUI Def;
    [SerializeField] TextMeshProUGUI MaxHP;
    [SerializeField] TextMeshProUGUI Crit;
    [SerializeField] TextMeshProUGUI MathTime;
    [SerializeField] TextMeshProUGUI MathMult;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Trait.text = playerUnit.trait;
        switch (playerUnit.trait)
        {
            case "Strong Body":
                playerSprites.sprite = characterSprites[0]; ;
                break;
            case "Agile Body":
                playerSprites.sprite = characterSprites[1]; ;
                break;
            case "Enhanced Mind":
                playerSprites.sprite = characterSprites[2]; ;
                break;
            case "Average Joe":
                playerSprites.sprite = characterSprites[3]; ;
                break;
            default:
                playerSprites.sprite = characterSprites[0]; ;
                break;

        }
        
        Level.text = playerUnit.unitLevel.ToString();

        Str.text = playerUnit.Str.ToString();
        Agi.text = playerUnit.Agi.ToString();
        Int.text = playerUnit.Int.ToString();

        Atk.text = playerUnit.Atk.ToString();
        Def.text = playerUnit.Def.ToString();
        MaxHP.text = playerUnit.maxHP.ToString();
        Crit.text = playerUnit.CRate.ToString("0.00") + "%" + "\n" + (playerUnit.CDmg * 100.0f).ToString("0.0") + "%";
        MathTime.text = playerUnit.ExtraMult.ToString("0.000");
        MathMult.text = playerUnit.ExtraTime.ToString("0.00");

    }

    public void closePage()
    {
        gameObject.SetActive(false);
        GameObject.Find("GlobalObject").GetComponent<GlobalControl>().inCharPage = false;
    }
}
