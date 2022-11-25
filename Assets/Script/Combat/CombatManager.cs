using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CombatManager : MonoBehaviour
{

    public GameObject combatUI;
    public GameObject questions;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    public void onPressed(string action)
    {
        print("pressed");
        combatUI.SetActive(false);
        questions.SetActive(true);
    }
}
