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

		ShowStats ();
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
		vision.text = "Light Radius " + Mathf.Round(DataStorage.lightRadius.GetComponent<Light> ().spotAngle/10).ToString();
		speed.text = "Speed " + Mathf.Round(DataStorage.player.GetComponent<Controls>().speed*100/100f).ToString();
		damage.text = "Damage " + Mathf.Round(DataStorage.damage + DataStorage.weaponDamage[DataStorage.curWeapon]).ToString();
		armor.text = "Armor " + DataStorage.fortitude.ToString();
		cash.text = "Cash $" + DataStorage.money.ToString();
		barter.text = "Barter " + Mathf.Round(price * 100) + "% Discount".ToString();
		maxWeight.text = "Weight " + DataStorage.curWeight +"/"+ DataStorage.maxWeight.ToString();
		maxHealth.text = "Health " + DataStorage.health +"/"+ DataStorage.maxHealth.ToString();
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
			DataStorage.maxHealth += 4;

			//we are later assigning that same percentage to the newly upgraded max health
			hp = Mathf.Round(DataStorage.maxHealth * hp);
			DataStorage.health = (int)hp;


			//play sounds
			ShowStats();
		}
		//else
			//playsound
	}


	public void Strength()
	{
		if (DataStorage.playerStats > 0)
		{
			DataStorage.playerStats -= 1;
			DataStorage.strength +=1;

			//adding the skill
			DataStorage.maxWeight += 2;

			//play sounds
			ShowStats();
		}
		//else
		//playsound
	}


	public void Luck()
	{
		if (DataStorage.playerStats > 0)
		{
			DataStorage.playerStats -= 1;
			DataStorage.luck +=1;
			//play sounds
			ShowStats();
		}
		//else
		//playsound
	}

	public void Dexterity()
	{
		if (DataStorage.playerStats > 0)
		{
			DataStorage.playerStats -= 1;
			DataStorage.dexterity +=1;
			DataStorage.damage += .2f;
			//play sounds
			ShowStats();
		}
		//else
		//playsound
	}

	public void Charisma()
	{
		if (DataStorage.playerStats > 0)
		{
			DataStorage.playerStats -= 1;
			DataStorage.charisma +=1;
			//play sounds
			ShowStats();
		}
		//else
		//playsound
	}

	public void Intelligence()
	{
		if (DataStorage.playerStats > 0)
		{
			DataStorage.playerStats -= 1;
			DataStorage.intelligence +=1;

			//play sounds
			ShowStats();
		}
		//else
		//playsound
	}
	public void Agility()
	{
		if (DataStorage.playerStats > 0)
		{
			DataStorage.playerStats -= 1;
			DataStorage.agility +=1;

			//adding the skill
			DataStorage.player.GetComponent<Controls>().speed += .02f;
			if (DataStorage.player.GetComponent<Controls> ().speed >= 10f)
				DataStorage.player.GetComponent<Controls> ().speed = 10f;

			//play sounds
			ShowStats();
		}
		//else
		//playsound
	}

	public void Perception()
	{
		if (DataStorage.playerStats > 0)
		{
			DataStorage.playerStats -= 1;
			DataStorage.perception +=1;

			//adding the skill
			DataStorage.lightRadius.GetComponent<Light> ().range += .2f;
			DataStorage.lightRadius.GetComponent<Light> ().spotAngle += .2f;

			//play sounds
			ShowStats();
		}
		//else
		//playsound
	}

	public void Fortitude()
	{
		if (DataStorage.playerStats > 0)
		{
			DataStorage.playerStats -= 1;
			DataStorage.fortitude +=1;

			//play sounds
			ShowStats();
		}
		//else
		//playsound
	}
}//end of class
