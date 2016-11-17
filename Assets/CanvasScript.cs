using UnityEngine;
using System.Collections;

public class CanvasScript : MonoBehaviour 
{

	GameObject levelStats;
	GameObject storageMenu;
	bool animBool = false;
	// Use this for initialization
	void Start () 
	{
		levelStats = GameObject.Find ("All Canvases/Canvas/LevelStats");
		storageMenu = GameObject.Find ("All Canvases/Canvas/StorageMenu");
	}
	
	// Update is called once per frame
	void Update () 
	{
		//accessing player stats
		if (Input.GetKeyDown ("p") && storageMenu.activeSelf == false && !DataStorage.textBox.activeSelf)
		if (!animBool)
				if (levelStats.activeSelf)
					{
					animBool = true;
					levelStats.GetComponent<Animator>().SetBool ("isOn", false);
					levelStats.GetComponent<AudioSource> ().pitch = 1.3f;
					levelStats.GetComponent<AudioSource> ().Play ();
					StartCoroutine (DisableStats (.5f));
					} 
				else 
					{
					animBool = true;
					levelStats.SetActive (true);
					levelStats.GetComponent<Animator>().SetBool ("isOn", true);
					levelStats.GetComponent<AudioSource> ().pitch = 1f;
					levelStats.GetComponent<AudioSource> ().Play ();
					DataStorage.player.GetComponent<Controls> ().enabled = false;
					StartCoroutine (EnableStats (.5f));
					DataStorage.pauseMenus.GetComponent<PauseMenu2> ().enabled = false;
					}//end of else

		//accessing  storage menu
		if (Input.GetKeyDown ("i") && levelStats.activeSelf == false && !DataStorage.textBox.activeSelf)
			if(storageMenu.activeSelf)
		   {
			 	storageMenu.SetActive (false);
				DataStorage.player.GetComponent<Controls> ().enabled = true;
				DataStorage.pauseMenus.GetComponent<PauseMenu2>().enabled = true;

		   } 
			else
			{
				storageMenu.SetActive (true);
				DataStorage.player.GetComponent<Controls> ().enabled = false;
				DataStorage.pauseMenus.GetComponent<PauseMenu2>().enabled = false;
			}
	}//end of update

	IEnumerator DisableStats(float waitTime)
	{
		yield return new WaitForSeconds (waitTime);
		DataStorage.player.GetComponent<Controls> ().enabled = true;
		levelStats.SetActive (false);
		animBool = false;
		DataStorage.pauseMenus.GetComponent<PauseMenu2>().enabled = true;
	}
	IEnumerator EnableStats(float waitTime)
	{
		yield return new WaitForSeconds (waitTime);
		animBool = false;
	}

			}//end of class
