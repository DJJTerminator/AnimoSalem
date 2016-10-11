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
		FileStream stream = new FileStream (Application.persistentDataPath + "/player.sav", FileMode.Create);
	
		PlayerData data = new PlayerData (player);
	
		bf.Serialize(stream, data);
		stream.Close();
	}

	public static float[] LoadGame()
	{
		if (File.Exists (Application.persistentDataPath + "/player.sav")) {
			BinaryFormatter bf = new BinaryFormatter ();
			FileStream stream = new FileStream (Application.persistentDataPath + "/player.sav", FileMode.Open);
			PlayerData data = bf.Deserialize (stream) as PlayerData;
			stream.Close ();
			return data.stats;
		} 
		else 
		{
			Debug.LogError("No saved data found");
			return new float[131];
		}
	}
}


[Serializable]
public class PlayerData{

	public float[] stats;
	

	public PlayerData(DataStorage player)
	{
		stats = new float[131];
			stats [0] = player.money;
			stats [1] = player.currentLevel;
			stats [2] = player.XP;
			stats [3] = player.maxXP;
			stats[4] = player.health;
			stats [5] = player.maxHealth;
			stats [6] = player.totalMoneyEarned;
			stats [7] = player.moneySpent;
			stats [8] = player.shotsFired;
			stats [9] = player.targetsHit;
			stats [10] = player.hitRatio;
			stats [11] = player.enemiesKilled;
			stats [12] = player.itemsUsed;
			stats [13] = player.damageTaken;
			stats [14] = player.damageDealt;
			stats [15] = player.numberOfSaves;
			stats [16] = player.currentTime;
			stats [17] = player.HGAmmo;
			stats [18] = player.SGAmmo;
			stats [19] = player.MGAmmo;
			stats [20] = player.rifleAmmo;
			stats [21] = player.magnumAmmo;

		//saving weapon damage
		stats [22] = player.weaponDamage[0];
		stats [23] = player.weaponDamage[1];
		stats [24] = player.weaponDamage[2];
		stats [25] = player.weaponDamage[3];
		stats [26] = player.weaponDamage[4];
		stats [27] = player.weaponDamage[5];
		stats [28] = player.weaponDamage[6];
		stats [29] = player.weaponDamage[7];
		stats [30] = player.weaponDamage[8];
		stats [31] = player.weaponDamage[9];
		stats [32] = player.weaponDamage[10];
		stats [33] = player.weaponDamage[11];
		stats [34] = player.weaponDamage[12];
		stats [35] = player.weaponDamage[13];
		stats [36] = player.weaponDamage[14];
		stats [37] = player.weaponDamage[15];
		stats [38] = player.weaponDamage[16];

		//capacity
		stats [39] = player.capacity[0];
		stats [40] = player.capacity[1];
		stats [41] = player.capacity[2];
		stats [42] = player.capacity[3];
		stats [43] = player.capacity[4];
		stats [44] = player.capacity[5];
		stats [45] = player.capacity[6];
		stats [46] = player.capacity[7];
		stats [47] = player.capacity[8];
		stats [48] = player.capacity[9];
		stats [49] = player.capacity[10];
		stats [50] = player.capacity[11];
		stats [51] = player.capacity[12];
		stats [52] = player.capacity[13];
		stats [53] = player.capacity[14];
		stats [54] = player.capacity[15];
		stats [55] = player.capacity[16];

		//reload

		stats [56] = player.reload[0];
		stats [57] = player.reload[1];
		stats [58] = player.reload[2];
		stats [59] = player.reload[3];
		stats [60] = player.reload[4];
		stats [61] = player.reload[5];
		stats [62] = player.reload[6];
		stats [63] = player.reload[7];
		stats [64] = player.reload[8];
		stats [65] = player.reload[9];
		stats [66] = player.reload[10];
		stats [67] = player.reload[11];
		stats [68] = player.reload[12];
		stats [69] = player.reload[13];
		stats [70] = player.reload[14];
		stats [71] = player.reload[15];
		stats [72] = player.reload[16];

		//accuracy

		stats [73] = player.accuracy[0];
		stats [74] = player.accuracy[1];
		stats [75] = player.accuracy[2];
		stats [76] = player.accuracy[3];
		stats [77] = player.accuracy[4];
		stats [78] = player.accuracy[5];
		stats [79] = player.accuracy[6];
		stats [80] = player.accuracy[7];
		stats [81] = player.accuracy[8];
		stats [82] = player.accuracy[9];
		stats [83] = player.accuracy[10];
		stats [84] = player.accuracy[11];
		stats [85] = player.accuracy[12];
		stats [86] = player.accuracy[13];
		stats [87] = player.accuracy[14];
		stats [88] = player.accuracy[15];
		stats [89] = player.accuracy[16];

		//range
		stats [90] = player.range[0];
		stats [91] = player.range[1];
		stats [92] = player.range[2];
		stats [93] = player.range[3];
		stats [94] = player.range[4];
		stats [95] = player.range[5];
		stats [96] = player.range[6];
		stats [97] = player.range[7];
		stats [98] = player.range[8];
		stats [99] = player.range[9];
		stats [100] = player.range[10];
		stats [101] = player.range[11];
		stats [102] = player.range[12];
		stats [103] = player.range[13];
		stats [104] = player.range[14];
		stats [105] = player.range[15];
		stats [106] = player.range[16];

		//critial

		stats [107] = player.criticalChance[0];
		stats [108] = player.criticalChance[1];
		stats [109] = player.criticalChance[2];
		stats [110] = player.criticalChance[3];
		stats [111] = player.criticalChance[4];
		stats [112] = player.criticalChance[5];
		stats [113] = player.criticalChance[6];
		stats [114] = player.criticalChance[7];
		stats [115] = player.criticalChance[8];
		stats [116] = player.criticalChance[9];
		stats [117] = player.criticalChance[10];
		stats [118] = player.criticalChance[11];
		stats [119] = player.criticalChance[12];
		stats [120] = player.criticalChance[13];
		stats [121] = player.criticalChance[14];
		stats [122] = player.criticalChance[15];
		stats [123] = player.criticalChance[16];

		//sell value
		stats [124] = player.sellValue[0];
		stats [125] = player.sellValue[1];
		stats [126] = player.sellValue[2];
		stats [127] = player.sellValue[3];
		stats [128] = player.sellValue[4];
		stats [129] = player.sellValue[5];
		stats [130] = player.sellValue[6];
		stats [131] = player.sellValue[7];
		stats [132] = player.sellValue[8];
		stats [133] = player.sellValue[9];
		stats [134] = player.sellValue[10];
		stats [135] = player.sellValue[11];
		stats [136] = player.sellValue[12];
		stats [137] = player.sellValue[13];
		stats [138] = player.sellValue[14];
		stats [139] = player.sellValue[15];
		stats [140] = player.sellValue[16];

		//fire rate
		stats [141] = player.fireRate[0];
		stats [142] = player.fireRate[1];
		stats [143] = player.fireRate[2];
		stats [144] = player.fireRate[3];
		stats [145] = player.fireRate[4];
		stats [146] = player.fireRate[5];
		stats [147] = player.fireRate[6];
		stats [148] = player.fireRate[7];
		stats [149] = player.fireRate[8];
		stats [150] = player.fireRate[9];
		stats [151] = player.fireRate[10];
		stats [152] = player.fireRate[11];
		stats [153] = player.fireRate[12];
		stats [154] = player.fireRate[13];
		stats [155] = player.fireRate[14];
		stats [156] = player.fireRate[15];
		stats [157] = player.fireRate[16];

        stats[158] = player.curWeapon;

        //weapons obtained
        stats[159] = player.obtainedWeapons[0];
        stats[160] = player.obtainedWeapons[1];
        stats[161] = player.obtainedWeapons[2];
        stats[162] = player.obtainedWeapons[3];
        stats[163] = player.obtainedWeapons[4];
        stats[164] = player.obtainedWeapons[5];
        stats[165] = player.obtainedWeapons[6];
        stats[166] = player.obtainedWeapons[7];
        stats[167] = player.obtainedWeapons[8];
        stats[168] = player.obtainedWeapons[9];
        stats[169] = player.obtainedWeapons[10];
        stats[170] = player.obtainedWeapons[11];
        stats[171] = player.obtainedWeapons[12];
        stats[172] = player.obtainedWeapons[13];
        stats[173] = player.obtainedWeapons[14];
        stats[174] = player.obtainedWeapons[15];
        stats[175] = player.obtainedWeapons[16];

		//the shop
		stats[176] = player.shopHandgunAmmo;
		stats[177] = player.shopShotgunAmmo;
		stats[178] = player.shopMachinegunAmmo;
		stats[179] = player.shopRifleAmmo;
		stats[180] = player.shopMagnumAmmo;
		stats[181] = player.shopSmallAid;
		stats[182] = player.shopMedAid;
		stats[183] = player.shopLargeAid;
		stats[184] = player.shopHolyWater;

		//items inventory
		stats[185] = player.itemHolyWater;
		stats[186] = player.itemLargeAid;
		stats[187] = player.itemMedAid;
		stats[188] = player.itemSmallAid;
		stats [189] = player.itemSmallKey;

		//current level of upgrades
		//damage
		stats [190] = player.curDamage[0];
		stats [191] = player.curDamage[1];
		stats [192] = player.curDamage[2];
		stats [193] = player.curDamage[3];
		stats [194] = player.curDamage[4];
		stats [195] = player.curDamage[5];
		stats [196] = player.curDamage[6];
		stats [197] = player.curDamage[7];
		stats [198] = player.curDamage[8];
		stats [199] = player.curDamage[9];
		stats [200] = player.curDamage[10];
		stats [201] = player.curDamage[11];
		stats [202] = player.curDamage[12];
		stats [203] = player.curDamage[13];
		stats [204] = player.curDamage[14];
		stats [205] = player.curDamage[15];
		stats [206] = player.curDamage[16];

		//reload
		stats [207] = player.curReload[0];
		stats [208] = player.curReload[1];
		stats [209] = player.curReload[2];
		stats [210] = player.curReload[3];
		stats [211] = player.curReload[4];
		stats [212] = player.curReload[5];
		stats [213] = player.curReload[6];
		stats [214] = player.curReload[7];
		stats [215] = player.curReload[8];
		stats [216] = player.curReload[9];
		stats [217] = player.curReload[10];
		stats [218] = player.curReload[11];
		stats [219] = player.curReload[12];
		stats [220] = player.curReload[13];
		stats [221] = player.curReload[14];
		stats [222] = player.curReload[15];
		stats [223] = player.curReload[16];
	

		//firerate
		stats [224] = player.curFireRate[0];
		stats [225] = player.curFireRate[1];
		stats [226] = player.curFireRate[2];
		stats [227] = player.curFireRate[3];
		stats [228] = player.curFireRate[4];
		stats [229] = player.curFireRate[5];
		stats [230] = player.curFireRate[6];
		stats [231] = player.curFireRate[7];
		stats [232] = player.curFireRate[8];
		stats [233] = player.curFireRate[9];
		stats [234] = player.curFireRate[10];
		stats [235] = player.curFireRate[11];
		stats [236] = player.curFireRate[12];
		stats [237] = player.curFireRate[13];
		stats [238] = player.curFireRate[14];
		stats [239] = player.curFireRate[15];
		stats [240] = player.curFireRate[16];

		//capacity
		stats [241] = player.curCapacity[0];
		stats [242] = player.curCapacity[1];
		stats [243] = player.curCapacity[2];
		stats [244] = player.curCapacity[3];
		stats [245] = player.curCapacity[4];
		stats [246] = player.curCapacity[5];
		stats [247] = player.curCapacity[6];
		stats [248] = player.curCapacity[7];
		stats [249] = player.curCapacity[8];
		stats [250] = player.curCapacity[9];
		stats [251] = player.curCapacity[10];
		stats [252] = player.curCapacity[11];
		stats [253] = player.curCapacity[12];
		stats [254] = player.curCapacity[13];
		stats [255] = player.curCapacity[14];
		stats [256] = player.curCapacity[15];
		stats [257] = player.curCapacity[16];


		//crit
		stats [258] = player.curCrit[0];
		stats [259] = player.curCrit[1];
		stats [260] = player.curCrit[2];
		stats [261] = player.curCrit[3];
		stats [262] = player.curCrit[4];
		stats [263] = player.curCrit[5];
		stats [264] = player.curCrit[6];
		stats [265] = player.curCrit[7];
		stats [266] = player.curCrit[8];
		stats [267] = player.curCrit[9];
		stats [268] = player.curCrit[10];
		stats [269] = player.curCrit[11];
		stats [270] = player.curCrit[12];
		stats [271] = player.curCrit[13];
		stats [272] = player.curCrit[14];
		stats [273] = player.curCrit[15];
		stats [274] = player.curCrit[16];

		//range
		stats [275] = player.curRange[0];
		stats [276] = player.curRange[1];
		stats [277] = player.curRange[2];
		stats [278] = player.curRange[3];
		stats [279] = player.curRange[4];
		stats [280] = player.curRange[5];
		stats [281] = player.curRange[6];
		stats [282] = player.curRange[7];
		stats [283] = player.curRange[8];
		stats [284] = player.curRange[9];
		stats [285] = player.curRange[10];
		stats [286] = player.curRange[11];
		stats [287] = player.curRange[12];
		stats [288] = player.curRange[13];
		stats [289] = player.curRange[14];
		stats [290] = player.curRange[15];
		stats [291] = player.curRange[16];


		//accuracy
		stats [292] = player.curAccuracy[0];
		stats [293] = player.curAccuracy[1];
		stats [294] = player.curAccuracy[2];
		stats [295] = player.curAccuracy[3];
		stats [296] = player.curAccuracy[4];
		stats [297] = player.curAccuracy[5];
		stats [298] = player.curAccuracy[6];
		stats [299] = player.curAccuracy[7];
		stats [300] = player.curAccuracy[8];
		stats [301] = player.curAccuracy[9];
		stats [302] = player.curAccuracy[10];
		stats [303] = player.curAccuracy[11];
		stats [304] = player.curAccuracy[12];
		stats [305] = player.curAccuracy[13];
		stats [306] = player.curAccuracy[14];
		stats [307] = player.curAccuracy[15];
		stats [308] = player.curAccuracy[16];

		stats [309] = player.totalXP;
		stats [310] = player.shopKeepTimer;


			//PlayerDisplay.UpdateDisplay();

	}
}

