using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class EnterDybbukShop : MonoBehaviour {

    public GameObject curMoney;


	void Start()
	{
        //turning the script off from the start
        gameObject.GetComponent<EnterDybbukShop>().enabled = false;
	}

    void Update()
    {
			//entering the shop
		if (!DataStorage.theShop.activeSelf && Input.GetKeyDown ("return") && DataStorage.canDo && !DataStorage.textBox.activeSelf)
			 {
				DataStorage.exclamation.SetActive (false);
				DataStorage.theShop.SetActive (true);
				DataStorage.upgrade.SetActive (false);
				DataStorage.sell.SetActive (false);
				DataStorage.buy.SetActive (true);
				curMoney.GetComponent<Text> ().text = "$" + DataStorage.money;
				DataStorage.player.GetComponent<Controls> ().enabled = false;
				DataStorage.pauseMenus.SetActive (false);
				DataStorage.gameManager.GetComponent<StatActivation> ().enabled = false;
				DataStorage.gameManager.GetComponent<InventoryActivation> ().enabled = false;
				DataStorage.canDo = false;
			}
			//exiting the shop
			if (DataStorage.theShop.activeSelf && Input.GetKeyDown ("escape")) {
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

