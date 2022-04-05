using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class StartGame : MonoBehaviour
{
    public InputField playerNameInput;
    public GameObject startButton;
    public GameObject howButton;

    public GameObject slideShow;



    public int currentImage;

    // Start is called before the first frame update
    void Start()
    {
        //PlayerPrefs.DeleteAll();
        currentImage = 1;
        
    }

    // Update is called once per frame
    void Update()
    {
        currentImage = Mathf.Clamp(currentImage, 1, 13);

    }
    public void LoadLevelMenu()
    {       
        SceneManager.LoadScene("Level Menu");
        if (PlayerPrefs.HasKey("playerName"))
        {
            Gaming.playerNameString = PlayerPrefs.GetString("playerName");
        }
        /*else 
        {
            Gaming.playerNameString = playerNameInput.text;
            PlayerPrefs.SetString("playerName", playerNameInput.text);
        }*/
    }

    public void ChangeName ()
    {
        PlayerPrefs.SetString("playerName", playerNameInput.text);
    }
    public void LoadHow()
    {
        SceneManager.LoadScene("Level Menu");
    }

    public void PreviousImage()
    {
        if(currentImage == 1)
        {
            GameObject.Find("Image" + currentImage).GetComponent<Image>().enabled = true;
        }
        else 
        { 
        GameObject.Find("Image" + currentImage).GetComponent<Image>().enabled = false;
        currentImage = currentImage-1;
        GameObject.Find("Image" + currentImage).GetComponent<Image>().enabled = true;
        }
    }
    public void NextImage()
    {
        
        GameObject.Find("Image" + currentImage).GetComponent<Image>().enabled = false;
        currentImage++;

        GameObject.Find("Image" + currentImage).GetComponent<Image>().enabled = true;
    }


    public void LoadSlideshow()
    {

        slideShow.gameObject.SetActive(true);


    }
    public void ExitSlideshow()
    {

        slideShow.gameObject.SetActive(false);


    }
}
