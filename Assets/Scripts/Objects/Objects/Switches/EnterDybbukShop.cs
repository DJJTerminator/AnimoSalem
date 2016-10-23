using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class EnterDybbukShop : MonoBehaviour {
    public GameObject exclamation;
    public GameObject theShop;
    public GameObject sell;
    public GameObject upgrade;
    public GameObject buy;
    public GameObject pause;
    public GameObject player;
    public GameObject curMoney;


    void Update()
    {
        if (!theShop.activeSelf && Input.GetKey ("return")) 
        {
            theShop.SetActive(true);
            upgrade.SetActive(false);
            sell.SetActive(false);
            buy.SetActive(true);
            curMoney.GetComponent<Text>().text = "$" + DataStorage.money;
            player.GetComponent<Controls>().enabled = false;
            pause.SetActive(false);
           // pause.GetComponent<PauseMenu2>().canUnPause = false;
        }
        if (theShop.activeSelf && Input.GetKeyDown("escape"))
        {
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
            gameObject.GetComponent<EnterDybbukShop>().enabled = true;
            exclamation.SetActive(true);
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

