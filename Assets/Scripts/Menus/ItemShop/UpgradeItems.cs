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
	int returnWeapon; //returns weapon back to what was originally equipped after the player leaves the shop

	[SerializeField]
	GameObject[] myActive;
	int index;

	[SerializeField]
	Texture[] curWeapon;

	//sounds

	public AudioSource upgradeSuccessful;
	public AudioSource noMoney;
	public AudioSource maxedOut;
	public AudioSource hoverOver;

	void OnEnable()
	{
		returnWeapon = DataStorage.curWeapon;
	}
	void OnDisable()
	{
		DataStorage.curWeapon = returnWeapon;
	}


	//cycling weapons to the left
	public void WeaponCycleLeft()
	{
		if (DataStorage.curWeapon <= 0)
			DataStorage.curWeapon = DataStorage.obtainedWeapons.Length - 1;
		else
			DataStorage.curWeapon -= 1;

		int i = DataStorage.obtainedWeapons.Length - 1;

		while (i > 0) 
		{
			//looping through the weapons to see which have been obtained and which have not
			if (DataStorage.obtainedWeapons[DataStorage.curWeapon] != 1) 
			{
				//No weapon has been found so I am pushing through the loop
				if (DataStorage.curWeapon > 0) 
				{
					DataStorage.curWeapon -= 1;
					i--;
				} 
				else 
				{
					DataStorage.curWeapon = DataStorage.obtainedWeapons.Length - 1;
					i--;
				}
			} 
			else 
			{
				//A weapon has been found so I am breaking the while loop
				_weaponName.GetComponent<Text> ().text = DataStorage.weaponName [DataStorage.curWeapon];
				upgradeSelect.GetComponent<RawImage> ().texture = curWeapon [DataStorage.curWeapon];
				HoverOverDamage ();
				break;
			}
		}
	}

	//cycling weapons to the right
	public void WeaponCycleRight()
	{
		if (DataStorage.curWeapon >= DataStorage.obtainedWeapons.Length - 1)
			DataStorage.curWeapon = 0;
		else
			DataStorage.curWeapon += 1;

		int i = DataStorage.obtainedWeapons.Length - 1;

		while (i > 0) 
		{
			//looping through the weapons to see which have been obtained and which have not

			if (DataStorage.obtainedWeapons[DataStorage.curWeapon] != 1) 
			{
				//No weapon has been found so I am pushing through the loop
				if (DataStorage.curWeapon < DataStorage.obtainedWeapons.Length - 1) 
				{
					DataStorage.curWeapon += 1;
					i--;
				} 
				else 
				{
					DataStorage.curWeapon = 0;
					i--;
				}
			} 
			else 
			{
				//A weapon has been found so I am breaking the while loop
				_weaponName.GetComponent<Text> ().text = DataStorage.weaponName [DataStorage.curWeapon];
				upgradeSelect.GetComponent<RawImage> ().texture = curWeapon [DataStorage.curWeapon];
				HoverOverDamage ();
				break;
			}
		}
	}




	//these are the hover over functions
	public void HoverOverDamage()
	{
		float _price = Exchange (DataStorage.damageCost [DataStorage.curWeapon]);

		if (!upgradeSuccessful.isPlaying)
			hoverOver.Play();
		
		if (DataStorage.curDamage [DataStorage.curWeapon] > 0) 
		{
			myIndex = DataStorage.curDamage [DataStorage.curWeapon] -1;
			ActivateUpgrade (myIndex);
		}else
			DeactivateUpgrades ();

		//display image
		upgradeSelect.GetComponent<RawImage> ().texture = curWeapon [DataStorage.curWeapon];

		//display weapon name
		_weaponName.GetComponent<Text> ().text = DataStorage.weaponName [DataStorage.curWeapon];

		//display current upgrade and next upgrade
		curText.GetComponent<Text> ().text = "Damage: " + DataStorage.weaponDamage [DataStorage.curWeapon];
		if (DataStorage.curDamage [DataStorage.curWeapon] < 5)
            nextText.GetComponent<Text>().text = "Next Level: " + (DataStorage.weaponDamage[DataStorage.curWeapon] + DataStorage.upDamage[DataStorage.curWeapon]);
		else
			nextText.GetComponent<Text> ().text = " ";	

			

		//display current money and price
		curMoney.GetComponent<Text> ().text = "$" + DataStorage.money.ToString("n0");;
		if (DataStorage.curDamage[DataStorage.curWeapon] < 5)
			price.GetComponent<Text> ().text = "Cost: $" + _price;
	}
	public void HoverOverReload()
	{
		float _price = Exchange (DataStorage.reloadCost [DataStorage.curWeapon]);
		if (!upgradeSuccessful.isPlaying)
			hoverOver.Play();
		//updates current upgrade
		if (DataStorage.curReload [DataStorage.curWeapon] > 0) 
		{
			myIndex = DataStorage.curReload [DataStorage.curWeapon] -1;
			ActivateUpgrade (myIndex);
		}else
			DeactivateUpgrades ();

		//display current upgrade and next upgrade
		curText.GetComponent<Text> ().text = "Reload: " + Mathf.Round(((DataStorage.reload[DataStorage.curWeapon] + (DataStorage.upReload[DataStorage.curWeapon] * DataStorage.curReload[DataStorage.curWeapon])) * 100) / DataStorage.reload[DataStorage.curWeapon]) + "%";
        if (DataStorage.curReload [DataStorage.curWeapon] < 5)
            nextText.GetComponent<Text>().text = "Next Level: " + Mathf.Round(((DataStorage.reload[DataStorage.curWeapon] + (DataStorage.upReload[DataStorage.curWeapon] * DataStorage.curReload[DataStorage.curWeapon])) * 100) / (DataStorage.reload[DataStorage.curWeapon] - DataStorage.upReload[DataStorage.curWeapon])) + "%";
        else
			nextText.GetComponent<Text> ().text = " ";	

		curMoney.GetComponent<Text> ().text = "$" + DataStorage.money.ToString("n0");;
		price.GetComponent<Text> ().text = "Cost: $" + _price;
	}
	public void HoverOverCapacity()
	{
		float _price = Exchange (DataStorage.capacityCost [DataStorage.curWeapon]);
		if (!upgradeSuccessful.isPlaying)
			hoverOver.Play();
		//updates current upgrade
		if (DataStorage.curCapacity [DataStorage.curWeapon] > 0) 
		{
			myIndex = DataStorage.curCapacity [DataStorage.curWeapon] -1;
			ActivateUpgrade (myIndex);
		}else
			DeactivateUpgrades ();

		//display current upgrade and next upgrade
		curText.GetComponent<Text> ().text = "Capacity: " + DataStorage.capacity [DataStorage.curWeapon];
		if (DataStorage.curCapacity [DataStorage.curWeapon] < 5)
            nextText.GetComponent<Text>().text = "Next Level: " + (DataStorage.capacity[DataStorage.curWeapon] + DataStorage.upCapacity[DataStorage.curWeapon]);
		else
			nextText.GetComponent<Text> ().text = " ";	

		//display current money and price
		curMoney.GetComponent<Text> ().text = "$" + DataStorage.money.ToString("n0");;
		price.GetComponent<Text> ().text = "Cost: $" + _price;
	}
	public void HoverOverFireRate()
	{
		float _price = Exchange (DataStorage.frCost [DataStorage.curWeapon]);
		if (!upgradeSuccessful.isPlaying)
			hoverOver.Play();
		//updates current upgrade
		if (DataStorage.curFireRate [DataStorage.curWeapon] > 0) 
		{
			myIndex = DataStorage.curFireRate [DataStorage.curWeapon] -1;
			ActivateUpgrade (myIndex);
		}else
			DeactivateUpgrades ();

        curText.GetComponent<Text>().text = "Fire Rate: " + Mathf.Round(((DataStorage.fireRate[DataStorage.curWeapon] + (DataStorage.upFireRate[DataStorage.curWeapon] * DataStorage.curFireRate[DataStorage.curWeapon]))*100)/ DataStorage.fireRate[DataStorage.curWeapon]) + "%";
        if (DataStorage.curFireRate [DataStorage.curWeapon] < 5)
            nextText.GetComponent<Text>().text = "Next Level: " + Mathf.Round(((DataStorage.fireRate[DataStorage.curWeapon] + (DataStorage.upFireRate[DataStorage.curWeapon] * DataStorage.curFireRate[DataStorage.curWeapon])) * 100) / (DataStorage.fireRate[DataStorage.curWeapon] - DataStorage.upFireRate[DataStorage.curWeapon])) + "%";
        else
			nextText.GetComponent<Text> ().text = null;	

		//display current money and price
		curMoney.GetComponent<Text> ().text = "$" + DataStorage.money.ToString("n0");;
		price.GetComponent<Text> ().text = "Cost: $" + _price;
	}
	public void HoverOverCritical()
	{
		float _price = Exchange (DataStorage.CCCost [DataStorage.curWeapon]);
		if (!upgradeSuccessful.isPlaying)
			hoverOver.Play();
		//updates current upgrade
		if (DataStorage.curCrit [DataStorage.curWeapon] > 0) 
		{
			myIndex = DataStorage.curCrit [DataStorage.curWeapon] -1;
			ActivateUpgrade (myIndex);
		}else
			DeactivateUpgrades ();

		//display current upgrade and next upgrade
		curText.GetComponent<Text> ().text = "Critical Hit: " + (DataStorage.criticalChance [DataStorage.curWeapon] * 100) + "%";
		if (DataStorage.curCrit [DataStorage.curWeapon] < 5)
            nextText.GetComponent<Text>().text = "Next Level: " + Mathf.Round((DataStorage.criticalChance[DataStorage.curWeapon] + DataStorage.upCritical[DataStorage.curWeapon]) * 100.0f)+ "%";
		else
			nextText.GetComponent<Text> ().text = " ";	
		//upgradeSelect.GetComponent<RawImage>().texture = _critical;

		//display current money and price
		curMoney.GetComponent<Text> ().text = "$" + DataStorage.money.ToString("n0");;
		price.GetComponent<Text> ().text = "Cost: $" + _price;
	}
	public void HoverOverAccuracy()
	{
		float _price = Exchange (DataStorage.acCost [DataStorage.curWeapon]);
		if (!upgradeSuccessful.isPlaying)
			hoverOver.Play();
		//updates current upgrade
		if (DataStorage.curAccuracy [DataStorage.curWeapon] > 0) 
		{
			myIndex = DataStorage.curAccuracy [DataStorage.curWeapon] -1;
			ActivateUpgrade (myIndex);
		}else
			DeactivateUpgrades ();
        //cur leve * 100/ next level +100
        //display current upgrade and next upgrade
        curText.GetComponent<Text>().text = "Accuracy: " + Mathf.Round(100 + (DataStorage.accuracy[DataStorage.curWeapon] * 100)) + "%";
		if (DataStorage.curAccuracy [DataStorage.curWeapon] < 5)
            nextText.GetComponent<Text>().text = "Next Level: " + Mathf.Round(100 + ((DataStorage.accuracy[DataStorage.curWeapon] + DataStorage.upAccuracy[DataStorage.curWeapon]) * 100)) + "%";
        else
			nextText.GetComponent<Text> ().text = " ";	

		//upgradeSelect.GetComponent<RawImage>().texture = _accuracy;

		//display current money and price
		curMoney.GetComponent<Text> ().text = "$" + DataStorage.money.ToString("n0");;
		price.GetComponent<Text> ().text = "Cost: $" + _price;
	}
	public void HoverOverRange()
	{
		float _price = Exchange (DataStorage.rangeCost [DataStorage.curWeapon]);
		if (!upgradeSuccessful.isPlaying)
			hoverOver.Play();
		//updates current upgrade
		if (DataStorage.curRange [DataStorage.curWeapon] > 0) {
			myIndex = DataStorage.curRange [DataStorage.curWeapon] - 1;
			ActivateUpgrade (myIndex);
		} else
			DeactivateUpgrades ();

		//display current upgrade and next upgrade
        curText.GetComponent<Text>().text = "Critical Damage: " + ((DataStorage.range[DataStorage.curWeapon] * 100)/1) + "%";
		if (DataStorage.curRange [DataStorage.curWeapon] < 5)
            nextText.GetComponent<Text>().text = "Next Level: " + Mathf.Round((DataStorage.range[DataStorage.curWeapon] + DataStorage.upRange[DataStorage.curWeapon]) * 100)/1  + "%";
		else
			nextText.GetComponent<Text> ().text = " ";	

		//upgradeSelect.GetComponent<RawImage>().texture = _range;



		//display current money and price
		curMoney.GetComponent<Text> ().text = "$" + DataStorage.money.ToString("n0");;
		price.GetComponent<Text> ().text = "Cost: $" + _price;
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


	//Upgrade Damage of current equiped weapon
	public void UpDamage()
	{
		float price = Exchange (DataStorage.damageCost [DataStorage.curWeapon]);
		//upgrade damage and sbtracting the cost
		//checking to see if weapon is not fully maxed
		if (DataStorage.curDamage[DataStorage.curWeapon] < 5)
			//checking to see if the player has enough money
		if (DataStorage.money > price) 
		{
			DataStorage.money -= (int)price;
			DataStorage.damageCost [DataStorage.curWeapon] += 25;
            DataStorage.weaponDamage[DataStorage.curWeapon] += DataStorage.upDamage[DataStorage.curWeapon];
			DataStorage.sellValue[DataStorage.curWeapon] += 8;
			//adding stats
			DataStorage.moneySpent += (int)price;
			//play sound
			upgradeSuccessful.Play();
			//display current upgrade and next upgrade
         
			//adding the current upgrade level
			DataStorage.curDamage [DataStorage.curWeapon] += 1;
			//display current money and price
			HoverOverDamage();

			//updates current upgrade
			myIndex = DataStorage.curDamage[DataStorage.curWeapon] - 1;
			ActivateUpgrade(myIndex);
		} 
		else 
		{
			//play sound
			noMoney.Play();
		}
	else 
	{
			print ("Maxed");
			//play sound
			maxedOut.Play();
	}
	}


	//Upgrade Reload of current equiped weapon
	public void UpReload()
	{
		float price = Exchange (DataStorage.reloadCost[DataStorage.curWeapon]);
		//checking to see if weapon is not fully maxed
		if (DataStorage.curReload[DataStorage.curWeapon] < 5)
		if (DataStorage.money > (int)price) {
			DataStorage.money -= (int)price;
			DataStorage.reloadCost [DataStorage.curWeapon] += 25;
            DataStorage.reload[DataStorage.curWeapon] -= DataStorage.upReload[DataStorage.curWeapon];
			DataStorage.sellValue[DataStorage.curWeapon] += 8;
			//adding stats
			DataStorage.moneySpent += (int)price;
			//play sound
			upgradeSuccessful.Play();

			//display current upgrade and next upgrade
			DataStorage.curReload [DataStorage.curWeapon] +=1;

			//display current money and price
			HoverOverReload();

			//updates current upgrade
			myIndex = DataStorage.curReload[DataStorage.curWeapon] - 1;
			ActivateUpgrade(myIndex);
		} 
		else 
		{
			print ("You have no money, stranger");
			//play sound
			noMoney.Play();
		}
		else 
		{
			print ("Maxed");
			//play sound
			maxedOut.Play();
		}
	}


	//Upgrade Damage of current equiped weapon
	public void UpCapacity()
	{
		float price = Exchange (DataStorage.capacityCost [DataStorage.curWeapon]);
		//checking to see if weapon is not fully maxed
		if (DataStorage.curCapacity[DataStorage.curWeapon] < 5)
		if (DataStorage.money > (int)price) {
			DataStorage.money -= (int)price;
			DataStorage.capacityCost [DataStorage.curWeapon] += 25;
            DataStorage.capacity[DataStorage.curWeapon] += DataStorage.upCapacity[DataStorage.curWeapon];
			DataStorage.sellValue [DataStorage.curWeapon] += 8;
			//adding stats
			DataStorage.moneySpent += (DataStorage.capacityCost [DataStorage.curWeapon]- DataStorage.charisma);
			//play sound
			upgradeSuccessful.Play();
			//display current upgrade and next upgrade
			DataStorage.curCapacity [DataStorage.curWeapon] +=1;

			//display current money and price
			HoverOverCapacity ();

			//updates current upgrade
			myIndex = DataStorage.curCapacity[DataStorage.curWeapon]-1;
			ActivateUpgrade(myIndex);
		}
		else 
		{
			print ("You have no money, stranger");
			//play sound
			noMoney.Play();
		}
		else 
		{
			print ("Maxed");
			//play sound
			maxedOut.Play();
		}
	}

	//Upgrade Fire Rate of current equiped weapon
	public void UpFireRate()
	{
		float price = Exchange (DataStorage.frCost [DataStorage.curWeapon]);
		//checking to see if weapon is not fully maxed
		if (DataStorage.curFireRate[DataStorage.curWeapon] < 5)
		if (DataStorage.money > (int)price) {
			DataStorage.money -= (int)price;
			DataStorage.frCost [DataStorage.curWeapon] += 25;
			DataStorage.fireRate[DataStorage.curWeapon] -= DataStorage.upFireRate[DataStorage.curWeapon];
			DataStorage.sellValue[DataStorage.curWeapon] += 8;
			//adding stats
			DataStorage.moneySpent += (DataStorage.frCost[DataStorage.curWeapon]- DataStorage.charisma);
			//play sound
			upgradeSuccessful.Play();
			//display current upgrade and next upgrade
			DataStorage.curFireRate [DataStorage.curWeapon] +=1;


			//display current money and price
			HoverOverFireRate();

			//updates current upgrade
			myIndex = DataStorage.curFireRate[DataStorage.curWeapon]-1;
			ActivateUpgrade(myIndex);
		} 
		else 
		{
			print ("You have no money, stranger");
			//play sound
			noMoney.Play();
		}
		else 
		{
			print ("Maxed");
			//play sound
			maxedOut.Play();
		}
	}



	//Upgrade accuracy of current equiped weapon
	public void UpAccuracy()
	{
		float price = Exchange (DataStorage.acCost [DataStorage.curWeapon]);
		//checking to see if weapon is not fully maxed
		if (DataStorage.curAccuracy[DataStorage.curWeapon] < 5)
		if (DataStorage.money > (int)price) {
			DataStorage.money -= (int)price;
			DataStorage.acCost [DataStorage.curWeapon] += 25;
			DataStorage.accuracy[DataStorage.curWeapon] += DataStorage.upAccuracy[DataStorage.curWeapon];
			DataStorage.sellValue[DataStorage.curWeapon] += 8;
			//adding stats
			DataStorage.moneySpent += (DataStorage.acCost[DataStorage.curWeapon]- DataStorage.charisma);
			//play sound
			upgradeSuccessful.Play();
			//display current upgrade and next upgrade
			DataStorage.curAccuracy [DataStorage.curWeapon] +=1;

			//display current money and price
			HoverOverAccuracy();

			//updates current upgrade
			myIndex = DataStorage.curAccuracy[DataStorage.curWeapon]-1;
			ActivateUpgrade(myIndex);
		} 
		else 
		{
			print ("You have no money, stranger");
			//play sound
			noMoney.Play();
		}
		else 
		{
			print ("Maxed");
			//play sound
			maxedOut.Play();
		}
	}


	//Upgrade accuracy of current equiped weapon
	public void UpRange()
	{
		float price = Exchange (DataStorage.rangeCost [DataStorage.curWeapon]);
		//checking to see if weapon is not fully maxed
		if (DataStorage.curRange[DataStorage.curWeapon] < 5)
		if (DataStorage.money > (int)price) {
			DataStorage.money -= (int)price;
			DataStorage.rangeCost [DataStorage.curWeapon] += 25;
            DataStorage.range[DataStorage.curWeapon] += DataStorage.upRange[DataStorage.curWeapon];
			DataStorage.sellValue[DataStorage.curWeapon] += 8;
			//adding stats
			DataStorage.moneySpent += (DataStorage.rangeCost[DataStorage.curWeapon]- DataStorage.charisma) ;
			//play sound
			upgradeSuccessful.Play();
			//display current upgrade and next upgrade
			DataStorage.curRange [DataStorage.curWeapon] +=1;

			//display current money and price
			HoverOverRange();

			//updates current upgrade
			myIndex = DataStorage.curRange[DataStorage.curWeapon]-1;
			ActivateUpgrade(myIndex);
		} 
		else 
		{
			print ("You have no money, stranger");
			//play sound
			noMoney.Play();
		}
		else 
		{
			print ("Maxed");
			//play sound
			maxedOut.Play();
		}
	}

	//Upgrade accuracy of current equiped weapon
	public void UpCritical()
	{
		float price = Exchange (DataStorage.CCCost [DataStorage.curWeapon]);
		//display current upgrade and next upgrade

		//checking to see if weapon is not fully maxed
		if (DataStorage.curCrit[DataStorage.curWeapon] < 5)
		if (DataStorage.money > (int)price) {
			DataStorage.money -= (int)price;
			DataStorage.CCCost [DataStorage.curWeapon] += 25;
			DataStorage.criticalChance [DataStorage.curWeapon] += DataStorage.upCritical[DataStorage.curWeapon];
			DataStorage.criticalChance [DataStorage.curWeapon] = Mathf.Round(DataStorage.criticalChance [DataStorage.curWeapon]*100.0f)/100.0f;
			DataStorage.sellValue [DataStorage.curWeapon] += 8;
			//adding stats
			DataStorage.moneySpent += (DataStorage.CCCost [DataStorage.curWeapon]- DataStorage.charisma);
			//play sound
			upgradeSuccessful.Play();

			//display current upgrade and next upgrade
			DataStorage.curCrit [DataStorage.curWeapon] +=1;
		


			//display current money and price
			HoverOverCritical ();

			//updates current upgrade
			myIndex = DataStorage.curCrit[DataStorage.curWeapon]-1;
			ActivateUpgrade(myIndex);
		}
		else 
		{
			print ("You have no money, stranger");
			//play sound
			noMoney.Play();
		}
		else 
		{
			print ("Maxed");
			//play sound
			maxedOut.Play();
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
