using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class InventoryDecisionScript : MonoBehaviour {
	public GameObject decision;

	public GameObject eventDecide;
	public GameObject eventStorage;
	public Text nameOfItem;
	public Text totalWeight;
	public GameObject myItem;
	public AudioSource discardSound;
	public int ammoAmount;
	public GameObject amountValue;
	public Text ammoText;
	int temp;
    GameObject buttons;
    public GameObject droppedText;
    [SerializeField]
    GameObject myEvent;


	void Start()
	{
    buttons = GameObject.Find ("All Canvases/Canvas/StorageMenu/Inventory/InventoryList/Decisions/Buttons");
    droppedText = GameObject.Find("All Canvases/Canvas/StorageMenu/DroppedItemText");
    }

	public void Use()
	{
		//destroy inventory gameobject
		//decrease current weight
		//decrease inventory variable
		//heal player or do whatever
	}

	public void Discard()
	{
		if (myItem.GetComponent<InventoryItem> ().type != InventoryItem.ItemType.itemAmmo)
			Drop ();
		else 
		{
			switch (myItem.GetComponent<InventoryItem>().ammo)
			{
			default:
				ammoAmount = DataStorage.HGAmmo;
				temp = DataStorage.HGAmmo;
				break;
			case InventoryItem.AmmoType.shotgunAmmo:
				ammoAmount = DataStorage.SGAmmo;
				temp = DataStorage.SGAmmo;
				break;
			case InventoryItem.AmmoType.machinegunAmmo:
				ammoAmount = DataStorage.MGAmmo;
				temp = DataStorage.MGAmmo;
				break;
			case InventoryItem.AmmoType.rifleAmmo:
				ammoAmount = DataStorage.rifleAmmo;
				temp = DataStorage.rifleAmmo;
				break;
			case InventoryItem.AmmoType.magnumAmmo:
				ammoAmount = DataStorage.magnumAmmo;
				temp = DataStorage.magnumAmmo;
				break;
			case InventoryItem.AmmoType.explosiveAmmo:
				ammoAmount = DataStorage.explosiveAmmo;
				temp = DataStorage.magnumAmmo;
				break;
			}
			amountValue.SetActive (true);
            ammoText.text = ammoAmount.ToString ();
            buttons.SetActive(false);
		}
	}

	public void DecreaseAmmo()
	{
		if (ammoAmount > 1)
			ammoAmount--;
		ammoText.text = ammoAmount.ToString ();
	}

	public void IncreaseAmmo()
	{
		if (ammoAmount < temp)
		ammoAmount++;
		ammoText.text = ammoAmount.ToString ();
	}
	public void Okay()
	{
        buttons.SetActive(true);
        amountValue.SetActive (false);
        Drop ();
        droppedText.SetActive(false);
        droppedText.SetActive(true);
        StartCoroutine(wait(2f));
    }

	//dropping the items
	public void Drop()
	{
		discardSound.Play ();

		//DataStorage.curWeight -= myItem.GetComponent<InventoryItem>().itemWeight;
		//create game object by player
		GameObject clone;
		switch (myItem.GetComponent<InventoryItem>().ammo)
		{
		default:
			//set created game object to the same value and weight that the player dropped
			//dropping handgun ammo
			clone = Instantiate (Resources.Load ("DroppedItems/Handgun Ammo", typeof(GameObject))) as GameObject;
			DataStorage.HGAmmo -= ammoAmount;
			myItem.GetComponent<InventoryItem> ().itemWeight -= (float)ammoAmount *.15f;
			myItem.GetComponent<InventoryItem>().myWeight.GetComponent<Text>().text = "lbs " + myItem.GetComponent<InventoryItem> ().itemWeight.ToString();
			myItem.GetComponent<InventoryItem>().myAmount.GetComponent<Text> ().text = "Bullets" + "(" + DataStorage.HGAmmo.ToString () + ")";
			clone.GetComponent<ItemPickups> ().ammoCount = ammoAmount;
            droppedText.GetComponent<Text>().text = "Dropped " + ammoAmount + " " + "handgun bullets";
                if (DataStorage.HGAmmo <= 0)
                {
                    int value = int.Parse(myItem.name);
                    Destroy(myItem);
                    //checking to see if an item is already selected in the event system, if not, we assign to it now
                    if (myEvent.GetComponent<UnityEngine.EventSystems.EventSystem>().currentSelectedGameObject == null)
                    {
                            value -= 1;
                            string myName = value.ToString();
                            GameObject nextItem = GameObject.Find("All Canvases/Canvas/StorageMenu/Inventory/InventoryList/ScrollRect/Content/"+myName);
                            myEvent.GetComponent<UnityEngine.EventSystems.EventSystem>().SetSelectedGameObject(nextItem);
                        //checking to see if an item is still not selected
                        if (myEvent.GetComponent<UnityEngine.EventSystems.EventSystem>().currentSelectedGameObject == null)
                        {
                            value += 2;
                            myName = value.ToString();
                            nextItem = nextItem = GameObject.Find("All Canvases/Canvas/StorageMenu/Inventory/InventoryList/ScrollRect/Content/" + myName);
                        }
                    }
                }
                //	print (DataStorage.HGAmmo);
                break;
			//dropping shotgnu ammo
		case InventoryItem.AmmoType.shotgunAmmo:
			clone = Instantiate (Resources.Load ("DroppedItems/Handgun Ammo", typeof(GameObject))) as GameObject;
			DataStorage.SGAmmo -= ammoAmount;
			myItem.GetComponent<InventoryItem> ().itemWeight -= (float)ammoAmount *.25f;
			myItem.GetComponent<InventoryItem>().myWeight.GetComponent<Text>().text = "lbs " + myItem.GetComponent<InventoryItem> ().itemWeight.ToString();
			myItem.GetComponent<InventoryItem>().myAmount.GetComponent<Text> ().text = "Bullets" + "(" + DataStorage.SGAmmo.ToString () + ")";
			clone.GetComponent<ItemPickups> ().ammoCount = ammoAmount;
            droppedText.GetComponent<Text>().text = "Dropped " + ammoAmount + " " + "shotgun shells";
                if (DataStorage.SGAmmo <= 0)
                {
                    int value = int.Parse(myItem.name);
                    Destroy(myItem);
                    //checking to see if an item is already selected in the event system, if not, we assign to it now
                    if (myEvent.GetComponent<UnityEngine.EventSystems.EventSystem>().currentSelectedGameObject == null)
                    {
                        value -= 1;
                        string myName = value.ToString();
                        GameObject nextItem = GameObject.Find("All Canvases/Canvas/StorageMenu/Inventory/InventoryList/ScrollRect/Content/" + myName);
                        myEvent.GetComponent<UnityEngine.EventSystems.EventSystem>().SetSelectedGameObject(nextItem);
                        //checking to see if an item is still not selected
                        if (myEvent.GetComponent<UnityEngine.EventSystems.EventSystem>().currentSelectedGameObject == null)
                        {
                            value += 2;
                            myName = value.ToString();
                            nextItem = nextItem = GameObject.Find("All Canvases/Canvas/StorageMenu/Inventory/InventoryList/ScrollRect/Content/" + myName);
                        }
                    }
                }
                //print (DataStorage.SGAmmo);
                break;
			//dropping machinegun ammo
		case InventoryItem.AmmoType.machinegunAmmo:
			clone = Instantiate (Resources.Load ("DroppedItems/Handgun Ammo", typeof(GameObject))) as GameObject;
			DataStorage.MGAmmo  -= ammoAmount;
			myItem.GetComponent<InventoryItem> ().itemWeight -= (float)ammoAmount *.12f;
			myItem.GetComponent<InventoryItem>().myWeight.GetComponent<Text>().text = "lbs " + myItem.GetComponent<InventoryItem> ().itemWeight.ToString();
			clone.GetComponent<ItemPickups> ().ammoCount = ammoAmount;
			myItem.GetComponent<InventoryItem>().myAmount.GetComponent<Text> ().text = "Bullets" + "(" + DataStorage.MGAmmo.ToString () + ")";
            droppedText.GetComponent<Text>().text = "Dropped " + ammoAmount + " " + "machinegun bullets";
                if (DataStorage.MGAmmo <= 0)
                {
                    int value = int.Parse(myItem.name);
                    Destroy(myItem);
                    //checking to see if an item is already selected in the event system, if not, we assign to it now
                    if (myEvent.GetComponent<UnityEngine.EventSystems.EventSystem>().currentSelectedGameObject == null)
                    {
                        value -= 1;
                        string myName = value.ToString();
                        GameObject nextItem = GameObject.Find("All Canvases/Canvas/StorageMenu/Inventory/InventoryList/ScrollRect/Content/" + myName);
                        myEvent.GetComponent<UnityEngine.EventSystems.EventSystem>().SetSelectedGameObject(nextItem);
                        //checking to see if an item is still not selected
                        if (myEvent.GetComponent<UnityEngine.EventSystems.EventSystem>().currentSelectedGameObject == null)
                        {
                            value += 2;
                            myName = value.ToString();
                            nextItem = nextItem = GameObject.Find("All Canvases/Canvas/StorageMenu/Inventory/InventoryList/ScrollRect/Content/" + myName);
                        }
                    }
                }
                break;
			//dropping machinegun ammo
		case InventoryItem.AmmoType.rifleAmmo:
			clone = Instantiate (Resources.Load ("DroppedItems/Handgun Ammo", typeof(GameObject))) as GameObject;
			DataStorage.rifleAmmo  -= ammoAmount;
			clone.GetComponent<ItemPickups> ().ammoCount = ammoAmount;
			myItem.GetComponent<InventoryItem> ().itemWeight -= (float)ammoAmount *.2f;
			myItem.GetComponent<InventoryItem>().myWeight.GetComponent<Text>().text = "lbs " + myItem.GetComponent<InventoryItem> ().itemWeight.ToString();
			myItem.GetComponent<InventoryItem>().myAmount.GetComponent<Text> ().text = "Bullets" + "(" + DataStorage.rifleAmmo.ToString () + ")";
            droppedText.GetComponent<Text>().text = "Dropped " + ammoAmount + " " + "rifle rounds";
                if (DataStorage.rifleAmmo <= 0)
                {
                    int value = int.Parse(myItem.name);
                    Destroy(myItem);
                    //checking to see if an item is already selected in the event system, if not, we assign to it now
                    if (myEvent.GetComponent<UnityEngine.EventSystems.EventSystem>().currentSelectedGameObject == null)
                    {
                        value -= 1;
                        string myName = value.ToString();
                        GameObject nextItem = GameObject.Find("All Canvases/Canvas/StorageMenu/Inventory/InventoryList/ScrollRect/Content/" + myName);
                        myEvent.GetComponent<UnityEngine.EventSystems.EventSystem>().SetSelectedGameObject(nextItem);
                        //checking to see if an item is still not selected
                        if (myEvent.GetComponent<UnityEngine.EventSystems.EventSystem>().currentSelectedGameObject == null)
                        {
                            value += 2;
                            myName = value.ToString();
                            nextItem = nextItem = GameObject.Find("All Canvases/Canvas/StorageMenu/Inventory/InventoryList/ScrollRect/Content/" + myName);
                        }
                    }
                }
                break;
			//dropping magnum ammo
		case InventoryItem.AmmoType.magnumAmmo:
			clone = Instantiate (Resources.Load ("DroppedItems/Handgun Ammo", typeof(GameObject))) as GameObject;
			DataStorage.magnumAmmo  -= ammoAmount;
			clone.GetComponent<ItemPickups> ().ammoCount = ammoAmount;
			myItem.GetComponent<InventoryItem> ().itemWeight -= (float)ammoAmount *.2f;
			myItem.GetComponent<InventoryItem>().myWeight.GetComponent<Text>().text = "lbs " + myItem.GetComponent<InventoryItem> ().itemWeight.ToString();
			myItem.GetComponent<InventoryItem>().myAmount.GetComponent<Text> ().text = "Bullets" + "(" + DataStorage.magnumAmmo.ToString () + ")";
                //	myItem.GetComponent<InventoryItem>().myWeight.GetComponent<Text>().text = "lbs " + ((float)DataStorage.SGAmmo * .25f).ToString();
            droppedText.GetComponent<Text>().text = "Dropped " + ammoAmount + " " + "magnum rounds";
                if (DataStorage.magnumAmmo <= 0)
                {
                    int value = int.Parse(myItem.name);
                    Destroy(myItem);
                    //checking to see if an item is already selected in the event system, if not, we assign to it now
                    if (myEvent.GetComponent<UnityEngine.EventSystems.EventSystem>().currentSelectedGameObject == null)
                    {
                        value -= 1;
                        string myName = value.ToString();
                        GameObject nextItem = GameObject.Find("All Canvases/Canvas/StorageMenu/Inventory/InventoryList/ScrollRect/Content/" + myName);
                        myEvent.GetComponent<UnityEngine.EventSystems.EventSystem>().SetSelectedGameObject(nextItem);
                        //checking to see if an item is still not selected
                        if (myEvent.GetComponent<UnityEngine.EventSystems.EventSystem>().currentSelectedGameObject == null)
                        {
                            value += 2;
                            myName = value.ToString();
                            nextItem = nextItem = GameObject.Find("All Canvases/Canvas/StorageMenu/Inventory/InventoryList/ScrollRect/Content/" + myName);
                        }
                    }
                }
                break;
			//dropping explosive ammo
		case InventoryItem.AmmoType.explosiveAmmo:
			clone = Instantiate (Resources.Load ("DroppedItems/Handgun Ammo", typeof(GameObject))) as GameObject;
			DataStorage.explosiveAmmo  -= ammoAmount;
			clone.GetComponent<ItemPickups> ().ammoCount = ammoAmount;
			myItem.GetComponent<InventoryItem> ().itemWeight -= (float)ammoAmount *.4f;
			myItem.GetComponent<InventoryItem>().myWeight.GetComponent<Text>().text = "lbs " + myItem.GetComponent<InventoryItem> ().itemWeight.ToString();
			myItem.GetComponent<InventoryItem>().myAmount.GetComponent<Text> ().text = "Bullets" + "(" + DataStorage.explosiveAmmo.ToString () + ")";
            droppedText.GetComponent<Text>().text = "Dropped " + ammoAmount + " " + "explosive rounds";
                if (DataStorage.explosiveAmmo <= 0)
                {
                    int value = int.Parse(myItem.name);
                    Destroy(myItem);
                    //checking to see if an item is already selected in the event system, if not, we assign to it now
                    if (myEvent.GetComponent<UnityEngine.EventSystems.EventSystem>().currentSelectedGameObject == null)
                    {
                        value -= 1;
                        string myName = value.ToString();
                        GameObject nextItem = GameObject.Find("All Canvases/Canvas/StorageMenu/Inventory/InventoryList/ScrollRect/Content/" + myName);
                        myEvent.GetComponent<UnityEngine.EventSystems.EventSystem>().SetSelectedGameObject(nextItem);
                        //checking to see if an item is still not selected
                        if (myEvent.GetComponent<UnityEngine.EventSystems.EventSystem>().currentSelectedGameObject == null)
                        {
                            value += 2;
                            myName = value.ToString();
                            nextItem = nextItem = GameObject.Find("All Canvases/Canvas/StorageMenu/Inventory/InventoryList/ScrollRect/Content/" + myName);
                        }
                    }
                }
                break;
		}

		clone.GetComponent<ItemPickups> ().thisWeight = myItem.GetComponent<InventoryItem> ().itemWeight;
		clone.transform.position = DataStorage.player.transform.position;
		//decrease inventory variable


		//exiting the decision
		eventDecide.SetActive (false);
		eventStorage.SetActive(true);
		gameObject.GetComponent<ScrollRect>().enabled = true;
		decision.SetActive (false);

		totalWeight.text = DataStorage.curWeight + "/" + DataStorage.maxWeight.ToString();

	}

	public void Cancel()
	{
		//canceling the decision
		eventDecide.SetActive (false);
		eventStorage.SetActive(true);
		gameObject.GetComponent<ScrollRect>().enabled = true;
		decision.SetActive (false);
	}

	public void Decide(GameObject item)
	{
		myItem = item;
		nameOfItem.text = myItem.GetComponent<InventoryItem>().itemName.ToString();
//		//starting the decision
		eventStorage.SetActive(false);
		eventDecide.SetActive (true);
		gameObject.GetComponent<ScrollRect>().enabled = false;
		decision.SetActive (true);
	}

    IEnumerator wait(float waitTime)
    {
        yield return new WaitForSeconds(waitTime);
        droppedText.SetActive(false);
    }
}
