using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public Transform canvas;
    public Transform basicMenus;
    public Transform advanceMenus;
    public Transform videoMenus;
    public Transform soundMenus;
    public Transform controlMenus;
    public Transform optionMenus;

    public bool BasicMenu = false;
    public float FMV = 5;
    public float soundVolume = .65f;
    public float musicVolume = .65f;

    // Use this for initialization
    void Start ()
    {
        //Unlocks the (Mouse)Cursor and makes it visible
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;

        //checking the sounds
        soundVolume = PlayerPrefs.GetFloat("Sound Volume", soundVolume);
        musicVolume = PlayerPrefs.GetFloat("Music Volume", musicVolume);

        if (PlayerPrefs.HasKey("Sound Volume"))
        {
            AudioListener.volume = PlayerPrefs.GetFloat("SoundVolume", soundVolume);
        }
        else
        {
            PlayerPrefs.SetFloat("Sound Volume", soundVolume);
            PlayerPrefs.SetFloat("Music Volume", musicVolume);
        }
    }


    /*
        // Update is called once per frame
        void Update()
        {
            //end of if "(DialogueBox.activeSelf == false)"

            if (BasicMenu == true)
            {
                if (Input.GetKeyDown(KeyCode.DownArrow) || Input.GetKeyDown(KeyCode.S))
                {
                    MoveSelectorDown();
                }
                if (Input.GetKeyDown(KeyCode.UpArrow) || Input.GetKeyDown(KeyCode.W))
                {
                    MoveSelectorUp();
                }

                if (Input.GetKeyDown(KeyCode.Return))
                {
                    Chosen();
                }
            }

        }
        //end of update
    */



    // To Resume back to Basic Menu
    public void Resume(bool Open)
    {
        if (canvas.gameObject.activeInHierarchy == false)
        {
            if (basicMenus.gameObject.activeInHierarchy == true)
            {
                basicMenus.gameObject.SetActive(true);
                advanceMenus.gameObject.SetActive(false);
                videoMenus.gameObject.SetActive(false);
                soundMenus.gameObject.SetActive(false);
                controlMenus.gameObject.SetActive(false);
                optionMenus.gameObject.SetActive(false);
            }
            canvas.gameObject.SetActive(true);
            Time.timeScale = 0;
        }
        else
        {
            canvas.gameObject.SetActive(true);
            Time.timeScale = 1;
        }
    }

    // Display Basic Menu
    public void BasicMenus(bool Open)
    {
        if (Open)
        {
            advanceMenus.gameObject.SetActive(false);
            basicMenus.gameObject.SetActive(true);
        }
        if (!Open)
        {
            advanceMenus.gameObject.SetActive(true);
            basicMenus.gameObject.SetActive(false);
        }
    }

    // Display Advance Menu
    public void AdvanceSettings(bool Open)
    {
        if (Open)
        {
            advanceMenus.gameObject.SetActive(true);
            basicMenus.gameObject.SetActive(false);
            videoMenus.gameObject.SetActive(false);
            soundMenus.gameObject.SetActive(false);
            controlMenus.gameObject.SetActive(false);
        }
        if (!Open)
        {
            advanceMenus.gameObject.SetActive(false);
            basicMenus.gameObject.SetActive(true);
            videoMenus.gameObject.SetActive(false);
            soundMenus.gameObject.SetActive(false);
            controlMenus.gameObject.SetActive(false);
        }
    }

    // Display Video Settings Menu
    public void VideoSettings(bool Open)
    {
        if (Open)
        {
            advanceMenus.gameObject.SetActive(false);
            videoMenus.gameObject.SetActive(true);
            basicMenus.gameObject.SetActive(false);
            controlMenus.gameObject.SetActive(false);
            soundMenus.gameObject.SetActive(false);
        }
        if (!Open)
        {
            advanceMenus.gameObject.SetActive(true);
            videoMenus.gameObject.SetActive(false);
            basicMenus.gameObject.SetActive(false);
            controlMenus.gameObject.SetActive(false);
            soundMenus.gameObject.SetActive(false);
        }
    }

    // Display Audio Settings Menu
    public void SoundsSettings(bool Open)
    {
        if (Open)
        {
            soundMenus.gameObject.SetActive(true);
            basicMenus.gameObject.SetActive(false);
            advanceMenus.gameObject.SetActive(false);
            videoMenus.gameObject.SetActive(false);
            controlMenus.gameObject.SetActive(false);
        }
        if (!Open)
        {
            advanceMenus.gameObject.SetActive(true);
            soundMenus.gameObject.SetActive(false);
            basicMenus.gameObject.SetActive(false);
            videoMenus.gameObject.SetActive(false);
            controlMenus.gameObject.SetActive(false);

        }
    }

    // Display Control Menu 
    public void Controls(bool Open)
    {
        if (Open)
        {
            controlMenus.gameObject.SetActive(true);
            basicMenus.gameObject.SetActive(false);
            advanceMenus.gameObject.SetActive(false);
        }
        if (!Open)
        {
            advanceMenus.gameObject.SetActive(true);
            controlMenus.gameObject.SetActive(false);
            basicMenus.gameObject.SetActive(false);
        }
    }


    // Display Options Menu 
    public void Options(bool Open)
    {
        if (Open)
        {
           optionMenus.gameObject.SetActive(true);
            basicMenus.gameObject.SetActive(false);
        }
        if (!Open)
        {
            optionMenus.gameObject.SetActive(false);
            basicMenus.gameObject.SetActive(true);
        }
    }

    // Go to Specific Level
    public void LoadLevel(string _levelName)
    {
        SceneManager.LoadScene(_levelName);
    }

    // Quit to leave Game
    public void Quit()
    {
        Application.Quit();
    }
}
