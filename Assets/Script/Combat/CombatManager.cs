using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CombatManager : MonoBehaviour
{

    public GameObject combatUI;
    public GameObject questions;

    float movementSpeed = 400f;
    private bool goDown = false;
    private Calculator calculatorScript;
    // Start is called before the first frame update
    void Start()
    {

        calculatorScript = gameObject.GetComponent<Calculator>();
        calculatorScript.enabled = false;
    }

    // Update is called once per frame
    void Update()
    {

        Vector3 aPos = combatUI.transform.position;
        if (aPos.y > -300 && goDown)
        {
            aPos.x = aPos.z = 0;
            aPos.y = -movementSpeed * Time.deltaTime;
            print(Time.deltaTime);
            print(aPos);
            combatUI.transform.position += aPos;
        }
    }

    public void onPressed(string action)
    {
        print("pressed");
        goDown = true;
        calculatorScript.enabled = true;
        questions.SetActive(true);
    }
}
