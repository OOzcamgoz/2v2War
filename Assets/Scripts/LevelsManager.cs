using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class LevelsManager : MonoBehaviour
{

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    

    public void LevelMenuLoad()
    {
        SceneManager.LoadScene("Level Menu");
    }
    public void Level2OpenLock()
    {
        PlayerPrefs.SetInt("level2Lock", 2);
        Levels.level2Lock = 2;
        SceneManager.LoadScene("Level Menu");
    }
    public void Level3OpenLock()
    {
        PlayerPrefs.SetInt("level3Lock", 2);
        Levels.level3Lock = 2;
        SceneManager.LoadScene("Level Menu");
    }
    public void Level4OpenLock()
    {
        PlayerPrefs.SetInt("level4Lock", 2);
        Levels.level4Lock = 2;
        SceneManager.LoadScene("Level Menu");
    }
    public void Level5OpenLock()
    {
        PlayerPrefs.SetInt("level5Lock", 2);
        Levels.level5Lock = 2;
        SceneManager.LoadScene("Level Menu");
    }
    public void Level6OpenLock()
    {
        PlayerPrefs.SetInt("level6Lock", 2);
        Levels.level6Lock = 2;
        SceneManager.LoadScene("Level Menu");
    }
    public void Level7OpenLock()
    {
        PlayerPrefs.SetInt("level7Lock", 2);
        Levels.level7Lock = 2;
        SceneManager.LoadScene("Level Menu");
    }
    public void Level8OpenLock()
    {
        PlayerPrefs.SetInt("level8Lock", 2);
        Levels.level8Lock = 2;
        SceneManager.LoadScene("Level Menu");
    }
    public void Level9OpenLock()
    {
        PlayerPrefs.SetInt("level9Lock", 2);
        Levels.level9Lock = 2;
        SceneManager.LoadScene("Level Menu");
    }
    public void Level10OpenLock()
    {
        PlayerPrefs.SetInt("level10Lock", 2);
        Levels.level10Lock = 2;
        SceneManager.LoadScene("Level Menu");
    }
}
