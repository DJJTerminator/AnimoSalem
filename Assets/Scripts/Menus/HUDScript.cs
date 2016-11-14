using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class HUDScript : MonoBehaviour {
	GameObject toolTip;
	Text toolTipText;

	// Use this for initialization
	void Start () 
	{
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
