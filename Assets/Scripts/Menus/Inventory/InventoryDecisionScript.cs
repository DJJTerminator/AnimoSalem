using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class InventoryDecisionScript : MonoBehaviour {
	public GameObject decision;

	public GameObject eventDecide;
	public GameObject eventStorage;
	public Text nameOfItem;


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

	public void Drop()
	{
		//destroy inventory gameobject
		//decrease current weight
		//decrease inventory variable
		//create game object by player
		//set created game object to the same value and weight that the player dropped

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
		nameOfItem.text = item.GetComponent<InventoryItem>().itemName.ToString();
//		//starting the decision
		//calling instances from outside of the static function
		eventStorage.SetActive(false);
		eventDecide.SetActive (true);
		gameObject.GetComponent<ScrollRect>().enabled = false;
		decision.SetActive (true);
	}
}
