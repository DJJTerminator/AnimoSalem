using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class InventoryListWindow : MonoBehaviour {
    public GameObject itemSlotPrefab;
    public ToggleGroup itemSlotToggleGroup;
    public GameObject content;

    int xPos = 0;
    int yPos = 0;
    GameObject itemSlot;

	public Text invStrngth;
	public Text invConst;
	public Text invFort;
	public Text invDex;
	public Text invAgil;
	public Text invLuck;
	public Text invChar;
	public Text invPerc;
	public Text invInt;
	public Text invHP;
	public Text invCurLevel;
	public Text invCurXp;
	public Text invNextXp;
	public Text invMoney;
	public GameObject xpBar;
    [SerializeField]
    GameObject myEvent;
	

    int itemCount;

    void OnEnable()
    {
	//calling the function that will set up the stats
		SetUpStats();

        //resetting itemCount
        itemCount = 0;
        
        //***LOADING AL WEAPONS TO INVENTORY***
        //FBI Custom
        if (DataStorage.obtainedWeapons[0] > 0)
        {
            itemSlot = Instantiate(Resources.Load("InventorySlots/FBIHandgun", typeof(GameObject))) as GameObject;
            CreateInventorySlotWindow();
            //checking to see if an item is already selected in the event system, if not, we assign to it now
            if (myEvent.GetComponent<UnityEngine.EventSystems.EventSystem>().currentSelectedGameObject == null)
            {
                myEvent.GetComponent<UnityEngine.EventSystems.EventSystem>().SetSelectedGameObject(itemSlot);
            }
        }
        //Oppressor
        if (DataStorage.obtainedWeapons[1] > 0)
        {
            itemSlot = Instantiate(Resources.Load("InventorySlots/Oppressor", typeof(GameObject))) as GameObject;
            CreateInventorySlotWindow();
        }
        //Blacklist
        if (DataStorage.obtainedWeapons[2] > 0)
        {
            itemSlot = Instantiate(Resources.Load("InventorySlots/The Blacklist", typeof(GameObject))) as GameObject;
            CreateInventorySlotWindow();
            //checking to see if an item is already selected in the event system, if not, we assign to it now
            if (myEvent.GetComponent<UnityEngine.EventSystems.EventSystem>().currentSelectedGameObject == null)
            {
                myEvent.GetComponent<UnityEngine.EventSystems.EventSystem>().SetSelectedGameObject(itemSlot);
            }
        }
        //Trident
        if (DataStorage.obtainedWeapons[3] > 0)
        {
            itemSlot = Instantiate(Resources.Load("InventorySlots/Trident", typeof(GameObject))) as GameObject;
            CreateInventorySlotWindow();
            //checking to see if an item is already selected in the event system, if not, we assign to it now
            if (myEvent.GetComponent<UnityEngine.EventSystems.EventSystem>().currentSelectedGameObject == null)
            {
                myEvent.GetComponent<UnityEngine.EventSystems.EventSystem>().SetSelectedGameObject(itemSlot);
            }
        }
        //Silencer
        if (DataStorage.obtainedWeapons[4] > 0)
        {
            itemSlot = Instantiate(Resources.Load("InventorySlots/Silencer", typeof(GameObject))) as GameObject;
            CreateInventorySlotWindow();
            //checking to see if an item is already selected in the event system, if not, we assign to it now
            if (myEvent.GetComponent<UnityEngine.EventSystems.EventSystem>().currentSelectedGameObject == null)
            {
                myEvent.GetComponent<UnityEngine.EventSystems.EventSystem>().SetSelectedGameObject(itemSlot);
            }
        }
        //Seeker
        if (DataStorage.obtainedWeapons[5] > 0)
        {
            itemSlot = Instantiate(Resources.Load("InventorySlots/Seeker", typeof(GameObject))) as GameObject;
            CreateInventorySlotWindow();
            //checking to see if an item is already selected in the event system, if not, we assign to it now
            if (myEvent.GetComponent<UnityEngine.EventSystems.EventSystem>().currentSelectedGameObject == null)
            {
                myEvent.GetComponent<UnityEngine.EventSystems.EventSystem>().SetSelectedGameObject(itemSlot);
            }
        }
        //Hunter Killer
        if (DataStorage.obtainedWeapons[6] > 0)
        {
            itemSlot = Instantiate(Resources.Load("InventorySlots/Hunter Killer", typeof(GameObject))) as GameObject;
            CreateInventorySlotWindow();
            //checking to see if an item is already selected in the event system, if not, we assign to it now
            if (myEvent.GetComponent<UnityEngine.EventSystems.EventSystem>().currentSelectedGameObject == null)
            {
                myEvent.GetComponent<UnityEngine.EventSystems.EventSystem>().SetSelectedGameObject(itemSlot);
            }
        }
        //Crow's Nest
        if (DataStorage.obtainedWeapons[7] > 0)
        {
            itemSlot = Instantiate(Resources.Load("InventorySlots/Crow's Nest", typeof(GameObject))) as GameObject;
            CreateInventorySlotWindow();
            //checking to see if an item is already selected in the event system, if not, we assign to it now
            if (myEvent.GetComponent<UnityEngine.EventSystems.EventSystem>().currentSelectedGameObject == null)
            {
                myEvent.GetComponent<UnityEngine.EventSystems.EventSystem>().SetSelectedGameObject(itemSlot);
            }
        }
        //Energy Rifle
        if (DataStorage.obtainedWeapons[19] > 0)
        {
            itemSlot = Instantiate(Resources.Load("InventorySlots/Energy Rifle", typeof(GameObject))) as GameObject;
            CreateInventorySlotWindow();
            //checking to see if an item is already selected in the event system, if not, we assign to it now
            if (myEvent.GetComponent<UnityEngine.EventSystems.EventSystem>().currentSelectedGameObject == null)
            {
                myEvent.GetComponent<UnityEngine.EventSystems.EventSystem>().SetSelectedGameObject(itemSlot);
            }
        }
        //12 Gauge
        if (DataStorage.obtainedWeapons[8] > 0)
        {
            itemSlot = Instantiate(Resources.Load("InventorySlots/12 Gauge", typeof(GameObject))) as GameObject;
            CreateInventorySlotWindow();
            //checking to see if an item is already selected in the event system, if not, we assign to it now
            if (myEvent.GetComponent<UnityEngine.EventSystems.EventSystem>().currentSelectedGameObject == null)
            {
                myEvent.GetComponent<UnityEngine.EventSystems.EventSystem>().SetSelectedGameObject(itemSlot);
            }
        }
        //Orthrus
        if (DataStorage.obtainedWeapons[9] > 0)
        {
            itemSlot = Instantiate(Resources.Load("InventorySlots/Orthrus", typeof(GameObject))) as GameObject;
            CreateInventorySlotWindow();
            //checking to see if an item is already selected in the event system, if not, we assign to it now
            if (myEvent.GetComponent<UnityEngine.EventSystems.EventSystem>().currentSelectedGameObject == null)
            {
                myEvent.GetComponent<UnityEngine.EventSystems.EventSystem>().SetSelectedGameObject(itemSlot);
            }
        }
        //Cerberus
        if (DataStorage.obtainedWeapons[10] > 0)
        {
            itemSlot = Instantiate(Resources.Load("InventorySlots/Cerberus", typeof(GameObject))) as GameObject;
            CreateInventorySlotWindow();
            //checking to see if an item is already selected in the event system, if not, we assign to it now
            if (myEvent.GetComponent<UnityEngine.EventSystems.EventSystem>().currentSelectedGameObject == null)
            {
                myEvent.GetComponent<UnityEngine.EventSystems.EventSystem>().SetSelectedGameObject(itemSlot);
            }
        }
        //Devestator
        if (DataStorage.obtainedWeapons[11] > 0)
        {
            itemSlot = Instantiate(Resources.Load("InventorySlots/Devestator", typeof(GameObject))) as GameObject;
            CreateInventorySlotWindow();
            //checking to see if an item is already selected in the event system, if not, we assign to it now
            if (myEvent.GetComponent<UnityEngine.EventSystems.EventSystem>().currentSelectedGameObject == null)
            {
                myEvent.GetComponent<UnityEngine.EventSystems.EventSystem>().SetSelectedGameObject(itemSlot);
            }
        }
        //Savage One
        if (DataStorage.obtainedWeapons[12] > 0)
        {
            itemSlot = Instantiate(Resources.Load("InventorySlots/Savage One", typeof(GameObject))) as GameObject;
            CreateInventorySlotWindow();
            //checking to see if an item is already selected in the event system, if not, we assign to it now
            if (myEvent.GetComponent<UnityEngine.EventSystems.EventSystem>().currentSelectedGameObject == null)
            {
                myEvent.GetComponent<UnityEngine.EventSystems.EventSystem>().SetSelectedGameObject(itemSlot);
            }
        }
        //Redeemer
        if (DataStorage.obtainedWeapons[17] > 0)
        {
            itemSlot = Instantiate(Resources.Load("InventorySlots/Redeemer", typeof(GameObject))) as GameObject;
            CreateInventorySlotWindow();
            //checking to see if an item is already selected in the event system, if not, we assign to it now
            if (myEvent.GetComponent<UnityEngine.EventSystems.EventSystem>().currentSelectedGameObject == null)
            {
                myEvent.GetComponent<UnityEngine.EventSystems.EventSystem>().SetSelectedGameObject(itemSlot);
            }
        }
        //Diminisher
        if (DataStorage.obtainedWeapons[13] > 0)
        {
            itemSlot = Instantiate(Resources.Load("InventorySlots/Diminisher", typeof(GameObject))) as GameObject;
            CreateInventorySlotWindow();
            //checking to see if an item is already selected in the event system, if not, we assign to it now
            if (myEvent.GetComponent<UnityEngine.EventSystems.EventSystem>().currentSelectedGameObject == null)
            {
                myEvent.GetComponent<UnityEngine.EventSystems.EventSystem>().SetSelectedGameObject(itemSlot);
            }
        }
        //Hellfire
        if (DataStorage.obtainedWeapons[18] > 0)
        {
            itemSlot = Instantiate(Resources.Load("InventorySlots/Hellfire", typeof(GameObject))) as GameObject;
            CreateInventorySlotWindow();
            //checking to see if an item is already selected in the event system, if not, we assign to it now
            if (myEvent.GetComponent<UnityEngine.EventSystems.EventSystem>().currentSelectedGameObject == null)
            {
                myEvent.GetComponent<UnityEngine.EventSystems.EventSystem>().SetSelectedGameObject(itemSlot);
            }
        }
        //Revolver
        if (DataStorage.obtainedWeapons[14] > 0)
        {
            itemSlot = Instantiate(Resources.Load("InventorySlots/Revolver", typeof(GameObject))) as GameObject;
            CreateInventorySlotWindow();
            //checking to see if an item is already selected in the event system, if not, we assign to it now
            if (myEvent.GetComponent<UnityEngine.EventSystems.EventSystem>().currentSelectedGameObject == null)
            {
                myEvent.GetComponent<UnityEngine.EventSystems.EventSystem>().SetSelectedGameObject(itemSlot);
            }
        }
        //Scylla
        if (DataStorage.obtainedWeapons[15] > 0)
        {
            itemSlot = Instantiate(Resources.Load("InventorySlots/Scylla", typeof(GameObject))) as GameObject;
            CreateInventorySlotWindow();
            //checking to see if an item is already selected in the event system, if not, we assign to it now
            if (myEvent.GetComponent<UnityEngine.EventSystems.EventSystem>().currentSelectedGameObject == null)
            {
                myEvent.GetComponent<UnityEngine.EventSystems.EventSystem>().SetSelectedGameObject(itemSlot);
            }
        }
        //Day Ender
        if (DataStorage.obtainedWeapons[16] > 0)
        {
            itemSlot = Instantiate(Resources.Load("InventorySlots/Day Ender", typeof(GameObject))) as GameObject;
            CreateInventorySlotWindow();
            //checking to see if an item is already selected in the event system, if not, we assign to it now
            if (myEvent.GetComponent<UnityEngine.EventSystems.EventSystem>().currentSelectedGameObject == null)
            {
                myEvent.GetComponent<UnityEngine.EventSystems.EventSystem>().SetSelectedGameObject(itemSlot);
            }
        }
        //Eradicator
        if (DataStorage.obtainedWeapons[20] > 0)
        {
            itemSlot = Instantiate(Resources.Load("InventorySlots/Eradicator", typeof(GameObject))) as GameObject;
            CreateInventorySlotWindow();
            //checking to see if an item is already selected in the event system, if not, we assign to it now
            if (myEvent.GetComponent<UnityEngine.EventSystems.EventSystem>().currentSelectedGameObject == null)
            {
                myEvent.GetComponent<UnityEngine.EventSystems.EventSystem>().SetSelectedGameObject(itemSlot);
            }
        }





        //handgun ammo
        if (DataStorage.HGAmmo > 0)
        {
            itemSlot = Instantiate(Resources.Load("InventorySlots/HandgunAmmo", typeof(GameObject))) as GameObject;
            CreateInventorySlotWindow();
            //checking to see if an item is already selected in the event system, if not, we assign to it now
            if (myEvent.GetComponent<UnityEngine.EventSystems.EventSystem>().currentSelectedGameObject == null)
            {
                myEvent.GetComponent<UnityEngine.EventSystems.EventSystem>().SetSelectedGameObject(itemSlot);
            }
        }
        //rifle ammo
        if (DataStorage.rifleAmmo > 0)
        {
            itemSlot = Instantiate(Resources.Load("InventorySlots/RifleAmmo", typeof(GameObject))) as GameObject;
            CreateInventorySlotWindow();
            //checking to see if an item is already selected in the event system, if not, we assign to it now
            if (myEvent.GetComponent<UnityEngine.EventSystems.EventSystem>().currentSelectedGameObject == null)
            {
                myEvent.GetComponent<UnityEngine.EventSystems.EventSystem>().SetSelectedGameObject(itemSlot);
            }
        }
        //shotgun ammo
        if (DataStorage.SGAmmo > 0)
        {
            itemSlot = Instantiate(Resources.Load("InventorySlots/ShotgunAmmo", typeof(GameObject))) as GameObject;
            CreateInventorySlotWindow();
            //checking to see if an item is already selected in the event system, if not, we assign to it now
            if (myEvent.GetComponent<UnityEngine.EventSystems.EventSystem>().currentSelectedGameObject == null)
            {
                myEvent.GetComponent<UnityEngine.EventSystems.EventSystem>().SetSelectedGameObject(itemSlot);
            }
        }
        //machine gun ammo
        if (DataStorage.MGAmmo > 0)
        {
            itemSlot = Instantiate(Resources.Load("InventorySlots/MachinegunAmmo", typeof(GameObject))) as GameObject;
            CreateInventorySlotWindow();
            //checking to see if an item is already selected in the event system, if not, we assign to it now
            if (myEvent.GetComponent<UnityEngine.EventSystems.EventSystem>().currentSelectedGameObject == null)
            {
                myEvent.GetComponent<UnityEngine.EventSystems.EventSystem>().SetSelectedGameObject(itemSlot);
            }
        }
        //magnum gun ammo
        if (DataStorage.magnumAmmo > 0)
        {
            itemSlot = Instantiate(Resources.Load("InventorySlots/MagnumAmmo", typeof(GameObject))) as GameObject;
            CreateInventorySlotWindow();
            //checking to see if an item is already selected in the event system, if not, we assign to it now
            if (myEvent.GetComponent<UnityEngine.EventSystems.EventSystem>().currentSelectedGameObject == null)
            {
                myEvent.GetComponent<UnityEngine.EventSystems.EventSystem>().SetSelectedGameObject(itemSlot);
            }
        }
               //explosive gun ammo
        if (DataStorage.explosiveAmmo > 0)
        {
            itemSlot = Instantiate(Resources.Load("InventorySlots/ExplosiveRounds", typeof(GameObject))) as GameObject;
            CreateInventorySlotWindow();
            //checking to see if an item is already selected in the event system, if not, we assign to it now
            if (myEvent.GetComponent<UnityEngine.EventSystems.EventSystem>().currentSelectedGameObject == null)
            {
                myEvent.GetComponent<UnityEngine.EventSystems.EventSystem>().SetSelectedGameObject(itemSlot);
            }
        }
        //small aid
        for  (int i = 0; i < DataStorage.itemSmallAid; i++)  //gameobject find player's inventory and get the count. In otherwords, find how many items the player has and replace 20 with that value
        {
            //itemSlot = Resources.Load("InventorySlots/ItemSlotList");
            //itemSlot = (GameObject)Instantiate(itemSlotPrefab);

            itemSlot = Instantiate(Resources.Load("InventorySlots/Small Aid", typeof(GameObject))) as GameObject;
            CreateInventorySlotWindow();
            //checking to see if an item is already selected in the event system, if not, we assign to it now
            if (myEvent.GetComponent<UnityEngine.EventSystems.EventSystem>().currentSelectedGameObject == null)
            {
                myEvent.GetComponent<UnityEngine.EventSystems.EventSystem>().SetSelectedGameObject(itemSlot);
            }
        }
        //med aid
        for (int i = 0; i < DataStorage.itemMedAid; i++)  //gameobject find player's inventory and get the count. In otherwords, find how many items the player has and replace 20 with that value
        {
            //itemSlot = Resources.Load("InventorySlots/ItemSlotList");
            //itemSlot = (GameObject)Instantiate(itemSlotPrefab);

            itemSlot = Instantiate(Resources.Load("InventorySlots/Med Aid", typeof(GameObject))) as GameObject;
            CreateInventorySlotWindow();
            //checking to see if an item is already selected in the event system, if not, we assign to it now
            if (myEvent.GetComponent<UnityEngine.EventSystems.EventSystem>().currentSelectedGameObject == null)
            {
                myEvent.GetComponent<UnityEngine.EventSystems.EventSystem>().SetSelectedGameObject(itemSlot);
            }
        }
        //large aid
        for (int i = 0; i < DataStorage.itemLargeAid; i++)  //gameobject find player's inventory and get the count. In otherwords, find how many items the player has and replace 20 with that value
        {
            //itemSlot = Resources.Load("InventorySlots/ItemSlotList");
            //itemSlot = (GameObject)Instantiate(itemSlotPrefab);

            itemSlot = Instantiate(Resources.Load("InventorySlots/Large Aid", typeof(GameObject))) as GameObject;
            CreateInventorySlotWindow();
            //checking to see if an item is already selected in the event system, if not, we assign to it now
            if (myEvent.GetComponent<UnityEngine.EventSystems.EventSystem>().currentSelectedGameObject == null)
            {
                myEvent.GetComponent<UnityEngine.EventSystems.EventSystem>().SetSelectedGameObject(itemSlot);
            }
        }
        //small key
        for (int i = 0; i < DataStorage.itemSmallKey; i++)  //gameobject find player's inventory and get the count. In otherwords, find how many items the player has and replace 20 with that value
        {
            //itemSlot = Resources.Load("InventorySlots/ItemSlotList");
            //itemSlot = (GameObject)Instantiate(itemSlotPrefab);

            itemSlot = Instantiate(Resources.Load("InventorySlots/Small Key", typeof(GameObject))) as GameObject;
            CreateInventorySlotWindow();
            //checking to see if an item is already selected in the event system, if not, we assign to it now
            if (myEvent.GetComponent<UnityEngine.EventSystems.EventSystem>().currentSelectedGameObject == null)
            {
                myEvent.GetComponent<UnityEngine.EventSystems.EventSystem>().SetSelectedGameObject(itemSlot);
            }
        }
        //potion
        for (int i = 0; i < DataStorage.itemHolyWater; i++)  //gameobject find player's inventory and get the count. In otherwords, find how many items the player has and replace 20 with that value
        {
            //itemSlot = Resources.Load("InventorySlots/ItemSlotList");
            //itemSlot = (GameObject)Instantiate(itemSlotPrefab);

            itemSlot = Instantiate(Resources.Load("InventorySlots/Potion", typeof(GameObject))) as GameObject;
            CreateInventorySlotWindow();
            //checking to see if an item is already selected in the event system, if not, we assign to it now
            if (myEvent.GetComponent<UnityEngine.EventSystems.EventSystem>().currentSelectedGameObject == null)
            {
                myEvent.GetComponent<UnityEngine.EventSystems.EventSystem>().SetSelectedGameObject(itemSlot);
            }
        }
    }
	//displaying information and content
	void SetUpStats()
	{
		try 
		{
			DataStorage.HUD.SetActive(false);
		}
		catch
		{
			DataStorage.HUD = GameObject.Find("All Canvases/Canvas/HUD");
			DataStorage.HUD.SetActive(false);
		}
		invStrngth.text = "Strength: " + DataStorage.strength.ToString();
		invConst.text = "Constitution: " + DataStorage.constitution.ToString();
		invFort.text = "Fortitude: " + DataStorage.fortitude.ToString();
		invDex.text = "Dexterity: " + DataStorage.dexterity.ToString();
		invAgil.text = "Agility: " + DataStorage.agility.ToString();
		invLuck.text = "Luck: " + DataStorage.luck.ToString();
		invChar.text = "Charisma: " + DataStorage.charisma.ToString();
		invPerc.text = "Perception: " + DataStorage.perception.ToString();
		invInt.text = "Intelligence: " + DataStorage.intelligence.ToString();
		invHP.text = "Health: " + DataStorage.health.ToString() + " / " + DataStorage.maxHealth;
		invMoney.text = "$" + DataStorage.money.ToString("n0");
		invCurLevel.text = "Level: " + DataStorage.currentLevel.ToString();
		invCurXp.text = DataStorage.XP.ToString("n0");
		invNextXp.text = DataStorage.maxXP.ToString("n0");
		xpBar.transform.localScale = new Vector3 ((float)DataStorage.XP/DataStorage.maxXP,1,1);
	}

    //destroying the items upon exit, so that when we enable the inventory, it wont double the items
    void OnDisable()
    {
        foreach (Transform child in content.transform)
        {
            GameObject.Destroy(child.gameObject);
        }
		DataStorage.HUD.SetActive(true);
     }
    //creating the width, height, and transform of each game object
    private void CreateInventorySlotWindow()
    {
        itemCount++;
        itemSlot.name = itemCount.ToString();
        itemSlot.GetComponent<Toggle>().group = itemSlotToggleGroup;
        itemSlot.transform.SetParent(content.transform);
        itemSlot.GetComponent<RectTransform>().localPosition = new Vector3(xPos, yPos, 0);
        itemSlot.GetComponent<RectTransform>().localRotation = Quaternion.identity;
        itemSlot.GetComponent<RectTransform>().localScale = new Vector3(1, 1, 1);
        yPos -= (int)itemSlot.GetComponent<RectTransform>().rect.height;
    }

}
