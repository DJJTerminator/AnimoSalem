using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class HUDScript : MonoBehaviour {
	GameObject toolTip;
	Text toolTipText;
   // Color myOriginalColor;

    void OnEnable()
    {
        switch (DataStorage.playerStats)
        {
            case 0:
                DataStorage.levelBackground = GameObject.Find("All Canvases/Canvas/HUD/CurrentLevel/Background").GetComponent<Animator>();
                DataStorage.levelBackground.Play("RevertColor");
                break;
            default:
                DataStorage.levelBackground = GameObject.Find("All Canvases/Canvas/HUD/CurrentLevel/Background").GetComponent<Animator>();
                DataStorage.levelBackground.Play("StatsRemain");
                break;
        }
    }

    // Use this for initialization
    void Start () 
	{
      //   myOriginalColor = GameObject.Find("All Canvases/Canvas/HUD/CurrentLevel/Background").GetComponent<Image>().color;
        toolTip = GameObject.Find ("All Canvases/Canvas/HUD/ToolTip");
		toolTipText = GameObject.Find ("All Canvases/Canvas/HUD/ToolTip/ToolTipText").GetComponent<Text>();
	}
	
	// Update is called once per frame
	public void TooltipOn () 
	{
		toolTip.SetActive(true);
		toolTipText.text = DataStorage.health + "/" + DataStorage.maxHealth.ToString ();
	}
	public void TooltipOff () 
	{
		toolTip.SetActive(false);
	}

}
