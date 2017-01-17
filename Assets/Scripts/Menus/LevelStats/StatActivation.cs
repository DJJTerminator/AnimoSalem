using UnityEngine;
using System.Collections;

public class StatActivation : MonoBehaviour 
{

	bool animBool = false;
	// Use this for initialization

	
	// Update is called once per frame
	void Update () 
	{
		//accessing player stats
		if (Input.GetKeyDown ("c"))
		if (!animBool)//accessing the UI (stats) and play the correct animaitons and audio depending upon whether it was enabled or disabled
			if (!DataStorage.canDo)
					{
					animBool = true;
                    DataStorage.levelStats.GetComponent<Animator>().SetBool ("isOn", false);
					DataStorage.levelStats.GetComponent<AudioSource> ().pitch = 1.3f;
					DataStorage.levelStats.GetComponent<AudioSource> ().Play ();
					StartCoroutine (DisableStats (.5f));
					} 
				else 
					{
					try 
					{
						DataStorage.HUD.SetActive(false);
					}
					catch
					{
						DataStorage.HUD = GameObject.Find("All Canvases/Canvas/HUD");
						DataStorage.HUD.SetActive(false);
					}
                    DataStorage.player.GetComponent<Controls>().healing.Stop();
                    DataStorage.gameManager.GetComponent<InventoryActivation> ().enabled = false;
					DataStorage.canDo = false;
					animBool = true;
					DataStorage.levelStats.SetActive (true);
					DataStorage.levelStats.GetComponent<Animator>().SetBool ("isOn", true);
					DataStorage.levelStats.GetComponent<AudioSource> ().pitch = 1f;
					DataStorage.levelStats.GetComponent<AudioSource> ().Play ();
					DataStorage.player.GetComponent<Controls> ().enabled = false;
					DataStorage.pauseMenus.GetComponent<PauseMenu2> ().enabled = false;
					StartCoroutine (EnableStats (.5f));
					}//end of else

	}//end of update
	//waiting before the UI disables
	IEnumerator DisableStats(float waitTime)
	{
		yield return new WaitForSeconds (waitTime);
		DataStorage.player.GetComponent<Controls> ().enabled = true;
		DataStorage.levelStats.SetActive (false);
		animBool = false;
		DataStorage.pauseMenus.GetComponent<PauseMenu2>().enabled = true;
		DataStorage.canDo = true;
		DataStorage.gameManager.GetComponent<InventoryActivation> ().enabled = true;
		DataStorage.HUD.SetActive(true);
	}

	IEnumerator EnableStats(float waitTime)
	{
		yield return new WaitForSeconds (waitTime);
		animBool = false;
	}

			}//end of class
