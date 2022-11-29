using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class BattleHUD : MonoBehaviour
{   
    public CombatManager combatManagerScript;
    public Calculator calculatorScript;
      
    public GameObject player;
    Unit playerUnit;

    public Image health;
    public TextMeshProUGUI health_text;
    public TextMeshProUGUI stamina_text;
    public Material graphmat_green;
    public Material graphmat_black;

    private float curr_health;
    private float max_health;
    private float curr_stamina;
    private float max_stamina;
    private float removed;
    void Start()
    {
        playerUnit = player.GetComponent<Unit>();
        curr_health = playerUnit.currentHP;
        max_health = playerUnit.maxHP;
        curr_stamina = playerUnit.currentStamina;
        max_stamina = playerUnit.maxStamina;

        health.fillAmount = curr_health/max_health;
        health_text.text = (curr_health/max_health*100).ToString();
        stamina_text.text = curr_stamina.ToString();

        removed = max_stamina - curr_stamina;
        graphmat_green.SetFloat("_segmentCount", max_stamina);
        graphmat_green.SetFloat("_RemovedSegment", removed);
        graphmat_black.SetFloat("_segmentCount", max_stamina);
    }
    public void SetHUD(Unit unit)
    {   curr_health = playerUnit.currentHP;
        max_health = playerUnit.maxHP;
        health.fillAmount = curr_health/max_health;
        health_text.text = (curr_health/max_health*100).ToString();

        curr_stamina = playerUnit.currentStamina;
        max_stamina = playerUnit.maxStamina;
        removed = max_stamina - curr_stamina;
        graphmat_green.SetFloat("_segmentCount", max_stamina);
        graphmat_green.SetFloat("_RemovedSegment", removed);
        graphmat_black.SetFloat("_segmentCount", max_stamina);
        stamina_text.text = curr_stamina.ToString();  
    }
    public void onPressAttack()
    {   
        curr_stamina -=1;
        playerUnit.currentStamina -= 1;
        removed = max_stamina - curr_stamina;
        graphmat_green.SetFloat("_RemovedSegment", removed);
        stamina_text.text = curr_stamina.ToString();
    }
    public void onPressDefend()
    {   
        curr_stamina -=1;
        playerUnit.currentStamina -= 1;
        removed = max_stamina - curr_stamina;
        graphmat_green.SetFloat("_RemovedSegment", removed);
        stamina_text.text = curr_stamina.ToString();     
    }
    public void onPressSpecial_1()
    {
        
    }
    public void onPressSpecial_2()
    {
        
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}