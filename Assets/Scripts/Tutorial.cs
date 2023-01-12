using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tutorial : MonoBehaviour
{

    public GameObject tutorial_background;
    public GameObject move_tutorial;
    public GameObject inventory_tutorial;
    public GameObject combat_tutorial;
    public GameObject skill_tutorial;
    public GameObject charpage_tutorial;
    // Start is called before the first frame update
    void Start()
    {
        tutorial_background = GameObject.Find("TutorialBg");
        move_tutorial = GameObject.Find("MoveTutorial");
        inventory_tutorial = GameObject.Find("InventoryTutorial");
        combat_tutorial = GameObject.Find("inCombatTutorial");
        skill_tutorial = GameObject.Find("Skill page tutorial");
        charpage_tutorial = GameObject.Find("charpage");

        tutorial_background.SetActive(true);
        move_tutorial.SetActive(true);
        inventory_tutorial.SetActive(false);
        combat_tutorial.SetActive(false);
        skill_tutorial.SetActive(false);
        charpage_tutorial.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown("d") || Input.GetKeyDown("a") ||Input.GetKeyDown("w") )
        {
            tutorial_background.SetActive(false);
            move_tutorial.SetActive(false);
            inventory_tutorial.SetActive(false);
        }
    }
}
