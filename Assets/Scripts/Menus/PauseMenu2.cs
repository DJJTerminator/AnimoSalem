using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class PauseMenu2 : MonoBehaviour
{

    public float FMV = 5;
    public float soundVolume = .65f;
    public float musicVolume = .65f;
    public bool canUnPause = true;
    public int selectionIndicator;

    public GameObject dialogueBox;

    [HideInInspector]
    //*bools
    public bool pause = false;

    //basic menus
    public GameObject[] basicMenus;

    //advance menus
    public GameObject[] advanceMenus;

    //video settings
    public GameObject[] videoMenus;

    //audio settings
    public GameObject[] soundMenus;

    //control settings
    public GameObject[] controlMenus;

    public GameObject[] everythingElse;
	public GameObject storageMenu;

    // Use this for initialization
    void Start()
    {

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

    void Update()
    {
        //Unlocks the (Mouse)Cursor and makes it visible
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;

		if (Input.GetKeyDown(KeyCode.Escape) && canUnPause == true && !dialogueBox.activeSelf && !storageMenu.activeSelf)
        {
            if (!pause)
            {
                pause = true;
                for (int i = 0; i < everythingElse.Length; i++)
                {
                    everythingElse[i].SetActive(false);
                }
                for (int j = 0; j < basicMenus.Length; j++)
                {
                    basicMenus[j].SetActive(true);
                }
            }
            else
            {
                pause = false;
                for (int i = 0; i < everythingElse.Length; i++)
                {

                    everythingElse[i].SetActive(true);
                }
                for (int j = 0; j < basicMenus.Length; j++)
                {
                    basicMenus[j].SetActive(false);
                }
            }

        }
    }
    //resuming the game from basic menus
    public void Resume()
    {
        pause = false;
        for (int i = 0; i < everythingElse.Length; i++)
        {
            everythingElse[i].SetActive(true);
        }
        for (int j = 0; j < basicMenus.Length; j++)
        {
            basicMenus[j].SetActive(false);
        }
    }

    //entering advance options
    public void Options()
    {
        for (int i = 0; i < basicMenus.Length; i++)
        {
            basicMenus[i].SetActive(false);
        }
        for (int j = 0; j < advanceMenus.Length; j++)
        {
            advanceMenus[j].SetActive(true);
        }
        canUnPause = false;
    }

    //entering vdieos
    public void Videos()
    {
        for (int i = 0; i < advanceMenus.Length; i++)
        {
            advanceMenus[i].SetActive(false);
        }
        for (int j = 0; j < videoMenus.Length; j++)
        {
            videoMenus[j].SetActive(true);
        }
    }
    //entering audios
    public void Audio()
    {
        for (int i = 0; i < advanceMenus.Length; i++)
        {
            advanceMenus[i].SetActive(false);
        }
        for (int j = 0; j < soundMenus.Length; j++)
        {
            soundMenus[j].SetActive(true);
        }
    }
    //entering controls
    public void Controls()
    {
        for (int i = 0; i < advanceMenus.Length; i++)
        {
            advanceMenus[i].SetActive(false);
        }
        for (int j = 0; j < controlMenus.Length; j++)
        {
            controlMenus[j].SetActive(true);
        }
    }

    //returning from certain options
    public void Return()
    {
        //returning from advance menus
        if (advanceMenus[0].activeSelf)
        {
            for (int i = 0; i < advanceMenus.Length; i++)
            {
                advanceMenus[i].SetActive(false);
            }
            for (int j = 0; j < basicMenus.Length; j++)
            {
                basicMenus[j].SetActive(true);
            }
            canUnPause = true;
        }

        //returning from controls menus
        else if (controlMenus[0].activeSelf)
        {
            for (int i = 0; i < controlMenus.Length; i++)
            {
                controlMenus[i].SetActive(false);
            }
            for (int j = 0; j < advanceMenus.Length; j++)
            {
                advanceMenus[j].SetActive(true);
            }
        }

        //returning from audio
        else if (soundMenus[0].activeSelf)
        {
            for (int i = 0; i < soundMenus.Length; i++)
            {
                soundMenus[i].SetActive(false);
            }
            for (int j = 0; j < advanceMenus.Length; j++)
            {
                advanceMenus[j].SetActive(true);
            }
        }

        //returning from video
        else if (videoMenus[0].activeSelf)
        {
            for (int i = 0; i < videoMenus.Length; i++)
            {
                videoMenus[i].SetActive(false);
            }
            for (int j = 0; j < advanceMenus.Length; j++)
            {
                advanceMenus[j].SetActive(true);
            }
        }

    }

    // Allows navigation with "W,S,Up, and Down Keys" between buttons
    public void SelectNewButton(GameObject button)
    {
        EventSystem.current.SetSelectedGameObject(button);
    }

    // Quit to leave Game
    public void Quit()
    {
        Application.Quit();
    }
}