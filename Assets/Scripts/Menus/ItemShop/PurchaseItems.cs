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

	public void HoverOverItems()
	{
		if (!itemBought.isPlaying)
			hoverOver.Play();
	}

	public void HGAmmo()
	{
		if (DataStorage.shopHandgunAmmo > 0)
			if (DataStorage.money > 10)
			{
				DataStorage.money -= 10;
				DataStorage.HGAmmo += 5;
				//adding stats
				DataStorage.moneySpent += 10;
			//display current money and price
			curMoney.GetComponent<Text> ().text = "$" + DataStorage.money;
				//play sound
				itemBought.Play();
				
				//subtracting from the shop
				DataStorage.shopHandgunAmmo -= 1;
			if (DataStorage.shopHandgunAmmo < 1)
				_shop.HGAmmoSoldOut ();

			} 
			else 
			{
			print ("You have no money, stranger");
			//play sound
			noMoney.Play();
		}
		else 
		{
			print ("Sorry, we're sold out");
			//play sound
		}
			
	}

	public void SGAmmo()
	{
	if (DataStorage.shopShotgunAmmo > 0)
		if (DataStorage.money > 10) {
			DataStorage.money -= 10;
			DataStorage.SGAmmo += 2;
			//adding stats
			DataStorage.moneySpent += 10;
			//display current money and price
			curMoney.GetComponent<Text> ().text = "$" + DataStorage.money;
			itemBought.Play();

			//subtracting from the shop
			DataStorage.shopShotgunAmmo -= 1;
			if (DataStorage.shopShotgunAmmo < 1)
			_shop.SGAmmoSoldOut ();
			print (DataStorage.shopShotgunAmmo);
		} 
		else 
		{
			print ("You have no money, stranger");
			//play sound
			noMoney.Play();
		}
		else 
		{
			print ("Sorry, we're sold out");
			//play sound
		}
	}
	public void MGAmmo()
	{
	if (DataStorage.shopMachinegunAmmo > 0)
		if (DataStorage.money > 5)
		{
			DataStorage.money -= 5;
			DataStorage.MGAmmo += 10;
			//adding stats
			DataStorage.moneySpent += 5;
			//display current money and price
			curMoney.GetComponent<Text> ().text = "$" + DataStorage.money;
			itemBought.Play();

			//subtracting from the shop
			DataStorage.shopMachinegunAmmo -= 1;
			if (DataStorage.shopMachinegunAmmo < 1)
				_shop.MGAmmoSoldOut ();
			print (DataStorage.shopMachinegunAmmo);
		} 
		else 
		{
			print ("You have no money, stranger");
			//play sound
			noMoney.Play();
		}
	else 
	{
		print ("Sorry, we're sold out");
		//play sound
	}
	}

	public void RifleAmmo()
	{
	if (DataStorage.shopRifleAmmo > 0)
		if (DataStorage.money > 10) 
		{
			DataStorage.money -= 10;
			DataStorage.rifleAmmo += 2;
			//adding stats
			DataStorage.moneySpent += 10;
			//display current money and price
			curMoney.GetComponent<Text> ().text = "$" + DataStorage.money;
			itemBought.Play();

			//subtracting frmo the shop
			DataStorage.shopRifleAmmo -= 1;
			if (DataStorage.shopRifleAmmo < 1)
				_shop.RifleAmmoSoldOut ();
			print (DataStorage.shopRifleAmmo);
		} 
		else 
		{
			print ("You have no money, stranger");
			//play sound
			noMoney.Play();
		}
	else 
	{
		print ("Sorry, we're sold out");
		//play sound
	}
	}

	public void MagnumAmmo()
	{
	if (DataStorage.shopMagnumAmmo > 0)
		if (DataStorage.money > 10) 
		{
			DataStorage.money -= 10;
			DataStorage.magnumAmmo += 1;
			//adding stats
			DataStorage.moneySpent += 10;
			//display current money and price
			curMoney.GetComponent<Text> ().text = "$" + DataStorage.money;
			itemBought.Play();

			//subtracting frmo the shop
			DataStorage.shopMagnumAmmo -= 1;
			if (DataStorage.shopMagnumAmmo < 1)
				_shop.MagnumAmmoSoldOut ();
			print (DataStorage.shopMagnumAmmo);
		} 
		else 
		{
			print ("You have no money, stranger");
			//play sound
			noMoney.Play();
		}
	else 
	{
		print ("Sorry, we're sold out");
		//play sound
	}
	}


	public void HealItem()
	{

	}
		
}
