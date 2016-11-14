using UnityEngine;
using System.Collections;
using System;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;

public static class SaveLoadManager{

	//public DataStorage player = new DataStorage(0,);
	public static void SavePlayer(DataStorage player)
	{
		BinaryFormatter bf = new BinaryFormatter ();
		FileStream stream = new FileStream (Application.persistentDataPath + "/DataStorage.sav", FileMode.Create);
	
		PlayerData data = new PlayerData (player);
	
		bf.Serialize(stream, data);
		stream.Close();
	}

	public static float[] LoadGame()
	{
		if (File.Exists (Application.persistentDataPath + "/DataStorage.sav")) {
			BinaryFormatter bf = new BinaryFormatter ();
			FileStream stream = new FileStream (Application.persistentDataPath + "/DataStorage.sav", FileMode.Open);
			PlayerData data = bf.Deserialize (stream) as PlayerData;
			stream.Close ();
			return data.stats;
		} 
		else 
		{
			Debug.LogError("No saved data found");
			return new float[346];
		}
	}
}


[Serializable]
public class PlayerData{

	public float[] stats;
	

	public PlayerData(DataStorage player)
	{
		stats = new float[346];
			stats [0] = DataStorage.money;
			stats [1] = DataStorage.currentLevel;
			stats [2] = DataStorage.XP;
			stats [3] = DataStorage.maxXP;
			stats[4] = DataStorage.health;
			stats [5] = DataStorage.maxHealth;
			stats [6] = DataStorage.totalMoneyEarned;
			stats [7] = DataStorage.moneySpent;
			stats [8] = DataStorage.shotsFired;
			stats [9] = DataStorage.targetsHit;
			stats [10] = DataStorage.hitRatio;
			stats [11] = DataStorage.enemiesKilled;
			stats [12] = DataStorage.itemsUsed;
			stats [13] = DataStorage.damageTaken;
			stats [14] = DataStorage.damageDealt;
			stats [15] = DataStorage.numberOfSaves;
			stats [16] = DataStorage.currentTime;
			stats [17] = DataStorage.HGAmmo;
			stats [18] = DataStorage.SGAmmo;
			stats [19] = DataStorage.MGAmmo;
			stats [20] = DataStorage.rifleAmmo;
			stats [21] = DataStorage.magnumAmmo;

		//saving weapon damage
		stats [22] = DataStorage.weaponDamage[0];
		stats [23] = DataStorage.weaponDamage[1];
		stats [24] = DataStorage.weaponDamage[2];
		stats [25] = DataStorage.weaponDamage[3];
		stats [26] = DataStorage.weaponDamage[4];
		stats [27] = DataStorage.weaponDamage[5];
		stats [28] = DataStorage.weaponDamage[6];
		stats [29] = DataStorage.weaponDamage[7];
		stats [30] = DataStorage.weaponDamage[8];
		stats [31] = DataStorage.weaponDamage[9];
		stats [32] = DataStorage.weaponDamage[10];
		stats [33] = DataStorage.weaponDamage[11];
		stats [34] = DataStorage.weaponDamage[12];
		stats [35] = DataStorage.weaponDamage[13];
		stats [36] = DataStorage.weaponDamage[14];
		stats [37] = DataStorage.weaponDamage[15];
		stats [38] = DataStorage.weaponDamage[16];

		//capacity
		stats [39] = DataStorage.capacity[0];
		stats [40] = DataStorage.capacity[1];
		stats [41] = DataStorage.capacity[2];
		stats [42] = DataStorage.capacity[3];
		stats [43] = DataStorage.capacity[4];
		stats [44] = DataStorage.capacity[5];
		stats [45] = DataStorage.capacity[6];
		stats [46] = DataStorage.capacity[7];
		stats [47] = DataStorage.capacity[8];
		stats [48] = DataStorage.capacity[9];
		stats [49] = DataStorage.capacity[10];
		stats [50] = DataStorage.capacity[11];
		stats [51] = DataStorage.capacity[12];
		stats [52] = DataStorage.capacity[13];
		stats [53] = DataStorage.capacity[14];
		stats [54] = DataStorage.capacity[15];
		stats [55] = DataStorage.capacity[16];

		//reload

		stats [56] = DataStorage.reload[0];
		stats [57] = DataStorage.reload[1];
		stats [58] = DataStorage.reload[2];
		stats [59] = DataStorage.reload[3];
		stats [60] = DataStorage.reload[4];
		stats [61] = DataStorage.reload[5];
		stats [62] = DataStorage.reload[6];
		stats [63] = DataStorage.reload[7];
		stats [64] = DataStorage.reload[8];
		stats [65] = DataStorage.reload[9];
		stats [66] = DataStorage.reload[10];
		stats [67] = DataStorage.reload[11];
		stats [68] = DataStorage.reload[12];
		stats [69] = DataStorage.reload[13];
		stats [70] = DataStorage.reload[14];
		stats [71] = DataStorage.reload[15];
		stats [72] = DataStorage.reload[16];

		//accuracy

		stats [73] = DataStorage.accuracy[0];
		stats [74] = DataStorage.accuracy[1];
		stats [75] = DataStorage.accuracy[2];
		stats [76] = DataStorage.accuracy[3];
		stats [77] = DataStorage.accuracy[4];
		stats [78] = DataStorage.accuracy[5];
		stats [79] = DataStorage.accuracy[6];
		stats [80] = DataStorage.accuracy[7];
		stats [81] = DataStorage.accuracy[8];
		stats [82] = DataStorage.accuracy[9];
		stats [83] = DataStorage.accuracy[10];
		stats [84] = DataStorage.accuracy[11];
		stats [85] = DataStorage.accuracy[12];
		stats [86] = DataStorage.accuracy[13];
		stats [87] = DataStorage.accuracy[14];
		stats [88] = DataStorage.accuracy[15];
		stats [89] = DataStorage.accuracy[16];

		//range
		stats [90] = DataStorage.range[0];
		stats [91] = DataStorage.range[1];
		stats [92] = DataStorage.range[2];
		stats [93] = DataStorage.range[3];
		stats [94] = DataStorage.range[4];
		stats [95] = DataStorage.range[5];
		stats [96] = DataStorage.range[6];
		stats [97] = DataStorage.range[7];
		stats [98] = DataStorage.range[8];
		stats [99] = DataStorage.range[9];
		stats [100] = DataStorage.range[10];
		stats [101] = DataStorage.range[11];
		stats [102] = DataStorage.range[12];
		stats [103] = DataStorage.range[13];
		stats [104] = DataStorage.range[14];
		stats [105] = DataStorage.range[15];
		stats [106] = DataStorage.range[16];

		//critial

		stats [107] = DataStorage.criticalChance[0];
		stats [108] = DataStorage.criticalChance[1];
		stats [109] = DataStorage.criticalChance[2];
		stats [110] = DataStorage.criticalChance[3];
		stats [111] = DataStorage.criticalChance[4];
		stats [112] = DataStorage.criticalChance[5];
		stats [113] = DataStorage.criticalChance[6];
		stats [114] = DataStorage.criticalChance[7];
		stats [115] = DataStorage.criticalChance[8];
		stats [116] = DataStorage.criticalChance[9];
		stats [117] = DataStorage.criticalChance[10];
		stats [118] = DataStorage.criticalChance[11];
		stats [119] = DataStorage.criticalChance[12];
		stats [120] = DataStorage.criticalChance[13];
		stats [121] = DataStorage.criticalChance[14];
		stats [122] = DataStorage.criticalChance[15];
		stats [123] = DataStorage.criticalChance[16];

		//sell value
		stats [124] = DataStorage.sellValue[0];
		stats [125] = DataStorage.sellValue[1];
		stats [126] = DataStorage.sellValue[2];
		stats [127] = DataStorage.sellValue[3];
		stats [128] = DataStorage.sellValue[4];
		stats [129] = DataStorage.sellValue[5];
		stats [130] = DataStorage.sellValue[6];
		stats [131] = DataStorage.sellValue[7];
		stats [132] = DataStorage.sellValue[8];
		stats [133] = DataStorage.sellValue[9];
		stats [134] = DataStorage.sellValue[10];
		stats [135] = DataStorage.sellValue[11];
		stats [136] = DataStorage.sellValue[12];
		stats [137] = DataStorage.sellValue[13];
		stats [138] = DataStorage.sellValue[14];
		stats [139] = DataStorage.sellValue[15];
		stats [140] = DataStorage.sellValue[16];

		//fire rate
		stats [141] = DataStorage.fireRate[0];
		stats [142] = DataStorage.fireRate[1];
		stats [143] = DataStorage.fireRate[2];
		stats [144] = DataStorage.fireRate[3];
		stats [145] = DataStorage.fireRate[4];
		stats [146] = DataStorage.fireRate[5];
		stats [147] = DataStorage.fireRate[6];
		stats [148] = DataStorage.fireRate[7];
		stats [149] = DataStorage.fireRate[8];
		stats [150] = DataStorage.fireRate[9];
		stats [151] = DataStorage.fireRate[10];
		stats [152] = DataStorage.fireRate[11];
		stats [153] = DataStorage.fireRate[12];
		stats [154] = DataStorage.fireRate[13];
		stats [155] = DataStorage.fireRate[14];
		stats [156] = DataStorage.fireRate[15];
		stats [157] = DataStorage.fireRate[16];

        stats[158] = DataStorage.curWeapon;

        //weapons obtained
        stats[159] = DataStorage.obtainedWeapons[0];
        stats[160] = DataStorage.obtainedWeapons[1];
        stats[161] = DataStorage.obtainedWeapons[2];
        stats[162] = DataStorage.obtainedWeapons[3];
        stats[163] = DataStorage.obtainedWeapons[4];
        stats[164] = DataStorage.obtainedWeapons[5];
        stats[165] = DataStorage.obtainedWeapons[6];
        stats[166] = DataStorage.obtainedWeapons[7];
        stats[167] = DataStorage.obtainedWeapons[8];
        stats[168] = DataStorage.obtainedWeapons[9];
        stats[169] = DataStorage.obtainedWeapons[10];
        stats[170] = DataStorage.obtainedWeapons[11];
        stats[171] = DataStorage.obtainedWeapons[12];
        stats[172] = DataStorage.obtainedWeapons[13];
        stats[173] = DataStorage.obtainedWeapons[14];
        stats[174] = DataStorage.obtainedWeapons[15];
        stats[175] = DataStorage.obtainedWeapons[16];

		//the shop
		stats[176] = DataStorage.shopHandgunAmmo;
		stats[177] = DataStorage.shopShotgunAmmo;
		stats[178] = DataStorage.shopMachinegunAmmo;
		stats[179] = DataStorage.shopRifleAmmo;
		stats[180] = DataStorage.shopMagnumAmmo;
		stats[181] = DataStorage.shopSmallAid;
		stats[182] = DataStorage.shopMedAid;
		stats[183] = DataStorage.shopLargeAid;
		stats[184] = DataStorage.shopHolyWater;

		//items inventory
		stats[185] = DataStorage.itemHolyWater;
		stats[186] = DataStorage.itemLargeAid;
		stats[187] = DataStorage.itemMedAid;
		stats[188] = DataStorage.itemSmallAid;
		stats [189] = DataStorage.itemSmallKey;

		//current level of upgrades
		//damage
		stats [190] = DataStorage.curDamage[0];
		stats [191] = DataStorage.curDamage[1];
		stats [192] = DataStorage.curDamage[2];
		stats [193] = DataStorage.curDamage[3];
		stats [194] = DataStorage.curDamage[4];
		stats [195] = DataStorage.curDamage[5];
		stats [196] = DataStorage.curDamage[6];
		stats [197] = DataStorage.curDamage[7];
		stats [198] = DataStorage.curDamage[8];
		stats [199] = DataStorage.curDamage[9];
		stats [200] = DataStorage.curDamage[10];
		stats [201] = DataStorage.curDamage[11];
		stats [202] = DataStorage.curDamage[12];
		stats [203] = DataStorage.curDamage[13];
		stats [204] = DataStorage.curDamage[14];
		stats [205] = DataStorage.curDamage[15];
		stats [206] = DataStorage.curDamage[16];

		//reload
		stats [207] = DataStorage.curReload[0];
		stats [208] = DataStorage.curReload[1];
		stats [209] = DataStorage.curReload[2];
		stats [210] = DataStorage.curReload[3];
		stats [211] = DataStorage.curReload[4];
		stats [212] = DataStorage.curReload[5];
		stats [213] = DataStorage.curReload[6];
		stats [214] = DataStorage.curReload[7];
		stats [215] = DataStorage.curReload[8];
		stats [216] = DataStorage.curReload[9];
		stats [217] = DataStorage.curReload[10];
		stats [218] = DataStorage.curReload[11];
		stats [219] = DataStorage.curReload[12];
		stats [220] = DataStorage.curReload[13];
		stats [221] = DataStorage.curReload[14];
		stats [222] = DataStorage.curReload[15];
		stats [223] = DataStorage.curReload[16];
	

		//firerate
		stats [224] = DataStorage.curFireRate[0];
		stats [225] = DataStorage.curFireRate[1];
		stats [226] = DataStorage.curFireRate[2];
		stats [227] = DataStorage.curFireRate[3];
		stats [228] = DataStorage.curFireRate[4];
		stats [229] = DataStorage.curFireRate[5];
		stats [230] = DataStorage.curFireRate[6];
		stats [231] = DataStorage.curFireRate[7];
		stats [232] = DataStorage.curFireRate[8];
		stats [233] = DataStorage.curFireRate[9];
		stats [234] = DataStorage.curFireRate[10];
		stats [235] = DataStorage.curFireRate[11];
		stats [236] = DataStorage.curFireRate[12];
		stats [237] = DataStorage.curFireRate[13];
		stats [238] = DataStorage.curFireRate[14];
		stats [239] = DataStorage.curFireRate[15];
		stats [240] = DataStorage.curFireRate[16];

		//capacity
		stats [241] = DataStorage.curCapacity[0];
		stats [242] = DataStorage.curCapacity[1];
		stats [243] = DataStorage.curCapacity[2];
		stats [244] = DataStorage.curCapacity[3];
		stats [245] = DataStorage.curCapacity[4];
		stats [246] = DataStorage.curCapacity[5];
		stats [247] = DataStorage.curCapacity[6];
		stats [248] = DataStorage.curCapacity[7];
		stats [249] = DataStorage.curCapacity[8];
		stats [250] = DataStorage.curCapacity[9];
		stats [251] = DataStorage.curCapacity[10];
		stats [252] = DataStorage.curCapacity[11];
		stats [253] = DataStorage.curCapacity[12];
		stats [254] = DataStorage.curCapacity[13];
		stats [255] = DataStorage.curCapacity[14];
		stats [256] = DataStorage.curCapacity[15];
		stats [257] = DataStorage.curCapacity[16];


		//crit
		stats [258] = DataStorage.curCrit[0];
		stats [259] = DataStorage.curCrit[1];
		stats [260] = DataStorage.curCrit[2];
		stats [261] = DataStorage.curCrit[3];
		stats [262] = DataStorage.curCrit[4];
		stats [263] = DataStorage.curCrit[5];
		stats [264] = DataStorage.curCrit[6];
		stats [265] = DataStorage.curCrit[7];
		stats [266] = DataStorage.curCrit[8];
		stats [267] = DataStorage.curCrit[9];
		stats [268] = DataStorage.curCrit[10];
		stats [269] = DataStorage.curCrit[11];
		stats [270] = DataStorage.curCrit[12];
		stats [271] = DataStorage.curCrit[13];
		stats [272] = DataStorage.curCrit[14];
		stats [273] = DataStorage.curCrit[15];
		stats [274] = DataStorage.curCrit[16];

		//range
		stats [275] = DataStorage.curRange[0];
		stats [276] = DataStorage.curRange[1];
		stats [277] = DataStorage.curRange[2];
		stats [278] = DataStorage.curRange[3];
		stats [279] = DataStorage.curRange[4];
		stats [280] = DataStorage.curRange[5];
		stats [281] = DataStorage.curRange[6];
		stats [282] = DataStorage.curRange[7];
		stats [283] = DataStorage.curRange[8];
		stats [284] = DataStorage.curRange[9];
		stats [285] = DataStorage.curRange[10];
		stats [286] = DataStorage.curRange[11];
		stats [287] = DataStorage.curRange[12];
		stats [288] = DataStorage.curRange[13];
		stats [289] = DataStorage.curRange[14];
		stats [290] = DataStorage.curRange[15];
		stats [291] = DataStorage.curRange[16];


		//accuracy
		stats [292] = DataStorage.curAccuracy[0];
		stats [293] = DataStorage.curAccuracy[1];
		stats [294] = DataStorage.curAccuracy[2];
		stats [295] = DataStorage.curAccuracy[3];
		stats [296] = DataStorage.curAccuracy[4];
		stats [297] = DataStorage.curAccuracy[5];
		stats [298] = DataStorage.curAccuracy[6];
		stats [299] = DataStorage.curAccuracy[7];
		stats [300] = DataStorage.curAccuracy[8];
		stats [301] = DataStorage.curAccuracy[9];
		stats [302] = DataStorage.curAccuracy[10];
		stats [303] = DataStorage.curAccuracy[11];
		stats [304] = DataStorage.curAccuracy[12];
		stats [305] = DataStorage.curAccuracy[13];
		stats [306] = DataStorage.curAccuracy[14];
		stats [307] = DataStorage.curAccuracy[15];
		stats [308] = DataStorage.curAccuracy[16];

		stats [309] = DataStorage.totalXP;
		stats [310] = DataStorage.shopKeepTimer;
        stats[311] = DataStorage.maxWeight;
        stats[312] = DataStorage.curItems;
		stats[313] = DataStorage.curWeight;

		//curent holsters
		stats[314] = DataStorage.holster[0];
		stats[315] = DataStorage.holster[1];
		stats[316] = DataStorage.holster[2];
		stats[317] = DataStorage.holster[3];
		stats[318] = DataStorage.holster[4];
		stats[319] = DataStorage.holster[5];
		stats[320] = DataStorage.holster[6];
		stats[321] = DataStorage.holster[7];
		stats[322] = DataStorage.holster[8];
		stats[323] = DataStorage.holster[9];
		stats[324] = DataStorage.holster[10];
		stats[325] = DataStorage.holster[11];
		stats[326] = DataStorage.holster[12];
		stats[327] = DataStorage.holster[13];
		stats[328] = DataStorage.holster[14];
		stats[329] = DataStorage.holster[15];
		stats[330] = DataStorage.holster[16];
		stats[331] = DataStorage.holster[17];
		stats[332] = DataStorage.holster[18];
		stats[333] = DataStorage.holster[19];
		stats[334] = DataStorage.holster[20];
		stats[335] = DataStorage.holster[21];

		//player stats (attributes)

		stats[336] = DataStorage.strength;
		stats[337] = DataStorage.constitution;
		stats[338] = DataStorage.fortitude;
		stats[339] = DataStorage.dexterity;
		stats[340] = DataStorage.luck;
		stats[341] = DataStorage.perception;
		stats[342] = DataStorage.agility;
		stats[343] = DataStorage.intelligence;
		stats[344] = DataStorage.charisma;
		stats[345] = DataStorage.playerStats;


		//need to add save/load upgrades for the new weapons

        
			//PlayerDisplay.UpdateDisplay();

	}
}

