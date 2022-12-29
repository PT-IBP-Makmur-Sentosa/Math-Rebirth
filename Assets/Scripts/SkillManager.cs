using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkillManager : MonoBehaviour
{
    public GameObject EquipSkillPrompt;

    public void openWindow()
    {
        EquipSkillPrompt.SetActive(true);
    }
}
