using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class TextManager : MonoBehaviour {

	public Text theText;

	[HideInInspector]
	public string[] currentPortrait; //the portrait that is currently being displayed
	[HideInInspector]
	public GameObject[] portrait;


	public TextAsset textFile;
	public string[] textLines; 

	public int curLine;
	public int endLine;

	public bool wait = true;
	public float waitTime;
	private float textSpeed = .06f;
	
	private int resumeCourotine; //used to resume the forloop of the courotine


	// Use this for initialization

	void Start () {

	}
	
	// Update is called once per frame
	void Update () 
	{


	//pressing enter after the text dialogue is completed but before the last peice of dialogue
			if (Input.GetKeyDown (KeyCode.Return) && curLine < endLine && wait == false && Time.time > waitTime)
		{
			curLine += 1;
			resumeCourotine = 0;
			textSpeed = .08f;
			theText.text = textLines[curLine];
			wait = true;
			StartCoroutine(AnimateText());
		}

		//pressing enter on the very last peice of dialogue will close the dialogue box
		if  (Input.GetKeyDown (KeyCode.Return) && curLine >= endLine && wait == false && Time.time > waitTime)
		{
			resumeCourotine = 0;
			textSpeed = .08f;
			curLine = 0;
			wait = true;
			curLine += 1;
			DisableText();

		}

		//pressing enter once the dialogue has commenced will result in a faster display of text
		if (Input.GetKeyDown (KeyCode.Return) && curLine <= endLine && resumeCourotine > 1 && wait == true)
		{
			SkipToNextText();
			StartCoroutine(AnimateText());
		}

	}

	//end of update


	//skipping the dialogue will stop the courotine, increase the speed of text, and then resume the courotine's forloop where it last left off
	public void SkipToNextText()
	{
		StopAllCoroutines ();
		textSpeed = .008f;

		//disable text box once the array reaches its end
		if (curLine > endLine) 
		{
			DataStorage.player.GetComponent<Controls> ().enabled = true;
			curLine = 0;
			DisableText();
		}
		AnimateText();

	}

	//this courotine will animate each text from the dialogue one word at a time. As a result, this gives a type writer effect.
	IEnumerator AnimateText()
	{
		wait = true;
		CharacterExpressions ();

		for (int i = resumeCourotine; i < (textLines[curLine].Length+1); i++)
		{
			resumeCourotine = i;
			theText.text = textLines[curLine].Substring(0, i);
			yield return new WaitForSeconds(textSpeed);
			//play typing soud if desired
		}
		waitTime = Time.time + 0.5f;
		textSpeed = .08f;
		wait = false;

	}

	//enabling or turning the dialogue box on
	public void EnableText()
	{
		curLine = 0;
		CharacterExpressions();
		//curLine = 0;
		resumeCourotine = 0;
		textSpeed = .08f;
		//			theText.text = textLines[curLine];
		wait = true;
		StartCoroutine(AnimateText());
		DataStorage.player.GetComponent<Controls> ().enabled = false;
		DataStorage.textBox.SetActive (true);
	}
	//disabling or turning the dialogue box off
	public void DisableText()
	{
		curLine = 0;
		wait = false;
		DataStorage.player.GetComponent<Controls> ().enabled = true;
		DataStorage.textBox.SetActive (false);
	}
	//this little function will change the text file completely. 
	//that way, every NPC will have a different conversations based upon which text document is loaded
	public void ReloadScript(TextAsset theText)
	{
		if (theText)
		{
			textLines = new string[1]; 
			textLines = (theText.text.Split('\n'));

		}
	}

	//this function disables all portraits other than the one that is intended for chatting

	public void CharacterExpressions()
	{
		int scan = curLine;

		if (currentPortrait[curLine] == "MainChar")
		{
			if (scan > 0)
			{
			scan -= 1;
			portrait[scan].gameObject.SetActive(false);
			}
			portrait[curLine].gameObject.SetActive(true);
		}
		if (currentPortrait[curLine] == "GhostChar")
			if (scan > 0)
			{
				scan -=1;
				portrait[scan].gameObject.SetActive(false);
			}
			portrait[curLine].gameObject.SetActive(true);
		//if "resume courotine" is greater than one, then animate the current portrait game object
		return;
	}

}
