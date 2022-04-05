using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class EnemyHealth : MonoBehaviour
{
    public float enemyHealthMax = 100f;
    public float normalHealthMax = 100f;
    public float enemyHealth;
    public float enemyMana;
    public float enemyDefence = 10f;
    private float skillCoolDownTimer = 1.5f;
    public float cureCoolDown = 2.5f;
    public float cureCoolDownTimer;
    public float lightAttackCoolDown = 1.5f;
    public float lightAttackCoolDownTimer;
    public float spikeAttackCoolDown = 8f;
    public float spikeAttackCoolDownTimer;
    public float thurstAttackCoolDown = 8f;
    public float thurstAttackCoolDownTimer;
    public float dbAttackCoolDown = 11f;
    public float dbAttackCoolDownTimer;
    public TMP_Text thursttextCooldown;
    public Image thurstimageCooldown;
    public TMP_Text spikeTextCooldown;
    public Image spikeImageCooldown;
    public TMP_Text lightAttackTextCooldown;
    public Image lightAttackImageCooldown;

    PlayerHealth playerHealth;

    public int spikeDamage;
    public int thurstDamage;
    public int lightAttackDamage;


    public bool debuffActive = false;
    public bool maliceActive = false;
    public bool reverseLifeActive = false;
    public bool confusionActive = false;

    public GameObject enemyPlayerHealthbarImage;
    public GameObject enemyPlayerHealthbarBoard;
    public Slider healthBarSlider;
    public Slider enemyManaBarSlider;

    public GameObject DebuffEffect;   
    public GameObject MaliceEffect;   
    public GameObject ReverseLifeEffect;    
    public GameObject ConfusionEffect;    
    public GameObject HealEffect;

    private IEnumerator dbmaliceCoroutine;
    private IEnumerator dbmaliceCoroutineAgain;

    public Animator playerAnimator;

    public void Awake()
    {

        playerHealth = GameObject.Find("Player").GetComponent<PlayerHealth>();

        spikeTextCooldown.gameObject.SetActive(false);
        spikeImageCooldown.fillAmount = 0.0f;
        thursttextCooldown.gameObject.SetActive(false);
        thurstimageCooldown.fillAmount = 0.0f;
        lightAttackTextCooldown.gameObject.SetActive(false);
        lightAttackImageCooldown.fillAmount = 0.0f;
       
    }
    //Game Start
    public void Start()
    {
        enemyMana = 1f;
        enemyHealth = enemyHealthMax;
        
        

    }


    //Light Attack
    public void LightAttackDelay(float delayTime)
    {
        StartCoroutine(LightAttackDelayAction(delayTime));
    }

    IEnumerator LightAttackDelayAction(float delayTime)
    {

        yield return new WaitForSeconds(delayTime);
        

    }




    //Cure
    public void Cure(float delayTime)
    {
        StartCoroutine(CureDelayAction(delayTime));
    }

    IEnumerator CureDelayAction(float delayTime)
    {

        yield return new WaitForSeconds(delayTime);
        if (cureCoolDownTimer > 0)
        {
            print("Cure is Cooldown");
        }
        if (cureCoolDownTimer < 0)
        {
            cureCoolDownTimer = 0;
            enemyDefence = 10f;
            GetComponent<EnemyHealth>().enemyHealthMax = 100f;
            enemyPlayerHealthbarBoard.GetComponent<RectTransform>().localScale = new Vector3(1f, 1f, 1f);
            enemyPlayerHealthbarImage.GetComponent<Image>().color = Color.red;
            cureCoolDownTimer = cureCoolDown;


        }
        if (cureCoolDownTimer == 0)
        {
            enemyDefence = 10f;
            GetComponent<EnemyHealth>().enemyHealthMax = 100f;
            enemyPlayerHealthbarBoard.GetComponent<RectTransform>().localScale = new Vector3(1f, 1f, 1f);
            enemyPlayerHealthbarImage.GetComponent<Image>().color = Color.red;
            cureCoolDownTimer = cureCoolDown;



        }

    }
    

    //DB Malice Enemy
    private IEnumerator DBMaliceCoroutineAgain(float dbmaliceWaitTime)
    {

        print("Game is Started");
            yield return new WaitForSecondsRealtime(dbmaliceWaitTime);
            StartCoroutine(dbmaliceCoroutine);

    }
    private IEnumerator DBMaliceEnemy(float maliceWaitTime)
    {
        DebuffEnemy(80);
        print("DB Geldi");
        yield return new WaitForSecondsRealtime(maliceWaitTime);
        enemyPlayerHealthbarImage.GetComponent<Image>().color = Color.magenta;
        enemyDefence = 0f;
        print("Malice Geldi");

    }





    //Spike Damage Enemy
    public void SpikeDamageEnemy()
    {
        if (playerHealth.playerMana >= 0.02f)
        {
            playerHealth.playerMana -= 0.02f;

            if (thurstAttackCoolDownTimer < 1)
            {
                thurstAttackCoolDownTimer = 1;
            }
            if (lightAttackCoolDownTimer < 1)
            {
                lightAttackCoolDownTimer = 1;
            }
            if (playerHealth.playerCureCoolDownTimer < 1)
            {
                playerHealth.playerCureCoolDownTimer = 1;
            }
            if (enemyHealth < 0)

                enemyHealth = 0;

            if (spikeAttackCoolDownTimer > 0)
            {
                GameObject.Find("SpikeButton").GetComponent<Button>().interactable = false;
                print("Spike Damage is Cooldown");
            }
            if (spikeAttackCoolDownTimer < 0)
            {
                spikeAttackCoolDownTimer = 0;
                GetComponent<EnemyHealth>().enemyHealth -= spikeDamage - (enemyDefence) * 1.5f;
                SetHealth();
                spikeAttackCoolDownTimer = spikeAttackCoolDown;
                playerAnimator.SetTrigger("Spike");


            }
            if (spikeAttackCoolDownTimer == 0)
            {
                GetComponent<EnemyHealth>().enemyHealth -= spikeDamage - (enemyDefence) * 1.5f;
                SetHealth();
                spikeAttackCoolDownTimer = spikeAttackCoolDown;
                playerAnimator.SetTrigger("Spike");


            }
        }

    }




    //Thurst Damage Enemy

    public void ThurstDamageEnemy()
    {
        if (playerHealth.playerMana >= 0.02f)
        {
            playerHealth.playerMana -= 0.02f;
            if (spikeAttackCoolDownTimer < 1)
            {
                spikeAttackCoolDownTimer = 1;
            }
            if (lightAttackCoolDownTimer < 1)
            {
                lightAttackCoolDownTimer = 1;
            }
            if (playerHealth.playerCureCoolDownTimer < 1)
            {
                playerHealth.playerCureCoolDownTimer = 1;
            }
            if (enemyHealth < 0)

                enemyHealth = 0;

            if (thurstAttackCoolDownTimer > 0)
            {
                print("Thurst Damage is Cooldown");
            }
            if (thurstAttackCoolDownTimer < 0)
            {
                thurstAttackCoolDownTimer = 0;
                GetComponent<EnemyHealth>().enemyHealth -= thurstDamage - (enemyDefence) * 2;
                SetHealth();
                thurstAttackCoolDownTimer = thurstAttackCoolDown;
                playerAnimator.SetTrigger("Thurst");


            }
            if (thurstAttackCoolDownTimer == 0)
            {
                GetComponent<EnemyHealth>().enemyHealth -= thurstDamage - (enemyDefence) * 2;
                SetHealth();
                thurstAttackCoolDownTimer = thurstAttackCoolDown;
                playerAnimator.SetTrigger("Thurst");


            }

        }
    }






    //Light Attack Damage Enemy

    public void LightAttacktDamageEnemy()
    {
        if (playerHealth.playerMana >= 0.02f)
        {
            playerHealth.playerMana -= 0.02f;
            if (spikeAttackCoolDownTimer < 1)
            {
                spikeAttackCoolDownTimer = 1;
            }
            if (thurstAttackCoolDownTimer < 1)
            {
                thurstAttackCoolDownTimer = 1;
            }
            if (playerHealth.playerCureCoolDownTimer < 1)
            {
                playerHealth.playerCureCoolDownTimer = 1;
            }
            if (enemyHealth < 0)

                enemyHealth = 0;

            if (lightAttackCoolDownTimer > 0)
            {
                print("Light Attack is Cooldown");
            }
            if (lightAttackCoolDownTimer < 0)
            {
                lightAttackCoolDownTimer = 0;
                GetComponent<EnemyHealth>().enemyHealth -= lightAttackDamage - enemyDefence / 2;
                SetHealth();
                lightAttackCoolDownTimer = lightAttackCoolDown;
                playerAnimator.SetTrigger("LightAttack");




            }
            if (lightAttackCoolDownTimer == 0)
            {
                GetComponent<EnemyHealth>().enemyHealth -= lightAttackDamage - enemyDefence / 2;
                SetHealth();
                lightAttackCoolDownTimer = lightAttackCoolDown;
                playerAnimator.SetTrigger("LightAttack");


            }

        }
    }




    //Debuff Enemy
    public void DebuffEnemy(float debuffHealth)
    {
        enemyPlayerHealthbarBoard.GetComponent<RectTransform>().localScale = new Vector3(0.8f, 1f, 1f);


        if (enemyHealth == enemyHealthMax)
        {
            GetComponent<EnemyHealth>().enemyHealthMax = debuffHealth;
            GetComponent<EnemyHealth>().enemyHealth = debuffHealth;
        }

        if (enemyHealth < enemyHealthMax && debuffHealth < enemyHealth)
        {
            GetComponent<EnemyHealth>().enemyHealthMax = debuffHealth;
            GetComponent<EnemyHealth>().enemyHealth = debuffHealth;
        }
        else if(enemyHealth < debuffHealth)
        {
            GetComponent<EnemyHealth>().enemyHealthMax = debuffHealth;
        }
        enemyPlayerHealthbarImage.GetComponent<Image>().color = Color.magenta;

        SetHealth();
        if (enemyHealth < 0)

            enemyHealth = 0;
    }









    //Set HP bar to Health
    public void SetHealth()
    {
        healthBarSlider.value = enemyHealth;
    }




    //Heal Enemy
    public void HealEnemy(float healDone)
    {
        GetComponent<EnemyHealth>().enemyHealth += healDone;
        SetHealth();
        if (enemyHealth > 100)

            enemyHealth = 100;
    }





  void Update()
    {
        if(playerHealth.confusionActive == false)
        { 
        spikeDamage = 33;
        thurstDamage = 45;
        lightAttackDamage = 13;
        }
        else
        {
            spikeDamage = 30;
            thurstDamage = 41;
            lightAttackDamage = 10;
        }
        SetHealth();
        enemyMana = Mathf.Clamp(enemyMana, 0f, 1f);
        enemyManaBarSlider.value = enemyMana;
        if (debuffActive == true)
        {
            enemyHealth = Mathf.Clamp(enemyHealth, 0, 55);
            enemyHealthMax = Mathf.Clamp(enemyHealthMax, 0, 55);
        }
        else if (reverseLifeActive == true)
        {
            enemyHealth = Mathf.Clamp(enemyHealth, 0, 63);
            enemyHealthMax = Mathf.Clamp(enemyHealthMax, 0, 63);
        }
        else if (confusionActive == true)
        {
            enemyHealth = Mathf.Clamp(enemyHealth, 0, 85);
            enemyHealthMax = Mathf.Clamp(enemyHealthMax, 0, 85);
        }
        else
        {
            enemyHealth = Mathf.Clamp(enemyHealth, 0, 100);
            enemyHealthMax = Mathf.Clamp(enemyHealthMax, 0, 100);
        }

        if (spikeAttackCoolDownTimer > 0)
        {
            GameObject.Find("SpikeButton").GetComponent<Button>().interactable = false;
        }
        else
        {
            GameObject.Find("SpikeButton").GetComponent<Button>().interactable = true;
        }

        if (thurstAttackCoolDownTimer > 0)
        {
            GameObject.Find("ThurstButton").GetComponent<Button>().interactable = false;
        }
        else
        {
            GameObject.Find("ThurstButton").GetComponent<Button>().interactable = true;
        }

        if (lightAttackCoolDownTimer > 0)
        {
            GameObject.Find("LightAttackButton").GetComponent<Button>().interactable = false;
        }
        else
        {
            GameObject.Find("LightAttackButton").GetComponent<Button>().interactable = true;
        }

        //Cure CoolDown
        if (cureCoolDownTimer > 0)
        {
            cureCoolDownTimer -= Time.deltaTime;
        }
        if (cureCoolDownTimer < 0)
        {
            cureCoolDownTimer = 0;
        }





        //Light Attack CoolDown
        if (lightAttackCoolDownTimer > 0)
        {
            lightAttackTextCooldown.gameObject.SetActive(true);
            lightAttackTextCooldown.text = Mathf.RoundToInt(lightAttackCoolDownTimer).ToString();
            lightAttackImageCooldown.fillAmount = lightAttackCoolDownTimer / lightAttackCoolDown;
            lightAttackCoolDownTimer -= Time.deltaTime;
        }
        if (lightAttackCoolDownTimer < 0)
        {
            lightAttackTextCooldown.gameObject.SetActive(false);
            lightAttackImageCooldown.fillAmount = 0.0f;
            lightAttackCoolDownTimer = 0;
        }


        //Spike Attack CoolDown
        if (spikeAttackCoolDownTimer > 0)
        {
            spikeTextCooldown.gameObject.SetActive(true);
            spikeTextCooldown.text = Mathf.RoundToInt(spikeAttackCoolDownTimer).ToString();
            spikeImageCooldown.fillAmount = spikeAttackCoolDownTimer / spikeAttackCoolDown;
            spikeAttackCoolDownTimer -= Time.deltaTime;
        }
        if (spikeAttackCoolDownTimer < 0)
        {
            spikeTextCooldown.gameObject.SetActive(false);
            spikeImageCooldown.fillAmount = 0.0f;
            spikeAttackCoolDownTimer = 0;
        }


        //Thurst Attack CoolDown
        if (thurstAttackCoolDownTimer > 0)
        {
            thursttextCooldown.gameObject.SetActive(true);
            thursttextCooldown.text = Mathf.RoundToInt(thurstAttackCoolDownTimer).ToString();
            thurstimageCooldown.fillAmount = thurstAttackCoolDownTimer / thurstAttackCoolDown;
            thurstAttackCoolDownTimer -= Time.deltaTime;
        }
        if (thurstAttackCoolDownTimer < 0)
        {
            thursttextCooldown.gameObject.SetActive(false);
            thurstimageCooldown.fillAmount = 0.0f;
            thurstAttackCoolDownTimer = 0;
        }


        // Debuff
        if (Input.GetKeyDown(KeyCode.DownArrow) && dbAttackCoolDownTimer == 0)
        {
            DebuffEnemy(80);
            print(enemyHealth);
            dbAttackCoolDownTimer = dbAttackCoolDown;
        }
        else if (Input.GetKeyDown(KeyCode.DownArrow) && dbAttackCoolDownTimer != 0)
        {
            print("Debuff is Cooldown");
        }

       
    }
}
