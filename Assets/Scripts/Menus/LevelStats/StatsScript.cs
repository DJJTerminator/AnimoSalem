using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class StatsScript : MonoBehaviour {
	Text strengthV;
	Text constitutionV;
	Text fortitudeV;
	Text dexterityV;
	Text agilityV;
	Text charismaV;
	Text intelligenceV;
	Text luckV;
	Text statsV;
	Text lvlV;
	Text curXPV;
	Text maxXPV;
	Text maxWeight;
	Text maxHealth;
	Text damage;
	Text armor;
	Text cash;
	Text speed;
	Text vision;
	Text barter;
	Text perceptionV;
	AudioSource noSound;
	AudioSource yesSound;
	//game information
	Text dmgTaken;
	Text dmgDealt;
	Text totalTime;
	Text totalSaves;
	Text shotsFired;
	Text shotsMissed;
	Text hitRatio;
	Text enemiesKilled;
	Text itemsUsed;
	Text itemsSold;
	Text totalMoney;
	Text moneySpent;
	Text totalXP;
	Text targetsHit;
	Text itemsBought;


	// Use this for initialization
	void Awake () 
	{
		//finding all game objects
		strengthV = GameObject.Find ("All Canvases/Canvas/LevelStats/Panel/Strength/Button/Value/Text").GetComponent<Text>();
		constitutionV = GameObject.Find ("All Canvases/Canvas/LevelStats/Panel/Constitution/Button/Value/Text").GetComponent<Text>();
		fortitudeV = GameObject.Find ("All Canvases/Canvas/LevelStats/Panel/Fortitude/Button/Value/Text").GetComponent<Text>();
		dexterityV = GameObject.Find ("All Canvases/Canvas/LevelStats/Panel/Dexterity/Button/Value/Text").GetComponent<Text>();
		charismaV = GameObject.Find ("All Canvases/Canvas/LevelStats/Panel/Charisma/Button/Value/Text").GetComponent<Text>();
		intelligenceV = GameObject.Find ("All Canvases/Canvas/LevelStats/Panel/Intelligence/Button/Value/Text").GetComponent<Text>();
		luckV = GameObject.Find ("All Canvases/Canvas/LevelStats/Panel/Luck/Button/Value/Text").GetComponent<Text>();
		perceptionV = GameObject.Find ("All Canvases/Canvas/LevelStats/Panel/Perception/Button/Value/Text").GetComponent<Text>();
		agilityV = GameObject.Find ("All Canvases/Canvas/LevelStats/Panel/Agility/Button/Value/Text").GetComponent<Text>();
		statsV = GameObject.Find ("All Canvases/Canvas/LevelStats/Panel/Stats/Text").GetComponent<Text>();
		curXPV = GameObject.Find ("All Canvases/Canvas/LevelStats/Panel/Level/CurXP/Text").GetComponent<Text>();
		maxXPV = GameObject.Find ("All Canvases/Canvas/LevelStats/Panel/Level/MaxXP/Text").GetComponent<Text>();
		lvlV = GameObject.Find ("All Canvases/Canvas/LevelStats/Panel/Level/LVL/Text").GetComponent<Text>();
		vision = GameObject.Find ("All Canvases/Canvas/LevelStats/Panel/Vision/Text").GetComponent<Text>();
		damage = GameObject.Find ("All Canvases/Canvas/LevelStats/Panel/Damage/Text").GetComponent<Text>();
		armor = GameObject.Find ("All Canvases/Canvas/LevelStats/Panel/Armor/Text").GetComponent<Text>();
		speed = GameObject.Find ("All Canvases/Canvas/LevelStats/Panel/Speed/Text").GetComponent<Text>();
		cash = GameObject.Find ("All Canvases/Canvas/LevelStats/Panel/Money/Text").GetComponent<Text>();
		barter = GameObject.Find ("All Canvases/Canvas/LevelStats/Panel/Barter/Text").GetComponent<Text>();
		maxHealth = GameObject.Find ("All Canvases/Canvas/LevelStats/Panel/Health/Text").GetComponent<Text>();
		maxWeight = GameObject.Find ("All Canvases/Canvas/LevelStats/Panel/Weight/Text").GetComponent<Text>();
		yesSound = GameObject.Find ("All Canvases/Canvas/LevelStats/Sounds/Yes").GetComponent<AudioSource>();
		noSound = GameObject.Find ("All Canvases/Canvas/LevelStats/Sounds/No").GetComponent<AudioSource>();

		dmgTaken = GameObject.Find ("All Canvases/Canvas/LevelStats/GameInfo/Image/Info/DamageTaken").GetComponent<Text>();
		totalXP = GameObject.Find ("All Canvases/Canvas/LevelStats/GameInfo/Image/Info/TotalXP").GetComponent<Text>();
		dmgDealt= GameObject.Find ("All Canvases/Canvas/LevelStats/GameInfo/Image/Info/DamageDealt").GetComponent<Text>();
		totalTime = GameObject.Find ("All Canvases/Canvas/LevelStats/GameInfo/Image/Info/TotalTime").GetComponent<Text>();
		totalSaves = GameObject.Find ("All Canvases/Canvas/LevelStats/GameInfo/Image/Info/TotalSaves").GetComponent<Text>();
		shotsFired= GameObject.Find ("All Canvases/Canvas/LevelStats/GameInfo/Image/Info/ShotsFired").GetComponent<Text>();
		shotsMissed= GameObject.Find ("All Canvases/Canvas/LevelStats/GameInfo/Image/Info/ShotsMissed").GetComponent<Text>();
		hitRatio= GameObject.Find ("All Canvases/Canvas/LevelStats/GameInfo/Image/Info/HitRatio").GetComponent<Text>();
		enemiesKilled= GameObject.Find ("All Canvases/Canvas/LevelStats/GameInfo/Image/Info/EnemiesKilled").GetComponent<Text>();
		itemsUsed= GameObject.Find ("All Canvases/Canvas/LevelStats/GameInfo/Image/Info/ItemsUsed").GetComponent<Text>();
		itemsSold= GameObject.Find ("All Canvases/Canvas/LevelStats/GameInfo/Image/Info/ItemsSold").GetComponent<Text>();
		totalMoney= GameObject.Find ("All Canvases/Canvas/LevelStats/GameInfo/Image/Info/TotalMoney").GetComponent<Text>();
		moneySpent= GameObject.Find ("All Canvases/Canvas/LevelStats/GameInfo/Image/Info/MoneySpent").GetComponent<Text>();
		targetsHit= GameObject.Find ("All Canvases/Canvas/LevelStats/GameInfo/Image/Info/ShotsHit").GetComponent<Text>();
		itemsBought= GameObject.Find ("All Canvases/Canvas/LevelStats/GameInfo/Image/Info/ItemsBought").GetComponent<Text>();
	

		ShowStats ();
	}

	void OnEnable()
	{
		dmgTaken.text = "Damage Taken: " + DataStorage.damageTaken.ToString();
		dmgDealt.text = "Damage Dealt: " + DataStorage.damageDealt.ToString();
		itemsUsed.text = "Items Used: " + DataStorage.itemsUsed.ToString();
		itemsSold.text = "Items Sold: " + DataStorage.itemsSold.ToString();
		itemsBought.text = "Items Bought: " + DataStorage.itemsBought.ToString();
		totalSaves.text = "Games Saved: " + DataStorage.numberOfSaves.ToString();
		enemiesKilled.text = "Kills: " + DataStorage.enemiesKilled.ToString();
		totalXP.text = "Total XP: " + DataStorage.totalXP.ToString();
		totalMoney.text = "Earned: " + "$"+ DataStorage.totalMoneyEarned.ToString();
		moneySpent.text = "Money Spent: " + DataStorage.moneySpent.ToString();
		totalTime.text = "Total Time: " + DataStorage.currentTime.ToString();
		hitRatio.text = "Accuracy: " + ((float)DataStorage.targetsHit/(float)DataStorage.shotsFired*100).ToString() + "%";
		targetsHit.text = "Shots Hit: " + DataStorage.targetsHit.ToString();
		shotsFired.text = "Shots Fired: " + DataStorage.shotsFired.ToString();
		shotsMissed.text = "Shots Missed: " + DataStorage.damageDealt.ToString();

		damage.text = "Damage " + (Mathf.Round((DataStorage.damage + DataStorage.weaponDamage[DataStorage.curWeapon])*100f)/100f).ToString();

	}

	//displays and updates all stats
	void ShowStats()
	{
		//getting the discount for the skill, "Charisma"
		float price = Discount (DataStorage.damageCost [DataStorage.curWeapon]);

		strengthV.text = DataStorage.strength.ToString();
		constitutionV.text = DataStorage.constitution.ToString();
		fortitudeV.text = DataStorage.fortitude.ToString();
		luckV.text = DataStorage.luck.ToString();
		agilityV.text = DataStorage.agility.ToString();
		intelligenceV.text = DataStorage.intelligence.ToString();
		dexterityV.text = DataStorage.dexterity.ToString();
		charismaV.text = DataStorage.charisma.ToString();
		perceptionV.text = DataStorage.perception.ToString();
		statsV.text = DataStorage.playerStats.ToString();
		curXPV.text = DataStorage.XP.ToString();
		maxXPV.text = DataStorage.maxXP.ToString();
		lvlV.text = DataStorage.currentLevel.ToString();
		float myVis = DataStorage.lightAngle / 10f;
		vision.text = "Light Radius " + (Mathf.Round(myVis*100)/100).ToString();
		speed.text = "Speed " + (Mathf.Round(DataStorage.speed*100f)/100f).ToString();
		armor.text = "Armor " + DataStorage.fortitude.ToString();
		cash.text = "Cash $" + DataStorage.money.ToString();
		barter.text = "Barter " + Mathf.Round(price * 100) + "% Discount".ToString();
		maxWeight.text = "Weight " + DataStorage.curWeight +"/"+ Mathf.Round(DataStorage.maxWeight).ToString();
		maxHealth.text = "Health " + DataStorage.health +"/"+ DataStorage.maxHealth.ToString();
		damage.text = "Damage " + (Mathf.Round((DataStorage.damage + DataStorage.weaponDamage[DataStorage.curWeapon])*100f)/100f).ToString();
	}

	//returning the exchange
	public float Discount(float value)
	{
		if (DataStorage.charisma > value * .4f)
			value = value * .4f;
		else
			value -= DataStorage.charisma;
		value = DataStorage.charisma / value;
		return value;
	}

	public void Constitution()
	{
		if (DataStorage.playerStats > 0)
		{
			//finding the percentage of current health to max health
			float hp = (float)DataStorage.health/(float)DataStorage.maxHealth;

			//adding stats
			DataStorage.playerStats -= 1;
			DataStorage.constitution +=1;
			DataStorage.maxHealth += 2;

			//we are later assigning that same percentage to the newly upgraded max health
			hp = Mathf.Round(DataStorage.maxHealth * hp);
			DataStorage.health = (int)hp;


			yesSound.Play ();
			ShowStats();
		}
		else
			noSound.Play ();
	}


	public void Strength()
	{
		if (DataStorage.playerStats > 0)
		{
			DataStorage.playerStats -= 1;
			DataStorage.strength +=1;

			//adding the skill
			DataStorage.maxWeight += .2f;

			yesSound.Play ();
			ShowStats();
		}
		else
			noSound.Play ();
	}


	public void Luck()
	{
		if (DataStorage.playerStats > 0)
		{
			DataStorage.playerStats -= 1;
			DataStorage.luck +=1;
			yesSound.Play ();
			ShowStats();
		}
		else
			noSound.Play ();
	}

	public void Dexterity()
	{
		if (DataStorage.playerStats > 0)
		{
			DataStorage.playerStats -= 1;
			DataStorage.dexterity += 1;
			DataStorage.damage += .2f;
			yesSound.Play ();
			ShowStats();
		}
		else
			noSound.Play ();
	}

	public void Charisma()
	{
		if (DataStorage.playerStats > 0)
		{
			DataStorage.playerStats -= 1;
			DataStorage.charisma +=1;
			yesSound.Play ();
			ShowStats();
		}
		else
			noSound.Play ();
	}

	public void Intelligence()
	{
		if (DataStorage.playerStats > 0)
		{
			DataStorage.playerStats -= 1;
			DataStorage.intelligence +=1;

			yesSound.Play ();
			ShowStats();
		}
		else
			noSound.Play ();
	}
	public void Agility()
	{
		if (DataStorage.playerStats > 0)
		{
			DataStorage.playerStats -= 1;
			DataStorage.agility +=1;

			//adding the skill
			DataStorage.speed += .02f;
			DataStorage.player.GetComponent<Controls> ().speed = DataStorage.speed;
			if (DataStorage.player.GetComponent<Controls> ().speed >= 10f)
				DataStorage.player.GetComponent<Controls> ().speed = 10f;

			yesSound.Play ();
			ShowStats();
		}
		else
			noSound.Play ();
	}

	public void Perception()
	{
		if (DataStorage.playerStats > 0)
		{
			DataStorage.playerStats -= 1;
			DataStorage.perception +=1;

			//adding the skill
			DataStorage.lightAngle += .2f;
			DataStorage.lightRange += .2f;
			DataStorage.lightRadius.GetComponent<Light> ().range = DataStorage.lightRange;
			DataStorage.lightRadius.GetComponent<Light> ().spotAngle = DataStorage.lightAngle;

			yesSound.Play ();
			ShowStats();
		}
		else
			noSound.Play ();
	}

	public void Fortitude()
	{
		if (DataStorage.playerStats > 0)
		{
			DataStorage.playerStats -= 1;
			DataStorage.fortitude +=1;

			yesSound.Play ();
			ShowStats();
		}
		else
			noSound.Play ();
	}
}//end of class
