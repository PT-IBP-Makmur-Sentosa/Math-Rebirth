using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class StageOverlayManager : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI currency;
    [SerializeField] TextMeshProUGUI HP;
    [SerializeField] TextMeshProUGUI Stages;
    [SerializeField] Unit playerUnit;
    [SerializeField] GameObject child;

    Dictionary<string, string> stageDict = new Dictionary<string, string>();

    GameObject glob;
    GlobalControl globc;
    
    // Start is called before the first frame update
    void Start()
    {
        stageDict.Add("StagePrototype", "Stage 0-0");
        stageDict.Add("Tutorial", "Tutorial");

        stageDict.Add("FirstStage", "Stage 1-1");
        stageDict.Add("SecondStage", "Stage 1-2");
        stageDict.Add("ThirdStage", "Stage 1-3");
        stageDict.Add("FourthStage", "Stage 1-4");
        stageDict.Add("FifthStage", "Stage 1-5");

        stageDict.Add("SixthStage", "Stage 2-1");
        stageDict.Add("SeventhStage", "Stage 2-2");
        stageDict.Add("EigthStage", "Stage 2-3");
        stageDict.Add("NinthStage", "Stage 2-4");
        stageDict.Add("TenthStage", "Stage 2-5");

        stageDict.Add("EleventhStage", "Stage 3-1");
        stageDict.Add("TwelfthStage", "Stage 3-2");
        stageDict.Add("ThirteenthStage", "Stage 3-3");
        stageDict.Add("FourteenthStage", "Stage 3-4");
        stageDict.Add("FifteenthStage", "Stage 3-5");

        glob = GameObject.Find("GlobalObject");
        globc = glob.GetComponent<GlobalControl>();
    }

    // Update is called once per frame
    void Update()
    {
        currency.text = globc.CurrencyGet().ToString();
        HP.text = (playerUnit.currentHP / playerUnit.maxHP * 100).ToString("0.00") + "%";
        Stages.text = stageDict[SceneManager.GetActiveScene().name];

        if (globc.inCombat)
        {
            child.SetActive(false);
        }
        else child.SetActive(true);
    }
    
    public void openMenu()
    {

    }
}
