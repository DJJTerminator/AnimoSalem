using UnityEngine;
using System.Collections;
using UnityEngine.UI; 

public class Pause : MonoBehaviour {

	public float FMV = 5;
	public float soundVolume = .65f;
	public float musicVolume = .65f;
	public bool canUnPause = true;
	public GameObject DialogueBox;
	[HideInInspector]
	//*bools
	public bool pause = false;
	bool options = false;
	bool videoS = false;
	bool audioS = false;
	bool controls = false;


	//image buttons
//	public Image[] selection;
//	public Image[] optionSection;
//	public Image[] controlSection;
//	public Image[] audioSection;
//	public Image[] videoSection;
//	public Image AnimationSelection;

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

		if (PlayerPrefs.HasKey ("Sound Volume")) {
			AudioListener.volume = PlayerPrefs.GetFloat ("SoundVolume", soundVolume);
		} else {
			PlayerPrefs.SetFloat ("Sound Volume", soundVolume);
			PlayerPrefs.SetFloat ("Music Volume", musicVolume);
		}

//		Color c = selection [0].color;
//		c.a = 1.0f;
//		selection [0].color = c;

	}

	void Update()
	{
		if (Input.GetKeyDown (KeyCode.Escape) && canUnPause == true) 
		{
			if (!pause) 
			{
				pause = true;
				for (int i = 0; i < everythingElse.Length; i++)
				{
					everythingElse [i].SetActive (false);
				}
				for (int j = 0; j < basicMenus.Length; j++) 
				{
					basicMenus [j].SetActive (true);
				}
			} 
			else 
			{
				pause = false;
				for (int i = 0; i < everythingElse.Length; i++) 
				{
					
					everythingElse [i].SetActive (true);
				}
				for (int j = 0; j < basicMenus.Length; j++) {
					basicMenus [j].SetActive (false);
				}
			}
				
		}
	}
	//resuming the game from basic menus
	public void Resume()
	{
		for (int i = 0; i < everythingElse.Length; i++)
		{
			everythingElse [i].SetActive (true);
		}
		for (int j = 0; j < basicMenus.Length; j++) 
		{
			basicMenus [j].SetActive (true);
		}
	}

	//entering advance options
	public void Options()
	{
		for (int i = 0; i < basicMenus.Length; i++) 
		{
			basicMenus [i].SetActive (false);
		}
		for (int j = 0; j < advanceMenus.Length; j++) 
		{
			advanceMenus [j].SetActive (true);
		}
		canUnPause = false;
	}

	//entering vdieos
	public void Videos()
	{
		for (int i = 0; i < advanceMenus.Length; i++) 
		{
			advanceMenus [i].SetActive (false);
		}
		for (int j = 0; j < advanceMenus.Length; j++) 
		{
			videoMenus [j].SetActive (true);
		}
	}
	//entering audios
	public void Audio()
	{
		for (int i = 0; i < advanceMenus.Length; i++) 
		{
			advanceMenus [i].SetActive (false);
		}
		for (int j = 0; j < advanceMenus.Length; j++) 
		{
			videoMenus [j].SetActive (true);
		}
	}
	//entering controls
	public void Controls()
	{
		for (int i = 0; i < advanceMenus.Length; i++) 
		{
			advanceMenus [i].SetActive (false);
		}
		for (int j = 0; j < controlMenus.Length; j++) 
		{
			controlMenus [j].SetActive (true);
		}
	}

	//returning from certain options
	public void Return()
	{
		//returning from advance menus
		if (advanceMenus [0].activeSelf) 
		{
			for (int i = 0; i < advanceMenus.Length; i++) {
				advanceMenus [i].SetActive (false);
			}
			for (int j = 0; j < basicMenus.Length; j++) 
			{
				basicMenus [j].SetActive (true);
			}
			canUnPause = true;
		}

		//returning from controls menus
		else if  (controlMenus [0].activeSelf) 
		{
			for (int i = 0; i < controlMenus.Length; i++) {
				controlMenus[i].SetActive (false);
			}
			for (int j = 0; j < advanceMenus.Length; j++) 
			{
				advanceMenus[j].SetActive (true);
			}
		}

		//returning from audio
		else if (soundMenus[0].activeSelf) 
		{
			for (int i = 0; i < soundMenus.Length; i++) {
				soundMenus[i].SetActive (false);
			}
			for (int j = 0; j < advanceMenus.Length; j++) 
			{
				advanceMenus[j].SetActive (true);
			}
		}

		//returning from video
		else if (videoMenus[0].activeSelf) 
		{
			for (int i = 0; i < videoMenus.Length; i++) {
				videoMenus[i].SetActive (false);
			}
			for (int j = 0; j < advanceMenus.Length; j++) 
			{
				advanceMenus [j].SetActive (true);
			}
		}

	}


}