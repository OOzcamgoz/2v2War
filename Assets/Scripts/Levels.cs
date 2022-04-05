using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class Levels : MonoBehaviour
{
    
    
    public static int level2Lock = 1;
    public static int level3Lock = 1;
    public static int level4Lock = 1;
    public static int level5Lock = 1;
    public static int level6Lock = 1;
    public static int level7Lock = 1;
    public static int level8Lock = 1;
    public static int level9Lock = 1;
    public static int level10Lock = 1;

    public GameObject level2Button;
    public GameObject level3Button;
    public GameObject level4Button;
    public GameObject level5Button;
    public GameObject level6Button;
    public GameObject level7Button;
    public GameObject level8Button;
    public GameObject level9Button;
    public GameObject level10Button;

    void Awake()
    {

    }


    // Start is called before the first frame update
    void Start()
    {
        if (PlayerPrefs.HasKey("level2Lock"))
        {
            level2Lock = PlayerPrefs.GetInt("level2Lock");
        }
        if (PlayerPrefs.HasKey("level3Lock"))
        {
            level3Lock = PlayerPrefs.GetInt("level3Lock");
        }
        if (PlayerPrefs.HasKey("level4Lock"))
        {
            level4Lock = PlayerPrefs.GetInt("level4Lock");
        }
        if (PlayerPrefs.HasKey("level5Lock"))
        {
            level5Lock = PlayerPrefs.GetInt("level5Lock");
        }
        if (PlayerPrefs.HasKey("level6Lock"))
        {
            level6Lock = PlayerPrefs.GetInt("level6Lock");
        }
        if (PlayerPrefs.HasKey("level7Lock"))
        {
            level7Lock = PlayerPrefs.GetInt("level7Lock");
        }
        if (PlayerPrefs.HasKey("level8Lock"))
        {
            level8Lock = PlayerPrefs.GetInt("level8Lock");
        }
        if (PlayerPrefs.HasKey("level9Lock"))
        {
            level9Lock = PlayerPrefs.GetInt("level9Lock");
        }
        if (PlayerPrefs.HasKey("level10Lock"))
        {
            level10Lock = PlayerPrefs.GetInt("level10Lock");
        }
    }


    // Update is called once per frame
    void Update()
    {
        
        if (level2Lock == 2)
        {
            level2Button.gameObject.GetComponent<Button>().interactable = true;
        }
        if (level3Lock == 2)
        {
            level3Button.gameObject.GetComponent<Button>().interactable = true;
        }
        if (level4Lock == 2)
        {
            level4Button.gameObject.GetComponent<Button>().interactable = true;
        }
        if (level5Lock == 2)
        {
            level5Button.gameObject.GetComponent<Button>().interactable = true;
        }
        if (level6Lock == 2)
        {
            level6Button.gameObject.GetComponent<Button>().interactable = true;
        }
        if (level7Lock == 2)
        {
            level7Button.gameObject.GetComponent<Button>().interactable = true;
        }
        if (level8Lock == 2)
        {
            level8Button.gameObject.GetComponent<Button>().interactable = true;
        }
        if (level9Lock == 2)
        {
            level9Button.gameObject.GetComponent<Button>().interactable = true;
        }
        if (level10Lock == 2)
        {
            level10Button.gameObject.GetComponent<Button>().interactable = true;
        }
    }

   
    public void MainScene()
    {

        SceneManager.LoadScene("Main Scene");

    }
    public void LoadLevel1()
    {
        Gaming.playerLevelMin = 1;
        Gaming.enemyLevelMin = 1;
        Gaming.playerLevelMax = 3;
        Gaming.enemyLevelMax = 3;

        SceneManager.LoadScene("Level 1");

    }
    public void LoadLevel2()
    {
        Gaming.playerLevelMin = 1;
        Gaming.enemyLevelMin = 1;
        Gaming.playerLevelMax = 3;
        Gaming.enemyLevelMax = 3;
        SceneManager.LoadScene("Level 2");
    }
    public void LoadLevel3()
    {
        Gaming.playerLevelMin = 1;
        Gaming.enemyLevelMin = 1;
        Gaming.playerLevelMax = 4;
        Gaming.enemyLevelMax = 4;

        SceneManager.LoadScene("Level 3");
    }
    public void LoadLevel4()
    {
        Gaming.playerLevelMin = 1;
        Gaming.enemyLevelMin = 1;
        Gaming.playerLevelMax = 4;
        Gaming.enemyLevelMax = 4;
        SceneManager.LoadScene("Level 4");
    }
    public void LoadLevel5()
    {
        Gaming.playerLevelMin = 1;
        Gaming.enemyLevelMin = 1;
        Gaming.playerLevelMax = 5;
        Gaming.enemyLevelMax = 4;

        SceneManager.LoadScene("Level 5");
    }
    public void LoadLevel6()
    {
        Gaming.playerLevelMin = 1;
        Gaming.enemyLevelMin = 1;
        Gaming.playerLevelMax = 5;
        Gaming.enemyLevelMax = 4;

        SceneManager.LoadScene("Level 6");
    }
    public void LoadLevel7()
    {
        Gaming.playerLevelMin = 1;
        Gaming.enemyLevelMin = 1;
        Gaming.playerLevelMax = 5;
        Gaming.enemyLevelMax = 4;

        SceneManager.LoadScene("Level 7");
    }
    public void LoadLevel8()
    {
        Gaming.playerLevelMin = 2;
        Gaming.enemyLevelMin = 2;
        Gaming.playerLevelMax = 5;
        Gaming.enemyLevelMax = 4;
        SceneManager.LoadScene("Level 8");
    }
    public void LoadLevel9()
    {
        Gaming.playerLevelMin = 2;
        Gaming.enemyLevelMin = 2;
        Gaming.playerLevelMax = 5;
        Gaming.enemyLevelMax = 4;
        SceneManager.LoadScene("Level 9");
    }
    public void LoadLevel10()
    {
        Gaming.playerLevelMin = 2;
        Gaming.enemyLevelMin = 2;
        Gaming.playerLevelMax = 5;
        Gaming.enemyLevelMax = 4;
        SceneManager.LoadScene("Level 10");
    }
}
