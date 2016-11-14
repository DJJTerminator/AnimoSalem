using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class PurchaseItems : MonoBehaviour {

	public GameObject _GameManager;
	public ShopUpkeep _shop;
	[SerializeField]
	Text curMoney;
	public AudioSource itemBought;
	public AudioSource noMoney;
	public AudioSource outOfStock;
	public AudioSource hoverOver;
	GameObject needMoney1;
	GameObject needMoney2;
	GameObject needMoney3;
	GameObject needMoney4;
	GameObject needMoney5;


	void Start ()
	{
		needMoney1 = GameObject.Find("All Canvases/Canvas/TheItemShop/Buy/Ammo/PurchaseHGAmmo/NoMoney");
		needMoney2 = GameObject.Find("All Canvases/Canvas/TheItemShop/Buy/Ammo/PurchaseSGAmmo/NoMoney");
		needMoney3 = GameObject.Find("All Canvases/Canvas/TheItemShop/Buy/Ammo/PurchaseMGAmmo/NoMoney");
		needMoney4 = GameObject.Find("All Canvases/Canvas/TheItemShop/Buy/Ammo/PurchaseRifleAmmo/NoMoney");
		needMoney5 = GameObject.Find("All Canvases/Canvas/TheItemShop/Buy/Ammo/PurchaseMagnumAmmo/NoMoney");
	}

	public void HoverOverItems()
	{
		if (!itemBought.isPlaying)
			hoverOver.Play();
	}


	IEnumerator WaitAndDisable(float waitTime) 
	{
		yield return new WaitForSeconds(waitTime);
		//disabling all animations
		needMoney1.SetActive (false);
		needMoney2.SetActive (false);
		needMoney3.SetActive (false);
		needMoney4.SetActive (false);
		needMoney5.SetActive (false);
	}

	public void SoldOut()
	{
		print ("Sorry, We're sold out!");
		//play sound
	}
	public void NoMoney(int mySwitch)
	{
		print ("You don't have enough money!");
		//play sound
		StopCoroutine("WaitAndDisable");

		switch (mySwitch)
		{
		default:
			needMoney1.SetActive (true);
			StartCoroutine (WaitAndDisable (.5f));
			break;
		case 2:
			needMoney2.SetActive (true);
			StartCoroutine (WaitAndDisable (.5f));
			print("yes");
			break;
		case 3:
			needMoney3.SetActive (true);
			StartCoroutine (WaitAndDisable (.5f));
			print("yes");
			break;
		case 4:
			needMoney4.SetActive (true);
			StartCoroutine (WaitAndDisable (.5f));
			break;
		case 5:
			needMoney5.SetActive (true);
			StartCoroutine (WaitAndDisable (.5f));
			break;
		}
	}

	//returning the exchange
	public float Exchange(float value)
	{
		//DataStorage.money - value;
		if (DataStorage.charisma > value * .4f)
			value -= value * .4f;
		else
			value -= DataStorage.charisma;
		return value;
	}
	public void HGAmmo()
	{
		float price = Exchange (25);

		if (DataStorage.shopHandgunAmmo > 0)
		if (DataStorage.money > price)
		{
			DataStorage.HGAmmo += 5;
			//play sound
			itemBought.Play ();
			DataStorage.money -= (int)price;
			DataStorage.moneySpent += (int)price;

			//display current money and price
			curMoney.GetComponent<Text> ().text = "$" + DataStorage.money;
			//subtracting from the shop
			DataStorage.shopHandgunAmmo -= 1;
			//play sound
			itemBought.Play ();
			//checking to see if the player bought the last one
			if (DataStorage.shopHandgunAmmo < 1)
				_shop.HGAmmoSoldOut ();
		} 
		else 
		{
			NoMoney (1);
		}
		else 
		{
			SoldOut ();
		}
	}//end of function

	public void SGAmmo()
	{
		float price = Exchange (35);

		if (DataStorage.shopShotgunAmmo > 0)
		if (DataStorage.money > price)
		{
			DataStorage.SGAmmo += 2;
			//play sound
			itemBought.Play ();
			DataStorage.money -= (int)price;
			DataStorage.moneySpent += (int)price;

			//display current money and price
			curMoney.GetComponent<Text> ().text = "$" + DataStorage.money;
			//subtracting from the shop
			DataStorage.shopShotgunAmmo -= 1;
			//play sound
			itemBought.Play ();
			//checking to see if the player bought the last one
			if (DataStorage.shopShotgunAmmo < 1)
				_shop.SGAmmoSoldOut ();
		} 
		else 
		{
			NoMoney (2);
		}
		else 
		{
			SoldOut ();
		}
	}//end of function

	public void MGAmmo()
	{
		float price = Exchange (25);

		if (DataStorage.shopMachinegunAmmo > 0)
		if (DataStorage.money > price)
		{
			DataStorage.MGAmmo += 2;
			//play sound
			itemBought.Play ();
			DataStorage.money -= (int)price;
			DataStorage.moneySpent += (int)price;

			//display current money and price
			curMoney.GetComponent<Text> ().text = "$" + DataStorage.money;
			//subtracting from the shop
			DataStorage.shopMachinegunAmmo -= 1;
			//play sound
			itemBought.Play ();
			//checking to see if the player bought the last one
			if (DataStorage.shopMachinegunAmmo < 1)
				_shop.MGAmmoSoldOut ();
		} 
		else 
		{
			NoMoney (3);
		}
		else 
		{
			SoldOut ();
		}
	}//end of function

	public void RifleAmmo()
	{
		float price = Exchange (25);

		if (DataStorage.shopRifleAmmo > 0)
		if (DataStorage.money > price)
		{
			DataStorage.rifleAmmo += 2;
			//play sound
			itemBought.Play ();
			DataStorage.money -= (int)price;
			DataStorage.moneySpent += (int)price;

			//display current money and price
			curMoney.GetComponent<Text> ().text = "$" + DataStorage.money;
			//subtracting from the shop
			DataStorage.shopRifleAmmo -= 1;
			//play sound
			itemBought.Play ();
			//checking to see if the player bought the last one
			if (DataStorage.shopRifleAmmo < 1)
				_shop.RifleAmmoSoldOut ();
		} 
		else 
		{
			NoMoney (4);
		}
		else 
		{
			SoldOut ();
		}
	}//end of function

	public void MagnumAmmo()
	{
		float price = Exchange (35);

		if (DataStorage.shopMagnumAmmo > 0)
		if (DataStorage.money > price)
		{
			DataStorage.magnumAmmo += 5;
			//play sound
			itemBought.Play ();
			DataStorage.money -= (int)price;
			DataStorage.moneySpent += (int)price;

			//display current money and price
			curMoney.GetComponent<Text> ().text = "$" + DataStorage.money;
			//subtracting from the shop
			DataStorage.shopMagnumAmmo -= 1;
			//play sound
			itemBought.Play ();
			//checking to see if the player bought the last one
			if (DataStorage.shopMagnumAmmo < 1)
				_shop.MagnumAmmoSoldOut ();
		} 
		else 
		{
			NoMoney (5);
		}
		else 
		{
			SoldOut ();
		}
	}//end of function


	public void HealItem()
	{

	}
		
}
