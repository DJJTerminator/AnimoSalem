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
		if (_GameManager.GetComponent<DataStorage> ().shopHandgunAmmo > 0)
			if (_GameManager.GetComponent<DataStorage> ().money > 10)
			{
				_GameManager.GetComponent<DataStorage> ().money -= 10;
				_GameManager.GetComponent<DataStorage> ().HGAmmo += 5;
				//adding stats
				_GameManager.GetComponent<DataStorage> ().moneySpent += 10;
			//display current money and price
			curMoney.GetComponent<Text> ().text = "$" + _GameManager.GetComponent<DataStorage> ().money;
				//play sound
				itemBought.Play();
				
				//subtracting from the shop
				_GameManager.GetComponent<DataStorage> ().shopHandgunAmmo -= 1;
			if (_GameManager.GetComponent<DataStorage> ().shopHandgunAmmo < 1)
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
	if (_GameManager.GetComponent<DataStorage> ().shopShotgunAmmo > 0)
		if (_GameManager.GetComponent<DataStorage> ().money > 10) {
			_GameManager.GetComponent<DataStorage> ().money -= 10;
			_GameManager.GetComponent<DataStorage> ().SGAmmo += 2;
			//adding stats
			_GameManager.GetComponent<DataStorage> ().moneySpent += 10;
			//display current money and price
			curMoney.GetComponent<Text> ().text = "$" + _GameManager.GetComponent<DataStorage> ().money;
			itemBought.Play();

			//subtracting from the shop
			_GameManager.GetComponent<DataStorage> ().shopShotgunAmmo -= 1;
			if (_GameManager.GetComponent<DataStorage> ().shopShotgunAmmo < 1)
			_shop.SGAmmoSoldOut ();
			print (_GameManager.GetComponent<DataStorage> ().shopShotgunAmmo);
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
	if (_GameManager.GetComponent<DataStorage> ().shopMachinegunAmmo > 0)
		if (_GameManager.GetComponent<DataStorage> ().money > 5)
		{
			_GameManager.GetComponent<DataStorage> ().money -= 5;
			_GameManager.GetComponent<DataStorage> ().MGAmmo += 10;
			//adding stats
			_GameManager.GetComponent<DataStorage> ().moneySpent += 5;
			//display current money and price
			curMoney.GetComponent<Text> ().text = "$" + _GameManager.GetComponent<DataStorage> ().money;
			itemBought.Play();

			//subtracting from the shop
			_GameManager.GetComponent<DataStorage> ().shopMachinegunAmmo -= 1;
			if (_GameManager.GetComponent<DataStorage> ().shopMachinegunAmmo < 1)
				_shop.MGAmmoSoldOut ();
			print (_GameManager.GetComponent<DataStorage> ().shopMachinegunAmmo);
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
	if (_GameManager.GetComponent<DataStorage> ().shopRifleAmmo > 0)
		if (_GameManager.GetComponent<DataStorage> ().money > 10) 
		{
			_GameManager.GetComponent<DataStorage> ().money -= 10;
			_GameManager.GetComponent<DataStorage> ().rifleAmmo += 2;
			//adding stats
			_GameManager.GetComponent<DataStorage> ().moneySpent += 10;
			//display current money and price
			curMoney.GetComponent<Text> ().text = "$" + _GameManager.GetComponent<DataStorage> ().money;
			itemBought.Play();

			//subtracting frmo the shop
			_GameManager.GetComponent<DataStorage> ().shopRifleAmmo -= 1;
			if (_GameManager.GetComponent<DataStorage> ().shopRifleAmmo < 1)
				_shop.RifleAmmoSoldOut ();
			print (_GameManager.GetComponent<DataStorage> ().shopRifleAmmo);
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
	if (_GameManager.GetComponent<DataStorage> ().shopMagnumAmmo > 0)
		if (_GameManager.GetComponent<DataStorage> ().money > 10) 
		{
			_GameManager.GetComponent<DataStorage> ().money -= 10;
			_GameManager.GetComponent<DataStorage> ().magnumAmmo += 1;
			//adding stats
			_GameManager.GetComponent<DataStorage> ().moneySpent += 10;
			//display current money and price
			curMoney.GetComponent<Text> ().text = "$" + _GameManager.GetComponent<DataStorage> ().money;
			itemBought.Play();

			//subtracting frmo the shop
			_GameManager.GetComponent<DataStorage> ().shopMagnumAmmo -= 1;
			if (_GameManager.GetComponent<DataStorage> ().shopMagnumAmmo < 1)
				_shop.MagnumAmmoSoldOut ();
			print (_GameManager.GetComponent<DataStorage> ().shopMagnumAmmo);
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
