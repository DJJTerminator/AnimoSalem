using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class EnterDybbukShop : MonoBehaviour {

    Text curMoney;


	void Start()
	{
        //turning the script off from the start
        gameObject.GetComponent<EnterDybbukShop>().enabled = false;
		curMoney  = GameObject.Find ("All Canvases/Canvas/TheItemShop/CurrentMoney").GetComponent<Text>();
	}

    void Update()
    {
			//entering the shop
		if (!DataStorage.theShop.activeSelf && Input.GetKeyDown ("return") && DataStorage.canDo && !DataStorage.textBox.activeSelf)
			 {
            //changing the track for the music
            //variables that are passed musicType, waitime, and a bool to check if the music can play
            MusicScript.PrepareTrack(1, 0f, true);
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
                DataStorage.exclamation.SetActive (false);
				DataStorage.theShop.SetActive (true);
				DataStorage.upgrade.SetActive (false);
				DataStorage.sell.SetActive (false);
				DataStorage.buy.SetActive (true);
				curMoney.text = "$" + DataStorage.money.ToString("n0");
				DataStorage.player.GetComponent<Controls> ().enabled = false;
				DataStorage.pauseMenus.SetActive (false);
				DataStorage.gameManager.GetComponent<StatActivation> ().enabled = false;
				DataStorage.gameManager.GetComponent<InventoryActivation> ().enabled = false;
				DataStorage.canDo = false;
			}
			//exiting the shop
			if (DataStorage.theShop.activeSelf && Input.GetKeyDown ("escape"))
        {
                //changing the track for the music
                //variables that are passed musicType, waitime, and a bool to check if the music can play
                MusicScript.PrepareTrack(0, 0f, true);
                DataStorage.exclamation.SetActive (true);
				DataStorage.upgrade.SetActive (false);
				DataStorage.sell.SetActive (false);
				DataStorage.buy.SetActive (true);
				DataStorage.theShop.SetActive (false);
				DataStorage.player.GetComponent<Controls> ().enabled = true;
				DataStorage.pauseMenus.SetActive (true);
				DataStorage.gameManager.GetComponent<StatActivation> ().enabled = true;
				DataStorage.gameManager.GetComponent<InventoryActivation> ().enabled = true;
				DataStorage.canDo = true;
				DataStorage.HUD.SetActive(true);
			}
			if (DataStorage.theShop.activeSelf) {
				DataStorage.player.GetComponent<Controls> ().enabled = false;
				DataStorage.pauseMenus.SetActive (false);
			}
    }

    void OnTriggerEnter(Collider other)
    {

        if (other.tag == "Player")
        {
			gameObject.GetComponent<EnterDybbukShop> ().enabled = true;
			DataStorage.exclamation.SetActive (true);

        }
    }
		void OnTriggerExit(Collider other)
		{
            if (other.tag == "Player")
            {

				DataStorage.exclamation.SetActive (false);
				gameObject.GetComponent<EnterDybbukShop> ().enabled = false;
	
            }
		}
}

