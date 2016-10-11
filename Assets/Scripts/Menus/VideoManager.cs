using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class VideoManager : MonoBehaviour
{

    //public GameObject currentWindow;

    public Slider qualitySlider;
    public Slider vSyncSlider;
    public Slider aaSlider;

    public Text qualityText;
    public Text vSyncText;
    public Text aaText;

    //public Toggle fullscreenToggle;

    // Use this for initialization
    void Start ()
    {
        GameObject.FindObjectOfType<Button>().Select();
        qualitySlider.maxValue = QualitySettings.names.Length - 1;

        qualitySlider.value = QualitySettings.GetQualityLevel();
        aaSlider.value = QualitySettings.antiAliasing;
        vSyncSlider.value = QualitySettings.vSyncCount;
	}

    #region BASIC SETTINGS

// Set up Quality Levels
    public void SetQuality()
    {
        QualitySettings.SetQualityLevel((int)qualitySlider.value);
        qualityText.text = "Quality Level: " + QualitySettings.names[QualitySettings.GetQualityLevel()];
        aaSlider.value = QualitySettings.antiAliasing;
        AA();
        vSyncSlider.value = QualitySettings.vSyncCount;
    }
// Turning vSync On and Off
    public void VSync()
    {
        QualitySettings.vSyncCount = (int)vSyncSlider.value;
        switch (QualitySettings.vSyncCount)
        {
            case 0:
                vSyncText.text = "vSync: OFF";
                break;
            default:
                vSyncText.text = "vSync: " + QualitySettings.vSyncCount.ToString();
                break;
        }
    }

// Changing Anti-Ailiasing values
    public void AA()
    {
        switch ((int)aaSlider.value)
        {
            case 0:
                aaText.text = "Anti-Ailiasing: Off";
                QualitySettings.antiAliasing = 0;
                break;

            case 1:
                aaText.text = "Anti-Ailiasing: x2";
                QualitySettings.antiAliasing = 2;
                break;

            case 2:
                aaText.text = "Anti-Ailiasing: x4";
                QualitySettings.antiAliasing = 4;
                break;

            case 3:
                aaText.text = "Anti-Ailiasing: x8";
                QualitySettings.antiAliasing = 8;
                break;
            default:
                break;
        }
    }
    #endregion 
}