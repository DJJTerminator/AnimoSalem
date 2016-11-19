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
	

        if (type == ItemType.itemAmmo && ammo == AmmoType.handgunAmmo)
            myAmount.GetComponent<Text>().text = "Bullets" + "(" + DataStorage.HGAmmo.ToString() +")";
        else if (type == ItemType.itemAmmo && ammo == AmmoType.shotgunAmmo)
            myAmount.GetComponent<Text>().text = "Shells" + "(" + DataStorage.SGAmmo.ToString() + ")";
        else if (type == ItemType.itemAmmo && ammo == AmmoType.machinegunAmmo)
            myAmount.GetComponent<Text>().text = "Bullet" + "(" + DataStorage.MGAmmo.ToString() + ")";
        else if (type == ItemType.itemAmmo && ammo == AmmoType.rifleAmmo)
            myAmount.GetComponent<Text>().text = "Rounds" + "(" + DataStorage.rifleAmmo.ToString() + ")";
        else if (type == ItemType.itemAmmo && ammo == AmmoType.magnumAmmo)
            myAmount.GetComponent<Text>().text = "Rounds" + "(" + DataStorage.magnumAmmo.ToString() + ")";
        else if (type == ItemType.itemAmmo && ammo == AmmoType.explosiveAmmo)
            myAmount.GetComponent<Text>().text = "Rounds" + "(" + DataStorage.explosiveAmmo.ToString() + ")";
        if (type == ItemType.itemWeapon)
            myAmount.GetComponent<Text>().text = DataStorage.weaponType[weaponNumber].ToString();
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


        myWeight.GetComponent<Text>().text = "lbs " + DataStorage.maxWeight.ToString();
	}

    public void Animation()
    {
       gameObject.GetComponent<Animator>().enabled = true;
    }

    public void AnimationOff()
    {
        gameObject.GetComponent<Animator>().enabled = false;
    }

    public void OnClick()
	{

		if (Input.GetKeyDown ("return")) 
		{
			decide.Decide (gameObject);
		} 

				//if it was not double clciked or if eneter was not hit, then we will display the item's content
				if (type == ItemType.itemWeapon) 
				{
					myDescription.GetComponent<Text> ().lineSpacing = 1.8f;
					myName.GetComponent<Text> ().text = DataStorage.weaponName [weaponNumber];
					myDescription.GetComponent<Text> ().text = "Damage: " + DataStorage.weaponDamage [weaponNumber] + "\n" + "Fire Rate: " + Mathf.Round (DataStorage.fireRate [weaponNumber] * 100.0f) + "%" + "\n" + "Capacity " + DataStorage.capacity [weaponNumber] + "\n" + "Reload: " + DataStorage.reload [weaponNumber] + "\n" + "Accuracy: " + Mathf.Round (DataStorage.accuracy [DataStorage.curWeapon] * 10) + "%" + "\n" + "Range: " + (DataStorage.range [DataStorage.curWeapon] * 10) / 1 + "%" + "\n" + "Crit Chance: " + (DataStorage.criticalChance [weaponNumber] * 100) + "%";
				} 
				else 
				{
					myDescription.GetComponent<Text> ().lineSpacing = 1f;
			myName.GetComponent<Text> ().text = itemName;
					myDescription.GetComponent<Text> ().text = "\n" + "\n" + "\n" + "\n" + "\n" + "\n" + "\n" + "\n" + "\n" + myDesc;
				}//end of third else
	}//end of function
}//end of class
