using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class Calculator : MonoBehaviour
{
    int final;
    public GameObject Question, Result;
    public TextMeshProUGUI PrimaryDigit, SecondaryDigit, SignDigit;
    public GameObject TMP_InputField_Answer;
    public GameObject Correct, Wrong, TimesUp;
    [SerializeField] TextMeshProUGUI countdownText;
    float currentTime = 0f, startTime = 10f;
    bool onTime = true;
    float lotteryTime = 3f;
    bool runLottery = false;
    // Start is called before the first frame update
    void Start()
    {

    }

    void OnEnable()
    {
        lotteryTime = 2f;
        runLottery = true;
        // CalculatorFn("addition");
        currentTime = startTime;
        TMP_InputField_Answer.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        // if (Input.GetKeyDown(KeyCode.Z))
        // {
        //     CalculatorFn("addition");
        // }
        // if (Input.GetKeyDown(KeyCode.X))
        // {
        //     CalculatorFn("minus");
        // }
        // if (Input.GetKeyDown(KeyCode.C))
        // {
        //     CalculatorFn("multiplication");
        // }
        // if (Input.GetKeyDown(KeyCode.V))
        // {
        //     CalculatorFn("division");
        // }
        // if (Input.GetKeyDown(KeyCode.B))
        // {
        //     CalculatorFn("modulus");
        // }
        if (Input.GetKeyDown(KeyCode.Return))
        {
            string answer = TMP_InputField_Answer.GetComponent<TMP_InputField>().text;
            Debug.Log("User Answer: " + answer);
            CheckAnswerFn(final, answer);
        }

        // timer
        currentTime -= 1 * Time.deltaTime;
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

        // lottery randomizer counter
        if (runLottery && lotteryTime > 1)
        {
            lotteryTime -= 1 * Time.deltaTime;
            setDigit(PrimaryDigit);
            setDigit(SecondaryDigit);
            setSign(SignDigit);
        }
        else if (runLottery && lotteryTime > 0.75)
        {
            lotteryTime -= 1 * Time.deltaTime;
            setDigit(SecondaryDigit);
        }
        else if (runLottery && lotteryTime > 0.5)
        {
            lotteryTime -= 1 * Time.deltaTime;
            setDigit(SecondaryDigit);
        }
        else if (runLottery)
        {
            runLottery = false;
            TMP_InputField_Answer.SetActive(true);
            TMP_InputField_Answer.GetComponent<TMP_InputField>().Select();
            TMP_InputField_Answer.GetComponent<TMP_InputField>().ActivateInputField();
            CalculatorFn(SignDigit.text, int.Parse(PrimaryDigit.text), int.Parse(SecondaryDigit.text));
        }

    }
    public void CalculatorFn(string operation, int primary, int secondary)
    {
        print("primary" + primary);
        print("secondary" + secondary);

        if (operation == "+")
        {
            final = primary + secondary;
        }
        if (operation == "-")
        {
            final = primary - secondary;
        }
        if (operation == "*")
        {
            final = primary * secondary;
        }
        if (operation == "/")
        {
            final = primary / secondary;
        }
        if (operation == "%")
        {
            final = primary % secondary;
        }
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
            TimesUp.SetActive(false);
            Debug.Log("wrong");
        }
        else if (user_answer == final)
        {
            Question.SetActive(false);
            Result.SetActive(true);
            Correct.SetActive(true);
            TimesUp.SetActive(false);
            Debug.Log("Correct");
        }
    }

    public void setDigit(TextMeshProUGUI digitUI)
    {
        digitUI.text = UnityEngine.Random.Range(1, 10).ToString();
    }

    public void setSign(TextMeshProUGUI signUI)
    {
        string[] signs = { "+", "-", "*", "/", "%" };
        signUI.text = signs[UnityEngine.Random.Range(0, signs.Length - 1)];
    }

}
