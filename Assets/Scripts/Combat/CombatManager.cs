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
    public GameObject soul;
    public bool answered = false;
    private float movementSpeed = 400f;
    public bool goDown = false;
    private Vector3 curr_position;
    public GameObject canvas_scroll;
    public Animator playerAnimator;
    public Animator skeletonAnimator;
    public Animator CalculatorAnimator;
    // Start is called before the first frame update
    Unit playerUnit;
    Unit enemyUnit;
    public BattleState state;
    bool isDead, isDefend=false;
    float actionMultiplier = 1.0f;
    float actionHit = 1.0f;

    public void StartCombat()
    {
        calculatorScript.enabled = false;
        playerHUD.SetHUD(playerUnit);
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
        actionHit = 1.0f;
        actionMultiplier = Random.Range(0.9f, 1.1f);
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
        CalculatorAnimator.SetTrigger("is_shielding");
        yield return new WaitForSeconds(1.0f);
        state = BattleState.ENEMYTURN;
        StartCoroutine(EnemyTurn());
    }
    IEnumerator PlayerAttack()
    {
        answered = false;
        float mathMult;
        yield return new WaitUntil(() => answered == true);

        bool isCrit = Random.Range(0.00f, 100.0f) <= playerUnit.CRate;
        float CDmg = 1.5f;

        if (isCrit)
        {
            print("Critical Damage!");
            CDmg = 1.5f;
        }
        else CDmg = 1.0f;
        

        if(calculatorScript.answer_correct == true && calculatorScript.onTime == true)
        {
            mathMult = 1.0f + ((1.0f + playerUnit.ExtraMult) * (0.1f + calculatorScript.currentTime/calculatorScript.maxTime));
            
            print(mathMult + "A");
            isDead = enemyUnit.TakeDamage(playerUnit.Atk * mathMult * actionMultiplier * actionHit * CDmg);
            print("Attack is succesful");
        }
        else if(calculatorScript.answer_correct == false || calculatorScript.onTime == false)
        {
            mathMult = 0.5f;
            print(mathMult + "B");
            isDead = enemyUnit.TakeDamage(playerUnit.Atk * mathMult);  
            print("Attack not successful");
            goDown = false;
        }
        
        print("enemy HP " + enemyUnit.currentHP);
        calculatorScript.answer_correct = false;
        
        moves.SetActive(false);
        playerAnimator.SetTrigger("is_attacking");
        yield return new WaitForSeconds(0.4f);
        CalculatorAnimator.SetTrigger("is_throwing");
        yield return new WaitForSeconds(1.6f);
        skeletonAnimator.SetBool("is_hurt", true);
        yield return new WaitForSeconds(0.5f);
        skeletonAnimator.SetBool("is_hurt", false);
        // yield return new WaitForSeconds(10f);
        if(isDead)
        {
            //end battle
            skeletonAnimator.SetTrigger("is_death");
            yield return new WaitForSeconds(3f);
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

        float EnemyMod = Random.Range(0.8f, 1.1f);

        if (isDefend)
        {
            isDead = playerUnit.TakeDamage(enemyUnit.Atk * EnemyMod * 0.5f);
        }
        else if(!isDefend)
        {
            isDead = playerUnit.TakeDamage(enemyUnit.Atk * EnemyMod);
        }
        isDefend = false;
        skeletonAnimator.SetTrigger("is_attacking");
        yield return new WaitForSeconds(1.5f);
        playerAnimator.SetBool("if_hurt", true);
        yield return new WaitForSeconds(0.5f);
        playerAnimator.SetBool("if_hurt", false);
        playerHUD.SetHUD(playerUnit);
        yield return new WaitForSeconds(2f);
        if(isDead)
        {
            print("player died");
            playerAnimator.SetTrigger("death");
            yield return new WaitForSeconds(4f);
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
        CameraSwitch.register(CMVir);
        moves.SetActive(true);
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
            //canvas_scroll.SetActive(true);
            player.GetComponent<Unit>().Reset(1);
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

            foreach (GameObject enemy in playerMov.enemys)
            {
                enemy.SetActive(true);
            }

            soul.SetActive(true);
            soul.transform.position = playerMov.transform.position;
            
            playerMov.transform.position = GameObject.Find("Player Start Pos").transform.position;
            StartCoroutine(Coroutine());
            player.GetComponent<Unit>().Reset(0);
            enemy.GetComponent<Unit>().Reset(0);
            //canvas_scroll.SetActive(true);
            
            CameraSwitch.swithcam(CMVir);
        }
        playerHUD.SetHUD(playerUnit);
    }

        IEnumerator Coroutine()
    {
        yield return new WaitForSecondsRealtime(2);
    }
}
