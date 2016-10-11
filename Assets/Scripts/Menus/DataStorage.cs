using UnityEngine;
using System.Collections;

public class DataStorage : MonoBehaviour

{
	public void Save()
	{
		SaveLoadManager.SavePlayer (this);
	}

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
		damageDealt = (int)loadedStats [14];
		numberOfSaves = (int)loadedStats [15];
		currentTime = loadedStats [16];

		//ammo

		HGAmmo = (int)loadedStats [17];
		SGAmmo = (int)loadedStats [18];
		MGAmmo = (int)loadedStats [19];
		rifleAmmo = (int)loadedStats [20];
		magnumAmmo = (int)loadedStats [21];
		//loading weapon damage
		weaponDamage[0] = (int)loadedStats [22];
		weaponDamage[1] = (int)loadedStats [23];
		weaponDamage[2] = (int)loadedStats [24];
		weaponDamage[3] = (int)loadedStats [25];
		weaponDamage[4] = (int)loadedStats [26];
		weaponDamage[5] = (int)loadedStats [27];
		weaponDamage[6] = (int)loadedStats [28];
		weaponDamage[7] = (int)loadedStats [29];
		weaponDamage[8] = (int)loadedStats [30];
		weaponDamage[9] = (int)loadedStats [31];
		weaponDamage[10] = (int)loadedStats [32];
		weaponDamage[11] = (int)loadedStats [33];
		weaponDamage[12] = (int)loadedStats [34];
		weaponDamage[13] = (int)loadedStats [35];
		weaponDamage[14] = (int)loadedStats [36];
		weaponDamage[15] = (int)loadedStats [37];
		weaponDamage[16] = (int)loadedStats [38];

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

	}

	//public int id;

	public int money;
	public int currentLevel;
	public int XP = 0;
	public int maxXP = 50;
	public int totalXP = 0;
	public int health = 10;
	public int maxHealth = 10;
	public int HGAmmo = 24;
	public int rifleAmmo = 0;
	public int SGAmmo = 0;
	public int MGAmmo = 0;
	public int magnumAmmo = 0;
	public int curWeapon = 0;
	public float shopKeepTimer = 0;

	//weapons
	[Space(10)]
	public string[] weaponName = new string[] {"FBI Custom", "9mm Oppressor", "The Blacklist", "Trident",   "Silencer", "Seeker", "Hunter Killer", "Crow's Nest",  "12 Gauge", "Orthrus",  "Cerberus",  "Devestator", "Savage One", "Diminisher", "Revolver", "Scylla",     "Doomsday"};
	[Space(10)]
	public string[] weaponType = new string[] {"Handgun",        "Handgun",       "Handgun",   "Handgun",     "Rifle",   "Rifle",    "Rifle",         "Rifle",      "Shotgun",   "Shotgun",   "Shotgun",    "Shotgun",    "Shotgun",   "Automatic",  "Magnum",  "Magnum",    "Magnum"};

	[Header ("Upgrades")]
	[Space(20)]
	public int[] weaponDamage =                         {8,               8,                8,            8,          12,        12,          12,              12,           15,         15,         15,           15,           15,           4,            20,        30,           100};
	[Space(10)]
	public int[] capacity =                             {12,              12,               12,           12,         4,         4,           4,                4,           12,         8,          6,            8,            10,           30,           8,         6,             1};
	[Space(10)]
	public float[] reload =                             {12,              12,               12,           12,         4,         4,           4,                4,           12,         8,          6,            8,            10,           30,           8,         6,             1};
	[Space(10)]
	public float[] fireRate =                           {.8f,            .8f,              .8f,          .8f,        .8f,       .8f,         .8f,             .8f,         .8f,         .8f,         .8f,         .8f,          .8f,          .8f,          .8f,       .8f,           .8f}; 
	[Space(10)]
	public float[] accuracy =                          {.3f,            .3f,              .3f,          .3f,        .3f,        3f,         .3f,              .3f,         .3f,        .3f,        .3f,          .3f,          .3f,          .3f,          .3f,        .3f,           .3f}; //the maximum size of the slider
	[Space(10)]
	public float[] range =                              {1f,              1f,               1f,           1f,         1f,       1f,           1f,              1f,           1f,        1f,         1f,           1f,            1f,          1f,           1f,         1f,             1f}; //the maximum size of the target bar
	[Space(10)]
	public float[] criticalChance =                    {.06f,           .06f,             .06f,          .06f,      .06f,     .06f,         .06f,            .06f,           0,         0,          0,            0,            0,           .03f,        .12f,        .12f,           .12f};
	[Space(10)]
	[Header ("Selling price of each weapon")]
	public int[] sellValue =                            {50,              75,               100,          100,         60,      80,           200,             150,           80,        120,        150,          180,          200,          75,         100,         200,            300};
	//weapons obtained
	//this is how we keep track of what weapons the player has in his or her inventory.
	//0 means there is no weapon of this type and 1 means the player has this weapon
	[Space(10)]
	[Header("Weapons that have been obtained: 0 means not obtained and 1 means obtained")]
	public int[] obtainedWeapons=                 {1,               0,                0,            0,          0,        0,          0,                0,           0,           0,           0,             0,           0,             0,           0,        0,             0};
	//this is how we keep track of what weapon is currently equipped.
	//the value of this variable determines what index the above variables are on

	[SerializeField]
	string elements = "These represent the upgrade elements";
	[SerializeField]

	

	//STATS
	[HideInInspector]
	public int totalMoneyEarned= 0;
	[HideInInspector]
	public int moneySpent = 0;
	[HideInInspector]
	public int shotsFired = 0;
	[HideInInspector]
	public int targetsHit = 0;
	[HideInInspector]
	public float hitRatio = 0.000f; //targets hit divided by shots fired + %
	[HideInInspector]
	public int enemiesKilled = 0;
	[HideInInspector]
	public int itemsUsed = 0;
	[HideInInspector]
	public int damageTaken = 0;
	[HideInInspector]
	public int damageDealt = 0;
	[HideInInspector]
	public int numberOfSaves = 0;
	[HideInInspector]
	public float currentTime = 0f; //Time.time

	[Header("The cost of upgrades for each weapon")]
	//reload cost
	public int[] reloadCost=                    {25,               25,                15,            30,          10,        20,          15,              20,           15,         20,         35,           20,           30,           10,           30,        40,           50};
	//damage cost
	[Space(10)]
	public int[] damageCost=                    {25,               25,                15,            30,          10,        20,          15,              20,           15,         20,         35,           20,           30,           10,           30,        40,           50};
	//capacity cost 
	[Space(10)]
	public int[] capacityCost=                  {25,               25,                15,            30,          10,        20,          15,              20,           15,         20,         35,           20,           30,           10,           30,        40,           50};
	//accuracy cost
	[Space(10)]
	public int[] acCost=                 		{25,               25,                15,            30,          10,        20,          15,              20,           15,         20,         35,           20,           30,           10,           30,        40,           50};
	//fire rate cost
	[Space(10)]
	public int[] frCost=                   	    {25,               25,                15,            30,          10,        20,          15,              20,           15,         20,         35,           20,           30,           10,           30,        40,           50};
	//range cost
	[Space(10)]
	public int[] rangeCost=                     {25,               25,                15,            30,          10,        20,          15,              20,           15,         20,         35,           20,           30,           10,           30,        40,           50};
	//critical chance cost
	[Space(10)]
	public int[] CCCost=                        {25,               25,                15,            30,          10,        20,          15,              20,           15,         20,         35,           20,           30,           10,           30,        40,           50};

	[HideInInspector]
	public int shopHandgunAmmo;
	[HideInInspector]
	public int shopShotgunAmmo;
	[HideInInspector]
	public int shopRifleAmmo;
	[HideInInspector]
	public int shopMachinegunAmmo;
	[HideInInspector]
	public int shopMagnumAmmo;
	[HideInInspector]
	public int shopSmallAid;
	[HideInInspector]
	public int shopMedAid;
	[HideInInspector]
	public int shopLargeAid;
	[HideInInspector]
	public int shopHolyWater;

	//player's items
	[HideInInspector]
	public int itemSmallAid;
	[HideInInspector]
	public int itemMedAid;
	[HideInInspector]
	public int itemLargeAid;
	[HideInInspector]
	public int itemHolyWater;
	[HideInInspector]
	public int itemSmallKey;


	//current level of upgrades
	[HideInInspector]
	public int[] curDamage =                         {0,               0,                0,            0,          0,        0,          0,              0,           0,         0,         0,           0,           0,           0,            0,        0,           0};
	[HideInInspector]
	public int[] curReload =                         {0,               0,                0,            0,          0,        0,          0,              0,           0,         0,         0,           0,           0,           0,            0,        0,           0};
	[HideInInspector]
	public int[] curFireRate =                         {0,               0,                0,            0,          0,        0,          0,              0,           0,         0,         0,           0,           0,           0,            0,        0,           0};
	[HideInInspector]
	public int[] curCapacity =                         {0,               0,                0,            0,          0,        0,          0,              0,           0,         0,         0,           0,           0,           0,            0,        0,           0};
	[HideInInspector]
	public int[] curCrit =                         		{0,               0,                0,            0,          0,        0,          0,              0,           0,         0,         0,           0,           0,           0,            0,        0,           0};
	[HideInInspector]
	public int[] curRange =                        		 {0,               0,                0,            0,          0,        0,          0,              0,           0,         0,         0,           0,           0,           0,            0,        0,           0};
	[HideInInspector]
	public int[] curAccuracy =                         {0,               0,                0,            0,          0,        0,          0,              0,           0,         0,         0,           0,           0,           0,            0,        0,           0};

}