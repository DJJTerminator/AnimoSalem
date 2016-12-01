﻿using UnityEngine;
using System.Collections;

public class Controls : MonoBehaviour {

	[Range(0,10)]
	public float speed = 4f;
	RaycastHit hit;
	public LayerMask targetLayer;
	public Rigidbody myBody;


	//In other words, it is the length that a player must wait before he can move.

	// Use this for initialization
	void Start () 
	{

	}

	// Update is called once per frame
	void Update () 
	{
		Vector3 up = -myBody.velocity;
		transform.Translate(Input.GetAxis ("Horizontal")*Time.deltaTime*speed,Input.GetAxis("Vertical")*Time.deltaTime*speed,0);


		if (Physics.Raycast (transform.position, up, targetLayer))
		{
			print ("There is something in front of the object!");
		}
			Debug.DrawRay(transform.position, up, Color.white);
        if (Input.GetKey("space"))
        {
            DataStorage.health -= 1;
            if (DataStorage.health <= 0)
                DataStorage.health = DataStorage.maxHealth;
            DataStorage.UpdateHUD();
        }

    }//end of update

}//end of class