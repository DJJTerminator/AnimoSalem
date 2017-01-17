using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class InventoryActivation : MonoBehaviour {
	public Text totalWeight;
	public GameObject decision;

	
	// Update is called once per frame
	void Update () 
	{

		//accessing  storage menu
		if (Input.GetKeyDown ("i") && !decision.activeSelf)
		if(!DataStorage.canDo)
		{
			DataStorage.storageMenu.SetActive (false);
			DataStorage.player.GetComponent<Controls> ().enabled = true;
			DataStorage.pauseMenus.GetComponent<PauseMenu2>().enabled = true;
			DataStorage.gameManager.GetComponent<StatActivation> ().enabled = true;
			DataStorage.canDo = true;
		} 
		else
		{
            DataStorage.player.GetComponent<Controls>().healing.Stop();
            DataStorage.storageMenu.SetActive (true);
			DataStorage.player.GetComponent<Controls> ().enabled = false;
			DataStorage.pauseMenus.GetComponent<PauseMenu2>().enabled = false;
			DataStorage.gameManager.GetComponent<StatActivation> ().enabled = false;
			DataStorage.canDo = false;
			totalWeight.text = DataStorage.curWeight + "/"  +DataStorage.maxWeight.ToString();
		}
	}
}
