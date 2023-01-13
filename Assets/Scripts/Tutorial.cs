using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tutorial : MonoBehaviour
{

    public GameObject tutorial_background;
    public GameObject move_tutorial;
    public GameObject inventory_tutorial;
    public GameObject skill_tutorial;
    public GameObject map_tutorial;
    int g = 1;
    // Start is called before the first frame update
    void Start()
    {
        tutorial_background = GameObject.Find("TutorialBg");
        move_tutorial = GameObject.Find("MoveTutorial");
        inventory_tutorial = GameObject.Find("InventoryTutorial");
        skill_tutorial = GameObject.Find("skillTutorial");
        map_tutorial = GameObject.Find("MapTutorial");

        tutorial_background.SetActive(true);
        move_tutorial.SetActive(true);
        inventory_tutorial.SetActive(false);
        skill_tutorial.SetActive(false);
        map_tutorial.SetActive(false);
        
    }

    // Update is called once per frame
    void Update()
    {
        if ((Input.GetKeyDown("d") || Input.GetKeyDown("a") ||Input.GetKeyDown("w") )&& g ==1 )
        {
            tutorial_background.SetActive(true);
            move_tutorial.SetActive(false);
            inventory_tutorial.SetActive(false);
            skill_tutorial.SetActive(true);
            map_tutorial.SetActive(false);
            g = 0;
        }
        if (Input.GetKeyDown("k") )
        {
            tutorial_background.SetActive(false);
            move_tutorial.SetActive(false);
            inventory_tutorial.SetActive(true);
            skill_tutorial.SetActive(false);
            map_tutorial.SetActive(false);
        }
        if (Input.GetKeyDown("i") )
        {
            // tutorial_background.SetActive(false);
            move_tutorial.SetActive(false);
            inventory_tutorial.SetActive(false);
            skill_tutorial.SetActive(false);
            map_tutorial.SetActive(true);
            StartCoroutine(map());
        }
        // if (Input.GetKeyDown("i") )
        // {
        //     tutorial_background.SetActive(false);
        //     move_tutorial.SetActive(false);
        //     inventory_tutorial.SetActive(false);
        //     skill_tutorial.SetActive(false);
        //     map_tutorial.SetActive(false);

        // }
    }

    IEnumerator map(){
        yield return new WaitForSeconds(3.0f);
        tutorial_background.SetActive(false);
        move_tutorial.SetActive(false);
        inventory_tutorial.SetActive(false);
        skill_tutorial.SetActive(false);
        map_tutorial.SetActive(false);
    }
        
    
}
