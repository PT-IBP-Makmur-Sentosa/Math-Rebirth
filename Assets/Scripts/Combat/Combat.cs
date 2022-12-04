using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class Combat : MonoBehaviour
{
    // Start is called before the first frame update
    public Material graphmat;
    public float curr_stamina;
    public float max_stamina;
    public TextMeshProUGUI txt;
    public GameObject tutorial_background;
    public GameObject Combat_tutorial;
    void Start()
    {
        graphmat.SetFloat("_segmentCount", max_stamina);
        graphmat.SetFloat("_RemovedSegment", curr_stamina);
        tutorial_background = GameObject.Find("TutorialBg");
        Combat_tutorial = GameObject.Find("inCombatTutorial");
        tutorial_background.SetActive(true);
        Combat_tutorial.SetActive(true);
    }

    // Update is called once per frame

}
