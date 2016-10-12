using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class PlayerShadow : MonoBehaviour {
	
	public Transform target;
	public Transform target2;
	public SpriteRenderer myShadow;
	public SpriteRenderer myShadow2;
	public Color color;
	public float myBrightness;

	public Transform shadowPivot1;
	public Transform shadowPivot2; 


	GameObject[] gos;
	float[] distance;

	float closestDistance = 999, secondClosest = 999;


	Vector3 diff;
	Vector3 position;
	float curDistance;

	Vector3 diff2;
	float curDistance2;

	void Start () 
	{
	//	SpriteRenderer myShadow = GetComponent<SpriteRenderer>();
//		SpriteRenderer myShadow2 = GetComponent<SpriteRenderer>();

		gos = GameObject.FindGameObjectsWithTag("Light");
		distance = new float[gos.Length]; // init the distances
	}

	void FindClosestLight () {
		
		position = transform.position;
		closestDistance = 999;
		secondClosest = 999;

		for (int i = 0; i < gos.Length; i++) {
			GameObject closestGO = null;
			GameObject secondClosestGO = null;

			distance [i] = FindDistance (gos [i], this.gameObject); // however you decide to get distance, origin is probably this gameobject?

			if (distance [i] <= closestDistance) 
			{
				closestDistance = distance [i]; 

				diff = gos [i].transform.position - position; //returning the one with the closest transform
				curDistance = diff.sqrMagnitude;
				closestGO = gos [i];
				distance [i] = curDistance;
				target = closestGO.transform; //assigning the transform of gos to target

				if (curDistance > 0f && curDistance < 50) 
				{ //checking distance of closest light
					shadowPivot1.transform.localScale = new Vector3 ((100 - curDistance) * .02f, (100 - curDistance) * .02f, 0); //increasing the shadow size based on distance
					myShadow.color = new Color (0f, 0f, 0f, (curDistance) * .01f + myBrightness); //tuning down the alpha based on distance
				
					shadowPivot1.transform.LookAt (target); //move shadow away opposite from light
					shadowPivot1.transform.Rotate (+90, 0, 0); //rotation of shadow
				}
				else
					myShadow2.color = new Color(0f,0f,0f,0f); //destroying second shadow
			} //end of checking distance to closest
			else if (distance [i] <= secondClosest) 
			{
				secondClosest = distance [i]; 

		
				diff2 = gos [i].transform.position - position; //returning the one with the closest transform
				curDistance2 = diff2.sqrMagnitude;
				secondClosestGO = gos [i];
				distance [i] = curDistance2;
				target2 = secondClosestGO.transform; //assigning the transform of gos to target
				
				if (curDistance2 > 0f && curDistance2 < 40) //check the angle of target and make sure its at a certain degrees!
				{ //checking distance of second closest light
					shadowPivot2.transform.localScale = new Vector3 ((100 - curDistance2) * .02f, (100 - curDistance2) * .02f, 0); //increasing the shadow size based on distance
					myShadow2.color = new Color (0f, 0f, 0f, (curDistance2) * .01f + myBrightness); //tuning down the alpha based on distance

					shadowPivot2.transform.LookAt (target2); ////move 2nd shadow away opposite from light
					shadowPivot2.transform.Rotate (+90, 0, 0); //rotation of shadow
				}
				else
					myShadow2.color = new Color(0f,0f,0f,0f); //destroying second shadow
			} //end of checking ditance of second closest
		}//end of forloop
	
	}

	float FindDistance(GameObject go, GameObject origin)
	{
		return Vector3.Distance(go.transform.position, origin.transform.position);
	}

	void ReturnShadow()
	{
//		if (shadowPivot1.eulerAngles.y != 0) 
	
			if (shadowPivot1.transform.rotation != Quaternion.identity)
				shadowPivot1.transform.rotation = Quaternion.Lerp (target.transform.rotation, Quaternion.identity, Time.time * .1f);

	}

	void OnTriggerStay(Collider other)
	{
		if (other.tag == "Light") 
		{
			myBrightness = other.GetComponent<Brightness>().brightness;  //returns the value of brightness from the other gameobject (light)
			FindClosestLight ();
		
		}

	}

	void OnTriggerExit(Collider other)
	{
		if (other.tag == "Light") 
		{
			//ReturnShadow ();
	
		}
	}
}