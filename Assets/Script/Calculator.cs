using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Calculator : MonoBehaviour
{   int primary,secondary,temp,final;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Z)){
            CalculatorFn("addition");  
        }
        if(Input.GetKeyDown(KeyCode.X)){
            CalculatorFn("minus");  
        }
        if(Input.GetKeyDown(KeyCode.C)){
            CalculatorFn("multiplication");  
        }
        if(Input.GetKeyDown(KeyCode.V)){
            CalculatorFn("division");  
        }
    }
    public void CalculatorFn(string operation){
        primary = Random.Range(1,10);
        secondary = Random.Range(1,10);

        if(primary - secondary < 0){
            temp = secondary;
            secondary = primary;
            primary = temp;
        }
        if(operation == "addition"){
            final = primary + secondary;
        }
        if(operation == "minus"){
            final = primary - secondary;
        }
        if(operation == "multiplication"){
            final = primary * secondary;
        }
        if(operation == "division"){
            final = primary / secondary;
        }
    
        Debug.Log("first: " + primary + " second: " + operation  + secondary + " final: " + final);

    }
}
