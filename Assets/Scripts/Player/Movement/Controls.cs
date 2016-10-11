using UnityEngine;
using System.Collections;

public class Controls : MonoBehaviour {





	//~~~Floats
	[Range(0,10)]
	public float speed = 4f;

	private float wait = 0f;    //The player cannot move until the animation is completely done turning. 
	//In other words, it is the length that a player must wait before he can move.

	// Use this for initialization
	void Start () 
	{

	}

	// Update is called once per frame
	void Update () 
	{
		this.transform.Translate(Input.GetAxis ("Horizontal")*Time.deltaTime*speed,Input.GetAxis("Vertical")*Time.deltaTime*speed,0);


	}
}