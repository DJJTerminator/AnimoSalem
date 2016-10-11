using UnityEngine;
using System.Collections;
//using UnityEngine.UI;

public class ScreenFade : MonoBehaviour {
public Animator anim;



	// Use this for initialization
	void Start () {
//		texture.color = new Color (0f, 0f, 0f, 0); //tuning down the alpha based on distance
		//FadeIn ();
	}
	
	// Update is called once per frame
	void Update () 
	{
	
	}

	public void FadeOut()
	{
//		float wait = 0f;
//		while (wait < 1) 
//		{
//			texture.color += new Color (0f, 0f, 0f, wait); //tuning down the alpha based on distance
//			wait += .000001f * Time.deltaTime;
//		}
	//	anim.SetTrigger("Dead");
		//anim.SetTrigger("Flashing");
	}

	public void FadeIn()
	{
//		float wait = 1.0f;
//		while (wait > 0) 
//		{
//			texture.color -= new Color (0f, 0f, 0f, wait); //tuning down the alpha based on distance
//			wait -= .000001f * Time.deltaTime;
//	}
//		FadeOut ();
		anim.SetTrigger("FadeOut");
	}
}
