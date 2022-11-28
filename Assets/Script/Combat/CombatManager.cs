using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public enum BattleState { START, PLAYERTURN, ENEMYTURN, WON, LOST}

public class CombatManager : MonoBehaviour
{
    public Calculator calculatorScript;
    public BattleHUD playerHUD;
    public PlayerMovement playerMov;
    public CinemachineVirtualCamera CMVir;

    public GameObject combatUI;
    public GameObject moves;
    public GameObject questions;
    public GameObject player;
    public GameObject enemy;
    public bool answered = false;
    private float movementSpeed = 400f;
    public bool goDown = false;
    private Vector3 curr_position;
    public GameObject canvas_scroll;
    // Start is called before the first frame update
    Unit playerUnit;
    Unit enemyUnit;
    public BattleState state;
    bool isDead, isDefend=false;
    void Start()
    {

        calculatorScript.enabled = false;
        state = BattleState.START;
        StartCoroutine(SetupBattle());
        curr_position = combatUI.transform.position;
    }
    IEnumerator SetupBattle()
    {
        playerUnit = player.GetComponent<Unit>();
        enemyUnit = enemy.GetComponent<Unit>();
        print("Battle Starts");
        yield return new WaitForSeconds(0.5f);
        state = BattleState.PLAYERTURN;
        PlayerTurn();
    }
    void PlayerTurn()
    {   
        print("Player Turn");
        moves.SetActive(true);
    }
    // Update is called once per frame
    void Update()
    {

        Vector3 aPos = combatUI.transform.position;
        if (aPos.y > -300 && goDown)
        {
            aPos.x = aPos.z = 0;
            aPos.y = -movementSpeed * Time.deltaTime;
            // print(Time.deltaTime);
            // print(aPos);
            combatUI.transform.position += aPos;
        }
        if(!goDown && state == BattleState.PLAYERTURN)
        {
            combatUI.transform.position = curr_position;
        }
    }

    public void onPressed(string action)
    {
        print("pressed");
        goDown = true;
        calculatorScript.enabled = true;
        questions.SetActive(true);
    }
    public void onAttackButton(){
        if(state != BattleState.PLAYERTURN)
            return;
        StartCoroutine(PlayerAttack());
    }
    public void onDefendButton(){
        if(state != BattleState.PLAYERTURN)
            return;
        moves.SetActive(false);
        StartCoroutine(PlayerDefend());
    }
    IEnumerator PlayerDefend()
    {  
        isDefend = true;
        yield return new WaitForSeconds(1.5f);
        state = BattleState.ENEMYTURN;
        StartCoroutine(EnemyTurn());
    }
    IEnumerator PlayerAttack()
    {   
        
        yield return new WaitUntil(() => answered == true);
        if(calculatorScript.answer_correct == true && calculatorScript.onTime == true)
        {
            isDead = enemyUnit.TakeDamage(playerUnit.damage);  
            print("Attack is succesful");
        }
        else if(calculatorScript.answer_correct == false || calculatorScript.onTime == false)
        {
            isDead = enemyUnit.TakeDamage( playerUnit.damage * 0.25f);  
            print("Attack not successful");
            goDown = false;
        }
        print("enemy HP " + enemyUnit.currentHP);
        calculatorScript.answer_correct = false;
        answered = false;
        moves.SetActive(false);
        // yield return new WaitForSeconds(10f);
        if(isDead)
        {
            //end battle
            state = BattleState.WON;
            EndBattle();
        }
        else{
            //enemy turn
            yield return new WaitForSeconds(2f);
            calculatorScript.enabled = false;
            state = BattleState.ENEMYTURN;
            StartCoroutine(EnemyTurn());
        }
    }
    IEnumerator EnemyTurn()
    {   
        print(enemyUnit.unitName + " attacks!");
        calculatorScript.Question.SetActive(false);
        calculatorScript.Result.SetActive(false);
        calculatorScript.Wrong.SetActive(false);
        calculatorScript.Correct.SetActive(false);
        calculatorScript.TimesUp.SetActive(false);
        yield return new WaitForSeconds(2f);
        if(isDefend)
        {
            isDead = playerUnit.TakeDamage(enemyUnit.damage * 0.5f);
        }
        else if(!isDefend)
        {
            isDead = playerUnit.TakeDamage(enemyUnit.damage);
        }
        isDefend = false;
        playerHUD.SetHUD(playerUnit);
        yield return new WaitForSeconds(2f);
        if(isDead)
        {
            state = BattleState.LOST;
            EndBattle();
        }
        else{
            state = BattleState.PLAYERTURN;
            goDown = false;
            calculatorScript.enabled = false;
            answered = false;
            if(playerUnit.currentStamina>0)
            {
                PlayerTurn();
            }
            else{
                state = BattleState.LOST;
                EndBattle();
            }
        }
    }
    void EndBattle(){
        if(state==BattleState.WON)
        {
            print("You won the battle!");
            calculatorScript.Question.SetActive(false);
            calculatorScript.Result.SetActive(false);
            calculatorScript.Wrong.SetActive(false);
            calculatorScript.Correct.SetActive(false);
            calculatorScript.TimesUp.SetActive(false);
            calculatorScript.enabled = false;
            StartCoroutine(Coroutine());
            // foreach (GameObject enemy in playerMov.enemys)
            // {
            //     enemy.SetActive(true);
            // }
            //CameraSwitch.register(CMVir);
            canvas_scroll.SetActive(true);
            CameraSwitch.swithcam(CMVir);
            print(CameraSwitch.isActiveCam(CMVir));
        }
        else if(state == BattleState.LOST)
        {
            print("You were defeated");
            calculatorScript.Question.SetActive(false);
            calculatorScript.Result.SetActive(false);
            calculatorScript.Wrong.SetActive(false);
            calculatorScript.Correct.SetActive(false);
            calculatorScript.TimesUp.SetActive(false);
            calculatorScript.enabled = false;
            StartCoroutine(Coroutine());
            // foreach (GameObject enemy in playerMov.enemys)
            // {
            //     enemy.SetActive(true);
            // }
            canvas_scroll.SetActive(true);
            CameraSwitch.register(CMVir);
            CameraSwitch.swithcam(CMVir);
        }
    }

        IEnumerator Coroutine()
    {
        yield return new WaitForSecondsRealtime(2);
    }
}
