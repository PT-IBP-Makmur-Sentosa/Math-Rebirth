using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class Calculator : MonoBehaviour
{
    int final;
    public CombatManager combatManagerScript;
    public BattleHUD playerHUD;

    public GameObject Question, Result;
    public TextMeshProUGUI PrimaryDigit, SecondaryDigit, SignDigit;
    public GameObject TMP_InputField_Answer;
    public GameObject Correct, Wrong, TimesUp;
    public Unit player;
    [SerializeField] TextMeshProUGUI countdownText;
    public float currentTime = 0f, startTime = 10f, maxTime = 0.0f;
    int temp;
    public bool onTime = true, answer_correct = false, keepTimer=true;
    float lotteryTime = 3f;
    bool runLottery = false;
    // Start is called before the first frame update
    void Start()
    {

    }

    void OnEnable()
    {
        print("enabled");
        lotteryTime = 2f;
        runLottery = true;
        keepTimer = true;
        // CalculatorFn("addition");
        countdownText.color = Color.white;
        maxTime = startTime + player.ExtraTime;
        currentTime = maxTime;
        Question.SetActive(true);
        TMP_InputField_Answer.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Return) && combatManagerScript.state == BattleState.PLAYERTURN && TMP_InputField_Answer.GetComponent<TMP_InputField>().text!="" && TMP_InputField_Answer.GetComponent<TMP_InputField>().text!="-")
        {
            string answer = TMP_InputField_Answer.GetComponent<TMP_InputField>().text;
            Debug.Log("User Answer: " + answer);
            combatManagerScript.answered = true;
            keepTimer = false;
            CheckAnswerFn(final, answer);
        }

        // timer
        if(keepTimer && combatManagerScript.state == BattleState.PLAYERTURN)
        {
            currentTime -= 1 * Time.deltaTime;
            countdownText.text = currentTime.ToString("0.0"); 
            print(currentTime);
        }
        
        if(currentTime <= 3)
        {
            countdownText.color = Color.red;
        }

        if (currentTime > 0)
        {
            onTime = true;
        }
        else if (currentTime <= 0)
        {
            currentTime = 0;
            combatManagerScript.answered = true;
            keepTimer = false;
            onTime = false;
            answer_correct = false;
            Question.SetActive(false);
            TimesUp.SetActive(true);
            Result.SetActive(true);
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
            answer_correct = true;
            Question.SetActive(false);
            Result.SetActive(true);
            Correct.SetActive(true);
            TimesUp.SetActive(false);
            Debug.Log("Correct");
        }
        TMP_InputField_Answer.GetComponent<TMP_InputField>().text = "";
        combatManagerScript.goDown = false;
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
