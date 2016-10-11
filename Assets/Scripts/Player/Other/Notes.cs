using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class Notes : MonoBehaviour {
	bool textAvailable;
	bool textBeingViewed;
	//public CanvasGroup myText;
	public GameObject myText;
	public AudioSource au_paper;
	// Use this for initialization
	void Start () {
		textAvailable = false;
		textBeingViewed = false;
		//	myText.alpha = 0;
		
		au_paper = gameObject.AddComponent <AudioSource>();
		AudioClip paper;
		// Resources must be in any folder named Resources.  load as type and cast as type because Unity returns Object by default.
		paper = (AudioClip)Resources.Load("Sounds/foley_paper_scrunch", typeof(AudioClip));
		au_paper.clip = paper;
	}
	
	// Update is called once per frame
	void Update () {
		if (textAvailable && Input.GetKeyDown (KeyCode.Space)) {
			if(!textBeingViewed){
				au_paper.Play();
				print(au_paper.clip);
				//			myText.alpha = 1;
				print ("turn me on");
				myText.gameObject.SetActive(true);
				print ("turn me on");
			} else{
				//			myText.alpha = 0;
				myText.gameObject.SetActive(false);
				au_paper.Play();
				print ("turn me off");
			}
			textBeingViewed = !textBeingViewed;
			print (au_paper.isPlaying);
		}
	}
	
	void OnTriggerEnter() {
		textAvailable = true;
	}
	
	void OnTriggerExit() {
		textAvailable = false;
		//		myText.alpha = 0;
		myText.gameObject.SetActive (false);
	}
}

