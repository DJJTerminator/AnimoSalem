using UnityEngine;
using System.Collections;

public class Speech : MonoBehaviour {

	GameObject pauseMenus;
	public TextAsset[] theText; //the text document
	public TextManager theTextBox; //the textmanager script

	public int maxText;
	private int randomText;

	GameObject exclamation;
    GameObject dialogueBox;

	public int startAtLine;
	public int endAtLine;

	public bool destroyWhenActivated;
	private bool active;

	public string[] portraitOrder; //order at which the portraits (for TextManager) are displayed
	public GameObject[] portrait; 

	// Use this for initialization
	void Start () 
	{
		theTextBox = FindObjectOfType <TextManager>();
        exclamation = GameObject.Find("Player/PlayerIcons/Exclamation");
        pauseMenus = GameObject.Find("All Canvases/Canvas/PauseMenus");
        dialogueBox = GameObject.Find("All Canvases/Canvas/TextManager/DialogueBox");
        //turning script off from the start
        GetComponent<Speech>().enabled = false;
       

	}
	
	// Update is called once per frame
	void Update () {
		if (pauseMenus.GetComponent<PauseMenu2>().pause == false)
		if (active)
		if  (Input.GetKeyDown(KeyCode.Return))
		{
			randomText = Random.Range (0, maxText);

			Portrait ();

			active = false;
			exclamation.SetActive (false);
			theTextBox.ReloadScript(theText[randomText]);
			theTextBox.theText.text = theTextBox.textLines[0]; 
			theTextBox.curLine = startAtLine;
			theTextBox.endLine = endAtLine;
			theTextBox.EnableText();
			if (destroyWhenActivated)
				Destroy (gameObject);
		}
        if (!dialogueBox.activeSelf)
        {
            exclamation.SetActive(true);
            active = true;
        }

	}
	void OnTriggerEnter (Collider other)
	{

		if (other.name == "Player")
		{
            GetComponent<Speech>().enabled = true;
			exclamation.SetActive (true);
			active = true;
		}
	}

	void OnTriggerExit (Collider other)
	{
		if (other.name == "Player") 
		{
			exclamation.SetActive (false);
			active = false;
            GetComponent<Speech>().enabled = false;
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
