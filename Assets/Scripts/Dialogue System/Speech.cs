using UnityEngine;
using System.Collections;

public class Speech : MonoBehaviour {

	public TextAsset[] theText; //the text document
	public TextManager theTextBox; //the textmanager script

	public int maxText;
	private int randomText;

	public int startAtLine;
	public int endAtLine;

	public bool destroyWhenActivated;
	bool isActive;

	public string[] portraitOrder; //order at which the portraits (for TextManager) are displayed
	public GameObject[] portrait; 

	// Use this for initialization
	void Start () 
	{
		theTextBox = FindObjectOfType <TextManager>();
        //turning script off from the start
        GetComponent<Speech>().enabled = false;

	//finding all portraits that have the same name as the given string
	//assigning the gameobject (image) a new array equal to that string
	//that way, we can prevent any future null references
       		portrait = new GameObject[portraitOrder.Length];

		for (int i = 0; i < portraitOrder.Length; i++)
		{
			portrait[i] = GameObject.Find ("All Canvases/Canvas/TextManager/DialogueBox/CharacterPortraits/" + portraitOrder[i]);
		}

	}
	
	// Update is called once per frame
	void Update () {
		if (DataStorage.pauseMenus.GetComponent<PauseMenu2>().pause == false && DataStorage.canDo) 
		if (isActive)
		if  (Input.GetKeyDown(KeyCode.Return))
		{
			//activating the textbox and text message
			DataStorage.textManager.GetComponent<TextManager> ().enabled = true;
			DataStorage.gameManager.GetComponent<StatActivation> ().enabled = false;
			DataStorage.gameManager.GetComponent<InventoryActivation> ().enabled = false;
			DataStorage.canDo = false;

			randomText = Random.Range (0, maxText);

			Portrait ();

			isActive = false;
			DataStorage.exclamation.SetActive (false);
			theTextBox.ReloadScript(theText[randomText]);
			theTextBox.theText.text = theTextBox.textLines[0]; 
			theTextBox.curLine = startAtLine;
			theTextBox.endLine = endAtLine;
			theTextBox.EnableText();
		}
		if (!DataStorage.textBox.activeSelf && DataStorage.textManager.GetComponent<TextManager> ().enabled == true)
        {
			DataStorage.textManager.GetComponent<TextManager> ().enabled = false;
			DataStorage.gameManager.GetComponent<StatActivation> ().enabled = true;
			DataStorage.gameManager.GetComponent<InventoryActivation> ().enabled = true;
			if (!destroyWhenActivated)
            DataStorage.exclamation.SetActive(true);
            isActive = true;
			DataStorage.canDo = true;
			if (destroyWhenActivated)
				Destroy (gameObject);
        }

	}
	void OnTriggerEnter (Collider other)
	{
		//checking for any null references
			if (DataStorage.exclamation == null || DataStorage.player == null)
			{
				DataStorage.exclamation = GameObject.Find ("Player/PlayerIcons/Exclamation");
				DataStorage.player = GameObject.Find ("Player");
			}
		if (other.name == "Player")
		{
            GetComponent<Speech>().enabled = true;
			isActive = true;
			DataStorage.exclamation.SetActive (true);
		}
	}

	void OnTriggerExit (Collider other)
	{
		if (other.name == "Player") 
		{
			DataStorage.exclamation.SetActive (false);
			isActive = false;
            GetComponent<Speech>().enabled = false;
			DataStorage.canDo = true;
		}
	}
	public void Portrait()
	{
		theTextBox.currentPortrait = new string[portraitOrder.Length];
		theTextBox.portrait = new GameObject[portraitOrder.Length];

		for (int i = 0; i < portraitOrder.Length; i++)
		{
			theTextBox.currentPortrait[i] = portraitOrder[i];
			theTextBox.portrait[i] = portrait[i];
		}
		return;
	}
}
