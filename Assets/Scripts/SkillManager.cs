using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SkillManager : MonoBehaviour
{
    public GameObject EquipSkillPrompt;
    public GameObject[] SkillButtons;
    public GameObject[] JoeSkillButtons;
    Dictionary<string, float[]> skillDict;

    GameObject glob;
    string skill1;
    string skill2;
    string Trait;

    public Sprite[] strImage;
    public Sprite[] agiImage;
    public Sprite[] intImage;
    public Sprite[] avgImage;

    string[] strSkill = { "Default_Skill1", "Str_Skill1", "Str_Skill2", "Str_Skill3", "Str_Skill4", "Agi_Skill1", "Int_Skill1" };
    string[] agiSkill = { "Default_Skill1", "Agi_Skill1", "Agi_Skill2", "Agi_Skill3", "Agi_Skill4", "Str_Skill1", "Int_Skill1" };
    string[] intSkill = { "Default_Skill1", "Int_Skill1", "Int_Skill2", "Int_Skill3", "Int_Skill4", "Agi_Skill1", "Str_Skill1" };
    string[] avgSkill = { "Default_Skill1", "Str_Skill1", "Str_Skill2", "Agi_Skill1", "Agi_Skill2", "Int_Skill1", "Int_Skill2" };

    string[] skillSet;

    int openedSkillIndex;
    void Start()
    {
        glob = GameObject.Find("GlobalObject");
        skillDict = glob.GetComponent<GlobalControl>().skillDict;
        // skill1 = glob.GetComponent<GlobalControl>().skill1;
        // skill2 = glob.GetComponent<GlobalControl>().skill2;
        // Trait = glob.GetComponent<GlobalControl>().playerTrait;
        skill1 = "Default_Skill1";
        skill2 = "Int_Skill1";
        Trait = "Enhanced Mind";

        switch (Trait)
        {
            case "Strong Body":
                skillSet = strSkill;
                break;
            case "Agile Body":
                skillSet = agiSkill;
                break;
            case "Enhanced Mind":
                skillSet = intSkill;
                break;
            case "Average Joe":
                skillSet = avgSkill;
                break;
            default:
                skillSet = strSkill;
                break;
        }
        if (Trait == "Average Joe")
        {
            print("trait is average joe");
            for (int i = 0; i < SkillButtons.Length; i++)
            {
                SkillButtons[i].SetActive(false);
                JoeSkillButtons[i].SetActive(true);
            }
        }
        else
        {
            for (int i = 0; i < SkillButtons.Length; i++)
            {
                SkillButtons[i].SetActive(true);
                JoeSkillButtons[i].SetActive(false);
            }
        }
        for (int i = 0; i < skillSet.Length; i++)
        {
            if (Trait == "Average Joe")
            {
                JoeSkillButtons[i].transform.GetChild(0).gameObject.GetComponent<Image>().sprite = avgImage[i];
            }
            else
            {
                switch (Trait)
                {
                    case "Strong Body":
                        SkillButtons[i].transform.GetChild(0).GetComponent<Image>().sprite = strImage[i];
                        break;
                    case "Agile Body":
                        SkillButtons[i].transform.GetChild(0).GetComponent<Image>().sprite = agiImage[i];
                        break;
                    case "Enhanced Mind":
                        SkillButtons[i].transform.GetChild(0).GetComponent<Image>().sprite = intImage[i];
                        break;
                    case "Average Joe":
                        SkillButtons[i].transform.GetChild(0).GetComponent<Image>().sprite = avgImage[i];
                        break;
                    default:
                        SkillButtons[i].transform.GetChild(0).GetComponent<Image>().sprite = strImage[i];
                        break;
                }
            }

        }
        for (int i = 0; i < skillSet.Length; i++)
        {
            if (skillSet[i] == skill1 || skillSet[i] == skill2)
            {
                if (Trait == "Average Joe")
                {
                    JoeSkillButtons[i].transform.GetChild(0).GetChild(0).gameObject.SetActive(true);

                }
                else
                {
                    SkillButtons[i].transform.GetChild(0).GetChild(0).gameObject.SetActive(true);
                }
            }
        }

    }
    public void openWindow(int index)
    {
        EquipSkillPrompt.SetActive(true);
        openedSkillIndex = index;
        if (skillSet[index] == skill1 || skillSet[index] == skill2)
        {
            EquipSkillPrompt.transform.GetChild(3).gameObject.SetActive(false);
            EquipSkillPrompt.transform.GetChild(4).gameObject.SetActive(false);
        }
        else
        {
            EquipSkillPrompt.transform.GetChild(3).gameObject.SetActive(true);
            EquipSkillPrompt.transform.GetChild(4).gameObject.SetActive(true);
        }
    }

    public void closeWindow()
    {
        EquipSkillPrompt.SetActive(false);
    }

    public void equipSlot1()
    {
        if (Trait == "Average Joe")
        {
            var oldIndex = Array.FindIndex(skillSet, row => row == skill1);
            JoeSkillButtons[oldIndex].transform.GetChild(0).GetChild(0).gameObject.SetActive(false);
            JoeSkillButtons[openedSkillIndex].transform.GetChild(0).GetChild(0).gameObject.SetActive(true);
            skill1 = skillSet[openedSkillIndex];
            glob.GetComponent<GlobalControl>().skill1 = skill1;
            EquipSkillPrompt.SetActive(false);
        }
        else
        {
            var oldIndex = Array.FindIndex(skillSet, row => row == skill1);
            SkillButtons[oldIndex].transform.GetChild(0).GetChild(0).gameObject.SetActive(false);
            SkillButtons[openedSkillIndex].transform.GetChild(0).GetChild(0).gameObject.SetActive(true);
            skill1 = skillSet[openedSkillIndex];
            glob.GetComponent<GlobalControl>().skill1 = skill1;
            EquipSkillPrompt.SetActive(false);
        }

    }

    public void equipSlot2()
    {
        if (Trait == "Average Joe")
        {
            var oldIndex = Array.FindIndex(skillSet, row => row == skill2);
            JoeSkillButtons[oldIndex].transform.GetChild(0).GetChild(0).gameObject.SetActive(false);
            JoeSkillButtons[openedSkillIndex].transform.GetChild(0).GetChild(0).gameObject.SetActive(true);
            skill2 = skillSet[openedSkillIndex];
            glob.GetComponent<GlobalControl>().skill2 = skill2;
            EquipSkillPrompt.SetActive(false);
        }
        else
        {
            var oldIndex = Array.FindIndex(skillSet, row => row == skill2);
            SkillButtons[oldIndex].transform.GetChild(0).GetChild(0).gameObject.SetActive(false);
            SkillButtons[openedSkillIndex].transform.GetChild(0).GetChild(0).gameObject.SetActive(true);
            skill2 = skillSet[openedSkillIndex];
            glob.GetComponent<GlobalControl>().skill2 = skill2;
            EquipSkillPrompt.SetActive(false);
        }


    }
}
