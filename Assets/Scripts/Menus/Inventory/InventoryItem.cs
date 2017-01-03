using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class InventoryItem : MonoBehaviour {
	public InventoryDecisionScript decide;
    public GameObject myImage;
    public Text myWeight;
    public Text myAmount;
    public GameObject myName;
    public GameObject myDescription;
    public string myDesc;
    public int weaponNumber;
	float doubleClick;
	public string itemName;
	public float itemWeight;
	public GameObject sellAmount;
    public GameObject content;
    //percentage bars
    GameObject dmg;
    GameObject rload;
    GameObject cap;
    GameObject fr;
    GameObject ac;
    GameObject crit;
    GameObject rg;
    //text values
    Text _damage;
    Text _capacity;
    Text _crit;
    Text _range;
    Text _accuracy;
    Text _fireRate;
    Text _reload;
	GameObject weaponStats;


    public enum ItemType
    {
        itemWeapon,
        itemAid,
        itemPotion,
        itemAmmo,
        itemKey,
        itemGem,
		itemMisc,
        itemQuest

    }
    public ItemType type;

    public enum AmmoType
    {
        handgunAmmo,
        machinegunAmmo,
        shotgunAmmo,
        rifleAmmo,
        magnumAmmo,
        explosiveAmmo,
    }

    public AmmoType ammo;

	// Use this for initialization
	void Start () {
        myDescription = GameObject.Find("ItemDescription");
        myName = GameObject.Find("ItemName");
		decide = GameObject.Find ("All Canvases/Canvas/StorageMenu/Inventory/InventoryList/ScrollRect").GetComponent<InventoryDecisionScript>();
        dmg = GameObject.Find("All Canvases/Canvas/StorageMenu/Inventory/Display/WeaponStats/Damage/Image/Bar");
        rload= GameObject.Find("All Canvases/Canvas/StorageMenu/Inventory/Display/WeaponStats/Reload/Image/Bar");
        cap= GameObject.Find("All Canvases/Canvas/StorageMenu/Inventory/Display/WeaponStats/Capacity/Image/Bar");
        fr= GameObject.Find("All Canvases/Canvas/StorageMenu/Inventory/Display/WeaponStats/FireRate/Image/Bar");
        ac= GameObject.Find("All Canvases/Canvas/StorageMenu/Inventory/Display/WeaponStats/Accuracy/Image/Bar");
        crit= GameObject.Find("All Canvases/Canvas/StorageMenu/Inventory/Display/WeaponStats/CriticalChance/Image/Bar");
        rg= GameObject.Find("All Canvases/Canvas/StorageMenu/Inventory/Display/WeaponStats/Range/Image/Bar");

		weaponStats = GameObject.Find("All Canvases/Canvas/StorageMenu/Inventory/Display/WeaponStats");
        _damage = GameObject.Find("All Canvases/Canvas/StorageMenu/Inventory/Display/WeaponStats/Damage/Value").GetComponent<Text>();
        _capacity = GameObject.Find("All Canvases/Canvas/StorageMenu/Inventory/Display/WeaponStats/Capacity/Value").GetComponent<Text>();
        _crit = GameObject.Find("All Canvases/Canvas/StorageMenu/Inventory/Display/WeaponStats/CriticalChance/Value").GetComponent<Text>();
        _range = GameObject.Find("All Canvases/Canvas/StorageMenu/Inventory/Display/WeaponStats/Range/Value").GetComponent<Text>();
        _accuracy = GameObject.Find("All Canvases/Canvas/StorageMenu/Inventory/Display/WeaponStats/Accuracy/Value").GetComponent<Text>();
        _fireRate = GameObject.Find("All Canvases/Canvas/StorageMenu/Inventory/Display/WeaponStats/FireRate/Value").GetComponent<Text>();
        _reload = GameObject.Find("All Canvases/Canvas/StorageMenu/Inventory/Display/WeaponStats/Reload/Value").GetComponent<Text>();
        content = GameObject.Find("All Canvases/Canvas/StorageMenu/Inventory/InventoryList/ScrollRect/Content");


        if (type == ItemType.itemAmmo && ammo == AmmoType.handgunAmmo) 
		{
			itemWeight = (float)DataStorage.HGAmmo * .15f;
			myAmount.GetComponent<Text> ().text = "Bullets" + "(" + DataStorage.HGAmmo.ToString () + ")";
			myWeight.GetComponent<Text>().text = "lbs " + ((float)DataStorage.HGAmmo * .15f).ToString();
		} else if (type == ItemType.itemAmmo && ammo == AmmoType.shotgunAmmo) {
			itemWeight = (float)DataStorage.SGAmmo * .25f;
			myAmount.GetComponent<Text> ().text = "Shells" + "(" + DataStorage.SGAmmo.ToString () + ")";
			myWeight.GetComponent<Text>().text = "lbs " + ((float)DataStorage.SGAmmo * .25f).ToString();
		} else if (type == ItemType.itemAmmo && ammo == AmmoType.machinegunAmmo) {
			itemWeight = (float)DataStorage.MGAmmo * .12f;
			myAmount.GetComponent<Text> ().text = "Bullet" + "(" + DataStorage.MGAmmo.ToString () + ")";
			myWeight.GetComponent<Text>().text = "lbs " + ((float)DataStorage.MGAmmo * .12f).ToString();
		} else if (type == ItemType.itemAmmo && ammo == AmmoType.rifleAmmo) {
			itemWeight = (float)DataStorage.rifleAmmo * .2f;
			myAmount.GetComponent<Text> ().text = "Rounds" + "(" + DataStorage.rifleAmmo.ToString () + ")";
			myWeight.GetComponent<Text>().text = "lbs " + ((float)DataStorage.rifleAmmo * .2f).ToString();
		} else if (type == ItemType.itemAmmo && ammo == AmmoType.magnumAmmo) {
			itemWeight = (float)DataStorage.magnumAmmo * .2f;
			myAmount.GetComponent<Text> ().text = "Rounds" + "(" + DataStorage.magnumAmmo.ToString () + ")";
			myWeight.GetComponent<Text>().text = "lbs " + ((float)DataStorage.magnumAmmo * .2f).ToString();
		} else if (type == ItemType.itemAmmo && ammo == AmmoType.explosiveAmmo) {
			itemWeight = (float)DataStorage.explosiveAmmo * .4f;
			myAmount.GetComponent<Text> ().text = "Rounds" + "(" + DataStorage.explosiveAmmo.ToString () + ")";
			myWeight.GetComponent<Text>().text = "lbs " + ((float)DataStorage.explosiveAmmo * .4f).ToString();
		}
		if (type == ItemType.itemWeapon) 
			myAmount.GetComponent<Text> ().text = DataStorage.weaponType [weaponNumber].ToString ();
		if (type == ItemType.itemWeapon)
			myAmount.GetComponent<Text>().text = DataStorage.weaponType[weaponNumber].ToString();
		if (type == ItemType.itemAid)
			myAmount.GetComponent<Text>().text = "Aid";
		if (type == ItemType.itemKey)
			myAmount.GetComponent<Text>().text = "Key";
		if (type == ItemType.itemGem)
			myAmount.GetComponent<Text>().text = "Soul Gem";
		if (type == ItemType.itemQuest)
			myAmount.GetComponent<Text>().text = "Quest";
		if (type == ItemType.itemMisc)
			myAmount.GetComponent<Text>().text = "Misc";
	}

    public void Animation()
    {
       gameObject.GetComponent<Animator>().enabled = true;
    }

    public void AnimationOff()
    {
        gameObject.GetComponent<Animator>().enabled = false;
    }
    //changing the view to the currently selected item
    public void OnClick()
	{
        //getting the temp position
            Vector3 temp = new Vector3(0, -gameObject.transform.localPosition.y, 0);
            StartCoroutine(MyLerp(content.transform.localPosition, temp, .2f));

         //   StartCoroutine(MyLerp(gameObject.transform.localPosition, temp, 1f));
   
        if (Input.GetKeyDown ("return")) 
		{
			decide.Decide (gameObject);
		} 

				//if it was not double clciked or if eneter was not hit, then we will display the item's content
				if (type == ItemType.itemWeapon) 
				{
					weaponStats.SetActive(true);
					myDescription.GetComponent<Text> ().lineSpacing = .8f;
					myName.GetComponent<Text> ().text = DataStorage.weaponName [weaponNumber];
                     //assinging the percentage bar accordingly to the weapon that is selected
                     dmg.transform.localScale = new Vector3((float)DataStorage.curDamage[weaponNumber] / 5f, 1, 0);
                     rload.transform.localScale = new Vector3((float)DataStorage.curReload[weaponNumber] / 5f, 1, 0);
                     cap.transform.localScale = new Vector3((float)DataStorage.curCapacity[weaponNumber] / 5f, 1, 0);
                     fr.transform.localScale = new Vector3((float)DataStorage.curFireRate[weaponNumber] / 5f, 1, 0);
                     ac.transform.localScale = new Vector3((float)DataStorage.curAccuracy[weaponNumber] / 5f, 1, 0);
                     crit.transform.localScale = new Vector3((float)DataStorage.curCrit[weaponNumber] / 5f, 1, 0);
                     rg.transform.localScale = new Vector3((float)DataStorage.curRange[weaponNumber] / 5f, 1, 0);
                        //assigning the values to each text
                     _damage.text = DataStorage.weaponDamage[weaponNumber].ToString();
                     _reload.text = (Mathf.Round(DataStorage.reload[weaponNumber] * 100f) / 100f).ToString(); 
            _fireRate.text = DataStorage.fireRate[weaponNumber].ToString();
                     _capacity.text = DataStorage.capacity[weaponNumber].ToString();
                     _crit.text = DataStorage.criticalChance[weaponNumber].ToString();
                     _range.text = DataStorage.range[weaponNumber].ToString();
                     _accuracy.text = DataStorage.accuracy[weaponNumber].ToString();
            //		myDescription.GetComponent<Text> ().text = "Damage: " + DataStorage.weaponDamage [weaponNumber] + "\n" + "Fire Rate: " + Mathf.Round (DataStorage.fireRate [weaponNumber] * 100.0f) + "%" + "\n" + "Capacity " + DataStorage.capacity [weaponNumber] + "\n" + "Reload: " + DataStorage.reload [weaponNumber] + "\n" + "Accuracy: " + Mathf.Round (DataStorage.accuracy [DataStorage.curWeapon] * 10) + "%" + "\n" + "Range: " + (DataStorage.range [DataStorage.curWeapon] * 10) / 1 + "%" + "\n" + "Crit Chance: " + (DataStorage.criticalChance [weaponNumber] * 100) + "%";
        } 
				else 
				{
					weaponStats.SetActive(false);
					myDescription.GetComponent<Text> ().lineSpacing = .8f;
			myName.GetComponent<Text> ().text = itemName;
					myDescription.GetComponent<Text> ().text = "\n" + "\n" + "\n" + "\n" + "\n" + "\n" + "\n" + "\n" + "\n" + myDesc;
				}//end of third else
	}//end of function
    IEnumerator MyLerp(Vector3 source, Vector3 target, float overTime)
    {
        float startTime = Time.time;
        while (Time.time < startTime + overTime)
        {
            content.transform.localPosition = Vector3.Lerp(source, target, (Time.time - startTime) / overTime);
            //content.transform.localPosition = new Vector3(0, -gameObject.transform.localPosition.y,0);
            yield return null;
        }
             content.transform.localPosition = target;
    }
}//end of class
