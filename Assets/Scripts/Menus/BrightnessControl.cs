using UnityEngine;
using System.Collections;

public class BrightnessControl : MonoBehaviour {
    float rbgValue = 0.5f;

    void OnGUI()
    {
        // Make changes to Light Settings by color changes
        rbgValue = GUI.HorizontalSlider (new Rect (Screen.width/2 - 98, 798, 255, 100), rbgValue, 2f, -5.0f);
        RenderSettings.ambientLight = new Color(rbgValue, rbgValue, rbgValue, 1);
    }
	
}
