using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Cinemachine;

public enum BattleState { START, PLAYERTURN, ENEMYTURN, WON, LOST }

public class CombatManager : MonoBehaviour
{
    public Calculator calculatorScript;
    public BattleHUD playerHUD;
    public PlayerMovement playerMov;
    public CinemachineVirtualCamera CMVir;

    public GameObject combatUI;
    public GameObject moves;

    public Button Attack;
    public Button Defend;
    public Button Special1;
    public Button Special2;

    public GameObject questions;
    public GameObject player;
    public GameObject enemy;
    public GameObject soul;

    public bool answered = false;
    private float movementSpeed = 400f;
    public bool goDown = false;

    private Vector3 curr_position;

    public Animator playerAnimator;
    public Animator skeletonAnimator;
    public Animator CalculatorAnimator;

    Unit playerUnit;
    Unit enemyUnit;
    public BattleState state;
    public int dead = 0;
    public int soulCurrency = 0;
    bool isDead, isDefend = false;
    float actionMultiplier = 1.0f;
    float actionHit = 1.0f;
    string actionName = "Attack";
    int buffTurns = 0;
    int debuffTurns = 0;

    GameObject glob;
    GlobalControl globc;

    Dictionary<string, int> currencyMult = new Dictionary<string, int>();
    Dictionary<string, float[]> skillDict = new Dictionary<string, float[]>();
    Dictionary<string, string[]> skillList = new Dictionary<string, string[]>();

    void Start()
    {
        glob = GameObject.Find("GlobalObject");
        globc = glob.GetComponent<GlobalControl>();

        skillDict = globc.skillDict;

        string[] animations;
        //                           player  , calculator
        animations = new string[2] { "AttackSpike", "NumbersTomb" };
        skillList.Add("Default_Skill1", animations);

        animations = new string[2] { "AttackSpike", "EarthSpike" };
        skillList.Add("Str_Skill1", animations);
        animations = new string[2] { "AttackSpike", "Fireball" };
        skillList.Add("Str_Skill2", animations);
        animations = new string[2] { "AttackSpike", "EarthCrusher" };
        skillList.Add("Str_Skill3", animations);
        animations = new string[2] { "AttackSpike", "FireExplode" };
        skillList.Add("Str_Skill4", animations);

        animations = new string[2] { "AttackSpike", "Waterball" };
        skillList.Add("Agi_Skill1", animations);
        animations = new string[2] { "AttackSpike", "DarkBolt" };
        skillList.Add("Agi_Skill2", animations);
        animations = new string[2] { "AttackSpike", "ShadowGhost" };
        skillList.Add("Agi_Skill3", animations);
        animations = new string[2] { "AttackSpike", "SparkLightning" };
        skillList.Add("Agi_Skill4", animations);

        animations = new string[2] { "AttackSpike", "Pulse" };
        skillList.Add("Int_Skill1", animations);
        animations = new string[2] { "AttackSpike", "CrossedPulse" };
        skillList.Add("Int_Skill2", animations);
        animations = new string[2] { "AttackSpike", "HolyHeal" };
        skillList.Add("Int_Skill3", animations);
        animations = new string[2] { "AttackSpike", "WaterBlast" };
        skillList.Add("Int_Skill4", animations);

        // Area 1
        currencyMult.Add("Skeleton", 8);
        currencyMult.Add("Bat", 8);
        currencyMult.Add("Zombie", 10);
        currencyMult.Add("Shade", 12);
        currencyMult.Add("TrashCave", 14);
        currencyMult.Add("Boss1", 30);

        // Area 2
        currencyMult.Add("SlimeForest", 8);
        currencyMult.Add("Mushroom", 8);
        currencyMult.Add("Tooth", 10);
        currencyMult.Add("Goblin", 12);
        currencyMult.Add("TrashForest", 14);
        currencyMult.Add("Boss2", 40);

        // Area 3
        currencyMult.Add("Eyeball", 8);
        currencyMult.Add("FlyEye", 8);
        currencyMult.Add("Fireworm", 10);
        currencyMult.Add("Imp", 12);
        currencyMult.Add("Demon", 14);
        currencyMult.Add("Boss3", 50);

    }
    public void StartCombat()
    {
        playerUnit = player.GetComponent<Unit>();
        enemyUnit = enemy.GetComponent<Unit>();
        enemyUnit.currentHP = enemyUnit.maxHP;
        calculatorScript.enabled = false;
        playerHUD.SetHUD(playerUnit);
        playerHUD.SetMaxHealth(enemyUnit.maxHP);
        state = BattleState.START;
        StartCoroutine(SetupBattle());
        curr_position = combatUI.transform.position;
        playerHUD.SetMaxHealth(enemyUnit.maxHP);

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
        playerHUD.battle_text.text = "Player Turn";
        // moves.SetActive(true);
        Attack.OnPointerExit(null);
        Defend.OnPointerExit(null);
        Special1.OnPointerExit(null);
        Special2.OnPointerExit(null);
        Attack.interactable = true;
        Defend.interactable = true;
        if (playerUnit.currentStamina >= (int)skillDict[globc.skill1][4])
        {
            Special1.interactable = true;
        }
        else Special1.interactable = false;

        if (playerUnit.currentStamina >= (int)skillDict[globc.skill2][4])
        {
            Special2.interactable = true;
        }
        else Special2.interactable = false;

        if (buffTurns == 0) playerUnit.ReStat();
        if (debuffTurns == 0) enemyUnit.ReStat();

        buffTurns -= 1;
        debuffTurns -= 1;
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
        if (!goDown && state == BattleState.PLAYERTURN)
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

    public void onAttackButton()
    {
        actionName = "Attack";
        actionHit = 1.0f;
        actionMultiplier = Random.Range(0.8f, 1.0f);
        calculatorScript.mode = UnityEngine.Random.Range(1, 4);

        if (state != BattleState.PLAYERTURN)
            return;
        StartCoroutine(PlayerAttack());
    }

    public void onDefendButton()
    {
        if (state != BattleState.PLAYERTURN)
            return;
        // moves.SetActive(false);
        Attack.OnPointerExit(null);
        Defend.OnPointerExit(null);
        Special1.OnPointerExit(null);
        Special2.OnPointerExit(null);
        Attack.interactable = false;
        Defend.interactable = false;
        Special1.interactable = false;
        Special2.interactable = false;

        StartCoroutine(PlayerDefend());
    }

    public void onSkill1Button()
    {
        actionName = "Skill1";
        actionHit = skillDict[globc.skill1][0];
        actionMultiplier = Random.Range(skillDict[globc.skill1][1], skillDict[globc.skill1][2]);
        calculatorScript.mode = UnityEngine.Random.Range(1, 4);

        if (state != BattleState.PLAYERTURN)
            return;
        StartCoroutine(PlayerAttack());
    }

    public void onSkill2Button()
    {
        actionName = "Skill2";
        actionHit = skillDict[globc.skill2][0];
        actionMultiplier = Random.Range(skillDict[globc.skill2][1], skillDict[globc.skill2][2]);
        calculatorScript.mode = UnityEngine.Random.Range(1, 4);

        if (state != BattleState.PLAYERTURN)
            return;
        StartCoroutine(PlayerAttack());
    }
    IEnumerator PlayerDefend()
    {
        isDefend = true;
        CalculatorAnimator.Play("Shield");
        yield return new WaitForSeconds(1.0f);
        state = BattleState.ENEMYTURN;
        StartCoroutine(EnemyTurn());
    }
    IEnumerator PlayerAttack()
    {

        yield return new WaitUntil(() => answered == true);

        float mathMult;
        float critRoller = Random.Range(0.01f, 100.0f);
        bool isCrit = critRoller <= playerUnit.CRate;
        float CDmg = playerUnit.CDmg;

        if (isCrit)
        {
            bool redCrit = playerUnit.CRate > 100.0f;
            float redCritRoller = Random.Range(0.01f, 100.0f);

            if (redCrit)
            {
                redCrit = redCritRoller <= (playerUnit.CRate - 100.0f);
            }

            if (redCrit)
            {
                print("Red Critical Damage! Rate Rolled: " + redCritRoller);
                CDmg = playerUnit.CDmg * 1.5f;
            }
            else if (!redCrit)
            {
                print("Critical Damage! Rate Rolled: " + critRoller);
                CDmg = playerUnit.CDmg;
            }
        }
        else CDmg = 1.0f;
        

        //moves.SetActive(false);
        if (actionName == "Attack")
        {
            playerAnimator.Play("attack");
            CalculatorAnimator.Play("CalculatorThrow");
            skeletonAnimator.Play("hurt");
            skeletonAnimator.Play("idle");
            CalculatorAnimator.Play("CalculatorIdle");
        }
        else if (actionName == "Skill1")
        {
            playerAnimator.Play(skillList[globc.skill1][0]);
            CalculatorAnimator.Play(skillList[globc.skill1][1]);
            if(skillDict[globc.skill1][5] == 1.0f)
            {
                skeletonAnimator.Play("hurt");
                skeletonAnimator.Play("idle");
            }
            else if (skillDict[globc.skill1][5] == 2.0f)
            {
                playerUnit.currentHP = playerUnit.maxHP;
                playerUnit.Def *= 1.6f;
                playerHUD.SetHUD(playerUnit);
                buffTurns = 1;

            }
            else if (skillDict[globc.skill1][5] == 3.0f)
            {
                enemyUnit.Def *= 0.7f;
                debuffTurns = 1;
            }

            if (skillDict[globc.skill1][3] > 0)
            {
                CalculatorAnimator.Play("CalculatorIdle");
            }
        }
        else if (actionName == "Skill2")
        {
            playerAnimator.Play(skillList[globc.skill2][0]);
            CalculatorAnimator.Play(skillList[globc.skill2][1]);
            if (skillDict[globc.skill1][5] == 1.0f)
            {
                skeletonAnimator.Play("hurt");
                skeletonAnimator.Play("idle");
            }
            else if (skillDict[globc.skill2][5] == 2.0f)
            {
                playerUnit.currentHP = playerUnit.maxHP;
                if (calculatorScript.answer_correct && calculatorScript.onTime)
                {
                    playerUnit.Def *= 1.0f + 0.6f * (0.1f + calculatorScript.currentTime / calculatorScript.maxTime);
                }
                else playerUnit.Def *= 1.0f + 0.6f * 0.2f;
                playerHUD.SetHUD(playerUnit);
                buffTurns = 1;
            }
            else if (skillDict[globc.skill2][5] == 3.0f)
            {
                if (calculatorScript.answer_correct && calculatorScript.onTime)
                {
                    enemyUnit.Def *= 1.0f - 0.3f * (0.1f + calculatorScript.currentTime / calculatorScript.maxTime);
                }
                else enemyUnit.Def *= 1.0f - 0.3f * 0.2f;
                debuffTurns = 1;
            }
            if (skillDict[globc.skill2][3] > 0)
            {
                CalculatorAnimator.Play("CalculatorIdle");
            }
        }

        yield return new WaitForSeconds(2.0f);
        if (calculatorScript.answer_correct == true && calculatorScript.onTime == true)
        {
            mathMult = 1.0f + ((1.0f + playerUnit.ExtraMult) * (0.1f + calculatorScript.currentTime / calculatorScript.maxTime));

            isDead = enemyUnit.TakeDamage(playerUnit.Atk * mathMult * actionMultiplier * actionHit * CDmg);
            print("Attack is succesful");
        }
        else if (calculatorScript.answer_correct == false || calculatorScript.onTime == false)
        {
            mathMult = 0.5f;
            isDead = enemyUnit.TakeDamage(playerUnit.Atk * mathMult * actionMultiplier * actionHit * CDmg);
            print("Attack not successful");
            goDown = false;
        }

        calculatorScript.answer_correct = false;
        answered = false;

        // moves.SetActive(false);
        Attack.OnPointerExit(null);
        Defend.OnPointerExit(null);
        Special1.OnPointerExit(null);
        Special2.OnPointerExit(null);
        Attack.interactable = false;
        Defend.interactable = false;
        Special1.interactable = false;
        Special2.interactable = false;
        playerHUD.SetHealth(enemyUnit.currentHP);
        playerHUD.SetHUD(playerUnit);

        if (isDead)
        {
            //end battle
            skeletonAnimator.Play("death");
            if(skeletonAnimator.tag == "Boss1" || skeletonAnimator.tag == "Boss2" || skeletonAnimator.tag == "Boss3") yield return new WaitForSeconds(7.0f);
            else yield return new WaitForSeconds(2.5f);
            state = BattleState.WON;
            EndBattle();
        }
        else
        {
            //enemy turn
            yield return new WaitForSeconds(2.0f);
            calculatorScript.enabled = false;
            state = BattleState.ENEMYTURN;
            StartCoroutine(EnemyTurn());
        }
    }
    IEnumerator EnemyTurn()
    {
        playerHUD.battle_text.text = enemyUnit.tag + " Turn";
        print(enemyUnit.tag + " attacks!");
        calculatorScript.Question.SetActive(false);
        calculatorScript.Result.SetActive(false);
        calculatorScript.Wrong.SetActive(false);
        calculatorScript.Correct.SetActive(false);
        calculatorScript.TimesUp.SetActive(false);
        yield return new WaitForSeconds(2.0f);

        playerHUD.battle_text.text = enemyUnit.tag + " attacks!";
        float EnemyMod = Random.Range(0.8f, 1.1f);
        float critRoller = Random.Range(0.01f, 100.0f);
        bool isCrit = critRoller <= enemyUnit.CRate;
        float CDmg = enemyUnit.CDmg;

        if (isCrit)
        {
            bool redCrit = enemyUnit.CRate > 100.0f;
            float redCritRoller = Random.Range(0.01f, 100.0f);

            if (redCrit)
            {
                redCrit = redCritRoller <= (enemyUnit.CRate - 100.0f);
            }

            if (redCrit)
            {
                print("Red Critical Damage! Rate Rolled: " + redCritRoller);
                CDmg = enemyUnit.CDmg * 1.5f;
            }
            else if (!redCrit)
            {
                print("Critical Damage! Rate Rolled: " + critRoller);
                CDmg = enemyUnit.CDmg;
            }
        }
        else CDmg = 1.0f;
        
        isDefend = false;
        skeletonAnimator.Play("attack");
        playerAnimator.Play("hurt");
        playerHUD.SetHUD(playerUnit);
        yield return new WaitForSeconds(2.0f);

        if (isDefend)
        {
            isDead = playerUnit.TakeDamage(enemyUnit.Atk * EnemyMod * CDmg * 0.5f);
        }
        else if (!isDefend)
        {
            isDead = playerUnit.TakeDamage(enemyUnit.Atk * EnemyMod * CDmg);
        }

        yield return new WaitForSeconds(1.0f);

        if (isDead)
        {
            print("player died");
            playerAnimator.Play("death");
            yield return new WaitForSeconds(2.0f);
            state = BattleState.LOST;
            EndBattle();
        }
        else
        {
            state = BattleState.PLAYERTURN;
            goDown = false;
            calculatorScript.enabled = false;
            answered = false;
            if (playerUnit.currentStamina > 0)
            {
                PlayerTurn();
            }
            else
            {
                print("player died");
                playerAnimator.Play("death");
                yield return new WaitForSeconds(2.0f);
                state = BattleState.LOST;
                EndBattle();
            }
        }
    }
    void EndBattle()
    {
        CameraSwitch.register(CMVir);

        PlayerMovement.instance.gameObject.GetComponent<Animator>().SetBool("inCombat", false);
        glob.GetComponent<GlobalControl>().inCombat = false;
        moves.SetActive(true);
        // Attack.enabled = true;
        // Defend.enabled = true;
        // Special1.enabled = true;
        // Special2.enabled = true;

        if (state == BattleState.WON)
        {
            print("You won the battle!");
            playerHUD.battle_text.text = "You Won the Battle!";
            calculatorScript.Question.SetActive(false);
            calculatorScript.Result.SetActive(false);
            calculatorScript.Wrong.SetActive(false);
            calculatorScript.Correct.SetActive(false);
            calculatorScript.TimesUp.SetActive(false);
            calculatorScript.enabled = false;
            playerMov.collidedd.SetActive(false);
            StartCoroutine(Coroutine());

            glob.GetComponent<GlobalControl>().playerCurrency += enemyUnit.unitLevel * currencyMult[enemyUnit.tag] * Random.Range(6, 9);

            CameraSwitch.swithcam(CMVir);
            //print(CameraSwitch.isActiveCam(CMVir));
            playerUnit.ReStat();
            player.GetComponent<Unit>().Reset(1);
        }
        else if (state == BattleState.LOST)
        {
            print("You were defeated");
            playerHUD.battle_text.text = "You Were Defeated!";

            calculatorScript.Question.SetActive(false);
            calculatorScript.Result.SetActive(false);
            calculatorScript.Wrong.SetActive(false);
            calculatorScript.Correct.SetActive(false);
            calculatorScript.TimesUp.SetActive(false);
            calculatorScript.enabled = false;
            foreach (GameObject enemy in playerMov.unityGameObjects)
            {
                enemy.SetActive(true);
            }

            soul.SetActive(true);
            soul.transform.position = playerMov.transform.position;
            soulCurrency = glob.GetComponent<GlobalControl>().playerCurrency;
            dead += 1;

            playerMov.transform.position = GameObject.Find("Player Start Pos").transform.position;
            StartCoroutine(Coroutine());

            glob.GetComponent<GlobalControl>().playerCurrency = 0;

            CameraSwitch.swithcam(CMVir);
            playerUnit.ReStat();
            player.GetComponent<Unit>().Reset(0);
            enemy.GetComponent<Unit>().Reset(0);
        }

        playerMov.collidedd.GetComponent<Animator>().Play("walk");
        playerHUD.SetHUD(playerUnit);
    }

    IEnumerator Coroutine()
    {
        yield return new WaitForSecondsRealtime(2.0f);
    }
}
