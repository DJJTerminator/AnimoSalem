using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class EnterDybbukShop : MonoBehaviour {
    GameObject exclamation;
    GameObject theShop;
    GameObject sell;
    GameObject upgrade;
    GameObject buy;
    GameObject pause;
    GameObject player;
    public GameObject curMoney;
	GameObject storageMenu;

	void Start()
	{
        //finding all game objects from the start
		theShop = GameObject.Find ("All Canvases/Canvas/TheItemShop");
        sell = GameObject.Find("All Canvases/Canvas/TheItemShop/Sell");
        upgrade = GameObject.Find("All Canvases/Canvas/TheItemShop/Upgrade");
        buy = GameObject.Find("All Canvases/Canvas/TheItemShop/Buy");
        pause = GameObject.Find("All Canvases/Canvas/PauseMenus");
        storageMenu = GameObject.Find("All Canvases/Canvas/StorageMenu");
		player = GameObject.Find ("Player");
		exclamation = GameObject.Find ("Player/PlayerIcons/Exclamation");
        //turning the script off from the start
        gameObject.GetComponent<EnterDybbukShop>().enabled = false;
	}

    void Update()
    {
        //entering the shop
		if (!theShop.activeSelf && Input.GetKey ("return") && !storageMenu.activeSelf) 
        {
			exclamation.SetActive(false);
            theShop.SetActive(true);
            upgrade.SetActive(false);
            sell.SetActive(false);
            buy.SetActive(true);
            curMoney.GetComponent<Text>().text = "$" + DataStorage.money;
            player.GetComponent<Controls>().enabled = false;
            pause.SetActive(false);
           // pause.GetComponent<PauseMenu2>().canUnPause = false;
        }
        //exiting the shop
		if (theShop.activeSelf && Input.GetKeyDown("escape"))
        {
			exclamation.SetActive(true);
            upgrade.SetActive(false);
            sell.SetActive(false);
            buy.SetActive(true);
            theShop.SetActive(false);
            player.GetComponent<Controls>().enabled = true;
            pause.SetActive(true);
           // pause.GetComponent<PauseMenu2>().canUnPause = true;
        }
        if (theShop.activeSelf)
        {
            player.GetComponent<Controls>().enabled = false;
            pause.SetActive(false);
        }

    
    }

    void OnTriggerEnter(Collider other)
    {

        if (other.tag == "Player")
        {

				gameObject.GetComponent<EnterDybbukShop> ().enabled = true;
				exclamation.SetActive (true);
			
        }
    }
         
		void OnTriggerExit(Collider other)
		{
            if (other.tag == "Player")
            {
                exclamation.SetActive(false);
                gameObject.GetComponent<EnterDybbukShop>().enabled = false;
            }
		}
}

