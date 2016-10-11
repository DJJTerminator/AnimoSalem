using UnityEngine;
using System.Collections;
using UnityEngine.UI;


public class UpgradeItems : MonoBehaviour {
	int myIndex;
	public GameObject _GameManager;
	public GameObject upgradeSelect;
	[SerializeField]
	Text curText;
	[SerializeField]
	Text nextText;
	[SerializeField]
	Text curMoney;
	[SerializeField]
	Text price;
	[SerializeField]
	Text _weaponName;

	[SerializeField]
	GameObject[] myActive;
	int index;

	[SerializeField]
	Texture[] curWeapon;

	//cycling weapons to the left
	public void WeaponCycleLeft()
	{
		if (_GameManager.GetComponent<DataStorage> ().curWeapon <= 0)
			_GameManager.GetComponent<DataStorage> ().curWeapon = _GameManager.GetComponent<DataStorage> ().obtainedWeapons.Length - 1;
		else
			_GameManager.GetComponent<DataStorage> ().curWeapon -= 1;

		int i = _GameManager.GetComponent<DataStorage> ().obtainedWeapons.Length - 1;

		while (i > 0) 
		{
			//looping through the weapons to see which have been obtained and which have not
			if (_GameManager.GetComponent<DataStorage> ().obtainedWeapons[_GameManager.GetComponent<DataStorage> ().curWeapon] != 1) 
			{
				//No weapon has been found so I am pushing through the loop
				if (_GameManager.GetComponent<DataStorage> ().curWeapon > 0) 
				{
					_GameManager.GetComponent<DataStorage> ().curWeapon -= 1;
					i--;
				} 
				else 
				{
					_GameManager.GetComponent<DataStorage> ().curWeapon = _GameManager.GetComponent<DataStorage> ().obtainedWeapons.Length - 1;
					i--;
				}
			} 
			else 
			{
				//A weapon has been found so I am breaking the while loop
				_weaponName.GetComponent<Text> ().text = _GameManager.GetComponent<DataStorage> ().weaponName [_GameManager.GetComponent<DataStorage> ().curWeapon];
				upgradeSelect.GetComponent<RawImage> ().texture = curWeapon [_GameManager.GetComponent<DataStorage> ().curWeapon];
				HoverOverDamage ();
				break;
			}
		}
	}

	//cycling weapons to the right
	public void WeaponCycleRight()
	{
		if (_GameManager.GetComponent<DataStorage> ().curWeapon >= _GameManager.GetComponent<DataStorage> ().obtainedWeapons.Length - 1)
			_GameManager.GetComponent<DataStorage> ().curWeapon = 0;
		else
			_GameManager.GetComponent<DataStorage> ().curWeapon += 1;

		int i = _GameManager.GetComponent<DataStorage> ().obtainedWeapons.Length - 1;

		while (i > 0) 
		{
			//looping through the weapons to see which have been obtained and which have not

			if (_GameManager.GetComponent<DataStorage> ().obtainedWeapons[_GameManager.GetComponent<DataStorage> ().curWeapon] != 1) 
			{
				//No weapon has been found so I am pushing through the loop
				if (_GameManager.GetComponent<DataStorage> ().curWeapon < _GameManager.GetComponent<DataStorage> ().obtainedWeapons.Length - 1) 
				{
					_GameManager.GetComponent<DataStorage> ().curWeapon += 1;
					i--;
				} 
				else 
				{
					_GameManager.GetComponent<DataStorage> ().curWeapon = 0;
					i--;
				}
			} 
			else 
			{
				//A weapon has been found so I am breaking the while loop
				_weaponName.GetComponent<Text> ().text = _GameManager.GetComponent<DataStorage> ().weaponName [_GameManager.GetComponent<DataStorage> ().curWeapon];
				upgradeSelect.GetComponent<RawImage> ().texture = curWeapon [_GameManager.GetComponent<DataStorage> ().curWeapon];
				HoverOverDamage ();
				break;
			}
		}
	}




	//these are the hover over functions
	public void HoverOverDamage()
	{
		if (_GameManager.GetComponent<DataStorage> ().curDamage [_GameManager.GetComponent<DataStorage> ().curWeapon] > 0) 
		{
			myIndex = _GameManager.GetComponent<DataStorage> ().curDamage [_GameManager.GetComponent<DataStorage> ().curWeapon] -1;
			ActivateUpgrade (myIndex);
		}else
			DeactivateUpgrades ();

		//display image
		upgradeSelect.GetComponent<RawImage> ().texture = curWeapon [_GameManager.GetComponent<DataStorage> ().curWeapon];

		//display weapon name
		_weaponName.GetComponent<Text> ().text = _GameManager.GetComponent<DataStorage> ().weaponName [_GameManager.GetComponent<DataStorage> ().curWeapon];

		//display current upgrade and next upgrade
		curText.GetComponent<Text> ().text = "Damage: " + _GameManager.GetComponent<DataStorage> ().weaponDamage [_GameManager.GetComponent<DataStorage> ().curWeapon];
		//upgradeSelect.GetComponent<RawImage>().texture = _damage;
		if (_GameManager.GetComponent<DataStorage> ().curDamage [_GameManager.GetComponent<DataStorage> ().curWeapon] < 5)
			nextText.GetComponent<Text> ().text = "Next Level: " + (_GameManager.GetComponent<DataStorage> ().weaponDamage [_GameManager.GetComponent<DataStorage> ().curWeapon] + 1);
		else
			nextText.GetComponent<Text> ().text = " ";	

			

		//display current money and price
		curMoney.GetComponent<Text> ().text = "$" + _GameManager.GetComponent<DataStorage> ().money;
		if (_GameManager.GetComponent<DataStorage> ().curDamage[_GameManager.GetComponent<DataStorage> ().curWeapon] < 5)
		price.GetComponent<Text> ().text = "Cost: $" + _GameManager.GetComponent<DataStorage> ().damageCost[_GameManager.GetComponent<DataStorage> ().curWeapon];
	}
	public void HoverOverReload()
	{
		//updates current upgrade
		if (_GameManager.GetComponent<DataStorage> ().curReload [_GameManager.GetComponent<DataStorage> ().curWeapon] > 0) 
		{
			myIndex = _GameManager.GetComponent<DataStorage> ().curReload [_GameManager.GetComponent<DataStorage> ().curWeapon] -1;
			ActivateUpgrade (myIndex);
		}else
			DeactivateUpgrades ();

		//display current upgrade and next upgrade
		curText.GetComponent<Text> ().text = "Reload: " + _GameManager.GetComponent<DataStorage> ().reload [_GameManager.GetComponent<DataStorage> ().curWeapon];
		if (_GameManager.GetComponent<DataStorage> ().curReload [_GameManager.GetComponent<DataStorage> ().curWeapon] < 5)
		nextText.GetComponent<Text> ().text = "Next Level: " + (_GameManager.GetComponent<DataStorage> ().reload [_GameManager.GetComponent<DataStorage> ().curWeapon] + 1);
		else
			nextText.GetComponent<Text> ().text = " ";	

		//upgradeSelect.GetComponent<RawImage>().texture = _reload;

		curMoney.GetComponent<Text> ().text = "$" + _GameManager.GetComponent<DataStorage> ().money;
		price.GetComponent<Text> ().text = "Cost: $" + _GameManager.GetComponent<DataStorage> ().reloadCost[_GameManager.GetComponent<DataStorage> ().curWeapon];
	}
	public void HoverOverCapacity()
	{

		//updates current upgrade
		if (_GameManager.GetComponent<DataStorage> ().curCapacity [_GameManager.GetComponent<DataStorage> ().curWeapon] > 0) 
		{
			myIndex = _GameManager.GetComponent<DataStorage> ().curCapacity [_GameManager.GetComponent<DataStorage> ().curWeapon] -1;
			ActivateUpgrade (myIndex);
		}else
			DeactivateUpgrades ();

		//display current upgrade and next upgrade
		curText.GetComponent<Text> ().text = "Capacity: " + _GameManager.GetComponent<DataStorage> ().capacity [_GameManager.GetComponent<DataStorage> ().curWeapon];
		if (_GameManager.GetComponent<DataStorage> ().curCapacity [_GameManager.GetComponent<DataStorage> ().curWeapon] < 5)
		nextText.GetComponent<Text> ().text = "Next Level: " + (_GameManager.GetComponent<DataStorage> ().capacity [_GameManager.GetComponent<DataStorage> ().curWeapon] + 1);
		else
			nextText.GetComponent<Text> ().text = " ";	

		//upgradeSelect.GetComponent<RawImage>().texture = _capacity;

		//display current money and price
		curMoney.GetComponent<Text> ().text = "$" + _GameManager.GetComponent<DataStorage> ().money;
		price.GetComponent<Text> ().text = "Cost: $" + _GameManager.GetComponent<DataStorage> ().capacityCost[_GameManager.GetComponent<DataStorage> ().curWeapon];
	}
	public void HoverOverFireRate()
	{
		//updates current upgrade
		if (_GameManager.GetComponent<DataStorage> ().curFireRate [_GameManager.GetComponent<DataStorage> ().curWeapon] > 0) 
		{
			myIndex = _GameManager.GetComponent<DataStorage> ().curFireRate [_GameManager.GetComponent<DataStorage> ().curWeapon] -1;
			ActivateUpgrade (myIndex);
		}else
			DeactivateUpgrades ();

		//display current upgrade and next upgrade
		curText.GetComponent<Text> ().text = "Fire Rate: " + _GameManager.GetComponent<DataStorage> ().fireRate [_GameManager.GetComponent<DataStorage> ().curWeapon];
		if (_GameManager.GetComponent<DataStorage> ().curFireRate [_GameManager.GetComponent<DataStorage> ().curWeapon] < 5)
		nextText.GetComponent<Text> ().text = "Next Level: " + (_GameManager.GetComponent<DataStorage> ().fireRate [_GameManager.GetComponent<DataStorage> ().curWeapon] + 1);
		else
			nextText.GetComponent<Text> ().text = " ";	

		//upgradeSelect.GetComponent<RawImage>().texture = _fireRate;

		//display current money and price
		curMoney.GetComponent<Text> ().text = "$" + _GameManager.GetComponent<DataStorage> ().money;
		price.GetComponent<Text> ().text = "Cost: $" + _GameManager.GetComponent<DataStorage> ().frCost[_GameManager.GetComponent<DataStorage> ().curWeapon];
	}
	public void HoverOverCritical()
	{
		//updates current upgrade
		if (_GameManager.GetComponent<DataStorage> ().curCrit [_GameManager.GetComponent<DataStorage> ().curWeapon] > 0) 
		{
			myIndex = _GameManager.GetComponent<DataStorage> ().curCrit [_GameManager.GetComponent<DataStorage> ().curWeapon] -1;
			ActivateUpgrade (myIndex);
		}else
			DeactivateUpgrades ();

		//display current upgrade and next upgrade
		curText.GetComponent<Text> ().text = "Critical Hit: " + (_GameManager.GetComponent<DataStorage> ().criticalChance [_GameManager.GetComponent<DataStorage> ().curWeapon] * 100) + "%";
		if (_GameManager.GetComponent<DataStorage> ().curCrit [_GameManager.GetComponent<DataStorage> ().curWeapon] < 5)
		nextText.GetComponent<Text> ().text = "Next Level: " + Mathf.Round((_GameManager.GetComponent<DataStorage> ().criticalChance [_GameManager.GetComponent<DataStorage> ().curWeapon]+.02f)*1000.0f)/10.0f + "%";
		else
			nextText.GetComponent<Text> ().text = " ";	
		//upgradeSelect.GetComponent<RawImage>().texture = _critical;

		//display current money and price
		curMoney.GetComponent<Text> ().text = "$" + _GameManager.GetComponent<DataStorage> ().money;
		price.GetComponent<Text> ().text = "Cost: $" + _GameManager.GetComponent<DataStorage> ().CCCost[_GameManager.GetComponent<DataStorage> ().curWeapon];
	}
	public void HoverOverAccuracy()
	{
		//updates current upgrade
		if (_GameManager.GetComponent<DataStorage> ().curAccuracy [_GameManager.GetComponent<DataStorage> ().curWeapon] > 0) 
		{
			myIndex = _GameManager.GetComponent<DataStorage> ().curAccuracy [_GameManager.GetComponent<DataStorage> ().curWeapon] -1;
			ActivateUpgrade (myIndex);
		}else
			DeactivateUpgrades ();

		//display current upgrade and next upgrade
		curText.GetComponent<Text> ().text = "Accuracy: " + _GameManager.GetComponent<DataStorage> ().accuracy[_GameManager.GetComponent<DataStorage> ().curWeapon];
		if (_GameManager.GetComponent<DataStorage> ().curAccuracy [_GameManager.GetComponent<DataStorage> ().curWeapon] < 5)
		nextText.GetComponent<Text> ().text = "Next Level: " + (_GameManager.GetComponent<DataStorage> ().accuracy [_GameManager.GetComponent<DataStorage> ().curWeapon] + 1);
		else
			nextText.GetComponent<Text> ().text = " ";	

		//upgradeSelect.GetComponent<RawImage>().texture = _accuracy;

		//display current money and price
		curMoney.GetComponent<Text> ().text = "$" + _GameManager.GetComponent<DataStorage> ().money;
		price.GetComponent<Text> ().text = "Cost: $" + _GameManager.GetComponent<DataStorage> ().acCost[_GameManager.GetComponent<DataStorage> ().curWeapon];
	}
	public void HoverOverRange()
	{
		//updates current upgrade
		if (_GameManager.GetComponent<DataStorage> ().curRange [_GameManager.GetComponent<DataStorage> ().curWeapon] > 0) {
			myIndex = _GameManager.GetComponent<DataStorage> ().curRange [_GameManager.GetComponent<DataStorage> ().curWeapon] - 1;
			ActivateUpgrade (myIndex);
		} else
			DeactivateUpgrades ();

		//display current upgrade and next upgrade
		curText.GetComponent<Text> ().text = "Range: " + _GameManager.GetComponent<DataStorage> ().range [_GameManager.GetComponent<DataStorage> ().curWeapon];
		if (_GameManager.GetComponent<DataStorage> ().curRange [_GameManager.GetComponent<DataStorage> ().curWeapon] < 5)
		nextText.GetComponent<Text> ().text = "Next Level: " + (_GameManager.GetComponent<DataStorage>().range [_GameManager.GetComponent<DataStorage> ().curWeapon] + 1);
		else
			nextText.GetComponent<Text> ().text = " ";	

		//upgradeSelect.GetComponent<RawImage>().texture = _range;



		//display current money and price
		curMoney.GetComponent<Text> ().text = "$" + _GameManager.GetComponent<DataStorage> ().money;
		price.GetComponent<Text> ().text = "Cost: $" + _GameManager.GetComponent<DataStorage> ().rangeCost[_GameManager.GetComponent<DataStorage> ().curWeapon];
	}



	//Upgrade Damage of current equiped weapon
	public void UpDamage()
	{
		//upgrade damage and sbtracting the cost
		//checking to see if weapon is not fully maxed
		if (_GameManager.GetComponent<DataStorage> ().curDamage[_GameManager.GetComponent<DataStorage> ().curWeapon] < 5)
			//checking to see if the player has enough money
		if (_GameManager.GetComponent<DataStorage> ().money > _GameManager.GetComponent<DataStorage> ().damageCost[_GameManager.GetComponent<DataStorage> ().curWeapon]) {
			_GameManager.GetComponent<DataStorage> ().money -= _GameManager.GetComponent<DataStorage> ().damageCost [_GameManager.GetComponent<DataStorage> ().curWeapon];
			_GameManager.GetComponent<DataStorage> ().damageCost [_GameManager.GetComponent<DataStorage> ().curWeapon] += 25;
			_GameManager.GetComponent<DataStorage> ().weaponDamage[_GameManager.GetComponent<DataStorage> ().curWeapon] += 1;
			_GameManager.GetComponent<DataStorage> ().sellValue[_GameManager.GetComponent<DataStorage> ().curWeapon] += 8;
			//adding stats
			_GameManager.GetComponent<DataStorage> ().moneySpent += _GameManager.GetComponent<DataStorage> ().damageCost [_GameManager.GetComponent<DataStorage> ().curWeapon];
			//play sound

			//display current upgrade and next upgrade
			curText.GetComponent<Text> ().text = "Damage: " + _GameManager.GetComponent<DataStorage> ().weaponDamage [_GameManager.GetComponent<DataStorage> ().curWeapon];
			if (_GameManager.GetComponent<DataStorage> ().curDamage[_GameManager.GetComponent<DataStorage> ().curWeapon] < 5)
			nextText.GetComponent<Text> ().text = "Next Level: " + (_GameManager.GetComponent<DataStorage> ().weaponDamage [_GameManager.GetComponent<DataStorage> ().curWeapon] + 1);
			//adding the current upgrade level
			_GameManager.GetComponent<DataStorage> ().curDamage [_GameManager.GetComponent<DataStorage> ().curWeapon] += 1;
			//display current money and price
			HoverOverDamage();

			//updates current upgrade
			myIndex = _GameManager.GetComponent<DataStorage> ().curDamage[_GameManager.GetComponent<DataStorage> ().curWeapon] - 1;
			ActivateUpgrade(myIndex);
		} 
		else 
		{
			print ("You have no money, stranger");
			//play sound
		}
	}


	//Upgrade Reload of current equiped weapon
	public void UpReload()
	{
		GetComponent<AudioSource>().Play();

		//checking to see if weapon is not fully maxed
		if (_GameManager.GetComponent<DataStorage> ().curReload[_GameManager.GetComponent<DataStorage> ().curWeapon] < 5)
		if (_GameManager.GetComponent<DataStorage> ().money > _GameManager.GetComponent<DataStorage> ().reloadCost[_GameManager.GetComponent<DataStorage> ().curWeapon]) {
			_GameManager.GetComponent<DataStorage> ().money -= _GameManager.GetComponent<DataStorage> ().reloadCost [_GameManager.GetComponent<DataStorage> ().curWeapon];
			_GameManager.GetComponent<DataStorage> ().reloadCost [_GameManager.GetComponent<DataStorage> ().curWeapon] += 25;
			_GameManager.GetComponent<DataStorage> ().reload[_GameManager.GetComponent<DataStorage> ().curWeapon] += 1;
			_GameManager.GetComponent<DataStorage> ().sellValue[_GameManager.GetComponent<DataStorage> ().curWeapon] += 8;
			//adding stats
			_GameManager.GetComponent<DataStorage> ().moneySpent += _GameManager.GetComponent<DataStorage> ().reloadCost [_GameManager.GetComponent<DataStorage> ().curWeapon];
			//play sound


			//display current upgrade and next upgrade
			_GameManager.GetComponent<DataStorage> ().curReload [_GameManager.GetComponent<DataStorage> ().curWeapon] +=1;
			curText.GetComponent<Text> ().text = "Reload: " + _GameManager.GetComponent<DataStorage> ().reload [_GameManager.GetComponent<DataStorage> ().curWeapon];
			nextText.GetComponent<Text> ().text = "Next Level: " + (_GameManager.GetComponent<DataStorage> ().reload [_GameManager.GetComponent<DataStorage> ().curWeapon] + 1);

			//display current money and price
			HoverOverReload();

			//updates current upgrade
			myIndex = _GameManager.GetComponent<DataStorage> ().curReload[_GameManager.GetComponent<DataStorage> ().curWeapon] - 1;
			ActivateUpgrade(myIndex);
		} 
		else 
		{
			print ("You have no money, stranger");
			//play sound
		}
	}


	//Upgrade Damage of current equiped weapon
	public void UpCapacity()
	{
		GetComponent<AudioSource>().Play();

		//checking to see if weapon is not fully maxed
		if (_GameManager.GetComponent<DataStorage> ().curCapacity[_GameManager.GetComponent<DataStorage> ().curWeapon] < 5)
		if (_GameManager.GetComponent<DataStorage> ().money > _GameManager.GetComponent<DataStorage> ().capacityCost [_GameManager.GetComponent<DataStorage> ().curWeapon]) {
			_GameManager.GetComponent<DataStorage> ().money -= _GameManager.GetComponent<DataStorage> ().capacityCost [_GameManager.GetComponent<DataStorage> ().curWeapon];
			_GameManager.GetComponent<DataStorage> ().capacityCost [_GameManager.GetComponent<DataStorage> ().curWeapon] += 25;
			_GameManager.GetComponent<DataStorage> ().capacity [_GameManager.GetComponent<DataStorage> ().curWeapon] += 2;
			_GameManager.GetComponent<DataStorage> ().sellValue [_GameManager.GetComponent<DataStorage> ().curWeapon] += 8;
			//adding stats
			_GameManager.GetComponent<DataStorage> ().moneySpent += _GameManager.GetComponent<DataStorage> ().capacityCost [_GameManager.GetComponent<DataStorage> ().curWeapon];
			//play sound

			//display current upgrade and next upgrade
			_GameManager.GetComponent<DataStorage> ().curCapacity [_GameManager.GetComponent<DataStorage> ().curWeapon] +=1;
			curText.GetComponent<Text> ().text = "Capacity: " + _GameManager.GetComponent<DataStorage> ().capacity [_GameManager.GetComponent<DataStorage> ().curWeapon];
			nextText.GetComponent<Text> ().text = "Next Level: " + (_GameManager.GetComponent<DataStorage> ().capacity [_GameManager.GetComponent<DataStorage> ().curWeapon] + 1);

			//display current money and price
			HoverOverCapacity ();

			//updates current upgrade
			myIndex = _GameManager.GetComponent<DataStorage> ().curCapacity[_GameManager.GetComponent<DataStorage> ().curWeapon]-1;
			ActivateUpgrade(myIndex);
		}
		else 
		{
			print ("You have no money, stranger");
			//play sound
		}
	}

	//Upgrade Fire Rate of current equiped weapon
	public void UpFireRate()
	{
		GetComponent<AudioSource>().Play();

		//checking to see if weapon is not fully maxed
		if (_GameManager.GetComponent<DataStorage> ().curFireRate[_GameManager.GetComponent<DataStorage> ().curWeapon] < 5)
		if (_GameManager.GetComponent<DataStorage> ().money > _GameManager.GetComponent<DataStorage> ().frCost[_GameManager.GetComponent<DataStorage> ().curWeapon]) {
			_GameManager.GetComponent<DataStorage> ().money -= _GameManager.GetComponent<DataStorage> ().frCost [_GameManager.GetComponent<DataStorage> ().curWeapon];
			_GameManager.GetComponent<DataStorage> ().frCost [_GameManager.GetComponent<DataStorage> ().curWeapon] += 25;
			_GameManager.GetComponent<DataStorage> ().fireRate[_GameManager.GetComponent<DataStorage> ().curWeapon] += 1;
			_GameManager.GetComponent<DataStorage> ().sellValue[_GameManager.GetComponent<DataStorage> ().curWeapon] += 8;
			//adding stats
			_GameManager.GetComponent<DataStorage> ().moneySpent += _GameManager.GetComponent<DataStorage> ().frCost [_GameManager.GetComponent<DataStorage> ().curWeapon];
			//play sound

			//display current upgrade and next upgrade
			_GameManager.GetComponent<DataStorage> ().curFireRate [_GameManager.GetComponent<DataStorage> ().curWeapon] +=1;
			curText.GetComponent<Text> ().text = "Fire Rate: " + _GameManager.GetComponent<DataStorage> ().fireRate [_GameManager.GetComponent<DataStorage> ().curWeapon];
			nextText.GetComponent<Text> ().text = "Next Level: " + (_GameManager.GetComponent<DataStorage> ().fireRate [_GameManager.GetComponent<DataStorage> ().curWeapon] + 1);


			//display current money and price
			HoverOverFireRate();

			//updates current upgrade
			myIndex = _GameManager.GetComponent<DataStorage> ().curFireRate[_GameManager.GetComponent<DataStorage> ().curWeapon]-1;
			ActivateUpgrade(myIndex);
		} 
		else 
		{
			print ("You have no money, stranger");
			//play sound
		}
	}



	//Upgrade accuracy of current equiped weapon
	public void UpAccuracy()
	{
		GetComponent<AudioSource>().Play();

		//checking to see if weapon is not fully maxed
		if (_GameManager.GetComponent<DataStorage> ().curAccuracy[_GameManager.GetComponent<DataStorage> ().curWeapon] < 5)
		if (_GameManager.GetComponent<DataStorage> ().money > _GameManager.GetComponent<DataStorage> ().acCost[_GameManager.GetComponent<DataStorage> ().curWeapon]) 
		{
			_GameManager.GetComponent<DataStorage> ().money -= _GameManager.GetComponent<DataStorage> ().acCost [_GameManager.GetComponent<DataStorage> ().curWeapon];
			_GameManager.GetComponent<DataStorage> ().acCost [_GameManager.GetComponent<DataStorage> ().curWeapon] += 25;
			_GameManager.GetComponent<DataStorage> ().accuracy[_GameManager.GetComponent<DataStorage> ().curWeapon] += 1;
			_GameManager.GetComponent<DataStorage> ().sellValue[_GameManager.GetComponent<DataStorage> ().curWeapon] += 8;
			//adding stats
			_GameManager.GetComponent<DataStorage> ().moneySpent += _GameManager.GetComponent<DataStorage> ().acCost [_GameManager.GetComponent<DataStorage> ().curWeapon];
			//play sound

			//display current upgrade and next upgrade
			_GameManager.GetComponent<DataStorage> ().curAccuracy [_GameManager.GetComponent<DataStorage> ().curWeapon] +=1;
			curText.GetComponent<Text> ().text = "Accuracy: " + _GameManager.GetComponent<DataStorage> ().accuracy[_GameManager.GetComponent<DataStorage> ().curWeapon];
			nextText.GetComponent<Text> ().text = "Next Level: " + (_GameManager.GetComponent<DataStorage> ().accuracy [_GameManager.GetComponent<DataStorage> ().curWeapon] + 1);

			//display current money and price
			HoverOverAccuracy();

			//updates current upgrade
			myIndex = _GameManager.GetComponent<DataStorage> ().curAccuracy[_GameManager.GetComponent<DataStorage> ().curWeapon]-1;
			ActivateUpgrade(myIndex);
		} 
		else 
		{
			print ("You have no money, stranger");
			//play sound
		}
	}


	//Upgrade accuracy of current equiped weapon
	public void UpRange()
	{
		GetComponent<AudioSource>().Play();

		//checking to see if weapon is not fully maxed
		if (_GameManager.GetComponent<DataStorage> ().curRange[_GameManager.GetComponent<DataStorage> ().curWeapon] < 5)
		if (_GameManager.GetComponent<DataStorage> ().money > _GameManager.GetComponent<DataStorage> ().rangeCost[_GameManager.GetComponent<DataStorage> ().curWeapon]) {
			_GameManager.GetComponent<DataStorage> ().money -= _GameManager.GetComponent<DataStorage> ().rangeCost [_GameManager.GetComponent<DataStorage> ().curWeapon];
			_GameManager.GetComponent<DataStorage> ().rangeCost [_GameManager.GetComponent<DataStorage> ().curWeapon] += 25;
			_GameManager.GetComponent<DataStorage> ().range[_GameManager.GetComponent<DataStorage> ().curWeapon] += 1;
			_GameManager.GetComponent<DataStorage> ().sellValue[_GameManager.GetComponent<DataStorage> ().curWeapon] += 8;
			//adding stats
			_GameManager.GetComponent<DataStorage> ().moneySpent += _GameManager.GetComponent<DataStorage> ().rangeCost [_GameManager.GetComponent<DataStorage> ().curWeapon];
			//play sound

			//display current upgrade and next upgrade
			_GameManager.GetComponent<DataStorage> ().curRange [_GameManager.GetComponent<DataStorage> ().curWeapon] +=1;
			curText.GetComponent<Text> ().text = "Range: " + _GameManager.GetComponent<DataStorage> ().range [_GameManager.GetComponent<DataStorage> ().curWeapon];
			nextText.GetComponent<Text> ().text = "Next Level: " + (_GameManager.GetComponent<DataStorage>().range [_GameManager.GetComponent<DataStorage> ().curWeapon] + 1);

			//display current money and price
			HoverOverRange();

			//updates current upgrade
			myIndex = _GameManager.GetComponent<DataStorage> ().curRange[_GameManager.GetComponent<DataStorage> ().curWeapon]-1;
			ActivateUpgrade(myIndex);
		} 
		else 
		{
			print ("You have no money, stranger");
			//play sound
		}
	}

	//Upgrade accuracy of current equiped weapon
	public void UpCritical()
	{
		GetComponent<AudioSource>().Play();
		//display current upgrade and next upgrade

		//checking to see if weapon is not fully maxed
		if (_GameManager.GetComponent<DataStorage> ().curCrit[_GameManager.GetComponent<DataStorage> ().curWeapon] < 5)
		if (_GameManager.GetComponent<DataStorage> ().money > _GameManager.GetComponent<DataStorage> ().CCCost [_GameManager.GetComponent<DataStorage> ().curWeapon]) {
			_GameManager.GetComponent<DataStorage> ().money -= _GameManager.GetComponent<DataStorage> ().CCCost [_GameManager.GetComponent<DataStorage> ().curWeapon];
			_GameManager.GetComponent<DataStorage> ().CCCost [_GameManager.GetComponent<DataStorage> ().curWeapon] += 25;
			_GameManager.GetComponent<DataStorage> ().criticalChance [_GameManager.GetComponent<DataStorage> ().curWeapon] += .02f;
			_GameManager.GetComponent<DataStorage> ().criticalChance [_GameManager.GetComponent<DataStorage> ().curWeapon] = Mathf.Round(_GameManager.GetComponent<DataStorage> ().criticalChance [_GameManager.GetComponent<DataStorage> ().curWeapon]*100.0f)/100.0f;
			_GameManager.GetComponent<DataStorage> ().sellValue [_GameManager.GetComponent<DataStorage> ().curWeapon] += 8;
			//adding stats
			_GameManager.GetComponent<DataStorage> ().moneySpent += _GameManager.GetComponent<DataStorage> ().CCCost [_GameManager.GetComponent<DataStorage> ().curWeapon];
			//play sound

			//display current upgrade and next upgrade
			_GameManager.GetComponent<DataStorage> ().curCrit [_GameManager.GetComponent<DataStorage> ().curWeapon] +=1;
			curText.GetComponent<Text> ().text = "Critical Hit: " + (_GameManager.GetComponent<DataStorage> ().criticalChance [_GameManager.GetComponent<DataStorage> ().curWeapon]) + "%";
			nextText.GetComponent<Text> ().text = "Next Level: " + Mathf.Round((_GameManager.GetComponent<DataStorage> ().criticalChance [_GameManager.GetComponent<DataStorage> ().curWeapon]+.02f)*1000.0f)/10.0f + "%";


			//display current money and price
			HoverOverCritical ();

			//updates current upgrade
			myIndex = _GameManager.GetComponent<DataStorage> ().curCrit[_GameManager.GetComponent<DataStorage> ().curWeapon]-1;
			ActivateUpgrade(myIndex);
		}
		else 
		{
			print ("You have no money, stranger");
			//play sound
		}
	}



	//this function activates the upgrade icon
	int ActivateUpgrade(int Index)
	{
		//disabling all upgrade above 
		myActive[0].SetActive(false);
		myActive[1].SetActive(false);
		myActive[2].SetActive(false);
		myActive[3].SetActive(false);
		myActive[4].SetActive(false);

		for (int i = 0; i < myIndex % myActive.Length; i++)
		{
			myActive[i].SetActive (true);

		}
		//setting current upgrade to active
		if (myIndex % myActive.Length < myActive.Length)
		{
			myActive[myIndex % myActive.Length].SetActive(!myActive[myIndex % myActive.Length].activeSelf);
		}
		return myIndex;
	}

	void DeactivateUpgrades()
	{
		//disabling all upgrade above 
		myActive[0].SetActive(false);
		myActive[1].SetActive(false);
		myActive[2].SetActive(false);
		myActive[3].SetActive(false);
		myActive[4].SetActive(false);
	}

}
