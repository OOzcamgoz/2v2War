using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class PlayerHealth : MonoBehaviour
{

    public float playerHealthMax = 100f;
    public float playerNormalHealthMax = 100f;
    public float playerHealth;
    public float playerMana;
    public float playerDefence = 10f;
    private float playerSkillCoolDownTimer = 1.5f;
    public float playerCureCoolDown = 1f;
    public float playerCureCoolDownTimer;
    public float playerManaCoolDown = 2.5f;
    public float playerManaCoolDownTimer;
    public float playerMinorCoolDown = 0f;
    public float playerMinorCoolDownTimer;
    public float playerLightAttackCoolDown = 1.5f;
    public float playerLightAttackCoolDownTimer;
    public float playerSpikeAttackCoolDown = 11f;
    public float playerSpikeAttackCoolDownTimer;
    public float playerThurstAttackCoolDown = 11f;
    public float playerThurstAttackCoolDownTimer;
    public float playerDBAttackCoolDown = 11f;
    public float playerDBAttackCoolDownTimer;

    public GameObject DebuffEffect;
    public GameObject DebuffImage;
    public GameObject MaliceEffect;
    public GameObject MaliceImage;
    public GameObject ReverseLifeEffect;
    public GameObject ReverseLifeImage;
    public GameObject ConfusionEffect;
    public GameObject ConfusionImage;
    public GameObject HealEffect;

    public TMP_Text cureTextCooldown;
    public Image cureImageCooldown;
    public TMP_Text minorTextCooldown;
    public Image minorImageCooldown;
    public TMP_Text manaTextCooldown;
    public Image manaImageCooldown;

    public GameObject playerHealthbarImage;
    public GameObject playerHealthbarBoard;
    public Slider playerHealthBarSlider;
    public Slider playerManaBarSlider;

    public bool debuffActive = false;
    public bool maliceActive = false;
    public bool reverseLifeActive = false;
    public bool confusionActive = false;

    public bool minorActive = true;

    private IEnumerator playerDBMaliceCoroutine;
    private IEnumerator playerDBMaliceCoroutineAgain;

    public Animator playerAnimator;

    EnemyHealth enemyHealth;
    public void Awake()
    {
        enemyHealth = GameObject.Find("Orc Warrior").GetComponent<EnemyHealth>();
    }
    


    // Start is called before the first frame update
    void Start()
    {
        playerMana = 1f;
        playerHealth = playerHealthMax;
        //playerDBMaliceCoroutine = PlayerDBMaliceEnemy(2f);
        //playerDBMaliceCoroutineAgain = PlayerDBMaliceCoroutineAgain(2f);
        //StartCoroutine(playerDBMaliceCoroutineAgain);
        cureTextCooldown.gameObject.SetActive(false);
        cureImageCooldown.fillAmount = 0.0f;
        manaTextCooldown.gameObject.SetActive(false);
        manaImageCooldown.fillAmount = 0.0f;
        minorTextCooldown.gameObject.SetActive(false);
        minorImageCooldown.fillAmount = 0.0f;
    }

    // Update is called once per frame
    void Update()
    {
        if(playerMana < 0f)
        { playerMana = 0f; }
        playerHealthBarSlider.value = playerHealth;
        playerMana = Mathf.Clamp(playerMana, 0f, 1f);
        playerManaBarSlider.value = playerMana;
        
        if (debuffActive == true)
        {
            playerHealth = Mathf.Clamp(playerHealth, 0, 55);
        }       
        else if(reverseLifeActive == true)
        {
            playerHealth = Mathf.Clamp(playerHealth, 0, 63);
        }
        else if (confusionActive == true)
        {
            playerHealth = Mathf.Clamp(playerHealth, 0, 85);
            playerHealthMax = Mathf.Clamp(playerHealthMax, 0, 85);
        }
        else
        {
            playerHealth = Mathf.Clamp(playerHealth, 0, 100);
        }

        //Cure CoolDown
        if (playerCureCoolDownTimer > 0)
        {
            cureTextCooldown.gameObject.SetActive(true);
            cureTextCooldown.text = Mathf.RoundToInt(playerCureCoolDownTimer).ToString();
            cureImageCooldown.fillAmount = playerCureCoolDownTimer / playerCureCoolDown;
            playerCureCoolDownTimer -= Time.deltaTime;
        }
        if (playerCureCoolDownTimer < 0)
        {
            cureTextCooldown.gameObject.SetActive(false);
            cureImageCooldown.fillAmount = 0.0f;
            playerCureCoolDownTimer = 0;
        }

        //Mana Cooldown
        if (playerManaCoolDownTimer > 0)
        {
            manaTextCooldown.gameObject.SetActive(true);
            manaTextCooldown.text = Mathf.RoundToInt(playerManaCoolDownTimer).ToString();
            manaImageCooldown.fillAmount = playerManaCoolDownTimer / playerManaCoolDown;
            playerManaCoolDownTimer -= Time.deltaTime;
        }
        if (playerManaCoolDownTimer < 0)
        {
            manaTextCooldown.gameObject.SetActive(false);
            manaImageCooldown.fillAmount = 0.0f;
            playerManaCoolDownTimer = 0;
        }
        //Minor Cooldown
        if (playerMinorCoolDownTimer > 0)
        {
            minorTextCooldown.gameObject.SetActive(true);
            minorTextCooldown.text = Mathf.RoundToInt(playerMinorCoolDownTimer).ToString();
            minorImageCooldown.fillAmount = playerMinorCoolDownTimer / playerMinorCoolDown;
            playerManaCoolDownTimer -= Time.deltaTime;
        }
        if (playerMinorCoolDownTimer < 0)
        {
            minorTextCooldown.gameObject.SetActive(false);
            minorImageCooldown.fillAmount = 0.0f;
            playerMinorCoolDownTimer = 0;
        }

    }



    //Cure Player
    public void CurePlayer()
    {
       

        
        
        if (enemyHealth.spikeAttackCoolDownTimer < 1.2f)
        {
            enemyHealth.spikeAttackCoolDownTimer = 1.2f;
        }
        if (enemyHealth.lightAttackCoolDownTimer < 1.2f)
        {
            enemyHealth.lightAttackCoolDownTimer = 1.2f;
        }
        if (enemyHealth.thurstAttackCoolDownTimer < 1.2f)
        {
            enemyHealth.thurstAttackCoolDownTimer = 1.2f;
        }
        if (playerCureCoolDownTimer < 0)
        {
            
            GameObject.Find("CureButton").GetComponent<Button>().interactable = false;

            StartCoroutine(PlayerCureDelayAction());
        }
        if (playerCureCoolDownTimer == 0)
        {
            
            GameObject.Find("CureButton").GetComponent<Button>().interactable = false;

            StartCoroutine(PlayerCureDelayAction());
        }
        

    }
    
    IEnumerator PlayerCureDelayAction()
    {
            minorActive = false;
            playerAnimator.SetTrigger("Cure");
            yield return new WaitForSeconds(1.5f);
            debuffActive = false;
            maliceActive = false;
            reverseLifeActive = false;
            confusionActive = false;
            MaliceEffect.SetActive(false);
            DebuffEffect.SetActive(false);        
            ReverseLifeEffect.SetActive(false);
            ConfusionEffect.SetActive(false);
            MaliceImage.SetActive(false);
            DebuffImage.SetActive(false);
            ReverseLifeImage.SetActive(false);
            ConfusionImage.SetActive(false);
            playerMana -= 0.02f;
            playerDefence = 10f;
            GetComponent<PlayerHealth>().playerHealthMax = 100f;
            playerHealthbarBoard.GetComponent<RectTransform>().localScale = new Vector3(1f, 1f, 1f);
            playerHealthbarImage.GetComponent<Image>().color = Color.red;
            playerCureCoolDownTimer = playerCureCoolDown;
            GameObject.Find("CureButton").GetComponent<Button>().interactable = true;
            minorActive = true;



    }


    //Minor Player
    public void MinorPlayer(float minorDone)
    {
        if(minorActive == true && playerMana > 0.13f)
        {
        GetComponent<PlayerHealth>().playerHealth += minorDone;
        playerMana -= 0.14f;
        SetHealth(playerHealth);
        if (playerHealth > 100)
            {
            playerHealth = 100;
            }
        }
    }



    //DB Malice Enemy
    private IEnumerator PlayerDBMaliceCoroutineAgain(float playerDBMaliceWaitTime)
    {

        print("Game is Started");
        yield return new WaitForSecondsRealtime(playerDBMaliceWaitTime);
        StartCoroutine(playerDBMaliceCoroutine);

    }
    private IEnumerator PlayerDBMaliceEnemy(float playerMaliceWaitTime)
    {
        
        DebuffPlayer(80);
        print("DB Geldi");
        yield return new WaitForSecondsRealtime(playerMaliceWaitTime);
        playerHealthbarImage.GetComponent<Image>().color = Color.magenta;
        MaliceEffect.SetActive(true);
        MaliceImage.SetActive(true);
        playerDefence = 0f;
        print("Malice Geldi");

    }


    //Debuff Player
    public void DebuffPlayer(float playerDebuffHealth)
    {
        playerHealthbarBoard.GetComponent<RectTransform>().localScale = new Vector3(0.8f, 1f, 1f);
        DebuffEffect.SetActive(true);
        DebuffImage.SetActive(true);

        debuffActive = true;

        if (playerHealth == playerHealthMax)
        {
            GetComponent<PlayerHealth>().playerHealthMax = playerDebuffHealth;
            GetComponent<PlayerHealth>().playerHealth = playerDebuffHealth;
            
        }

        if (playerHealth < playerHealthMax && playerDebuffHealth < playerHealth)
        {
            GetComponent<PlayerHealth>().playerHealthMax = playerDebuffHealth;
            GetComponent<PlayerHealth>().playerHealth = playerDebuffHealth;
        }
        else if (playerHealth < playerDebuffHealth)
        {
            GetComponent<PlayerHealth>().playerHealthMax = playerDebuffHealth;
        }
        playerHealthbarImage.GetComponent<Image>().color = Color.magenta;

        SetHealth(playerHealth);
        if (playerHealth < 0)

            playerHealth = 0;
    }

    //Mana

    public void ManaPlayer()
    {
        
        
        if (playerManaCoolDownTimer > 0)
        {
            print("Mana is Cooldown");
        }
        if (playerManaCoolDownTimer < 0)
        {
            playerManaCoolDownTimer = 0;
            playerMana += 0.5f;
            playerManaCoolDownTimer = playerManaCoolDown;
            
        }
        if (playerManaCoolDownTimer == 0)
        {
            playerMana += 0.5f;
            playerManaCoolDownTimer = playerManaCoolDown;


        }
    }

    //Set HP bar to Health
    public void SetHealth(float playerHealth)
    {
        playerHealthBarSlider.value = playerHealth;
    }


    //Set Mana bar to Mana
    public void SetMana(float playerMana)
    {
        playerManaBarSlider.value = playerMana;
    }
}
