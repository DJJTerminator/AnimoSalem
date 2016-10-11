using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Radius : MonoBehaviour
{
	public Light playerLight;

//	public List<Transform> walls = new List<Transform>();
//	public Transform        closestWall;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	

	
	}


	GameObject FindClosestWall() {
		GameObject[] gos;
		gos = GameObject.FindGameObjectsWithTag("Wall");
		GameObject closest = null;
		float distance = Mathf.Infinity;
		Vector3 position = transform.position;
		foreach (GameObject go in gos) {
			Vector3 diff = go.transform.position - position;
			float curDistance = diff.sqrMagnitude;
			if (curDistance < distance) {
				closest = go;
				distance = curDistance;
			//	print (curDistance);
				if (distance < 5 && distance > 0)
					playerLight.spotAngle = distance * 30.0f;
			}
		}
		return closest;
	}



	void OnTriggerStay(Collider other)
	{
		FindClosestWall();
	//	print ("touching");
	}
	void OnTriggerEnter(Collider other)
	{
		FindClosestWall();
		//	print ("touching");
	}

	void OnTriggerExit(Collider other)
	{
		//closestWall = GetClosestWall (Radius.List<Transform> walls, this.transform);
		playerLight.spotAngle = 150;
	}
}
