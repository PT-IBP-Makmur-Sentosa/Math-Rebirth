using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum BattleState { START, PLAYERTURN, ENEMYTURN, WON, LOST}

public class CombatManager : MonoBehaviour
{
    public Calculator calculatorScript;
    public BattleHUD playerHUD;

    public GameObject combatUI;
    public GameObject questions;
    public GameObject player;
    public GameObject enemy;
    public bool answered = false;
    private float movementSpeed = 400f;
    public bool goDown = false;
    private Vector3 curr_position;
    // Start is called before the first frame update
    Unit playerUnit;
    Unit enemyUnit;
    public BattleState state;
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
    IEnumerator PlayerAttack()
    {
        bool isDead = enemyUnit.TakeDamage(playerUnit.damage);
        print("Attack is succesful");
        yield return new WaitUntil(() => answered == true);
        // yield return new WaitForSeconds(10f);
        if(isDead)
        {
            //end battle
            state = BattleState.WON;
            EndBattle();
        }
        else{
            //enemy turn
            calculatorScript.enabled = false;
            state = BattleState.ENEMYTURN;
            StartCoroutine(EnemyTurn());
        }
        
    }
    IEnumerator EnemyTurn()
    {
        print(enemyUnit.unitName + " attacks!");
        yield return new WaitForSeconds(2f);
        bool isDead = playerUnit.TakeDamage(enemyUnit.damage);
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
        }
        else if(state == BattleState.LOST)
        {
            print("You were defeated");
        }
    }
}
