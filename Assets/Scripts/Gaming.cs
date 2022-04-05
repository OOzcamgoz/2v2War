using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class Gaming : MonoBehaviour
{
    public static string playerNameString;
    public static int enemyLevelMax=2;
    public static int playerLevelMax = 2;
    public static int enemyLevelMin = 1;
    public static int playerLevelMin = 1;
    private float skillCoolDownTimer = 1.5f;
    public float cureCoolDown = 2.5f;
    public float cureCoolDownTimer;
    public float lightAttackCoolDown = 1.5f;
    public float lightAttackCoolDownTimer;
    public float spikeAttackCoolDown = 11f;
    public float spikeAttackCoolDownTimer;
    public float thurstAttackCoolDown = 11f;
    public float thurstAttackCoolDownTimer;
    public float enemyPlayerManaCoolDown = 2.5f;
    public float enemyPlayerManaCoolDownTimer;

    public Animator enemyAnimator;
    public Animator priestAnimator;
    public Animator orcPriestAnimator;

    public int humanScore;
    public int orcScore;
    public TMP_Text playerScore;
    public TMP_Text enemyScore;

    public TMP_Text start;
    public TMP_Text countdown;

    public TMP_Text playerName;
    public GameObject startObject;
    public GameObject countdownObject;
    public GameObject informationObject;

    public GameObject winObject;
    public GameObject loseObject;
    public GameObject levelFinish;

    public Button rezButton;

    public GameObject enemyBubble;
    public GameObject priestBubble;
    public Text enemyText;
    public Text priestText;

    public string deadText;

    public int spikeDamagePlayer;
    public int thurstDamagePlayer;
    public int lightAttackDamagePlayer;

    private int attackTurn;
    private bool attackOn = false;
    public bool enemyMinorActive = true;

    PlayerHealth playerHealth;
    EnemyHealth enemyHealth;

    private int randDead;

    private bool playerDeath = false;
    private bool enemyDeath = false;

    private bool playerDeathStatus = false;
    private bool enemyDeathStatus = false;


    public void Awake()
    {
        //BeginF();
        playerHealth = GameObject.Find("Player").GetComponent<PlayerHealth>();
        enemyHealth = GameObject.Find("Orc Warrior").GetComponent<EnemyHealth>();
    }
    // Start is called before the first frame update
    void Start()
    {
        playerName.text = playerNameString;
        BeginF();
        humanScore = 0;
        orcScore = 0;
        
        randDead = Random.Range(0, 7);
        
        
        int start = Random.Range(1, 3);
        if (start == 1)
        { attackTurn = 1; }
        else if (start == 2)
        { attackTurn = 2; }
        //InvokeRepeating("DamagePlayer", 2.0f, 13.0f);
        //InvokeRepeating("Randomness", 2.0f, 13.0f);
        //InvokeRepeating("CombatSwitch", 5.0f, 7.0f);
        


    }
    public void WinF()
    {
        StartCoroutine(Win());
        CancelInvoke();
    }

    IEnumerator Win()
    {
        CancelInvoke();
        GameObject.Find("LightAttackButton").GetComponent<Button>().interactable = false;
        GameObject.Find("SpikeButton").GetComponent<Button>().interactable = false;
        GameObject.Find("ThurstButton").GetComponent<Button>().interactable = false;
        GameObject.Find("CureButton").GetComponent<Button>().interactable = false;
        GameObject.Find("ManaButton").GetComponent<Button>().interactable = false;
        GameObject.Find("MinorButton").GetComponent<Button>().interactable = false;
        priestBubble.gameObject.SetActive(false);
        yield return new WaitForSeconds(1f);
        CancelInvoke();
        winObject.gameObject.SetActive(true);
           yield return new WaitForSeconds(3f);
        CancelInvoke();
        winObject.gameObject.SetActive(false);
        yield return new WaitForSeconds(0.5f);
        CancelInvoke();
        levelFinish.gameObject.SetActive(true);
    }

    public void LoseF()
    {
        StartCoroutine(Lose());
        CancelInvoke();
    }

    IEnumerator Lose()
    {
        GameObject.Find("LightAttackButton").GetComponent<Button>().interactable = false;
        GameObject.Find("SpikeButton").GetComponent<Button>().interactable = false;
        GameObject.Find("ThurstButton").GetComponent<Button>().interactable = false;
        GameObject.Find("CureButton").GetComponent<Button>().interactable = false;
        GameObject.Find("ManaButton").GetComponent<Button>().interactable = false;
        GameObject.Find("MinorButton").GetComponent<Button>().interactable = false;
        priestBubble.gameObject.SetActive(false);
        yield return new WaitForSeconds(1f);
        CancelInvoke();
        loseObject.gameObject.SetActive(true);
        yield return new WaitForSeconds(4f);
        CancelInvoke();
        SceneManager.LoadScene("Level Menu");
    }
    public void BeginF()
    {
        StartCoroutine(Begin());
    }

    IEnumerator Begin()
    {
        informationObject.gameObject.SetActive(true);
        yield return new WaitForSeconds(2f);
        informationObject.gameObject.SetActive(false);
        countdown.text = "3";
        countdownObject.gameObject.SetActive(true);
        yield return new WaitForSeconds(1f);
        countdown.text = "2";
        yield return new WaitForSeconds(1f);
        countdown.text = "1";
        yield return new WaitForSeconds(1f);
        countdownObject.gameObject.SetActive(false);
        startObject.gameObject.SetActive(true);
        yield return new WaitForSeconds(1f);
        startObject.gameObject.SetActive(false);
        attackOn = true;
        InvokeRepeating("AutoAttack", 0.5f, 1.2f);
    }

    public void DeadF()
    {
        StartCoroutine(Dead());
    }
    IEnumerator Dead()
    {
        
        yield return new WaitForSeconds(1.5f);
        


    }
    public void Close()
    {
        SceneManager.LoadScene("Level Menu");
    }
    //Auto Attack to Player
    public void AutoAttack()
    {
        LightAttacktDamagePlayer();
    }

    // Update is called once per frame
    void Update()
    {
        if(orcScore == 10)
        {
            CancelInvoke();
            LoseF();
            orcScore = 99;
        }
        if (humanScore == 10)
        {
            CancelInvoke();
            WinF();
            humanScore = 99;
        }
        if (orcScore == 99)
        {
            CancelInvoke();

        }
        if (humanScore == 99)
        {
            CancelInvoke();

        }
        enemyScore.text = orcScore.ToString();
        playerScore.text = humanScore.ToString();
        if (playerHealth.playerHealth == 0)
        {
            playerDeath = true;
            CancelInvoke();
            StopAllCoroutines();
            playerHealth.playerHealth = 0;
            GameObject.Find("LightAttackButton").GetComponent<Button>().interactable = false;
            GameObject.Find("SpikeButton").GetComponent<Button>().interactable = false;
            GameObject.Find("ThurstButton").GetComponent<Button>().interactable = false;
            GameObject.Find("CureButton").GetComponent<Button>().interactable = false;
            GameObject.Find("ManaButton").GetComponent<Button>().interactable = false;
            GameObject.Find("MinorButton").GetComponent<Button>().interactable = false;
            priestBubble.gameObject.SetActive(false);
            enemyText.GetComponent<Text>().text = deadText;
            playerHealth.playerAnimator.SetInteger("Dead", 2);
            playerHealth.playerAnimator.SetInteger("StillDead", 2);
            enemyAnimator.SetInteger("Win", 2);

            enemyBubble.gameObject.SetActive(true);
            rezButton.gameObject.SetActive(true);

        }
        if(enemyHealth.enemyHealth == 0)
        {
            enemyDeath = true;
            CancelInvoke();
            StopAllCoroutines();
            enemyHealth.enemyHealth = 0;
            GameObject.Find("LightAttackButton").GetComponent<Button>().interactable = false;
            GameObject.Find("SpikeButton").GetComponent<Button>().interactable = false;
            GameObject.Find("ThurstButton").GetComponent<Button>().interactable = false;
            GameObject.Find("CureButton").GetComponent<Button>().interactable = false;
            GameObject.Find("ManaButton").GetComponent<Button>().interactable = false;
            GameObject.Find("MinorButton").GetComponent<Button>().interactable = false;
            rezButton.gameObject.SetActive(true);
            priestBubble.gameObject.SetActive(false);
            enemyAnimator.SetInteger("Dead", 2);
            enemyAnimator.SetInteger("StillDead", 2);
            playerHealth.playerAnimator.SetInteger("Win", 2);
        }
        enemyHealth.enemyMana = Mathf.Clamp(enemyHealth.enemyMana, 0f, 1f);
        ManaEnemyPlayer();
        if (enemyHealth.enemyHealth < enemyHealth.enemyHealthMax)
        {
            MinorEnemyPlayerF();
        }
        if(attackOn == true)
        {
            attackOn = false;
            Invoke("CombatSwitch", 0.0f);
        }

        if (enemyHealth.confusionActive == true)
        {
            spikeDamagePlayer = 30;
            thurstDamagePlayer = 41;
            lightAttackDamagePlayer = 10;
        }
        else
        {
            spikeDamagePlayer = 33;
            thurstDamagePlayer = 45;
            lightAttackDamagePlayer = 13;
        }

        SetHealth();

        //Enemy Player Mana Cooldown
        if (enemyPlayerManaCoolDownTimer > 0)
        {
            enemyPlayerManaCoolDownTimer -= Time.deltaTime;
        }
        if (enemyPlayerManaCoolDownTimer < 0)
        {

            enemyPlayerManaCoolDownTimer = 0;
        }
        if (randDead == 0)
        { deadText = "qweqweqweqweq"; }
        if (randDead == 1)
        { deadText = "Not amuseddd"; }
        if (randDead == 2)
        { deadText = "Slapped youuuu"; }
        if (randDead == 3)
        { deadText = "Lie downnn"; }
        if (randDead == 4)
        { deadText = "Not in my caliberrr"; }
        if (randDead == 5)
        { deadText = "Not in my leagueee"; }
        if (randDead == 6)
        { deadText = "You are not a warriorrr"; }
    }
    
    void DamagePlayer()
    {
        playerHealth.playerHealth -= 10f;
        SetHealth();
        enemyAnimator.SetTrigger("LightAttack");
    }

    

    //Set HP bar to Health
    public void SetHealth()
    {
        playerHealth.playerHealthBarSlider.value = playerHealth.playerHealth;
    }




    /*public void CombatSwitchF()
    {
        StartCoroutine(CombatSwitch());
    }*/


    public void CombatSwitch()
    {
        

        //CancelInvoke("AutoAttack");
        
        if (attackTurn == 1)
        {
            int rand9 = Random.Range(enemyLevelMin, enemyLevelMax);
            if (rand9 == 1)
            {
                //Enemy Attack
                int rand = Random.Range(0, 5);
                switch (rand)
                {
                    case 0:
                    Invoke("CombatPatternEnemyF1", 0.1f);
                    break;
                    case 1:
                    Invoke("CombatPatternEnemyF2", 0.1f);
                    break;
                    case 2:
                    Invoke("CombatPatternEnemyF3", 0.1f);
                    break;
                    case 3:
                    Invoke("CombatPatternEnemyF4", 0.1f);
                    break;
                    case 4:
                    Invoke("CombatPatternEnemyF5", 0.1f);
                    break;
                    default: break;
                }
            }
            if (rand9 == 2)
            {
                //Enemy Attack
                int rand = Random.Range(0, 5);
                switch (rand)
                {
                    case 0:
                        Invoke("CombatPatternEnemyF12", 0.1f);
                        break;
                    case 1:
                        Invoke("CombatPatternEnemyF22", 0.1f);
                        break;
                    case 2:
                        Invoke("CombatPatternEnemyF32", 0.1f);
                        break;
                    case 3:
                        Invoke("CombatPatternEnemyF42", 0.1f);
                        break;
                    case 4:
                        Invoke("CombatPatternEnemyF52", 0.1f);
                        break;
                    default: break;
                }
            }
            if (rand9 == 3)
            {
                //Enemy Attack
                int rand = Random.Range(0, 5);
                switch (rand)
                {
                    case 0:
                        Invoke("CombatPatternEnemyF13", 0.1f);
                        break;
                    case 1:
                        Invoke("CombatPatternEnemyF23", 0.1f);
                        break;
                    case 2:
                        Invoke("CombatPatternEnemyF33", 0.1f);
                        break;
                    case 3:
                        Invoke("CombatPatternEnemyF43", 0.1f);
                        break;
                    case 4:
                        Invoke("CombatPatternEnemyF53", 0.1f);
                        break;
                    default: break;
                }
            }

        }
        

        if (attackTurn == 2)
        {
            int rand2 = Random.Range(playerLevelMin, playerLevelMax);
            if(rand2 == 1)
            {
            //Our Attack
            int rand = Random.Range(0, 6);
            switch (rand)
            {
                case 0:
                    Invoke("CombatPatternFriendF1", 0.1f);
                    break;
                case 1:
                    Invoke("CombatPatternFriendF2", 0.1f);
                    break;
                case 2:
                    Invoke("CombatPatternFriendF3", 0.1f);
                    break;
                case 3:
                    Invoke("CombatPatternFriendF4", 0.1f);
                    break;
                case 4:
                    Invoke("CombatPatternFriendF5", 0.1f);
                    break;
                case 5:
                    Invoke("CombatPatternFriendF6", 0.1f);
                    break;
                default: break;
            }
            }
            if (rand2 == 2)
            {
                //Our Attack
                int rand3 = Random.Range(0, 6);
                switch (rand3)
                {
                    case 0:
                        Invoke("CombatPatternFriendF12", 0.1f);
                        break;
                    case 1:
                        Invoke("CombatPatternFriendF22", 0.1f);
                        break;
                    case 2:
                        Invoke("CombatPatternFriendF32", 0.1f);
                        break;
                    case 3:
                        Invoke("CombatPatternFriendF42", 0.1f);
                        break;
                    case 4:
                        Invoke("CombatPatternFriendF52", 0.1f);
                        break;
                    case 5:
                        Invoke("CombatPatternFriendF62", 0.1f);
                        break;
                    default: break;
                }

            }
            if (rand2 == 3)
            {
                //Our Attack
                int rand4 = Random.Range(0, 6);
                switch (rand4)
                {
                    case 0:
                        Invoke("CombatPatternFriendF13", 0.1f);
                        break;
                    case 1:
                        Invoke("CombatPatternFriendF23", 0.1f);
                        break;
                    case 2:
                        Invoke("CombatPatternFriendF33", 0.1f);
                        break;
                    case 3:
                        Invoke("CombatPatternFriendF43", 0.1f);
                        break;
                    case 4:
                        Invoke("CombatPatternFriendF53", 0.1f);
                        break;
                    case 5:
                        Invoke("CombatPatternFriendF63", 0.1f);
                        break;
                    default: break;
                }

            }

            if (rand2 == 4)
            {
                //Our Attack
                int rand5 = Random.Range(0, 5);
                switch (rand5)
                {
                    case 0:
                        Invoke("CombatPatternFriendF14", 0.1f);
                        break;
                    case 1:
                        Invoke("CombatPatternFriendF24", 0.1f);
                        break;
                    case 2:
                        Invoke("CombatPatternFriendF34", 0.1f);
                        break;
                    case 3:
                        Invoke("CombatPatternFriendF44", 0.1f);
                        break;
                    case 4:
                        Invoke("CombatPatternFriendF54", 0.1f);
                        break;
                    default: break;
                }

            }
        }
    }



    public void CombatPatternEnemyF1()
    {
        StartCoroutine(CombatPatternEnemy1());
    }
    IEnumerator CombatPatternEnemy1()
    {
        print("1e");
        CancelInvoke("AutoAttack");
        DebuffPlayerF(55);
        yield return new WaitForSeconds(1.5f);
        MalicePlayerF();
        float rand2 = Random.Range(0, 0.2f);
        yield return new WaitForSeconds(rand2);
        SpikeAttacktDamagePlayer();
        HealPlayerF();
        //InvokeRepeating("HealPlayerF", 0.0f, 1.5f);
        yield return new WaitForSeconds(1.5f - rand2);
        float rand4 = Random.Range(0.1f, 0.4f);
        yield return new WaitForSeconds(rand4);
        ThurstAttacktDamagePlayer();
        attackTurn = 2;
        attackOn = true;
        yield return new WaitForSeconds(1f);
        InvokeRepeating("AutoAttack", 0.1f, 1.2f);



    }

    public void CombatPatternEnemyF2()
    {
        StartCoroutine(CombatPatternEnemy2());
    }
    IEnumerator CombatPatternEnemy2()
    {
        print("2e");
        ConfusionPlayerF();
        yield return new WaitForSeconds(1.5f);
        DebuffPlayerF(55);
        float rand5 = Random.Range(0.5f, 1f);
        yield return new WaitForSeconds(rand5);
        HealPlayerF();
        yield return new WaitForSeconds(1.5f - rand5);
        MalicePlayerF();
        float rand2 = Random.Range(0, 0.5f);
        yield return new WaitForSeconds(rand2);
        CancelInvoke("AutoAttack");
        yield return new WaitForSeconds(1f);
        SpikeAttacktDamagePlayer();
        yield return new WaitForSeconds(1f);
        ThurstAttacktDamagePlayer();
        HealPlayerF();
        yield return new WaitForSeconds(1f);
        attackTurn = 2;
        attackOn = true;
        InvokeRepeating("AutoAttack", 0.1f, 1.2f);



    }

    public void CombatPatternEnemyF3()
    {
        StartCoroutine(CombatPatternEnemy3());
    }
    IEnumerator CombatPatternEnemy3()
    {
        print("3e");
        DebuffPlayerF(55);
        yield return new WaitForSeconds(1.5f);
        ReverseLifePlayerF();
        float rand3 = Random.Range(0, 0.1f);
        yield return new WaitForSeconds(rand3);
        HealPlayerF();
        CancelInvoke("AutoAttack");
        //InvokeRepeating("HealPlayerF", 0.0f, 1.5f);
        yield return new WaitForSeconds(1.5f - rand3);
        MalicePlayerF();
        HealPlayerF();
        float rand2 = Random.Range(0.5f, 1.5f);
        yield return new WaitForSeconds(rand2);
        SpikeAttacktDamagePlayer();        
        yield return new WaitForSeconds(1.5f - rand2 - rand3);
        float rand4 = Random.Range(0.1f, 0.4f);
        yield return new WaitForSeconds(rand4);
        ThurstAttacktDamagePlayer();
        
        CancelInvoke("HealPlayerF");
        attackTurn = 2;
        attackOn = true;
        yield return new WaitForSeconds(1f);
        InvokeRepeating("AutoAttack", 0.1f, 1.2f);



    }


    public void CombatPatternEnemyF4()
    {
        StartCoroutine(CombatPatternEnemy4());
    }
    IEnumerator CombatPatternEnemy4()
    {
        print("4e");
        ConfusionPlayerF();
        yield return new WaitForSeconds(1.5f);
        DebuffPlayerF(55);
        yield return new WaitForSeconds(1.5f);
        ReverseLifePlayerF();
        float rand3 = Random.Range(0, 1.5f);
        yield return new WaitForSeconds(rand3);
        HealPlayerF();
        //InvokeRepeating("HealPlayerF", 0.0f, 1.5f);
        yield return new WaitForSeconds(1.5f - rand3);
        CancelInvoke("AutoAttack");
        MalicePlayerF();
        float rand2 = Random.Range(0, 0.5f);
        yield return new WaitForSeconds(rand2);
        SpikeAttacktDamagePlayer();       
        yield return new WaitForSeconds(1.5f - rand2);
        float rand4 = Random.Range(0.1f, 0.4f);
        yield return new WaitForSeconds(rand4);
        ThurstAttacktDamagePlayer();
        HealPlayerF();
        yield return new WaitForSeconds(1.5f);
        attackTurn = 2;
        attackOn = true;
        InvokeRepeating("AutoAttack", 0.1f, 1.2f);



    }


    public void CombatPatternEnemyF5()
    {
        StartCoroutine(CombatPatternEnemy5());
    }
    IEnumerator CombatPatternEnemy5()
    {
        print("5e");
        ConfusionPlayerF();
        yield return new WaitForSeconds(1.5f);
        CancelInvoke("AutoAttack");
        ReverseLifePlayerF();
        yield return new WaitForSeconds(1.5f);
        MalicePlayerF();
        HealPlayerF();
        float rand2 = Random.Range(0, 0.5f);
        yield return new WaitForSeconds(rand2);
        SpikeAttacktDamagePlayer();
        float rand3 = Random.Range(0, 0.5f);
        yield return new WaitForSeconds(rand3);
        //InvokeRepeating("HealPlayerF", 0.0f, 1.5f);
        yield return new WaitForSeconds(1.5f - rand2 - rand3);
        float rand4 = Random.Range(0.1f, 0.4f);
        yield return new WaitForSeconds(rand4);
        ThurstAttacktDamagePlayer();
        
        HealPlayerF();
        yield return new WaitForSeconds(1f);
        attackTurn = 2;
        attackOn = true;
        InvokeRepeating("AutoAttack", 0.1f, 1.2f);



    }

    public void CombatPatternEnemyF12()
    {
        StartCoroutine(CombatPatternEnemy12());
    }
    IEnumerator CombatPatternEnemy12()
    {
        print("12e");
        CancelInvoke("AutoAttack");
        DebuffPlayerF(55);
        yield return new WaitForSeconds(1.5f);
        MalicePlayerF();
        float rand2 = Random.Range(0, 0.2f);
        yield return new WaitForSeconds(rand2);
        SpikeAttacktDamagePlayer();
        HealPlayerF();
        //InvokeRepeating("HealPlayerF", 0.0f, 1.5f);
        yield return new WaitForSeconds(1.5f - rand2);
        float rand4 = Random.Range(0.1f, 0.4f);
        yield return new WaitForSeconds(rand4);
        ThurstAttacktDamagePlayer();
        
        CancelInvoke("HealPlayerF");
        attackTurn = 2;
        attackOn = true;
        yield return new WaitForSeconds(1f);
        InvokeRepeating("AutoAttack", 0.1f, 1.2f);



    }

    public void CombatPatternEnemyF22()
    {
        StartCoroutine(CombatPatternEnemy22());
    }
    IEnumerator CombatPatternEnemy22()
    {
        print("22e");
        //print("22");
        ConfusionPlayerF();
        yield return new WaitForSeconds(1.5f);
        CancelInvoke("AutoAttack");
        DebuffPlayerF(55);
        yield return new WaitForSeconds(1.5f);
        MalicePlayerF();
        float rand2 = Random.Range(0, 0.5f);
        yield return new WaitForSeconds(rand2);
        SpikeAttacktDamagePlayer();
        float rand3 = Random.Range(0, 0.5f);
        yield return new WaitForSeconds(rand3);
        HealPlayerF();
        //InvokeRepeating("HealPlayerF", 0.0f, 1.5f);
        yield return new WaitForSeconds(1.5f - rand2 - rand3);
        float rand4 = Random.Range(0.1f, 0.4f);
        yield return new WaitForSeconds(rand4);
        ThurstAttacktDamagePlayer();
        
        CancelInvoke("HealPlayerF");
        attackTurn = 2;
        attackOn = true;
        yield return new WaitForSeconds(1f);
        InvokeRepeating("AutoAttack", 0.1f, 1.2f);



    }

    public void CombatPatternEnemyF32()
    {
        StartCoroutine(CombatPatternEnemy32());
    }
    IEnumerator CombatPatternEnemy32()
    {
        print("32e");
        //print("32");
        CancelInvoke("AutoAttack");
        DebuffPlayerF(55);
        yield return new WaitForSeconds(1.5f);
        ReverseLifePlayerF();
        float rand3 = Random.Range(0, 0.1f);
        yield return new WaitForSeconds(rand3);
        HealPlayerF();
        //InvokeRepeating("HealPlayerF", 0.0f, 1.5f);
        yield return new WaitForSeconds(1.5f - rand3);
        MalicePlayerF();
        float rand2 = Random.Range(0, 0.5f);
        yield return new WaitForSeconds(rand2);
        SpikeAttacktDamagePlayer();
        yield return new WaitForSeconds(1.5f - rand2 - rand3);
        float rand4 = Random.Range(0.1f, 0.4f);
        yield return new WaitForSeconds(rand4);
        ThurstAttacktDamagePlayer();
        
        CancelInvoke("HealPlayerF");
        attackTurn = 2;
        attackOn = true;
        yield return new WaitForSeconds(1f);
        InvokeRepeating("AutoAttack", 0.1f, 1.2f);



    }


    public void CombatPatternEnemyF42()
    {
        StartCoroutine(CombatPatternEnemy42());
    }
    IEnumerator CombatPatternEnemy42()
    {
        print("42e");
        //print("42");
        ConfusionPlayerF();
        yield return new WaitForSeconds(1.5f);
        DebuffPlayerF(55);
        yield return new WaitForSeconds(1.5f);
        HealPlayerF();
        ReverseLifePlayerF();
        yield return new WaitForSeconds(1.5f);
        MalicePlayerF();
        float rand3 = Random.Range(1, 1.5f);
        yield return new WaitForSeconds(rand3);
        CancelInvoke("AutoAttack");
        SpikeAttacktDamagePlayer();
        float rand = Random.Range(0.5f, 1f);
        yield return new WaitForSeconds(rand);
        ThurstAttacktDamagePlayer();
        HealPlayerF();
        yield return new WaitForSeconds(1f);
        attackTurn = 2;
        attackOn = true;
        InvokeRepeating("AutoAttack", 0.1f, 1.2f);



    }


    public void CombatPatternEnemyF52()
    {
        StartCoroutine(CombatPatternEnemy52());
    }
    IEnumerator CombatPatternEnemy52()
    {
        print("52e");
        //print("52");
        ConfusionPlayerF();
        yield return new WaitForSeconds(1.5f);
        CancelInvoke("AutoAttack");
        ReverseLifePlayerF();
        yield return new WaitForSeconds(1.5f);
        MalicePlayerF();
        float rand2 = Random.Range(0, 0.5f);
        yield return new WaitForSeconds(rand2);
        SpikeAttacktDamagePlayer();
        float rand3 = Random.Range(0, 0.5f);
        yield return new WaitForSeconds(rand3);
        HealPlayerF();
        yield return new WaitForSeconds(1.5f - rand2);
        ThurstAttacktDamagePlayer();

        CancelInvoke("HealPlayerF");
        attackTurn = 2;
        attackOn = true;
        yield return new WaitForSeconds(1f);
        InvokeRepeating("AutoAttack", 0.1f, 1.2f);



    }


    public void CombatPatternEnemyF13()
    {
        StartCoroutine(CombatPatternEnemy13());
    }
    IEnumerator CombatPatternEnemy13()
    {
        print("13e");
        //print("13");
        CancelInvoke("AutoAttack");
        DebuffPlayerF(55);
        yield return new WaitForSeconds(1.5f);
        MalicePlayerF();
        float rand2 = Random.Range(0.5f, 1f);
        yield return new WaitForSeconds(rand2);
        SpikeAttacktDamagePlayer();
        HealPlayerF();
        //InvokeRepeating("HealPlayerF", 0.0f, 1.5f);
        yield return new WaitForSeconds(1.5f - rand2);
        float rand4 = Random.Range(0.1f, 0.4f);
        yield return new WaitForSeconds(rand4);
        ThurstAttacktDamagePlayer();
        yield return new WaitForSeconds(1f);
        CancelInvoke("HealPlayerF");
        attackTurn = 2;
        attackOn = true;
        InvokeRepeating("AutoAttack", 0.1f, 1.2f);



    }

    public void CombatPatternEnemyF23()
    {
        StartCoroutine(CombatPatternEnemy23());
    }
    IEnumerator CombatPatternEnemy23()
    {
        print("23e");
        //print("23");
        ConfusionPlayerF();
        yield return new WaitForSeconds(1.5f);
        CancelInvoke("AutoAttack");
        DebuffPlayerF(55);
        yield return new WaitForSeconds(1.5f);
        MalicePlayerF();
        float rand2 = Random.Range(0.5f, 1f);
        yield return new WaitForSeconds(rand2);
        SpikeAttacktDamagePlayer();
        HealPlayerF();
        //InvokeRepeating("HealPlayerF", 0.0f, 1.5f);
        yield return new WaitForSeconds(1.5f - rand2);
        float rand4 = Random.Range(0.1f, 0.4f);
        yield return new WaitForSeconds(rand4);
        ThurstAttacktDamagePlayer();
        yield return new WaitForSeconds(1f);
        CancelInvoke("HealPlayerF");
        attackTurn = 2;
        attackOn = true;
        InvokeRepeating("AutoAttack", 0.1f, 1.2f);



    }

    public void CombatPatternEnemyF33()
    {
        StartCoroutine(CombatPatternEnemy33());
    }
    IEnumerator CombatPatternEnemy33()
    {
        print("33e");
        //print("33");
        CancelInvoke("AutoAttack");
        DebuffPlayerF(55);
        yield return new WaitForSeconds(1.5f);
        ReverseLifePlayerF();
        yield return new WaitForSeconds(1.5f);
        MalicePlayerF();
        float rand2 = Random.Range(0.5f, 1f);
        yield return new WaitForSeconds(rand2);
        SpikeAttacktDamagePlayer();
        HealPlayerF();
        //InvokeRepeating("HealPlayerF", 0.0f, 1.5f);
        yield return new WaitForSeconds(1.5f - rand2);
        float rand4 = Random.Range(0.1f, 0.4f);
        yield return new WaitForSeconds(rand4);
        ThurstAttacktDamagePlayer();
        yield return new WaitForSeconds(1f);
        CancelInvoke("HealPlayerF");
        attackTurn = 2;
        attackOn = true;
        InvokeRepeating("AutoAttack", 0.1f, 1.2f);



    }


    public void CombatPatternEnemyF43()
    {
        StartCoroutine(CombatPatternEnemy43());
    }
    IEnumerator CombatPatternEnemy43()
    {
        print("43e");
        //print("43");
        ConfusionPlayerF();
        yield return new WaitForSeconds(1.5f);
        DebuffPlayerF(55);
        yield return new WaitForSeconds(1.5f);
        ReverseLifePlayerF();
        yield return new WaitForSeconds(1.5f);  
        MalicePlayerF();
        CancelInvoke("AutoAttack");
        float rand2 = Random.Range(0.5f, 1f);
        yield return new WaitForSeconds(rand2);
        SpikeAttacktDamagePlayer();
        HealPlayerF();
        yield return new WaitForSeconds(1f);
        float rand4 = Random.Range(0.1f, 0.4f);
        yield return new WaitForSeconds(rand4);
        ThurstAttacktDamagePlayer();
        yield return new WaitForSeconds(1f);
        CancelInvoke("HealPlayerF");
        attackTurn = 2;
        attackOn = true;
        InvokeRepeating("AutoAttack", 0.1f, 1.2f);



    }


    public void CombatPatternEnemyF53()
    {
        StartCoroutine(CombatPatternEnemy53());
    }
    IEnumerator CombatPatternEnemy53()
    {
        print("53e");
        //print("53");
        ConfusionPlayerF();
        yield return new WaitForSeconds(1.5f);
        CancelInvoke("AutoAttack");
        ReverseLifePlayerF();
        yield return new WaitForSeconds(1.5f);
        MalicePlayerF();
        float rand2 = Random.Range(0.5f, 1f);
        yield return new WaitForSeconds(rand2);
        SpikeAttacktDamagePlayer();
        HealPlayerF();
        //InvokeRepeating("HealPlayerF", 0.0f, 1.5f);
        yield return new WaitForSeconds(1.5f - rand2);
        float rand4 = Random.Range(0.1f, 0.4f);
        yield return new WaitForSeconds(rand4);
        ThurstAttacktDamagePlayer();
        yield return new WaitForSeconds(1f);
        CancelInvoke("HealPlayerF");
        attackTurn = 2;
        attackOn = true;
        InvokeRepeating("AutoAttack", 0.1f, 1.2f);



    }



    public void CombatPatternFriendF1()
    {
        StartCoroutine(CombatPatternFriend1());
    }
    IEnumerator CombatPatternFriend1()
    {
        print("1");
        priestText.GetComponent<Text>().text = "Debuff Malice";
        priestBubble.gameObject.SetActive(true);
        DebuffEnemyPlayerF(55);
        yield return new WaitForSeconds(1.5f);
        MaliceEnemyPlayerF();
        float rand2 = Random.Range(0, 0.5f);
        yield return new WaitForSeconds(rand2);
        CancelInvoke("AutoAttack");
        enemyMinorActive = false;
        InvokeRepeating("CureEnemyPlayerF", 0.1f, 2.6f);
        float rand = Random.Range(0, 1f);
        yield return new WaitForSeconds(rand);
        HealEnemyPlayerF();
        CancelInvoke("AutoAttack");
        priestBubble.gameObject.SetActive(false);
        float rand3 = Random.Range(1, 1.5f);
        yield return new WaitForSeconds(rand3);
        CancelInvoke("CureEnemyPlayerF");
        InvokeRepeating("AutoAttack", 0.1f, 1.2f);
        attackTurn = 1;
        attackOn = true;

    }


    public void CombatPatternFriendF2()
    {
        StartCoroutine(CombatPatternFriend2());
    }
    IEnumerator CombatPatternFriend2()
    {
        print("2");
        priestText.GetComponent<Text>().text = "Don't hit Conf, wait Debuff Malice";
        priestBubble.gameObject.SetActive(true);
        ConfusionEnemyPlayerF();
        float rand = Random.Range(0, 1f);
        yield return new WaitForSeconds(rand);
        CancelInvoke("AutoAttack");
        CureEnemyPlayerF();
        yield return new WaitForSeconds(1.5f - rand);
        DebuffEnemyPlayerF(55);
        float rand4 = Random.Range(0, 1f);
        yield return new WaitForSeconds(rand4);
        InvokeRepeating("HealEnemyPlayerF", 0.1f, 1.5f);
        yield return new WaitForSeconds(1.5f - rand4);
        InvokeRepeating("AutoAttack", 0.1f, 1.2f);
        MaliceEnemyPlayerF();
        float rand3 = Random.Range(0.5f, 1f);
        yield return new WaitForSeconds(rand3);
        priestBubble.gameObject.SetActive(false);
        CancelInvoke("AutoAttack");
        CureEnemyPlayerF();
        CancelInvoke("HealEnemyPlayerF");
        yield return new WaitForSeconds(1.5f);
        CancelInvoke("CureEnemyPlayerF");
        attackTurn = 1;
        attackOn = true;
        InvokeRepeating("AutoAttack", 0.1f, 1.2f);


    }

    public void CombatPatternFriendF3()
    {
        StartCoroutine(CombatPatternFriend3());
    }
    IEnumerator CombatPatternFriend3()
    {
        print("3");
        priestText.GetComponent<Text>().text = "Wait Debuff Malice";
        priestBubble.gameObject.SetActive(true);
        ReverseLifeEnemyPlayerF();
        float rand4 = Random.Range(0, 1f);
        yield return new WaitForSeconds(rand4);
        CancelInvoke("AutoAttack");
        enemyMinorActive = false;
        InvokeRepeating("CureEnemyPlayerF", 0.1f, 2.6f);
        yield return new WaitForSeconds(1.5f - rand4);
        DebuffEnemyPlayerF(55);
        yield return new WaitForSeconds(1.5f);
        MaliceEnemyPlayerF();
        float rand = Random.Range(0, 2f);
        yield return new WaitForSeconds(rand);
        HealEnemyPlayerF();      
        priestBubble.gameObject.SetActive(false);
        yield return new WaitForSeconds(1.5f);
        CancelInvoke("CureEnemyPlayerF");
        attackTurn = 1;
        attackOn = true;
        InvokeRepeating("AutoAttack", 0.1f, 1.2f);



    }


    public void CombatPatternFriendF4()
    {
        StartCoroutine(CombatPatternFriend4());
    }
    IEnumerator CombatPatternFriend4()
    {
        print("4");

        priestText.GetComponent<Text>().text = "ReverseLife Malice";
        priestBubble.gameObject.SetActive(true);
        ReverseLifeEnemyPlayerF();
        yield return new WaitForSeconds(1.5f);
        MaliceEnemyPlayerF();
        float rand2 = Random.Range(0, 1f);
        yield return new WaitForSeconds(rand2);
        enemyMinorActive = false;
        CancelInvoke("AutoAttack");
        CureEnemyPlayerF();
        float rand = Random.Range(0, 1f);
        yield return new WaitForSeconds(rand);
        HealEnemyPlayerF();
        yield return new WaitForSeconds(1.5f);
        priestBubble.gameObject.SetActive(false);
        InvokeRepeating("AutoAttack", 0.1f, 1.2f);
        attackTurn = 1;
        attackOn = true;



    }

    public void CombatPatternFriendF5()
    {
        StartCoroutine(CombatPatternFriend5());
    }
    IEnumerator CombatPatternFriend5()
    {
        print("5");
        priestText.GetComponent<Text>().text = "Don't Hit Conf, Wait Reverse Life Malice";
        priestBubble.gameObject.SetActive(true);
        ConfusionEnemyPlayerF();
        float rand4 = Random.Range(0, 1f);
        yield return new WaitForSeconds(rand4);
        CancelInvoke("AutoAttack");
        enemyMinorActive = false;
        InvokeRepeating("CureEnemyPlayerF", 0.1f, 2.6f);
        yield return new WaitForSeconds(1.5f - rand4);
        ReverseLifeEnemyPlayerF();
        yield return new WaitForSeconds(1.5f);
        MaliceEnemyPlayerF();
        float rand = Random.Range(0, 2f);
        yield return new WaitForSeconds(rand);
        HealEnemyPlayerF();
        priestBubble.gameObject.SetActive(false);
        yield return new WaitForSeconds(1.5f);
        CancelInvoke("CureEnemyPlayerF");
        attackTurn = 1;
        attackOn = true;
        InvokeRepeating("AutoAttack", 0.1f, 1.2f);



    }

    public void CombatPatternFriendF6()
    {
        StartCoroutine(CombatPatternFriend6());
    }
    IEnumerator CombatPatternFriend6()
    {
        print("6");
        priestText.GetComponent<Text>().text = "Hit Only Malice";
        priestBubble.gameObject.SetActive(true);
        ReverseLifeEnemyPlayerF();        
        float rand4 = Random.Range(0, 1f);
        yield return new WaitForSeconds(rand4);
        CancelInvoke("AutoAttack");
        enemyMinorActive = false;
        CureEnemyPlayerF();
        yield return new WaitForSeconds(1.5f - rand4);
        ConfusionEnemyPlayerF();
        yield return new WaitForSeconds(1.5f);
        InvokeRepeating("AutoAttack", 0.1f, 1.2f);
        MaliceEnemyPlayerF();
        float rand = Random.Range(2f, 2.5f);
        yield return new WaitForSeconds(rand);
        CancelInvoke("AutoAttack");
        enemyMinorActive = false;
        CureEnemyPlayerF();
        HealEnemyPlayerF();
        priestBubble.gameObject.SetActive(false);
        yield return new WaitForSeconds(1.5f);
        CancelInvoke("CureEnemyPlayerF");
        attackTurn = 1;
        attackOn = true;
        InvokeRepeating("AutoAttack", 0.1f, 1.2f);



    }

    public void CombatPatternFriendF12()
    {
        StartCoroutine(CombatPatternFriend12());
    }
    IEnumerator CombatPatternFriend12()
    {
        print("12");
        priestText.GetComponent<Text>().text = "Debuff Malice";
        priestBubble.gameObject.SetActive(true);
        DebuffEnemyPlayerF(55);
        float rand4 = Random.Range(0, 1f);
        yield return new WaitForSeconds(rand4);
        CancelInvoke("AutoAttack");
        enemyMinorActive = false;
        InvokeRepeating("CureEnemyPlayerF", 0.1f, 2.6f);
        yield return new WaitForSeconds(1.5f - rand4);
        MaliceEnemyPlayerF();
        float rand = Random.Range(0, 2f);
        yield return new WaitForSeconds(rand);
        InvokeRepeating("AutoAttack", 0.1f, 1.2f);
        priestBubble.gameObject.SetActive(false);
        HealEnemyPlayerF();
        yield return new WaitForSeconds(1.5f);
        CancelInvoke("CureEnemyPlayerF");
        attackTurn = 1;
        attackOn = true;

    }


    public void CombatPatternFriendF22()
    {
        StartCoroutine(CombatPatternFriend22());
    }
    IEnumerator CombatPatternFriend22()
    {
        print("22");
        priestText.GetComponent<Text>().text = "Wait Debuff Malice";
        priestBubble.gameObject.SetActive(true);
        ConfusionEnemyPlayerF();
        float rand4 = Random.Range(0, 1f);
        yield return new WaitForSeconds(rand4);
        CancelInvoke("AutoAttack");
        enemyMinorActive = false;
        InvokeRepeating("CureEnemyPlayerF", 0.1f, 2.6f);
        yield return new WaitForSeconds(1.5f - rand4);
        DebuffEnemyPlayerF(55);
        yield return new WaitForSeconds(1.5f);
        MaliceEnemyPlayerF();
        float rand = Random.Range(0, 2f);
        yield return new WaitForSeconds(rand);
        HealEnemyPlayerF();
        priestBubble.gameObject.SetActive(false);
        yield return new WaitForSeconds(1.5f);
        CancelInvoke("CureEnemyPlayerF");
        attackTurn = 1;
        attackOn = true;
        InvokeRepeating("AutoAttack", 0.1f, 1.2f);



    }

    public void CombatPatternFriendF32()
    {
        StartCoroutine(CombatPatternFriend32());
    }
    IEnumerator CombatPatternFriend32()
    {
        print("32");
        priestText.GetComponent<Text>().text = "Wait Debuff Malice";
        priestBubble.gameObject.SetActive(true);
        ReverseLifeEnemyPlayerF();
        yield return new WaitForSeconds(1.5f);
        DebuffEnemyPlayerF(55);
        float rand4 = Random.Range(0, 1f);
        yield return new WaitForSeconds(rand4);
        InvokeRepeating("HealEnemyPlayerF", 0.1f, 1.5f);
        float rand = Random.Range(0.3f, 0.5f);
        yield return new WaitForSeconds(rand);
        CancelInvoke("AutoAttack");
        CureEnemyPlayerF();
        yield return new WaitForSeconds(1.5f - rand4 - rand);
        MaliceEnemyPlayerF();
        float rand3 = Random.Range(0.5f, 1f);
        yield return new WaitForSeconds(rand3);
        priestBubble.gameObject.SetActive(false);
        CancelInvoke("AutoAttack");
        CancelInvoke("HealEnemyPlayerF");
        CureEnemyPlayerF();
        yield return new WaitForSeconds(1f);
        attackTurn = 1;
        attackOn = true;
        InvokeRepeating("AutoAttack", 0.1f, 1.2f);



    }


    public void CombatPatternFriendF42()
    {
        StartCoroutine(CombatPatternFriend42());
    }
    IEnumerator CombatPatternFriend42()
    {
        print("42");
        priestText.GetComponent<Text>().text = "ReverseLife Malice";
        priestBubble.gameObject.SetActive(true);
        ReverseLifeEnemyPlayerF();
        float rand4 = Random.Range(0, 1f);
        yield return new WaitForSeconds(rand4);
        CancelInvoke("AutoAttack");
        enemyMinorActive = false;
        InvokeRepeating("CureEnemyPlayerF", 0.1f, 2.6f);
        yield return new WaitForSeconds(1.5f - rand4);
        MaliceEnemyPlayerF();
        float rand = Random.Range(0, 2f);
        yield return new WaitForSeconds(rand);
        InvokeRepeating("AutoAttack", 0.1f, 1.2f);
        priestBubble.gameObject.SetActive(false);
        HealEnemyPlayerF();
        yield return new WaitForSeconds(1.5f);
        CancelInvoke("CureEnemyPlayerF");
        attackTurn = 1;
        attackOn = true;



    }

    public void CombatPatternFriendF52()
    {
        StartCoroutine(CombatPatternFriend52());
    }
    IEnumerator CombatPatternFriend52()
    {
        print("52");
        priestText.GetComponent<Text>().text = "Don't Hit Conf, Wait Reverse Life Malice";
        priestBubble.gameObject.SetActive(true);
        ConfusionEnemyPlayerF();
        yield return new WaitForSeconds(1.5f);
        ReverseLifeEnemyPlayerF();
        float rand4 = Random.Range(0, 1f);
        yield return new WaitForSeconds(rand4);
        InvokeRepeating("HealEnemyPlayerF", 0.1f, 1.5f);
        float rand = Random.Range(0.3f, 0.5f);
        yield return new WaitForSeconds(rand);
        CancelInvoke("AutoAttack");
        CureEnemyPlayerF();
        yield return new WaitForSeconds(1.5f - rand4 - rand);
        MaliceEnemyPlayerF();
        float rand3 = Random.Range(0.5f, 1f);
        yield return new WaitForSeconds(rand3);
        priestBubble.gameObject.SetActive(false);
        CancelInvoke("AutoAttack");
        CancelInvoke("HealEnemyPlayerF");
        CureEnemyPlayerF();
        yield return new WaitForSeconds(1f);
        attackTurn = 1;
        attackOn = true;
        InvokeRepeating("AutoAttack", 0.1f, 1.2f);



    }


    public void CombatPatternFriendF62()
    {
        StartCoroutine(CombatPatternFriend62());
    }
    IEnumerator CombatPatternFriend62()
    {
        print("62");
        priestText.GetComponent<Text>().text = "Don't Hit fake, Attack to Conf, Burst at Malice";
        priestBubble.gameObject.SetActive(true);
        ReverseLifeEnemyPlayerF();
        float rand4 = Random.Range(0, 1f);
        yield return new WaitForSeconds(rand4);
        CancelInvoke("AutoAttack");
        CureEnemyPlayerF();
        yield return new WaitForSeconds(1.5f - rand4);
        ConfusionEnemyPlayerF();
        yield return new WaitForSeconds(1.5f);
        MaliceEnemyPlayerF();
        float rand3 = Random.Range(0.5f, 1f);
        yield return new WaitForSeconds(rand3);
        enemyMinorActive = false;
        InvokeRepeating("AutoAttack", 0.1f, 1.2f);
        float rand = Random.Range(1, 2f);
        yield return new WaitForSeconds(rand);
        CancelInvoke("AutoAttack");
        CureEnemyPlayerF();
        HealEnemyPlayerF();
        priestBubble.gameObject.SetActive(false);
        yield return new WaitForSeconds(1.5f);
        CancelInvoke("CureEnemyPlayerF");
        attackTurn = 1;
        attackOn = true;
        InvokeRepeating("AutoAttack", 0.1f, 1.2f);



    }

    public void CombatPatternFriendF13()
    {
        StartCoroutine(CombatPatternFriend13());
    }
    IEnumerator CombatPatternFriend13()
    {
        print("13");
        priestText.GetComponent<Text>().text = "Debuff Malice";
        priestBubble.gameObject.SetActive(true);
        DebuffEnemyPlayerF(55);
        float rand2 = Random.Range(0.5f, 1.5f);
        yield return new WaitForSeconds(rand2);
        CancelInvoke("AutoAttack");
        enemyMinorActive = false;
        InvokeRepeating("CureEnemyPlayerF", 0.1f, 2.6f);
        HealEnemyPlayerF();
        yield return new WaitForSeconds(1.5f - rand2);
        MaliceEnemyPlayerF();
        yield return new WaitForSeconds(rand2);
        priestBubble.gameObject.SetActive(false);
        CancelInvoke("CureEnemyPlayerF");
        InvokeRepeating("AutoAttack", 0.1f, 1.2f);
        attackTurn = 1;
        attackOn = true;

    }


    public void CombatPatternFriendF23()
    {
        StartCoroutine(CombatPatternFriend23());
    }
    IEnumerator CombatPatternFriend23()
    {
        print("23");
        priestText.GetComponent<Text>().text = "Don't hit Conf, wait Debuff Malice";
        priestBubble.gameObject.SetActive(true);
        ConfusionEnemyPlayerF();
        float rand = Random.Range(0, 1f);
        yield return new WaitForSeconds(rand);
        CancelInvoke("AutoAttack");
        CureEnemyPlayerF();
        yield return new WaitForSeconds(1.5f - rand);
        DebuffEnemyPlayerF(55);
        float rand4 = Random.Range(0, 1f);
        yield return new WaitForSeconds(rand4);
        InvokeRepeating("HealEnemyPlayerF", 0.1f, 1.5f);
        yield return new WaitForSeconds(1.5f - rand4);
        InvokeRepeating("AutoAttack", 0.1f, 1.2f);
        MaliceEnemyPlayerF();
        float rand3 = Random.Range(0.5f, 1f);
        yield return new WaitForSeconds(rand3);
        priestBubble.gameObject.SetActive(false);
        CancelInvoke("AutoAttack");
        CureEnemyPlayerF();
        CancelInvoke("HealEnemyPlayerF");
        CancelInvoke("CureEnemyPlayerF");
        attackTurn = 1;
        attackOn = true;
        InvokeRepeating("AutoAttack", 0.1f, 1.2f);


    }

    public void CombatPatternFriendF33()
    {
        StartCoroutine(CombatPatternFriend33());
    }
    IEnumerator CombatPatternFriend33()
    {
        print("33");
        priestText.GetComponent<Text>().text = "Wait Debuff Malice";
        priestBubble.gameObject.SetActive(true);
        ReverseLifeEnemyPlayerF();
        yield return new WaitForSeconds(1.5f);
        DebuffEnemyPlayerF(55);
        float rand2 = Random.Range(0.5f, 1f);
        yield return new WaitForSeconds(rand2);
        CancelInvoke("AutoAttack");
        enemyMinorActive = false;
        InvokeRepeating("CureEnemyPlayerF", 0.1f, 2.6f);
        HealEnemyPlayerF();
        yield return new WaitForSeconds(1.5f - rand2);
        MaliceEnemyPlayerF();
        yield return new WaitForSeconds(rand2);
        priestBubble.gameObject.SetActive(false);
        CancelInvoke("CureEnemyPlayerF");
        InvokeRepeating("AutoAttack", 0.1f, 1.2f);
        attackTurn = 1;
        attackOn = true;



    }


    public void CombatPatternFriendF43()
    {
        StartCoroutine(CombatPatternFriend43());
    }
    IEnumerator CombatPatternFriend43()
    {
        print("43");
        priestText.GetComponent<Text>().text = "ReverseLife Malice";
        priestBubble.gameObject.SetActive(true);
        ReverseLifeEnemyPlayerF();
        float rand2 = Random.Range(1f, 1.5f);
        yield return new WaitForSeconds(rand2);
        CancelInvoke("AutoAttack");
        enemyMinorActive = false;
        InvokeRepeating("CureEnemyPlayerF", 0.1f, 2.6f);
        HealEnemyPlayerF();
        yield return new WaitForSeconds(1.5f - rand2);
        MaliceEnemyPlayerF();
        yield return new WaitForSeconds(rand2);
        priestBubble.gameObject.SetActive(false);
        CancelInvoke("CureEnemyPlayerF");
        InvokeRepeating("AutoAttack", 0.1f, 1.2f);
        attackTurn = 1;
        attackOn = true;



    }

    public void CombatPatternFriendF53()
    {
        StartCoroutine(CombatPatternFriend53());
    }
    IEnumerator CombatPatternFriend53()
    {
        print("53");
        priestText.GetComponent<Text>().text = "Don't Hit Conf, Wait Reverse Life Malice";
        priestBubble.gameObject.SetActive(true);
        ConfusionEnemyPlayerF();
        yield return new WaitForSeconds(1.5f);
        ReverseLifeEnemyPlayerF();
        float rand2 = Random.Range(0.5f, 1f);
        yield return new WaitForSeconds(rand2);
        CancelInvoke("AutoAttack");
        enemyMinorActive = false;
        InvokeRepeating("CureEnemyPlayerF", 0.1f, 2.6f);
        HealEnemyPlayerF();
        yield return new WaitForSeconds(1.5f - rand2);
        MaliceEnemyPlayerF();
        yield return new WaitForSeconds(rand2);
        priestBubble.gameObject.SetActive(false);
        CancelInvoke("CureEnemyPlayerF");
        InvokeRepeating("AutoAttack", 0.1f, 1.2f);
        attackTurn = 1;
        attackOn = true;



    }
    public void CombatPatternFriendF63()
    {
        StartCoroutine(CombatPatternFriend63());
    }
    IEnumerator CombatPatternFriend63()
    {
        print("63");
        priestText.GetComponent<Text>().text = "Hit Only Malice";
        priestBubble.gameObject.SetActive(true);
        ReverseLifeEnemyPlayerF();
        float rand4 = Random.Range(0, 1f);
        yield return new WaitForSeconds(rand4);
        CancelInvoke("AutoAttack");
        enemyMinorActive = false;
        CureEnemyPlayerF();
        yield return new WaitForSeconds(1.5f - rand4);
        ConfusionEnemyPlayerF();
        yield return new WaitForSeconds(1.5f);
        InvokeRepeating("AutoAttack", 0.1f, 1.2f);
        MaliceEnemyPlayerF();
        float rand = Random.Range(1, 2f);
        yield return new WaitForSeconds(rand);
        CancelInvoke("AutoAttack");
        enemyMinorActive = false;
        CureEnemyPlayerF();
        HealEnemyPlayerF();
        priestBubble.gameObject.SetActive(false);
        yield return new WaitForSeconds(1.5f);
        CancelInvoke("CureEnemyPlayerF");
        attackTurn = 1;
        attackOn = true;
        InvokeRepeating("AutoAttack", 0.1f, 1.2f);



    }


    public void CombatPatternFriendF14()
    {
        StartCoroutine(CombatPatternFriend14());
    }
    IEnumerator CombatPatternFriend14()
    {
        print("14");
        priestText.GetComponent<Text>().text = "Debuff Malice";
        priestBubble.gameObject.SetActive(true);
        DebuffEnemyPlayerF(55);
        float rand2 = Random.Range(0.2f, 1f);
        yield return new WaitForSeconds(rand2);
        CancelInvoke("AutoAttack");
        enemyMinorActive = false;
        CureEnemyPlayerF();
        HealEnemyPlayerF();
        yield return new WaitForSeconds(1.5f - rand2);
        MaliceEnemyPlayerF();
        float rand3 = Random.Range(0.5f, 1.3f);
        yield return new WaitForSeconds(rand3);
        priestBubble.gameObject.SetActive(false);
        CureEnemyPlayerF();
        InvokeRepeating("AutoAttack", 0.1f, 1.2f);
        attackTurn = 1;
        attackOn = true;

    }


    public void CombatPatternFriendF24()
    {
        StartCoroutine(CombatPatternFriend24());
    }
    IEnumerator CombatPatternFriend24()
    {
        print("24");
        priestText.GetComponent<Text>().text = "Don't hit Conf, wait Debuff Malice";
        priestBubble.gameObject.SetActive(true);
        ConfusionEnemyPlayerF();
        yield return new WaitForSeconds(1.5f);
        DebuffEnemyPlayerF(55);
        float rand2 = Random.Range(0.2f, 1f);
        yield return new WaitForSeconds(rand2);
        CancelInvoke("AutoAttack");
        enemyMinorActive = false;
        CureEnemyPlayerF();
        HealEnemyPlayerF();
        yield return new WaitForSeconds(1.5f - rand2);
        MaliceEnemyPlayerF();
        float rand3 = Random.Range(0.5f, 1.3f);
        yield return new WaitForSeconds(rand3);
        priestBubble.gameObject.SetActive(false);
        CureEnemyPlayerF();
        InvokeRepeating("AutoAttack", 0.1f, 1.2f);
        attackTurn = 1;
        attackOn = true;


    }
    public void CombatPatternFriendF34()
    {
        StartCoroutine(CombatPatternFriend34());
    }
    IEnumerator CombatPatternFriend34()
    {
        print("34");
        priestText.GetComponent<Text>().text = "Wait Debuff Malice";
        priestBubble.gameObject.SetActive(true);
        ReverseLifeEnemyPlayerF();
        yield return new WaitForSeconds(1.5f);
        DebuffEnemyPlayerF(55);
        float rand2 = Random.Range(0.2f, 1f);
        yield return new WaitForSeconds(rand2);
        CancelInvoke("AutoAttack");
        enemyMinorActive = false;
        CureEnemyPlayerF();
        HealEnemyPlayerF();
        yield return new WaitForSeconds(1.5f - rand2);
        MaliceEnemyPlayerF();
        float rand3 = Random.Range(0.5f, 1.3f);
        yield return new WaitForSeconds(rand3);
        priestBubble.gameObject.SetActive(false);
        CureEnemyPlayerF();
        InvokeRepeating("AutoAttack", 0.1f, 1.2f);
        attackTurn = 1;
        attackOn = true;



    }


    public void CombatPatternFriendF44()
    {
        StartCoroutine(CombatPatternFriend44());
    }
    IEnumerator CombatPatternFriend44()
    {
        print("44");
        priestText.GetComponent<Text>().text = "ReverseLife Malice";
        priestBubble.gameObject.SetActive(true);
        ReverseLifeEnemyPlayerF();
        float rand2 = Random.Range(0.2f, 1f);
        yield return new WaitForSeconds(rand2);
        CancelInvoke("AutoAttack");
        enemyMinorActive = false;
        CureEnemyPlayerF();
        HealEnemyPlayerF();
        yield return new WaitForSeconds(1.5f - rand2);
        MaliceEnemyPlayerF();
        float rand3 = Random.Range(0.5f, 1.3f);
        yield return new WaitForSeconds(rand3);
        priestBubble.gameObject.SetActive(false);
        CureEnemyPlayerF();
        InvokeRepeating("AutoAttack", 0.1f, 1.2f);
        attackTurn = 1;
        attackOn = true;



    }

    public void CombatPatternFriendF54()
    {
        StartCoroutine(CombatPatternFriend54());
    }
    IEnumerator CombatPatternFriend54()
    {
        print("54");
        priestText.GetComponent<Text>().text = "Don't Hit Conf, Wait Reverse Life Malice";
        priestBubble.gameObject.SetActive(true);
        ConfusionEnemyPlayerF();
        yield return new WaitForSeconds(1.5f);
        ReverseLifeEnemyPlayerF();
        float rand2 = Random.Range(0.2f, 1f);
        yield return new WaitForSeconds(rand2);
        CancelInvoke("AutoAttack");
        enemyMinorActive = false;
        CureEnemyPlayerF();
        HealEnemyPlayerF();
        yield return new WaitForSeconds(1.5f - rand2);
        MaliceEnemyPlayerF();
        float rand3 = Random.Range(0.5f, 1.3f);
        yield return new WaitForSeconds(rand3);
        priestBubble.gameObject.SetActive(false);
        CureEnemyPlayerF();
        InvokeRepeating("AutoAttack", 0.1f, 1.2f);
        attackTurn = 1;
        attackOn = true;



    }

    //Light Attack Damage Player

    public void LightAttacktDamagePlayer()
    {

        playerHealth.playerHealth -= lightAttackDamagePlayer - (playerHealth.playerDefence)/2;
        SetHealth();
        enemyAnimator.SetTrigger("LightAttack");


    }

    //Thurst Attack Damage Player

    public void ThurstAttacktDamagePlayer()
    {

        playerHealth.playerHealth -= thurstDamagePlayer - (playerHealth.playerDefence)*2;
        SetHealth();
        thurstAttackCoolDownTimer = thurstAttackCoolDown;
        enemyAnimator.SetTrigger("Thurst");


    }

    //Spike Attack Damage Player

    public void SpikeAttacktDamagePlayer()
    {

        playerHealth.playerHealth -= spikeDamagePlayer - (playerHealth.playerDefence) * 1.5f;
        SetHealth();
        spikeAttackCoolDownTimer = spikeAttackCoolDown;
        enemyAnimator.SetTrigger("Spike");


    }



    //Heal Player

   
    public void HealPlayerF()
    {
        StartCoroutine(HealPlayer());
    }
    IEnumerator HealPlayer()
    {
        priestAnimator.SetTrigger("Heal");
        yield return new WaitForSeconds(1.5f);
        playerHealth.playerHealth += 100;
        playerHealth.HealEffect.SetActive(true);
        yield return new WaitForSeconds(3.5f);
        playerHealth.HealEffect.SetActive(false);

    }


    //Heal Enemy Player


    public void HealEnemyPlayerF()
    {
        StartCoroutine(HealEnemyPlayer());
    }
    IEnumerator HealEnemyPlayer()
    {
        orcPriestAnimator.SetTrigger("Heal");
        yield return new WaitForSeconds(1.5f);
        enemyHealth.enemyHealth += 100;
        enemyHealth.HealEffect.SetActive(true);
        yield return new WaitForSeconds(3.5f);
        enemyHealth.HealEffect.SetActive(false);


    }







    //Debuff Player

    public void DebuffPlayerF(float playerDebuffHealth)
    {
        StartCoroutine(DebuffPlayer(playerDebuffHealth));
    }
    IEnumerator DebuffPlayer(float playerDebuffHealth)
    {
        orcPriestAnimator.SetTrigger("Debuff");
        yield return new WaitForSeconds(1.5f);
        playerHealth.debuffActive = true;
        playerHealth.playerHealthbarBoard.GetComponent<RectTransform>().localScale = new Vector3(playerDebuffHealth/100, 1f, 1f);
        playerHealth.playerHealthbarImage.GetComponent<Image>().color = Color.magenta;
        playerHealth.DebuffEffect.SetActive(true);
        playerHealth.DebuffImage.SetActive(true);
        


        if (playerHealth.playerHealth == playerHealth.playerHealthMax)
        {
            playerHealth.playerHealthMax = playerDebuffHealth;
            playerHealth.playerHealth = playerDebuffHealth;

        }

        if (playerHealth.playerHealth < playerHealth.playerHealthMax && playerDebuffHealth < playerHealth.playerHealth)
        {
            playerHealth.playerHealthMax = playerDebuffHealth;
            playerHealth.playerHealth = playerDebuffHealth;
        }
        else if (playerHealth.playerHealth < playerDebuffHealth)
        {
            playerHealth.playerHealthMax = playerDebuffHealth;
        }


        yield return new WaitForSeconds(3.5f);
        playerHealth.DebuffEffect.SetActive(false);

    }

    //Malice Player


    public void MalicePlayerF()
    {
        StartCoroutine(MalicePlayer());
    }
    IEnumerator MalicePlayer()
    {
        orcPriestAnimator.SetTrigger("Debuff");
        yield return new WaitForSeconds(1.5f);
        playerHealth.maliceActive = true;
        playerHealth.MaliceEffect.SetActive(true);
        playerHealth.MaliceImage.SetActive(true);
        playerHealth.playerDefence = 0f;
        playerHealth.playerHealthbarImage.GetComponent<Image>().color = Color.magenta;
        yield return new WaitForSeconds(3.5f);
        playerHealth.MaliceEffect.SetActive(false);

    }







    //Debuff Enemy Player

    public void DebuffEnemyPlayerF(float enemyPlayerDebuffHealth)
    {
        StartCoroutine(DebuffEnemyPlayer(enemyPlayerDebuffHealth));
    }
    IEnumerator DebuffEnemyPlayer(float enemyPlayerDebuffHealth)
    {
        priestAnimator.SetTrigger("Debuff");
        yield return new WaitForSeconds(1.5f);
        enemyHealth.debuffActive = true;
        enemyHealth.enemyPlayerHealthbarBoard.GetComponent<RectTransform>().localScale = new Vector3(enemyPlayerDebuffHealth / 100, 1f, 1f);
        enemyHealth.enemyPlayerHealthbarImage.GetComponent<Image>().color = Color.magenta;
        enemyHealth.DebuffEffect.SetActive(true);
        //enemyHealth.DebuffImage.SetActive(true);



        if (enemyHealth.enemyHealth == enemyHealth.enemyHealthMax)
        {
            enemyHealth.enemyHealthMax = enemyPlayerDebuffHealth;
            enemyHealth.enemyHealth = enemyPlayerDebuffHealth;

        }

        if (enemyHealth.enemyHealth < enemyHealth.enemyHealthMax && enemyPlayerDebuffHealth < enemyHealth.enemyHealth)
        {
            enemyHealth.enemyHealthMax = enemyPlayerDebuffHealth;
            enemyHealth.enemyHealth = enemyPlayerDebuffHealth;
        }
        else if (enemyHealth.enemyHealth < enemyPlayerDebuffHealth)
        {
            enemyHealth.enemyHealthMax = enemyPlayerDebuffHealth;
        }


        yield return new WaitForSeconds(3.5f);
        enemyHealth.DebuffEffect.SetActive(false);

    }



    //Malice Enemy Player


    public void MaliceEnemyPlayerF()
    {
        StartCoroutine(MaliceEnemyPlayer());
    }
    IEnumerator MaliceEnemyPlayer()
    {
        priestAnimator.SetTrigger("Debuff");
        yield return new WaitForSeconds(1.5f);
        enemyHealth.maliceActive = true;
        enemyHealth.enemyPlayerHealthbarImage.GetComponent<Image>().color = Color.magenta;
        enemyHealth.MaliceEffect.SetActive(true);
        //enemyHealth.MaliceImage.SetActive(true);
        enemyHealth.enemyDefence = 0f;
        yield return new WaitForSeconds(3.5f);
        enemyHealth.MaliceEffect.SetActive(false);

    }


    //Cure Enemy Player
    public void CureEnemyPlayerF()
    {

        StartCoroutine(CureEnemyPlayer());

    }

    IEnumerator CureEnemyPlayer()
    {
            CancelInvoke("AutoAttack");
            enemyMinorActive = false;
            enemyAnimator.SetTrigger("Cure");
            yield return new WaitForSeconds(1.5f);
        //enemyHealth.enemyHealthMax = 100f;
            enemyHealth.debuffActive = false;
            enemyHealth.maliceActive = false;
            enemyHealth.reverseLifeActive = false;
            enemyHealth.confusionActive = false;
            enemyHealth.MaliceEffect.SetActive(false);
            enemyHealth.DebuffEffect.SetActive(false);
            enemyHealth.ReverseLifeEffect.SetActive(false);
            enemyHealth.ConfusionEffect.SetActive(false);
            enemyHealth.enemyDefence = 10f;
            enemyHealth.enemyHealthMax = 100;
            enemyHealth.enemyPlayerHealthbarBoard.GetComponent<RectTransform>().localScale = new Vector3(1f, 1f, 1f);
            enemyHealth.enemyPlayerHealthbarImage.GetComponent<Image>().color = Color.red;
            enemyMinorActive = true;


    }


    // 3sn Enemy Player

    public void ReverseLifeEnemyPlayerF()
    {
        StartCoroutine(ReverseLifeEnemyPlayer2());
    }

    IEnumerator ReverseLifeEnemyPlayer2()
    {
        StartCoroutine(ReverseLifeEnemyPlayer());
        yield return new WaitForSeconds(0.01f);
    }

    IEnumerator ReverseLifeEnemyPlayer()
    {

        priestAnimator.SetTrigger("Debuff");
        yield return new WaitForSeconds(1.5f);
        if (enemyHealth.debuffActive == true)
        {
            
            enemyHealth.ReverseLifeEffect.SetActive(true);
            //enemyHealth.ReverseLifeImage.SetActive(true);
            yield return new WaitForSeconds(1.5f);
            enemyHealth.ReverseLifeEffect.SetActive(false);
            yield return new WaitForSeconds(1.5f);

        }
        else 
        {
        enemyHealth.enemyPlayerHealthbarBoard.GetComponent<RectTransform>().localScale = new Vector3(0.63f, 1f, 1f);
        enemyHealth.enemyPlayerHealthbarImage.GetComponent<Image>().color = Color.magenta;
        enemyHealth.ReverseLifeEffect.SetActive(true);
            //enemyHealth.ReverseLifeImage.SetActive(true);
            enemyHealth.reverseLifeActive = true;
        

        if (enemyHealth.enemyHealth == enemyHealth.enemyHealthMax)
        {
            enemyHealth.enemyHealthMax = 63;
            enemyHealth.enemyHealth = 63;

        }

        if (enemyHealth.enemyHealth < enemyHealth.enemyHealthMax && 63 < enemyHealth.enemyHealth)
        {
            enemyHealth.enemyHealthMax = 63;
            enemyHealth.enemyHealth = 63;
        }
        else if (enemyHealth.enemyHealth < 63)
        {
            enemyHealth.enemyHealthMax = 63;
        }
        
        SetHealth();
            yield return new WaitForSeconds(1.5f);
            enemyHealth.ReverseLifeEffect.SetActive(false);
            yield return new WaitForSeconds(1.5f);
            //enemyHealth.ReverseLifeImage.SetActive(false);
            enemyHealth.reverseLifeActive = false;
            if (enemyHealth.debuffActive == false)
            {
                enemyHealth.enemyPlayerHealthbarBoard.GetComponent<RectTransform>().localScale = new Vector3(1f, 1f, 1f);
            }
            if (enemyHealth.debuffActive == false && enemyHealth.maliceActive == false && enemyHealth.confusionActive == false)
            {
                enemyHealth.enemyPlayerHealthbarImage.GetComponent<Image>().color = Color.red;
            }
            
        }
        
        

    }


    // 3sn Player

    public void ReverseLifePlayerF()
    {
        StartCoroutine(ReverseLifePlayer2());
    }

    IEnumerator ReverseLifePlayer2()
    {
        StartCoroutine(ReverseLifePlayer());
        yield return new WaitForSeconds(0.01f);
        
    }

    IEnumerator ReverseLifePlayer()
    {
        orcPriestAnimator.SetTrigger("Debuff");
        
        yield return new WaitForSeconds(1.5f);

        if (playerHealth.debuffActive == true)
        {            
            playerHealth.ReverseLifeEffect.SetActive(true);
            playerHealth.ReverseLifeImage.SetActive(true);
            yield return new WaitForSeconds(1.5f);
            playerHealth.ReverseLifeEffect.SetActive(false);
            yield return new WaitForSeconds(1.5f);
            playerHealth.ReverseLifeImage.SetActive(false);
        }
        else
        {
            playerHealth.playerHealthbarBoard.GetComponent<RectTransform>().localScale = new Vector3(0.63f, 1f, 1f);
            playerHealth.playerHealthbarImage.GetComponent<Image>().color = Color.magenta;
            playerHealth.ReverseLifeEffect.SetActive(true);
            playerHealth.ReverseLifeImage.SetActive(true);
            playerHealth.reverseLifeActive = true;


            if (playerHealth.playerHealth == playerHealth.playerHealthMax)
            {
                playerHealth.playerHealthMax = 63;
                playerHealth.playerHealth = 63;

            }

            if (playerHealth.playerHealth < playerHealth.playerHealthMax && 63 < playerHealth.playerHealth)
            {
                playerHealth.playerHealthMax = 63;
                playerHealth.playerHealth = 63;
            }
            else if (playerHealth.playerHealth < 63)
            {
                playerHealth.playerHealthMax = 63;
            }

            SetHealth();
            yield return new WaitForSeconds(1.5f);
            playerHealth.ReverseLifeEffect.SetActive(false);
            yield return new WaitForSeconds(1.5f);
            playerHealth.ReverseLifeImage.SetActive(false);
            playerHealth.reverseLifeActive = false;
            if (playerHealth.debuffActive == false)
            {
                playerHealth.playerHealthbarBoard.GetComponent<RectTransform>().localScale = new Vector3(1f, 1f, 1f);
            }
            if (playerHealth.debuffActive == false && playerHealth.maliceActive == false && playerHealth.confusionActive == false)
            {
                playerHealth.playerHealthbarImage.GetComponent<Image>().color = Color.red;
            }
        }



    }


    //Confusion Enemy Player


    public void ConfusionEnemyPlayerF()
    {
        StartCoroutine(ConfusionEnemyPlayer());
    }
    IEnumerator ConfusionEnemyPlayer()
    {
        priestAnimator.SetTrigger("Debuff");
        yield return new WaitForSeconds(1.5f);
        if (enemyHealth.debuffActive == true)
        {
            enemyHealth.enemyPlayerHealthbarImage.GetComponent<Image>().color = Color.magenta;
            enemyHealth.ConfusionEffect.SetActive(true);
            enemyHealth.confusionActive = true;
            yield return new WaitForSeconds(1.5f);
            enemyHealth.ConfusionEffect.SetActive(false);

        }
        else
        {


            if (enemyHealth.enemyHealth == enemyHealth.enemyHealthMax)
            {
                enemyHealth.enemyHealthMax = 85;
                enemyHealth.enemyHealth = 85;

            }

            if (enemyHealth.enemyHealth < enemyHealth.enemyHealthMax && 85 < enemyHealth.enemyHealth)
            {
                enemyHealth.enemyHealthMax = 85;
                enemyHealth.enemyHealth = 85;
            }
            else if (enemyHealth.enemyHealth < 85)
            {
                enemyHealth.enemyHealthMax = 85;
            }
            enemyHealth.enemyPlayerHealthbarBoard.GetComponent<RectTransform>().localScale = new Vector3(85 / 100, 1f, 1f);
            enemyHealth.enemyPlayerHealthbarImage.GetComponent<Image>().color = Color.magenta;
            enemyHealth.ConfusionEffect.SetActive(true);
            enemyHealth.confusionActive = true;
            yield return new WaitForSeconds(1.5f);
            enemyHealth.ConfusionEffect.SetActive(false);
        }
    }

    //Confusion Player


    public void ConfusionPlayerF()
    {
        StartCoroutine(ConfusionPlayer());
    }
    IEnumerator ConfusionPlayer()
    {
        orcPriestAnimator.SetTrigger("Debuff");
        yield return new WaitForSeconds(1.5f);
        if (playerHealth.debuffActive == true)
        {
            playerHealth.playerHealthbarImage.GetComponent<Image>().color = Color.magenta;
            playerHealth.ConfusionEffect.SetActive(true);
            playerHealth.ConfusionImage.SetActive(true);
            playerHealth.confusionActive = true;
            yield return new WaitForSeconds(1.5f);
            playerHealth.ConfusionEffect.SetActive(false);

        }
        else
        {


            if (playerHealth.playerHealth == playerHealth.playerHealthMax)
            {
                playerHealth.playerHealthMax = 85;
                playerHealth.playerHealth = 85;

            }

            if (playerHealth.playerHealth < playerHealth.playerHealthMax && 85 < playerHealth.playerHealth)
            {
                playerHealth.playerHealthMax = 85;
                playerHealth.playerHealth = 85;
            }
            else if (playerHealth.playerHealth < 85)
            {
                playerHealth.playerHealthMax = 85;
            }
            playerHealth.playerHealthbarBoard.GetComponent<RectTransform>().localScale = new Vector3(85 / 100, 1f, 1f);
            playerHealth.playerHealthbarImage.GetComponent<Image>().color = Color.magenta;
            playerHealth.ConfusionEffect.SetActive(true);
            playerHealth.ConfusionImage.SetActive(true);
            playerHealth.confusionActive = true;
            yield return new WaitForSeconds(1.5f);
            playerHealth.ConfusionEffect.SetActive(false);
        }

    }

    //Minor Enemy Player

    public void MinorEnemyPlayerF()
    {
        StartCoroutine(MinorEnemyPlayer());
    }
    IEnumerator MinorEnemyPlayer()
    {
        float rand = Random.Range(1, 3.8f);
        yield return new WaitForSeconds(rand);
        if (enemyMinorActive == true && enemyHealth.enemyMana > 0.13f)
        {
            enemyHealth.enemyHealth += 7;
            enemyHealth.enemyMana -= 0.14f;
            if (enemyHealth.enemyHealth > 100)
            { 
                enemyHealth.enemyHealth = 100;
            }
        }
    
    }

    //Mana Enemy Player

    public void ManaEnemyPlayer()
    {

        if (enemyPlayerManaCoolDownTimer < 0)
        {
            enemyPlayerManaCoolDownTimer = 0;
            enemyHealth.enemyMana += 0.5f;
            enemyPlayerManaCoolDownTimer = enemyPlayerManaCoolDown;

        }
        if (enemyPlayerManaCoolDownTimer == 0)
        {
            enemyHealth.enemyMana += 0.5f;
            enemyPlayerManaCoolDownTimer = enemyPlayerManaCoolDown;


        }
    }

    //Revive Game

    public void Revive()
    {
        
        if (playerHealth.playerHealth == 0)
        {
            orcScore++;

        }
        if (enemyHealth.enemyHealth == 0)
        {
            humanScore++;
        }

        playerHealth.playerHealth = 100;
        playerHealth.playerHealthMax = 100;
        playerHealth.playerMana = 1f;
        enemyHealth.enemyHealth = 100;
        enemyHealth.enemyHealthMax = 100;
        enemyHealth.enemyMana = 1f;
        StartCoroutine(Rez());
        

    }

    IEnumerator Rez()
    {
        if (playerDeath == true)
        {
            priestAnimator.SetTrigger("Rez");
        }
        else if (enemyDeath == true)
        {
            orcPriestAnimator.SetTrigger("Rez");
        }
        yield return new WaitForSeconds(1.5f);

        InvokeRepeating("AutoAttack", 0.1f, 1.2f);

        playerDeath = false;
        enemyDeath = false;
        randDead = Random.Range(0, 7);
        rezButton.gameObject.SetActive(false);
        enemyBubble.gameObject.SetActive(false);
        attackOn = true;
        playerHealth.playerAnimator.SetInteger("Dead", 0);
        playerHealth.playerAnimator.SetInteger("StillDead", 0);
        playerHealth.playerAnimator.SetInteger("Win", 0);
        enemyAnimator.SetInteger("Dead", 0);
        enemyAnimator.SetInteger("StillDead", 0);
        enemyAnimator.SetInteger("Win", 0);
        playerHealth.debuffActive = false;
        playerHealth.maliceActive = false;
        playerHealth.reverseLifeActive = false;
        playerHealth.confusionActive = false;
        enemyHealth.debuffActive = false;
        enemyHealth.maliceActive = false;
        enemyHealth.reverseLifeActive = false;
        enemyHealth.confusionActive = false;


        playerHealth.DebuffImage.SetActive(false);
        playerHealth.MaliceImage.SetActive(false);
        playerHealth.ConfusionImage.SetActive(false);
        playerHealth.ReverseLifeImage.SetActive(false);
        playerHealth.DebuffEffect.SetActive(false);
        playerHealth.MaliceEffect.SetActive(false);
        playerHealth.ConfusionEffect.SetActive(false);
        playerHealth.ReverseLifeEffect.SetActive(false);
        playerHealth.HealEffect.SetActive(false);
        enemyHealth.DebuffEffect.SetActive(false);
        enemyHealth.MaliceEffect.SetActive(false);
        enemyHealth.ConfusionEffect.SetActive(false);
        enemyHealth.ReverseLifeEffect.SetActive(false);
        enemyHealth.HealEffect.SetActive(false);
        playerHealth.playerDefence = 10f;
        enemyHealth.enemyDefence = 10f;

        enemyHealth.enemyPlayerHealthbarBoard.GetComponent<RectTransform>().localScale = new Vector3(1f, 1f, 1f);
        playerHealth.playerHealthbarBoard.GetComponent<RectTransform>().localScale = new Vector3(1f, 1f, 1f);
        enemyHealth.enemyPlayerHealthbarImage.GetComponent<Image>().color = Color.red;
        playerHealth.playerHealthbarImage.GetComponent<Image>().color = Color.red;

        GameObject.Find("LightAttackButton").GetComponent<Button>().interactable = true;
        GameObject.Find("SpikeButton").GetComponent<Button>().interactable = true;
        GameObject.Find("ThurstButton").GetComponent<Button>().interactable = true;
        GameObject.Find("CureButton").GetComponent<Button>().interactable = true;
        GameObject.Find("ManaButton").GetComponent<Button>().interactable = true;
        GameObject.Find("MinorButton").GetComponent<Button>().interactable = true;

        playerHealth.playerHealth = 100;
        playerHealth.playerHealthMax = 100;
        playerHealth.playerMana = 1f;
        enemyHealth.enemyHealth = 100;
        enemyHealth.enemyHealthMax = 100;
        enemyHealth.enemyMana = 1f;
        attackTurn = Random.Range(1, 3);

    }
}
