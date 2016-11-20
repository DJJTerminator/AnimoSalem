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


	void Start()
	{
	}

	public void Use()
	{
		//destroy inventory gameobject
		//decrease current weight
		//decrease inventory variable
		//heal player or do whatever
	}

	//dropping the items
	public void Drop()
	{
		discardSound.Play ();

		DataStorage.curWeight -= myItem.GetComponent<InventoryItem>().itemWeight;
		//create game object by player
		GameObject clone;
		switch (myItem.GetComponent<InventoryItem>().ammo)
		{

		default:
			//set created game object to the same value and weight that the player dropped
			//dropping handgun ammo
			clone = Instantiate (Resources.Load ("DroppedItems/Handgun Ammo", typeof(GameObject))) as GameObject;
			clone.GetComponent<ItemPickups> ().ammoCount = DataStorage.HGAmmo;
			DataStorage.HGAmmo = 0;
		//	print (DataStorage.HGAmmo);
			break;
			//dropping shotgnu ammo
		case InventoryItem.AmmoType.shotgunAmmo:
			clone = Instantiate (Resources.Load ("DroppedItems/Handgun Ammo", typeof(GameObject))) as GameObject;
			clone.GetComponent<ItemPickups> ().ammoCount = DataStorage.HGAmmo;
			DataStorage.SGAmmo = 0;
			//print (DataStorage.SGAmmo);
			break;
			//dropping machinegun ammo
		case InventoryItem.AmmoType.machinegunAmmo:
			clone = Instantiate (Resources.Load ("DroppedItems/Handgun Ammo", typeof(GameObject))) as GameObject;
			clone.GetComponent<ItemPickups> ().ammoCount = DataStorage.MGAmmo;
			DataStorage.MGAmmo = 0;
		break;
			//dropping machinegun ammo
		case InventoryItem.AmmoType.rifleAmmo:
			clone = Instantiate (Resources.Load ("DroppedItems/Handgun Ammo", typeof(GameObject))) as GameObject;
			clone.GetComponent<ItemPickups> ().ammoCount = DataStorage.rifleAmmo;
			DataStorage.rifleAmmo = 0;
			break;
			//dropping magnum ammo
		case InventoryItem.AmmoType.magnumAmmo:
			clone = Instantiate (Resources.Load ("DroppedItems/Handgun Ammo", typeof(GameObject))) as GameObject;
			clone.GetComponent<ItemPickups> ().ammoCount = DataStorage.magnumAmmo;
			DataStorage.magnumAmmo = 0;
			break;
			//dropping explosive ammo
		case InventoryItem.AmmoType.explosiveAmmo:
			clone = Instantiate (Resources.Load ("DroppedItems/Handgun Ammo", typeof(GameObject))) as GameObject;
			clone.GetComponent<ItemPickups> ().ammoCount = DataStorage.explosiveAmmo;
		DataStorage.explosiveAmmo = 0;
			break;
		}

		clone.GetComponent<ItemPickups> ().thisWeight = myItem.GetComponent<InventoryItem> ().itemWeight;
		clone.transform.position = DataStorage.player.transform.position;
		//decrease inventory variable

		Destroy (myItem);

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
}
