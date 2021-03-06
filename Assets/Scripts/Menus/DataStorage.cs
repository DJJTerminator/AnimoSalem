using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class DataStorage : MonoBehaviour

{
  //  public static Text HPValue;
 //   public static Text XPValue;
    public static Text levelNumber;
    public static GameObject HPbar;
    public static GameObject XPbar;
    public static GameObject goldValue;
    public static GameObject goldHUD;
    public static Text ammoText;
    public static GameObject player;
    public static Light lightRadius;
    public static GameObject textBox;
    public static GameObject textManager;
    public static GameObject pauseMenus;
    public static GameObject exclamation;
    public static GameObject theShop;
    public static GameObject sell;
    public static GameObject upgrade;
    public static GameObject buy;
    public static GameObject storageMenu;
    public static GameObject gameManager;
    public static GameObject levelStats;
    public static bool canDo = true;  //this is the state at which players can access inentory, or other UI menus
    //items and weapons HUD
	public static GameObject HUD;
    public static GameObject itemBar; //the bar that indicated how long it takes before an item is used
    public static Animator greenFlash; //the animation for when an item is used
	public static Animator blueSwirl; //the animation for when an item is used
    public static Text itemCount;
    public static Image HUDItemImage;
    public static GameObject CBTHealth;
    public static GameObject reloadingText;
    public static GameObject reloadBar;
    public static GameObject reloadPiBar;
    public static GameObject crosshair;
    public static GameObject combat;
    public static GameObject battleSystem;
	public static Animator screenFader;
	public static Animator levelBackground;

    void Start()
	{
		//all the shops
		theShop = GameObject.Find ("All Canvases/Canvas/TheItemShop");
		sell = GameObject.Find("All Canvases/Canvas/TheItemShop/Sell");
		upgrade = GameObject.Find("All Canvases/Canvas/TheItemShop/Upgrade");
		buy = GameObject.Find("All Canvases/Canvas/TheItemShop/Buy");
		storageMenu = GameObject.Find("All Canvases/Canvas/StorageMenu");
		//when pausing the game, unity cannot read the informaition from the gameobjects that the pause menu disables.
		//need to fix this
		player = GameObject.Find ("Player");
		lightRadius = GameObject.Find ("Player/PlayerLight").GetComponent<Light>();
		levelNumber = GameObject.Find ("All Canvases/Canvas/HUD/CurrentLevel/Number").GetComponent<Text>();
		levelBackground = GameObject.Find ("All Canvases/Canvas/HUD/CurrentLevel/Background").GetComponent<Animator>();
//		HPValue = GameObject.Find ("All Canvases/Canvas/HUD/XP/XPValue").GetComponent<Text>();
//		XPValue = GameObject.Find ("All Canvases/Canvas/HUD/XP/XPValue").GetComponent<Text>();
		goldValue = GameObject.Find ("All Canvases/Canvas/HUD/Gold/GoldValue");
		goldHUD = GameObject.Find ("All Canvases/Canvas/HUD/Gold");
		HPbar = GameObject.Find ("All Canvases/Canvas/HUD/HP/Background/Health");
		XPbar = GameObject.Find ("All Canvases/Canvas/HUD/XP/Background/Experience");
		ammoText = GameObject.Find ("All Canvases/Canvas/HUD/Equipment/AmmoBackground/AmmoText").GetComponent<Text>();
		HUD = GameObject.Find("All Canvases/Canvas/HUD");
        itemBar = GameObject.Find("All Canvases/Canvas/HUD/Equipment/Items/LoadingBar");
        HUDItemImage = GameObject.Find("All Canvases/Canvas/HUD/Equipment/Items/Image/ItemImage").GetComponent<Image>();
        greenFlash = GameObject.Find("All Canvases/Canvas/HUD/Equipment/Items/GreenFlash").GetComponent<Animator>();
		blueSwirl = GameObject.Find("All Canvases/Canvas/HUD/Equipment/Items/BlueSwirl").GetComponent<Animator>();
        reloadingText = GameObject.Find("All Canvases/BattleSystem/ReloadingText");
        reloadBar = GameObject.Find("All Canvases/BattleSystem/ReloadBar/Image");
        reloadPiBar = GameObject.Find("All Canvases/BattleSystem/ReloadBar");
        itemCount = GameObject.Find("All Canvases/Canvas/HUD/Equipment/Items/ItemCount/Text").GetComponent<Text>();
        CBTHealth = GameObject.Find("All Canvases/Canvas/HUD/Equipment/Items/ItemCount/CBTHealthText");
        pauseMenus = GameObject.Find ("All Canvases/Canvas/PauseMenus");
		textBox = GameObject.Find ("All Canvases/Canvas/TextManager/DialogueBox");
		textManager = GameObject.Find ("All Canvases/Canvas/TextManager");
		levelStats = GameObject.Find ("All Canvases/Canvas/LevelStats");
		exclamation = GameObject.Find ("Player/PlayerIcons/Exclamation");
		gameManager = GameObject.Find ("GameManager");
        crosshair = GameObject.Find("All Canvases/BattleSystem/Combat/HandleSlideArea/Crosshair");
        combat = GameObject.Find("All Canvases/BattleSystem/Combat");
        battleSystem = GameObject.Find("All Canvases/BattleSystem");
		screenFader = GameObject.Find("All Canvases/Canvas/ScreenEffects/Effects").GetComponent<Animator>();

		//checking to see if heath is greater than max health
		if (DataStorage.health > DataStorage.maxHealth) 
		DataStorage.health = DataStorage.maxHealth;


		DataStorage.UpdateHUD ();
	}//end of function

	public void Save()
	{
		SaveLoadManager.SavePlayer (this);
	}
	//animating the money for the HUD when a player picks up cash
	public static void DisplayGold()
	{
		//updating gold, displayig gold, and animating gold
		DataStorage.goldValue.GetComponent<Text>().text = money.ToString ("n0");
		if (!DataStorage.goldHUD.GetComponent<Animator> ().GetCurrentAnimatorStateInfo (0).IsName ("MoneyHUD")) 
		{
			DataStorage.goldHUD.GetComponent<Animator>().Play ("MoneyHUD", 0, 0);
		} 
		else if (DataStorage.goldHUD.GetComponent<Animator> ().GetCurrentAnimatorStateInfo (0).normalizedTime > .2f)
			DataStorage.goldHUD.GetComponent<Animator>().Play ("MoneyHUD", 0, .2f);

	}

	//ending the game in death
	public static void GameOver()
    {
			SceneManager.LoadScene("StartMenuScene", LoadSceneMode.Single);
    }

    public static void UpdateHolster()
    {
        switch (DataStorage.weaponType[DataStorage.curWeapon])
        {
            default:
                ammoText.text = DataStorage.holster[DataStorage.curWeapon] + "/" + (DataStorage.HGAmmo).ToString();
                break;
            case "Rifle":
                ammoText.text = DataStorage.holster[DataStorage.curWeapon] + "/" + (DataStorage.rifleAmmo).ToString();
                break;
            case "Shotgun":
                ammoText.text = DataStorage.holster[DataStorage.curWeapon] + "/" + (DataStorage.SGAmmo).ToString();
                break;
            case "Automatic":
                ammoText.text = DataStorage.holster[DataStorage.curWeapon] + "/" + (DataStorage.MGAmmo).ToString();
                break;
            case "Magnum":
                ammoText.text = DataStorage.holster[DataStorage.curWeapon] + "/" + (DataStorage.magnumAmmo).ToString();
                break;
            case "Explosive":
                ammoText.text = DataStorage.holster[DataStorage.curWeapon] + "/" + (DataStorage.explosiveAmmo).ToString();
                break;
        }
    }
    //just callign the health
    public static void UpdateHUDHealth()
    {
        HPbar.transform.localScale = new Vector3((float)health / maxHealth, 1, 0);
        //	HPValue.text = Mathf.Round ((float)health / maxHealth * 100) + "%";
        //changing the color based on the portion of health
        HPbar.GetComponent<Image>().color = new Color(1, 0, 1, (float)health / maxHealth);
		if (DataStorage.health > 0)
        HPbar.GetComponent<Animator>().speed = (float)maxHealth / (float)health;
        if (health > maxHealth)
           health = maxHealth;
    }
    //calling all HUDs
    public static void UpdateHUD()
	{
        //updating the hotkeys for items
        switch (DataStorage.equippedItem)
        {
            default:
                itemCount.text = itemSmallAid.ToString();
                break;
            case 2:
                itemCount.text = itemMedAid.ToString();
                break;
            case 3:
                itemCount.text = itemLargeAid.ToString();
                break;
        }

		if (lightRadius == null || player == null)
		{
			lightRadius = GameObject.Find ("Player/PlayerLight").GetComponent<Light>();
			player = GameObject.Find ("Player");
		}
        lightRadius.spotAngle = lightAngle;
		lightRadius.range = lightRange;
		player.GetComponent<Controls> ().speed = speed;
		//updating HUD
		HPbar.transform.localScale = new Vector3 ((float)health/maxHealth,1,0);
		XPbar.transform.localScale = new Vector3 ((float)XP / maxXP,1,0);
	//	HPValue.text = Mathf.Round ((float)health / maxHealth * 100) + "%";
        //changing the color based on the portion of health
        HPbar.GetComponent<Image>().color = new Color(1, 0, 1, (float)health / maxHealth);
		if (DataStorage.health > 0)
        HPbar.GetComponent <Animator>().speed = (float)maxHealth/(float)health;
  //    XPValue.text = Mathf.Round ((float)XP / maxXP * 100) + "%";
		levelNumber.text  = currentLevel.ToString();

		switch (DataStorage.weaponType[DataStorage.curWeapon])
		{
            default:
                ammoText.text = DataStorage.holster[DataStorage.curWeapon] + "/" + (DataStorage.HGAmmo).ToString();
                break;
            case "Rifle":
                ammoText.text = DataStorage.holster[DataStorage.curWeapon] + "/" + (DataStorage.rifleAmmo).ToString();
                break;
            case "Shotgun":
                ammoText.text = DataStorage.holster[DataStorage.curWeapon] + "/" + (DataStorage.SGAmmo).ToString();
                break;
            case "Automatic":
                ammoText.text = DataStorage.holster[DataStorage.curWeapon] + "/" + (DataStorage.MGAmmo).ToString();
                break;
            case "Magnum":
                ammoText.text = DataStorage.holster[DataStorage.curWeapon] + "/" + (DataStorage.magnumAmmo).ToString();
                break;
            case "Explosive":
                ammoText.text = DataStorage.holster[DataStorage.curWeapon] + "/" + (DataStorage.explosiveAmmo).ToString();
                break;
        }
	}//end of function



	//loading all player stats and game information
	public void Load()
	{
		float[] loadedStats = SaveLoadManager.LoadGame ();

		money = (int)loadedStats [0];
		currentLevel = (int)loadedStats [1];
		XP = (int)loadedStats [2];
		maxXP = (int)loadedStats [3];
		health = (int)loadedStats [4];
		maxHealth = (int)loadedStats [5];
		totalMoneyEarned = (int)loadedStats [6];
		moneySpent = (int)loadedStats [7];
		shotsFired = (int)loadedStats [8];
		targetsHit = (int)loadedStats [9];
		hitRatio = loadedStats [10];
		enemiesKilled = (int)loadedStats [11];
		itemsUsed = (int)loadedStats [12];
		damageTaken = (int)loadedStats [13];
		damageDealt = loadedStats [14];
		numberOfSaves = (int)loadedStats [15];
		currentTime = loadedStats [16];

		//ammo

		HGAmmo = (int)loadedStats [17];
		SGAmmo = (int)loadedStats [18];
		MGAmmo = (int)loadedStats [19];
		rifleAmmo = (int)loadedStats [20];
		magnumAmmo = (int)loadedStats [21];
		//loading weapon damage
		weaponDamage[0] = loadedStats [22];
		weaponDamage[1] = loadedStats [23];
		weaponDamage[2] = loadedStats [24];
		weaponDamage[3] = loadedStats [25];
		weaponDamage[4] = loadedStats [26];
		weaponDamage[5] = loadedStats [27];
		weaponDamage[6] = loadedStats [28];
		weaponDamage[7] = loadedStats [29];
		weaponDamage[8] = loadedStats [30];
		weaponDamage[9] = loadedStats [31];
		weaponDamage[10] = loadedStats [32];
		weaponDamage[11] = loadedStats [33];
		weaponDamage[12] = loadedStats [34];
		weaponDamage[13] = loadedStats [35];
		weaponDamage[14] = loadedStats [36];
		weaponDamage[15] = loadedStats [37];
		weaponDamage[16] = loadedStats [38];

		//loading capacity
		capacity[0] = (int)loadedStats [39];
		capacity[1] = (int)loadedStats [40];
		capacity[2] = (int)loadedStats [41];
		capacity[3] = (int)loadedStats [42];
		capacity[4] = (int)loadedStats [43];
		capacity[5] = (int)loadedStats [44];
		capacity[6] = (int)loadedStats [45];
		capacity[7] = (int)loadedStats [46];
		capacity[8] = (int)loadedStats [47];
		capacity[9] = (int)loadedStats [48];
		capacity[10] = (int)loadedStats [49];
		capacity[11] = (int)loadedStats [50];
		capacity[12] = (int)loadedStats [51];
		capacity[13] = (int)loadedStats [52];
		capacity[14] = (int)loadedStats [53];
		capacity[15] = (int)loadedStats [54];
		capacity[16] = (int)loadedStats [55];

		//reload
		reload[0] = (int)loadedStats [56];
		reload[1] = (int)loadedStats [57];
		reload[2] = (int)loadedStats [58];
		reload[3] = (int)loadedStats [59];
		reload[4] = (int)loadedStats [60];
		reload[5] = (int)loadedStats [61];
		reload[6] = (int)loadedStats [62];
		reload[7] = (int)loadedStats [63];
		reload[8] = (int)loadedStats [64];
		reload[9] = (int)loadedStats [65];
		reload[10] = (int)loadedStats [66];
		reload[11] = (int)loadedStats [67];
		reload[12] = (int)loadedStats [68];
		reload[13] = (int)loadedStats [69];
		reload[14] = (int)loadedStats [70];
		reload[15] = (int)loadedStats [71];
		reload[16] = (int)loadedStats [72];

		//ACCURACY

		accuracy[0] = loadedStats [73];
		accuracy[1] = loadedStats [74];
		accuracy[2] = loadedStats [75];
		accuracy[3] = loadedStats [76];
		accuracy[4] = loadedStats [77];
		accuracy[5] = loadedStats [78];
		accuracy[6] = loadedStats [79];
		accuracy[7] = loadedStats [80];
		accuracy[8] = loadedStats [81];
		accuracy[9] = loadedStats [82];
		accuracy[10] = loadedStats [83];
		accuracy[11] = loadedStats [84];
		accuracy[12] = loadedStats [85];
		accuracy[13] = loadedStats [86];
		accuracy[14] = loadedStats [87];
		accuracy[15] = loadedStats [88];
		accuracy[16] = loadedStats [89];

		//range

		range[0] = (int)loadedStats [90];
		range[1] = (int)loadedStats [91];
		range[2] = (int)loadedStats [92];
		range[3] = (int)loadedStats [93];
		range[4] = (int)loadedStats [94];
		range[5] = (int)loadedStats [95];
		range[6] = (int)loadedStats [96];
		range[7] = (int)loadedStats [97];
		range[8] = (int)loadedStats [98];
		range[9] = (int)loadedStats [99];
		range[10] = (int)loadedStats [100];
		range[11] = (int)loadedStats [101];
		range[12] = (int)loadedStats [102];
		range[13] = (int)loadedStats [103];
		range[14] = (int)loadedStats [104];
		range[15] = (int)loadedStats [105];
		range[16] = (int)loadedStats [106];

		//critical
		criticalChance[0] = loadedStats [107];
		criticalChance[1] = loadedStats [108];
		criticalChance[2] = loadedStats [109];
		criticalChance[3] = loadedStats [110];
		criticalChance[4] = loadedStats [111];
		criticalChance[5] = loadedStats [112];
		criticalChance[6] = loadedStats [113];
		criticalChance[7] = loadedStats [114];
		criticalChance[8] = loadedStats [115];
		criticalChance[9] = loadedStats [116];
		criticalChance[10] = loadedStats [117];
		criticalChance[11] = loadedStats [118];
		criticalChance[12] = loadedStats [119];
		criticalChance[13] = loadedStats [120];
		criticalChance[14] = loadedStats [121];
		criticalChance[15] = loadedStats [122];
		criticalChance[16] = loadedStats [123];

		//sell value
		sellValue[0] = (int)loadedStats [124];
		sellValue[1] = (int)loadedStats [125];
		sellValue[2] = (int)loadedStats [126];
		sellValue[3] = (int)loadedStats [127];
		sellValue[4] = (int)loadedStats [128];
		sellValue[5] = (int)loadedStats [129];
		sellValue[6] = (int)loadedStats [130];
		sellValue[7] = (int)loadedStats [131];
		sellValue[8] = (int)loadedStats [132];
		sellValue[9] = (int)loadedStats [133];
		sellValue[10] = (int)loadedStats [134];
		sellValue[11] = (int)loadedStats [135];
		sellValue[12] = (int)loadedStats [136];
		sellValue[13] = (int)loadedStats [137];
		sellValue[14] = (int)loadedStats [138];
		sellValue[15] = (int)loadedStats [139];
		sellValue[16] = (int)loadedStats [140];

		//sell value
		fireRate[0] = (int)loadedStats [141];
		fireRate[1] = (int)loadedStats [142];
		fireRate[2] = (int)loadedStats [143];
		fireRate[3] = (int)loadedStats [144];
		fireRate[4] = (int)loadedStats [145];
		fireRate[5] = (int)loadedStats [146];
		fireRate[6] = (int)loadedStats [147];
		fireRate[7] = (int)loadedStats [148];
		fireRate[8] = (int)loadedStats [149];
		fireRate[9] = (int)loadedStats [150];
		fireRate[10] = (int)loadedStats [151];
		fireRate[11] = (int)loadedStats [152];
		fireRate[12] = (int)loadedStats [153];
		fireRate[13] = (int)loadedStats [154];
		fireRate[14] = (int)loadedStats [155];
		fireRate[15] = (int)loadedStats [156];
		fireRate[16] = (int)loadedStats [157];

        curWeapon = (int)loadedStats[158];

		//PlayerDisplay.UpdateDisplay;

        //loading all the weapons the player last had
        obtainedWeapons[0] = (int)loadedStats[159];
        obtainedWeapons[1] = (int)loadedStats[160];
        obtainedWeapons[2] = (int)loadedStats[161];
        obtainedWeapons[3] = (int)loadedStats[162];
        obtainedWeapons[4] = (int)loadedStats[163];
        obtainedWeapons[5] = (int)loadedStats[164];
        obtainedWeapons[6] = (int)loadedStats[165];
        obtainedWeapons[7] = (int)loadedStats[166];
        obtainedWeapons[8] = (int)loadedStats[167];
        obtainedWeapons[9] = (int)loadedStats[168];
        obtainedWeapons[10] = (int)loadedStats[169];
        obtainedWeapons[11] = (int)loadedStats[170];
        obtainedWeapons[12] = (int)loadedStats[171];
        obtainedWeapons[13] = (int)loadedStats[172];
        obtainedWeapons[14] = (int)loadedStats[173];
        obtainedWeapons[15] = (int)loadedStats[174];
        obtainedWeapons[16] = (int)loadedStats[175];

		//the shop
		shopHandgunAmmo = (int)loadedStats[176];
		shopShotgunAmmo = (int)loadedStats[177];
		shopMachinegunAmmo = (int)loadedStats[178];
		shopRifleAmmo = (int)loadedStats[179];
		shopMagnumAmmo = (int)loadedStats[180];
		shopSmallAid = (int)loadedStats[181];
		shopMedAid = (int)loadedStats[182];
		shopLargeAid= (int)loadedStats[183];
		shopHolyWater = (int)loadedStats[184];

		//Item Inventory
		itemHolyWater = (int)loadedStats[185];
		itemLargeAid = (int)loadedStats[186];
		itemMedAid = (int)loadedStats[187];
		itemSmallAid = (int)loadedStats[188];
		itemSmallKey = (int)loadedStats[189];

		//damage
		curDamage[0] = (int)loadedStats[190];
		curDamage[1] = (int)loadedStats[191];
		curDamage[2] = (int)loadedStats[192];
		curDamage[3] = (int)loadedStats[193];
		curDamage[4] = (int)loadedStats[194];
		curDamage[5] = (int)loadedStats[195];
		curDamage[6] = (int)loadedStats[196];
		curDamage[7] = (int)loadedStats[197];
		curDamage[8] = (int)loadedStats[198];
		curDamage[9] = (int)loadedStats[199];
		curDamage[10] = (int)loadedStats[200];
		curDamage[11] = (int)loadedStats[201];
		curDamage[12] = (int)loadedStats[202];
		curDamage[13] = (int)loadedStats[203];
		curDamage[14] = (int)loadedStats[204];
		curDamage[15] = (int)loadedStats[205];
		curDamage[16] = (int)loadedStats[206];

		//reload
		curReload[0] = (int)loadedStats[207];
		curReload[1] = (int)loadedStats[208];
		curReload[2] = (int)loadedStats[209];
		curReload[3] = (int)loadedStats[210];
		curReload[4] = (int)loadedStats[211];
		curReload[5] = (int)loadedStats[212];
		curReload[6] = (int)loadedStats[213];
		curReload[7] = (int)loadedStats[214];
		curReload[8] = (int)loadedStats[215];
		curReload[9] = (int)loadedStats[216];
		curReload[10] = (int)loadedStats[217];
		curReload[11] = (int)loadedStats[218];
		curReload[12] = (int)loadedStats[219];
		curReload[13] = (int)loadedStats[220];
		curReload[14] = (int)loadedStats[221];
		curReload[15] = (int)loadedStats[222];
		curReload[16] = (int)loadedStats[223];

		//firerate
		curFireRate[0] = (int)loadedStats[224];
		curFireRate[1] = (int)loadedStats[225];
		curFireRate[2] = (int)loadedStats[226];
		curFireRate[3] = (int)loadedStats[227];
		curFireRate[4] = (int)loadedStats[228];
		curFireRate[5] = (int)loadedStats[229];
		curFireRate[6] = (int)loadedStats[230];
		curFireRate[7] = (int)loadedStats[231];
		curFireRate[8] = (int)loadedStats[232];
		curFireRate[9] = (int)loadedStats[233];
		curFireRate[10] = (int)loadedStats[234];
		curFireRate[11] = (int)loadedStats[235];
		curFireRate[12] = (int)loadedStats[236];
		curFireRate[13] = (int)loadedStats[237];
		curFireRate[14] = (int)loadedStats[238];
		curFireRate[15] = (int)loadedStats[239];
		curFireRate[16] = (int)loadedStats[240];

		//capacity
		curCapacity[0] = (int)loadedStats[241];
		curCapacity[1] = (int)loadedStats[242];
		curCapacity[2] = (int)loadedStats[243];
		curCapacity[3] = (int)loadedStats[244];
		curCapacity[4] = (int)loadedStats[245];
		curCapacity[5] = (int)loadedStats[246];
		curCapacity[6] = (int)loadedStats[247];
		curCapacity[7] = (int)loadedStats[248];
		curCapacity[8] = (int)loadedStats[249];
		curCapacity[9] = (int)loadedStats[250];
		curCapacity[10] = (int)loadedStats[251];
		curCapacity[11] = (int)loadedStats[252];
		curCapacity[12] = (int)loadedStats[253];
		curCapacity[13] = (int)loadedStats[254];
		curCapacity[14] = (int)loadedStats[255];
		curCapacity[15] = (int)loadedStats[256];
		curCapacity[16] = (int)loadedStats[257];

		//crit
		curCrit[0] = (int)loadedStats[258];
		curCrit[1] = (int)loadedStats[259];
		curCrit[2] = (int)loadedStats[261];
		curCrit[3] = (int)loadedStats[262];
		curCrit[4] = (int)loadedStats[263];
		curCrit[5] = (int)loadedStats[264];
		curCrit[6] = (int)loadedStats[265];
		curCrit[7] = (int)loadedStats[266];
		curCrit[8] = (int)loadedStats[267];
		curCrit[9] = (int)loadedStats[268];
		curCrit[10] = (int)loadedStats[269];
		curCrit[11] = (int)loadedStats[270];
		curCrit[12] = (int)loadedStats[271];
		curCrit[14] = (int)loadedStats[272];
		curCrit[15] = (int)loadedStats[273];
		curCrit[16] = (int)loadedStats[274];

		//range
		curRange[0] = (int)loadedStats[275];
		curRange[1] = (int)loadedStats[276];
		curRange[2] = (int)loadedStats[277];
		curRange[3] = (int)loadedStats[278];
		curRange[4] = (int)loadedStats[279];
		curRange[5] = (int)loadedStats[280];
		curRange[6] = (int)loadedStats[281];
		curRange[7] = (int)loadedStats[282];
		curRange[8] = (int)loadedStats[283];
		curRange[9] = (int)loadedStats[284];
		curRange[10] = (int)loadedStats[285];
		curRange[11] = (int)loadedStats[286];
		curRange[12] = (int)loadedStats[287];
		curRange[13] = (int)loadedStats[288];
		curRange[14] = (int)loadedStats[289];
		curRange[15] = (int)loadedStats[290];
		curRange[16] = (int)loadedStats[291];


		//accuracy
		curRange[0] = (int)loadedStats[292];
		curRange[1] = (int)loadedStats[293];
		curRange[2] = (int)loadedStats[294];
		curRange[3] = (int)loadedStats[295];
		curRange[4] = (int)loadedStats[296];
		curRange[5] = (int)loadedStats[297];
		curRange[6] = (int)loadedStats[298];
		curRange[7] = (int)loadedStats[299];
		curRange[8] = (int)loadedStats[300];
		curRange[9] = (int)loadedStats[301];
		curRange[10] = (int)loadedStats[302];
		curRange[11] = (int)loadedStats[303];
		curRange[12] = (int)loadedStats[304];
		curRange[13] = (int)loadedStats[305];
		curRange[14] = (int)loadedStats[306];
		curRange[15] = (int)loadedStats[307];
		curRange[16] = (int)loadedStats[308];

		totalXP = (int)loadedStats[309];
		shopKeepTimer = loadedStats[310];
        maxWeight = loadedStats[311];
        curItems = (int)loadedStats[312];
		curWeight = loadedStats[313];
		//saving current holster
		holster[0] = (int)loadedStats[314];
		holster[1] = (int)loadedStats[315];
		holster[2] = (int)loadedStats[316];
		holster[3] = (int)loadedStats[317];
		holster[4] = (int)loadedStats[318];
		holster[5] = (int)loadedStats[319];
		holster[6] = (int)loadedStats[320];
		holster[7] = (int)loadedStats[321];
		holster[8] = (int)loadedStats[322];
		holster[9] = (int)loadedStats[323];
		holster[10] = (int)loadedStats[324];
		holster[11] = (int)loadedStats[325];
		holster[12] = (int)loadedStats[326];
		holster[13] = (int)loadedStats[327];
		holster[14] = (int)loadedStats[328];
		holster[15] = (int)loadedStats[329];
		holster[16] = (int)loadedStats[330];
		holster[17] = (int)loadedStats[331];
		holster[18] = (int)loadedStats[332];
		holster[19] = (int)loadedStats[333];
		holster[20] = (int)loadedStats[334];
		damage = (int)loadedStats[335];

		//player stats (attributes)

		strength = (int)loadedStats[336];
		constitution = (int)loadedStats[337];
		fortitude = (int)loadedStats[338];
		dexterity = (int)loadedStats[339];
		luck = (int)loadedStats[340];
		perception = (int)loadedStats[341];
		agility = (int)loadedStats[342];
		intelligence = (int)loadedStats[343];
		charisma = (int)loadedStats[344];
		playerStats = (int)loadedStats[345];
		lightAngle = loadedStats[346];
		lightRange = loadedStats[347];
		speed = loadedStats[348];

		weaponDamage[17] = loadedStats[349];
		weaponDamage[18] = loadedStats[350];
		weaponDamage[19] = loadedStats[351];
		weaponDamage[20] = loadedStats[352];

		capacity[17] = (int)loadedStats[353];
		capacity[18] = (int)loadedStats[354];
		capacity[19] = (int)loadedStats[355];
		capacity[20] = (int)loadedStats[356];

		reload[17] = (int)loadedStats[357];
		reload[18] = (int)loadedStats[358];
		reload[19] = (int)loadedStats[359];
		reload[20] = (int)loadedStats[360];

		accuracy[17] =(int)loadedStats[361];
		accuracy[18] =(int)loadedStats[362];
		accuracy[19] =(int)loadedStats[363];
		accuracy[20] =(int)loadedStats[364];

		range[17] = (int)loadedStats[365];
		range[18] = (int)loadedStats[366];
		range[19] = (int)loadedStats[367];
		range[20] = (int)loadedStats[368];

		criticalChance[17] = (int)loadedStats[369];
		criticalChance[18] = (int)loadedStats[370];
		criticalChance[19] = (int)loadedStats[371];
		criticalChance[20] = (int)loadedStats[372];
	
		fireRate[17] = (int)loadedStats[373];
		fireRate[18] = (int)loadedStats[374];
		fireRate[19] = (int)loadedStats[375];
		fireRate[20] = (int)loadedStats[376];

		itemsSold = (int)loadedStats[377];
		itemsBought = (int)loadedStats[378];
        equippedItem= (int)loadedStats[379];

        //the difficuty
        difficulty = (int)loadedStats[380];
		//shotsMissed
        shotsMissed = (int)loadedStats[381];
		//Battles Won
        battlesWon = (int)loadedStats[382];



        //need to add save/load upgrades for the new weapons (from 16 t 21)
    }
    public static int difficulty = 1;

       //public static int id;
       [SerializeField]
    public static int money = 10000;
	public static int currentLevel = 1;
	public static int XP = 33000;
	public static int maxXP = 128000;
	public static int totalXP = 0;
	public static int health = 10;
	public static int maxHealth = 120;
	public static int HGAmmo = 24;
	public static int rifleAmmo = 5;
	public static int SGAmmo = 50;
    public static int MGAmmo = 80;
	public static int magnumAmmo = 5;
	public static int explosiveAmmo = 5;
	public static float shopKeepTimer = 0;
    public static float maxWeight = 10.0f;
	public static float curWeight;
    public static int curItems;
    public static int equippedItem = 1;
    public static float damage = 0f;

	//player stats
	public static int strength = 2; //weight capacity and push speed
	public static int constitution = 5; //max health
	public static int fortitude = 1; //defense
	public static int dexterity = 2; //damage
	public static int luck = 2; //chance of finding beter items and chance of landing critical hits
	public static int perception = 3; //light radius and item observation
	public static int agility = 3; //run/walk speed, reload speed, and item use speed
	public static int intelligence = 4; //item effectiveness, and awarded experience
	public static int charisma = 2; //barter - dybbuk shop item capacity, item restore time, item sell value, and item cost
	public static int playerStats = 0; //the ammount a player can spend on an attribute or stat - these are gained upon leveling up

	public static float speed = 3.7f;
	public static float lightRange = 12f;
	public static float lightAngle = 150f;

    public static int curWeapon = 13;
    //weapons
    [Space(10)]                                                                                                                                                                                                                                                                                      //DLC weapons                    
	public static string[] weaponName = new string[] {"FBI Custom", "Oppressor", "The Blacklist", "Trident",   "Silencer", "Seeker", "Hunter Killer", "Crow's Nest",  "12 Gauge", "Orthrus",  "Cerberus",  "Devestator", "Savage One", "Diminisher", "Revolver", "Scylla",     "Day Ender",     "Redeemer",    "Hellfire",   "Energy Rifle",     "Eradicator"};
	[Space(10)]
	public static string[] weaponType = new string[] {"Handgun",        "Handgun",       "Handgun",   "Handgun",     "Rifle",   "Rifle",    "Rifle",         "Rifle",      "Shotgun",   "Shotgun",   "Shotgun",    "Shotgun",    "Shotgun",   "Automatic",  "Magnum",  "Magnum",    "Magnum",           "Shotgun",   "Automatic",    "Rifle",          "Explosive"};
    [Header ("Upgrades")]
	[Space(20)]
	public static float[] weaponDamage =                         {8f,               12f,                16f,            8f,          24f,        24f,          27f,              22f,           24f,         32f,         36f,           30f,           40f,           3f,            65f,        80f,           100f,        5f,              8f,             20f,                   500f};
	[Space(10)]
	public static int[] capacity =                             {8,              12,               12,           18,         4,         4,           4,                4,           2,         2,          3,            7,            10,           30,           8,         6,             6,         12,              300,            8,                   1};
	[Space(10)]
	public static float[] reload =                             {4.0f,              3f,               3f,                3f,              3f,        3f,              3f,               3f,                2f,              2f,         2f,          2f,            2f,         1.5f,           6f,           6f,         6f,             6f,          3f,              3f,            3f,                     3f};
	[Space(10)]
	public static float[] fireRate =                           {.8f,            .8f,            .8f,          1.0f,       2.4f,            2.4f,            2.4f,          1.2f,          2.4f,            2.4f,            2.4f,          .8f,         2.4f,          .1f,            2.4f,          2.4f,           2.4f,            2.4f,            .06f,          2.4f,                    3.0f};
	[Space(10)] 
	public static float[] accuracy =                          {0,                  0,             0,              0,         0,             0,          0,                 0,          2f,      0f,           0f,             0f,              0f,           0,             -.5f,           -.5f,        -.5f,            0,              0,              0,                   0f }; //the maximum size of the slider
	[Space(10)]
	public static float[] range =                              {1f,              1f,               1f,           1f,         1f,       1f,           1f,              1f,           1f,        1f,         1f,           1f,            1f,          1f,           1f,         1f,             1f,        1f,           1f,             1f,                     1f}; //the maximum size of the target bar
	[Space(10)]
	public static float[] criticalChance =                    {.16f,           .16f,             .16f,          .16f,      .3f,     .3f,         .3f,            .3f,           .08f,        .03f,          .03f,            .03f,            .03f,           .03f,        .12f,        .12f,           .12f,       .12f,        .12f,              .12f,               .12f};
	[Space(10)]
	[Header ("Selling price of each weapon")]
	public static int[] sellValue =                            {50,              75,               100,          100,         60,      80,           200,             150,           80,        120,        150,          180,          200,          75,         100,         200,            300,        400,            625,           600,                   1000};
	//do not save this following infrmation
    //it is ONLY used to determine the value of what each gun recieved per upgrade
    //    weapon names          {"FBI Custom", "9mm Oppressor", "The Blacklist", "Trident",   "Silencer", "Seeker", "Hunter Killer", "Crow's Nest",  "12 Gauge", "Orthrus",  "Cerberus",  "Devestator", "Savage One", "Diminisher", "Revolver", "Scylla",     "Doomsday",     "Redeemer",    "Hellfire",   "Energy Rifle",     "Eradicator"};
    public static int[] upDamage = { 4, 4, 6, 4, 8, 10, 12, 10, 8, 10, 8, 16, 10, 1, 16, 20, 24, 10, 3, 16, 40 };
    public static float[] upReload = { .2f, .2f, .2f, .2f, .2f, .2f, .2f, .2f, .2f, .2f, .2f, .2f, .2f, .1f, .1f, .1f, .2f, .2f, .2f, .2f, .2f};
    public static int[] upCapacity = { 1, 1, 2, 3, 2, 3, 2, 1, 0, 0, 0, 1, 2, 10, 1, 1, 1, 1, 30, 1, 0 };
    public static float[] upFireRate = { .06f, .06f, .06f, .06f, .06f, .06f, .06f, .06f, .06f, .06f, .06f, .06f, .06f, 0, .06f, .06f, .06f, .06f, 0, .06f, .06f };
    public static float[] upAccuracy = { .2f, .2f, .2f, .2f, .2f, .2f, .2f, .2f, .2f, .2f, .2f, .2f, .2f, .2f, .05f, .05f, .05f, .2f, .2f, .2f, .2f, };
    public static float[] upCritical = { .02f, .02f, .02f, .02f, .02f, .02f,.02f, .02f, .02f, .02f, .02f, .02f, .02f, .02f, .02f, .02f, .02f, .02f, .02f, .02f, .02f };
    public static float[] upRange = { .2f, .2f, .2f, .2f, .2f, .2f, .2f, .2f, .2f, .2f, .2f, .2f, .2f, .2f, .2f, .2f, .2f, .2f, .2f, .2f, .2f, };
    
    //weapons obtained
	//this is how we keep track of what weapons the player has in his or her inventory.
	//0 means there is no weapon of this type and 1 means the player has this weapon
	[Space(10)]
	[Header("Weapons that have been obtained: 0 means not obtained and 1 means obtained")]
	public static int[] obtainedWeapons=                 {1,               1,                1,            1,         1,        1,          1,                1,           1,           1,           1,             1,           1,             1,          1,        1,             1,    1,           1,        1,             1};
	//this is how we keep track of what weapon is currently equipped.
	//the value of this variable determines what index the above variables are on

	//the amount of ammo in the current holster
	public static int[] holster = {0,0,0,0,0,0,0,0,2,0,0,0,0,0,0,0,0,0,0,0,0};
		


	//STATS
	[HideInInspector]
	public static int totalMoneyEarned= 0;
	[HideInInspector]
	public static int moneySpent = 0;
	[HideInInspector]
	public static int shotsFired = 0;
	[HideInInspector]
	public static int shotsMissed = 0;
	[HideInInspector]
	public static int targetsHit = 0;
	[HideInInspector]
	public static float hitRatio = 0.00f; //targets hit divided by shots fired + %
	[HideInInspector]
	public static int enemiesKilled = 0;
	[HideInInspector]
	public static int battlesWon = 0;
	[HideInInspector]
	public static int itemsUsed = 0;
	[HideInInspector]
	public static int damageTaken = 0;
	[HideInInspector]
	public static float damageDealt = 0;
	[HideInInspector]
	public static int numberOfSaves = 0;
	[HideInInspector]
	public static float currentTime = 0f; //Time.time
	[HideInInspector]
	public static int itemsSold = 0;
	[HideInInspector]
	public static int itemsBought = 0;


	[Header("The cost of upgrades for each weapon")]
	//reload cost
	public static int[] reloadCost=                    {75,               75,                75,            75,               75,                75,            75,               75,                75,          75,               75,                75,            75,               75,                75,         75,               75,                75,            75,               75,                75};
	//damage cost
	[Space(10)]
	public static int[] damageCost=                    {75,               75,                75,            75,               75,                75,            75,               75,                75,          75,               75,                75,            75,               75,                75,         75,               75,                75,            75,               75,                75};
	//capacity cost 
	[Space(10)]
	public static int[] capacityCost=                  {75,               75,                75,            75,               75,                75,            75,               75,                75,          75,               75,                75,            75,               75,                75,         75,               75,                75,            75,               75,                75};
	//accuracy cost
	[Space(10)]
	public static int[] acCost=                 		{75,               75,                75,            75,               75,                75,            75,               75,                75,          75,               75,                75,            75,               75,                75,         75,               75,                75,            75,               75,                75};
	//fire rate cost
	[Space(10)]
	public static int[] frCost=                   	    {75,               75,                75,            75,               75,                75,            75,               75,                75,          75,               75,                75,            75,               75,                75,         75,               75,                75,            75,               75,                75};
	//range cost
	[Space(10)]
	public static int[] rangeCost=                    {75,               75,                75,            75,               75,                75,            75,               75,                75,          75,               75,                75,            75,               75,                75,         75,               75,                75,            75,               75,                75};
	//critical chance cost
	[Space(10)]
	public static int[] CCCost=                        {75,               75,                75,            75,               75,                75,            75,               75,                75,          75,               75,                75,            75,               75,                75,         75,               75,                75,            75,               75,                75};

	[HideInInspector]
	public static int shopHandgunAmmo;
	[HideInInspector]
	public static int shopShotgunAmmo;
	[HideInInspector]
	public static int shopRifleAmmo;
	[HideInInspector]
	public static int shopMachinegunAmmo;
	[HideInInspector]
	public static int shopMagnumAmmo;
	[HideInInspector]
	public static int shopSmallAid;
	[HideInInspector]
	public static int shopMedAid;
	[HideInInspector]
	public static int shopLargeAid;
	[HideInInspector]
	public static int shopHolyWater;

	//player's items
	[HideInInspector]//the numbers represent the order which they are given for item switching
	public static int itemSmallAid = 1; //1
	[HideInInspector]
	public static int itemMedAid = 1; //2
	[HideInInspector]
	public static int itemLargeAid = 1; //3
	[HideInInspector]
	public static int itemHolyWater = 1; //4
	[HideInInspector]
	public static int itemSmallKey = 1;


	//current level of upgrades
	[HideInInspector]
	public static int[] curDamage =                         {0,               0,                0,            0,          0,        0,          0,              0,           0,         0,         0,           0,           0,           0,            0,        0,           0,0,        0,           0,0};
	[HideInInspector]
	public static int[] curReload =                         {0,               0,                0,            0,          0,        0,          0,              0,           0,         0,         0,           0,           0,           0,            0,        0,           0,0,        0,           0,0};
	[HideInInspector]
	public static int[] curFireRate =                         {0,               0,                0,            0,          0,        0,          0,              0,           0,         0,         0,           0,           0,           5,            0,        0,           0,0,        5,           0,0};
	[HideInInspector]
	public static int[] curCapacity =                         {0,               0,                0,            0,          0,        0,          0,              0,           5,         5,         5,           0,           0,           0,            0,        0,           0,0,        0,           0,5};
	[HideInInspector]
	public static int[] curCrit =                         		{0,               0,                0,            0,          0,        0,          0,              0,           0,         0,         0,           0,           0,           0,            0,        0,           0,0,        0,           0,0};
	[HideInInspector]
	public static int[] curRange =                        		 {0,               0,                0,            0,          0,        0,          0,              0,           0,         0,         0,           0,           0,           0,            0,        0,           0,0,        0,           0,0};
	[HideInInspector]
	public static int[] curAccuracy =                         {0,               0,                0,            0,          0,        0,          0,              0,           0,         0,         0,           0,           0,           0,            0,        0,           0,0,        0,           0,0};
}