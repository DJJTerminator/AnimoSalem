using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine.EventSystems;

public class MainMenu : MonoBehaviour
{
    public Slider[] volumeSliders;
    public Toggle[] resolutionToggles;
    public Toggle   fullscreenToggle;
    public int[] screenWidths;
    int activeScreenResIndex;

    public Transform canvas;
    public Transform basicMenus;
    public Transform advanceMenus;
    public Transform videoMenus;
    public Transform soundMenus;
    public Transform controlMenus;
    public Transform optionMenus;

    public bool BasicMenu = false;
    

    // Use this for initialization
    void Start ()
    {
        //Unlocks the (Mouse)Cursor and makes it visible
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;

        // Check Resolutions
        activeScreenResIndex = PlayerPrefs.GetInt ("screen res index");
        bool isFullscreen = (PlayerPrefs.GetInt ("fullscreen") == 1) ? true : false;

        /*volumeSliders [0].value = AudioManager.instance.masterVolumePErcent;
          volumeSliders [1].value = AudioManager.instance.musicVolumePercent;
          volumeSliders [2].value = AudioManager.instance.sfxVolumePercent;
        */

        for (int i = 0; i < resolutionToggles.Length; i++)
        {
            resolutionToggles[i].isOn = i == activeScreenResIndex;
        }

        fullscreenToggle.isOn = isFullscreen;
    }

    //Changing and saving Resolutions
    public void SetScreenResolution (int i)
    {
        if (resolutionToggles[i].isOn)
        {
            activeScreenResIndex = 1;
            float aspectRatio = 16 / 9;
            Screen.SetResolution (screenWidths [i], (int)(screenWidths [i] / aspectRatio), false);
            PlayerPrefs.SetInt("screen res index", activeScreenResIndex);
            PlayerPrefs.Save();
        }
    }

    // Activate Fullscreen
    public void SetFullscreen(bool isFullscreen)
    {
        for (int i =0; i < resolutionToggles.Length; i++)
        {
            resolutionToggles [i].interactable = !isFullscreen;
        }

        if (isFullscreen)
        {
            Resolution[] allResolutions = Screen.resolutions;
            Resolution maxResolution = allResolutions [allResolutions.Length - 1];
            Screen.SetResolution (maxResolution.width, maxResolution.height, true);
        }
        else
        {
            SetScreenResolution (activeScreenResIndex);
        }

        //Save Fullscreen settings
        PlayerPrefs.SetInt("fullscreen", ((isFullscreen) ? 1 : 0));
        PlayerPrefs.Save();
    }

    //Sound Check
    public void SetMusicVolume(float value)
    {

    }

    public void SetSfxVolume(float value)
    {

    }

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
