using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class Calculator : MonoBehaviour
{
    int primary, secondary, temp, final;
    public GameObject Question, Result;
    public TextMeshProUGUI PrimaryDigit, SecondaryDigit, SignDigit;
    public GameObject TMP_InputField_Answer;
    public GameObject Correct, Wrong, TimesUp;
    [SerializeField] TextMeshProUGUI countdownText;
    float currentTime = 0f, startTime = 10f;
    bool onTime = true;
    // Start is called before the first frame update
    void Start()
    {
        CalculatorFn("addition");
        currentTime = startTime;

    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Z))
        {
            CalculatorFn("addition");
        }
        if (Input.GetKeyDown(KeyCode.X))
        {
            CalculatorFn("minus");
        }
        if (Input.GetKeyDown(KeyCode.C))
        {
            CalculatorFn("multiplication");
        }
        if (Input.GetKeyDown(KeyCode.V))
        {
            CalculatorFn("division");
        }
        if (Input.GetKeyDown(KeyCode.B))
        {
            CalculatorFn("modulus");
        }
        if (Input.GetKeyDown(KeyCode.Return))
        {
            string answer = TMP_InputField_Answer.GetComponent<TMP_InputField>().text;
            Debug.Log("User Answer: " + answer);
            CheckAnswerFn(final, answer);
        }
        currentTime -= 1 * Time.deltaTime;
        print(currentTime);
        countdownText.text = currentTime.ToString("0.0");
        if (currentTime <= 0)
        {
            currentTime = 0;
            countdownText.color = Color.red;
            onTime = false;

            Question.SetActive(false);
            Result.SetActive(true);
            TimesUp.SetActive(true);
        }

    }
    public void CalculatorFn(string operation)
    {
        TMP_InputField_Answer.GetComponent<TMP_InputField>().Select();
        TMP_InputField_Answer.GetComponent<TMP_InputField>().ActivateInputField();
        primary = UnityEngine.Random.Range(1, 10);
        secondary = UnityEngine.Random.Range(1, 10);

        if (primary - secondary < 0)
        {
            temp = secondary;
            secondary = primary;
            primary = temp;
        }
        if (operation == "addition")
        {
            final = primary + secondary;
            SignDigit.text = "+";
        }
        if (operation == "minus")
        {
            final = primary - secondary;
            SignDigit.text = "-";
        }
        if (operation == "multiplication")
        {
            final = primary * secondary;
            SignDigit.text = "*";
        }
        if (operation == "division")
        {
            final = primary / secondary;
            SignDigit.text = "/";
        }
        if (operation == "modulus")
        {
            final = primary % secondary;
            SignDigit.text = "%";
        }

        PrimaryDigit.text = primary.ToString();
        SecondaryDigit.text = secondary.ToString();
        Debug.Log("first: " + primary + " second: " + operation + secondary + " final: " + final);
    }
    public void CheckAnswerFn(int final, string answer)
    {
        int user_answer = Convert.ToInt32(answer);
        // int.TryParse(inputField.text, out user_answer);
        // string user_answer = inputField.text;
        // if (int.TryParse(inputField.text, out int result)){
        // // Do something with the result
        //     user_answer = result;
        // } else {
        //     Debug.Log ("Not a valid int");
        // }
        Debug.Log("user_answer: " + answer.GetType() + user_answer + answer + " " + "final: " + final);
        if (user_answer != final)
        {
            Question.SetActive(false);
            Result.SetActive(true);
            Wrong.SetActive(true);
            Debug.Log("wrong");
        }
        else if (user_answer == final)
        {
            Question.SetActive(false);
            Result.SetActive(true);
            Correct.SetActive(true);
            Debug.Log("Correct");
        }
    }

}
