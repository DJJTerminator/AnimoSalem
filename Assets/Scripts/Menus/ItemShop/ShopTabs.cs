using UnityEngine;
using System.Collections;

public class ShopTabs : MonoBehaviour {
	public GameObject buy;
	public GameObject sell;
	public GameObject upgrade;


	public void Buy()
	{
		buy.SetActive (true);
		sell.SetActive (false);
		upgrade.SetActive (false);
        //calling this function to update the UI
        upgrade.GetComponent<UpgradeItems>().HoverOverDamage();

        //play sound
    }

	public void Sell()
	{
		buy.SetActive (false);
		sell.SetActive (true);
		upgrade.SetActive (false);
		//play sound
	}

	public void Upgrade()
	{
		buy.SetActive (false);
		sell.SetActive (false);
		upgrade.SetActive (true);
		//play sound

		//calling this function to update the UI
		upgrade.GetComponent<UpgradeItems>().HoverOverDamage ();
	}

}
