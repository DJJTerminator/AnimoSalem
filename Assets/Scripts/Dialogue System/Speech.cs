using UnityEngine;
using System.Collections;

public class Speech : MonoBehaviour {

	public GameObject PauseMenus;
	public TextAsset[] theText;
	public TextManager theTextBox;

	public int maxText;
	private int randomText;

	public GameObject exclamation;

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

	}
	
	// Update is called once per frame
	void Update () {
		if (PauseMenus.GetComponent<Pause>().pause == false)
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
	
	}
	void OnTriggerEnter (Collider other)
	{

		if (other.name == "Player")
		{
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
