using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class Pause : MonoBehaviour {

	public float FMV = 5;
	public float soundVolume = .65f;
	public float musicVolume = .65f;

	public bool canUnPause = true;
	public int selectionIndicator;
	
	public GameObject DialogueBox;
	[HideInInspector]
	//*bools
	public bool pause = false;
	public bool options = false;
	public bool videoS = false;
	public bool audioS = false;
	public bool controls = false;


	//image buttons
	public Image[] selection;
	public Image[] optionSection;
	public Image[] controlSection;
	public Image[] audioSection;
	public Image[] videoSection;
	public Image AnimationSelection;

	//basic menus
	public GameObject[] basicMenus;

	
	public GameObject selectorAnimation;

	//advance menus
	public GameObject[] advanceMenus;

	//video settings
	public GameObject[] videoMenus;

	//audio settings
	public GameObject[] soundMenus;

	//control settings
	public GameObject[] controlMenus;

	public GameObject[] everythingElse;

	// Use this for initialization
	void Start () {


        //checking the sounds
        soundVolume = PlayerPrefs.GetFloat ("Sound Volume", soundVolume);
		musicVolume = PlayerPrefs.GetFloat ("Music Volume", musicVolume);

		if (PlayerPrefs.HasKey ("Sound Volume"))
		{
			AudioListener.volume = PlayerPrefs.GetFloat ("SoundVolume", soundVolume);
		}
		else 
		{
			PlayerPrefs.SetFloat ("Sound Volume", soundVolume);
			PlayerPrefs.SetFloat ("Music Volume", musicVolume);
		}

		selectionIndicator = 0;

		Color c = selection[0].color;
		c.a = 1.0f;
		selection[0].color = c;

		for (int i = 1; i < selection.Length; i++) 
		{
			c.a = .3f;
			selection[i].color = c;
		}

		for (int i = 1; i < optionSection.Length; i++) 
		{
			c.a = .3f;
			optionSection[i].color = c;
		}

		for (int i = 1; i < audioSection.Length; i++) 
		{
			c.a = .3f;
			audioSection[i].color = c;
		}

		for (int i = 1; i < videoSection.Length; i++) 
		{
			c.a = .3f;
			videoSection[i].color = c;
		}

		for (int i = 1; i < controlSection.Length; i++) 
		{
			c.a = .3f;
			controlSection[i].color = c;
		}

//		Color c = resumeButton.color;
//		c.a = 1.0f;
//		resumeButton.color = c;
//		c.a = 0.3f;
//		optionButton.color = c;
//		saveButton.color = c;
//		quitButton.color = c;
//		exit1Button.color = c;
//		exit2Button.color = c;
//		exit3Button.color = c;
//		controlButton.color = c;
	}
	
	// Update is called once per frame
	void Update () 
	{

		if (DialogueBox.activeSelf == false)
			if (Input.GetKeyDown (KeyCode.Escape) && canUnPause == true)
		{
			if (pause == false) 
			{
				pause = true;
				Activate();
			} else {
				pause = false;
				Deactivate();
			}
		}
		//end of if "(DialogueBox.activeSelf == false)"

		if (pause == true) {

            //Unlocks the (Mouse)Cursor and makes it visible
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;

            if (Input.GetKeyDown (KeyCode.DownArrow) || Input.GetKeyDown (KeyCode.S)) {
				MoveSelectorDown ();
			}
			if (Input.GetKeyDown (KeyCode.UpArrow) || Input.GetKeyDown (KeyCode.W)) {
				MoveSelectorUp ();
			}
		
			if (Input.GetKeyDown (KeyCode.Return)) {
				Chosen ();
			}
		}

	}
	//end of update

	//turn pause on
	
		public void Activate()
		{
			Color c = selection [selectionIndicator].color;
			c.a = 1.0f;
			selection [selectionIndicator].color = c;
			selectionIndicator = 0;

			//selectorAnimation.SetActive(true);

		for (int i = 0; i < basicMenus.Length; i++) 
		{
			basicMenus[i].SetActive(true);
		}

		for (int i = 0; i < everythingElse.Length;i++)
		{
			everythingElse[i].SetActive(false);
				
		}
		Time.timeScale = 0.0000001f;
		}

	//turn pause off

		public void Deactivate()
		{
			pause = false;
			Color c = selection [selectionIndicator].color;
			c.a = 0.3f;
			selection [selectionIndicator].color = c;
			selectionIndicator = 0;
	

			selectorAnimation.SetActive(false);

		for (int i = 0; i < basicMenus.Length; i++) 
		{
			basicMenus[i].SetActive(false);
		}

		for (int i = 0; i < everythingElse.Length;i++)
		    {
			everythingElse[i].SetActive(true);
			}
		Time.timeScale = 1;
		}

	//move selector up
		public void MoveSelectorUp()
		{
		if (selectionIndicator > 0)
		{
			//basic menu navigation
			if (options == false && audioS == false && videoS == false && controls == false)
			{
			Color c = selection [selectionIndicator].color;
			c.a = 0.3f;
			selection [selectionIndicator].color = c;
			selection[selectionIndicator].transform.localScale = new Vector3(.4f, .4f, 0);

			selectionIndicator--;
			c.a = 1.0f;
			selection [selectionIndicator].color = c;
			selection[selectionIndicator].transform.localScale = new Vector3(.5f, .5f, 0);
			}
			//advance menu navigation
			if (options == true)
			{
				Color c = optionSection [selectionIndicator].color;
				c.a = 0.3f;
				optionSection [selectionIndicator].color = c;
				optionSection[selectionIndicator].transform.localScale = new Vector3(.4f, .4f, 0);
				
				selectionIndicator--;
				c.a = 1.0f;
				optionSection [selectionIndicator].color = c;
				optionSection[selectionIndicator].transform.localScale = new Vector3(.5f, .5f, 0);

			}
			//video settings navigation
			if (videoS == true)
			{
				Color c = videoSection [selectionIndicator].color;
				c.a = 0.3f;
				videoSection [selectionIndicator].color = c;
				videoSection[selectionIndicator].transform.localScale = new Vector3(.4f, .4f, 0);
				
				selectionIndicator--;
				c.a = 1.0f;
				videoSection [selectionIndicator].color = c;
				videoSection[selectionIndicator].transform.localScale = new Vector3(.5f, .5f, 0);
			}
			//video settings navigation
			if (audioS == true)
			{
				Color c = audioSection [selectionIndicator].color;
				c.a = 0.3f;
				audioSection [selectionIndicator].color = c;
				audioSection[selectionIndicator].transform.localScale = new Vector3(.4f, .4f, 0);
				
				selectionIndicator--;
				c.a = 1.0f;
				audioSection [selectionIndicator].color = c;
				audioSection [selectionIndicator].transform.localScale = new Vector3(.5f, .5f, 0);
			}
			if (controls == true)
			{
				Color c = controlSection [selectionIndicator].color;
				c.a = 0.3f;
				controlSection [selectionIndicator].color = c;
				controlSection [selectionIndicator].transform.localScale = new Vector3(.4f, .4f, 0);
				
				selectionIndicator--;
				c.a = 1.0f;
				controlSection [selectionIndicator].color = c;
				controlSection [selectionIndicator].transform.localScale = new Vector3(.5f, .5f, 0);
			}
		}


		}
	//move selector down
		public void MoveSelectorDown()
		{


		if (options == false && audioS == false && videoS == false && controls == false)
		{

			if (selectionIndicator < (selection.Length - 1)) {
				Color c = selection [selectionIndicator].color;
				c.a = 0.3f;
				selection [selectionIndicator].color = c;
				selection [selectionIndicator].transform.localScale = new Vector3(.4f, .4f, 0);
		
				selectionIndicator++;
				c.a = 1.0f;
				selection [selectionIndicator].color = c;
				selection [selectionIndicator].transform.localScale = new Vector3(.5f, .5f, 0);
			}
		}

		//advance menu navigation
		if (options == true) {
			if (selectionIndicator < (optionSection.Length - 1)) {
				Color c = optionSection [selectionIndicator].color;
				c.a = 0.3f;
				optionSection [selectionIndicator].color = c;
				optionSection [selectionIndicator].transform.localScale = new Vector3(.4f, .4f, 0);
			
				selectionIndicator++;
				c.a = 1.0f;
				optionSection [selectionIndicator].color = c;
				optionSection [selectionIndicator].transform.localScale = new Vector3(.5f, .5f, 0);
			}
		
		}
		//video settings navigation
		if (videoS == true) {
			if (selectionIndicator < (videoSection.Length - 1)) {
				Color c = videoSection [selectionIndicator].color;
				c.a = 0.3f;
				videoSection [selectionIndicator].color = c;
				videoSection [selectionIndicator].transform.localScale = new Vector3(.4f, .4f, 0);
			
				selectionIndicator++;
				c.a = 1.0f;
				videoSection [selectionIndicator].color = c;
				videoSection [selectionIndicator].transform.localScale = new Vector3(.5f, .5f, 0);
			}
		}
		//video settings navigation
		if (audioS == true) {
			if (selectionIndicator < (audioSection.Length - 1)) {
				Color c = audioSection [selectionIndicator].color;
				c.a = 0.3f;
				audioSection [selectionIndicator].color = c;
				audioSection [selectionIndicator].transform.localScale = new Vector3(.4f, .4f, 0);
			
				selectionIndicator++;
				c.a = 1.0f;
				audioSection [selectionIndicator].color = c;
				audioSection [selectionIndicator].transform.localScale = new Vector3(.5f, .5f, 0);
			}
		}
		if (controls == true) {
			if (selectionIndicator < (controlSection.Length - 1)) {
				Color c = audioSection [selectionIndicator].color;
				c.a = 0.3f;
				controlSection [selectionIndicator].color = c;
				controlSection [selectionIndicator].transform.localScale = new Vector3(.4f, .4f, 0);
				
				selectionIndicator++;
				c.a = 1.0f;
				controlSection [selectionIndicator].color = c;
				controlSection [selectionIndicator].transform.localScale = new Vector3(.5f, .5f, 0);
			}
		}
	}

	//Chosen With Selector
	public void Chosen()
	{
		Color c = selection [selectionIndicator].color;
		c.a = 0.3f;

		//resuming the game
		if (selectionIndicator == 0 && options == false && audioS == false && videoS == false && controls == false)
		{
			Deactivate ();
			return;
		}

		//selecting options
		if (selectionIndicator == 1 && options == false && audioS == false && videoS == false && controls == false)
		{
			c.a = 0.3f;
			selection [selectionIndicator].color = c;
			selection[selectionIndicator].transform.localScale = new Vector3(.4f, .4f, 0);

			canUnPause = false;
			options = true;
			selectionIndicator = 0;

			c.a = 1.0f;
			selection [selectionIndicator].color = c;
			selection[selectionIndicator].transform.localScale = new Vector3(.5f, .5f, 0);


		if (selectionIndicator < (selection.Length - 1)) 
		{
				//turning off basic menus so that advance options can be displayed
			for (int i = 0; i < basicMenus.Length; i++) 
			{
				basicMenus[i].SetActive(false);
			}

				//turning on advance options
		for (int i = 0; i < advanceMenus.Length; i++) 
			{
			advanceMenus[i].SetActive(true);
			}

			selectionIndicator = 0;
			c.a = 1.0f;
			optionSection [selectionIndicator].color = c;
			optionSection[selectionIndicator].transform.localScale = new Vector3(.5f, .5f, 0);
		}
			return;
	}

				//Saving the game
		if (selectionIndicator == 2 && options == false && audioS == false && videoS == false && controls == false)
		{
			selectionIndicator = 0;
	
		}

				//Quiting the game
		if (selectionIndicator == 3 && options == false && audioS == false && videoS == false && controls == false)
		{
			selectionIndicator = 0;
			
		}
				//Controls
		if (selectionIndicator == 0 && options == true)
		{
			selectionIndicator = 0;
			options = false;
			controls = true;
			
			c.a = 0.3f;
			optionSection[selectionIndicator].color = c;
			optionSection[selectionIndicator].transform.localScale = new Vector3(.4f, .4f, 0);
			
			
			//turning off advance options
			for (int i = 0; i < advanceMenus.Length; i++) 
			{
				advanceMenus[i].SetActive(false);
			}
			
			//turning on video options
			for (int i = 0; i < controlMenus.Length; i++) 
			{
				controlMenus[i].SetActive(true);
			}
			return;
		}

				//video Menus
		if (selectionIndicator == 1 && options == true)
		{
			c.a = 0.3f;
			optionSection[selectionIndicator].color = c;
			optionSection[selectionIndicator].transform.localScale = new Vector3(.4f, .4f, 0);

				selectionIndicator = 0;
				options = false;
				videoS = true;


				
				
				//turning off advance options
				for (int i = 0; i < advanceMenus.Length; i++) 
				{
					advanceMenus[i].SetActive(false);
				}
				
				//turning on video options
				for (int i = 0; i < videoMenus.Length; i++) 
				{
					videoMenus[i].SetActive(true);
				}
			return;
		}
						//audio Settings
		if (selectionIndicator == 2 && options == true)
		{

			c.a = 0.3f;
			optionSection[selectionIndicator].color = c;
			optionSection[selectionIndicator].transform.localScale = new Vector3(.4f, .4f, 0);

			selectionIndicator = 0;
			options = false;
			audioS = true;
			
			
			//turning off advance options
			for (int i = 0; i < advanceMenus.Length; i++) 
			{
				advanceMenus[i].SetActive(false);
			}
			
			//turning on video options
			for (int i = 0; i < soundMenus.Length; i++) 
			{
				soundMenus[i].SetActive(true);
			}
			return;
		}
						//return to basic menu from advance menu
		if (selectionIndicator == 3 && options == true && audioS == false && videoS == false && controls == false)
		{
			c.a = 0.3f;
			optionSection[selectionIndicator].color = c;
			optionSection[selectionIndicator].transform.localScale = new Vector3(.4f, .4f, 0);

			selectionIndicator = 0;
			options = false;
			canUnPause = true;


				//turning off advance options
			for (int i = 0; i < advanceMenus.Length; i++) 
			{
				advanceMenus[i].SetActive(false);
			}
			
				//returning to basic menus
			for (int i = 0; i < basicMenus.Length; i++) 
			{
				basicMenus[i].SetActive(true);
			}
			return;
		}
		//return to advance menu from controls menu
		if (selectionIndicator == 1 && controls == true)
		{
			c.a = 0.3f;
			controlSection[selectionIndicator].color = c;
			controlSection[selectionIndicator].transform.localScale = new Vector3(.4f, .4f, 0);
			
			selectionIndicator = 0;
			options = true;
			controls = false;
	
			c.a = 1.0f;
			controlSection[selectionIndicator].color = c;
			controlSection[selectionIndicator].transform.localScale = new Vector3(.5f, .5f, 0);

			c.a = 1.0f;
			optionSection[selectionIndicator].color = c;
			optionSection[selectionIndicator].transform.localScale = new Vector3(.5f, .5f, 0);
			
			//turning off advance options
			for (int i = 0; i < controlMenus.Length; i++) 
			{
				controlMenus[i].SetActive(false);
			}
			
			//returning to basic menus
			for (int i = 0; i < advanceMenus.Length; i++) 
			{
				advanceMenus[i].SetActive(true);
			}
			return;
		}
		//return to advance menu from video menu
		if (selectionIndicator == 3 && videoS == true)
		{
			c.a = 0.3f;
			videoSection[selectionIndicator].color = c;
			videoSection[selectionIndicator].transform.localScale = new Vector3(.4f, .4f, 0);
			
			selectionIndicator = 0;
			options = true;
			videoS = false;

			c.a = 1.0f;
			videoSection[selectionIndicator].color = c;
			videoSection[selectionIndicator].transform.localScale = new Vector3(.5f, .5f, 0);

			c.a = 1.0f;
			optionSection[selectionIndicator].color = c;
			optionSection[selectionIndicator].transform.localScale = new Vector3(.5f, .5f, 0);
			
			//turning off advance options
			for (int i = 0; i < videoMenus.Length; i++) 
			{
				videoMenus[i].SetActive(false);
			}
			
			//returning to basic menus
			for (int i = 0; i < advanceMenus.Length; i++) 
			{
				advanceMenus[i].SetActive(true);
			}
			return;
		}
		//return to advance menu from audio menu
		if (selectionIndicator == 2 && audioS == true)
		{
			c.a = 0.3f;
			audioSection[selectionIndicator].color = c;
			audioSection[selectionIndicator].transform.localScale = new Vector3(.4f, .4f, 0);
			
			selectionIndicator = 0;
			options = true;
			audioS = false;

			c.a = 1.0f;
			audioSection[selectionIndicator].color = c;
			audioSection[selectionIndicator].transform.localScale = new Vector3(.5f, .5f, 0);

			c.a = 1.0f;
			optionSection[selectionIndicator].color = c;
			optionSection[selectionIndicator].transform.localScale = new Vector3(.5f, .5f, 0);
			
			//turning off advance options
			for (int i = 0; i < soundMenus.Length; i++) 
			{
				soundMenus[i].SetActive(false);
			}
			
			//returning to basic menus
			for (int i = 0; i < advanceMenus.Length; i++) 
			{
				advanceMenus[i].SetActive(true);
			}
		return;
		}
	}
}

