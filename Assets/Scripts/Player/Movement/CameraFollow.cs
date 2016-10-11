using UnityEngine;
using System.Collections;

public class CameraFollow : MonoBehaviour {

	public Transform target;
	public float smoothness = 0.6f;
	public float maxZoom = 10.0f;
	public float minZoom = 8.0f;
	bool canZoom = true;

	private Vector3 dir= Vector3.zero;

	void Update () {
		//getting the target position
		Vector3 targetPos = target.position;
		//getting the x and z pos of target and the y position of the camera itself
		targetPos.y = transform.position.y;
		//moving to the goal, the target position x and z
		transform.position = Vector3.SmoothDamp (transform.position, targetPos, ref dir, smoothness);

		if (Input.GetKey (KeyCode.LeftControl) && canZoom == true) 
		{
			//zoom in
			if (GetComponent<Camera> ().orthographicSize > minZoom)
				GetComponent<Camera> ().orthographicSize -= Time.deltaTime * 2.0f;
			else
				canZoom = false;
		}
		if (Input.GetKey (KeyCode.LeftControl) && canZoom == false) 
		{
			//zoom out
			if (GetComponent<Camera> ().orthographicSize < maxZoom)
				GetComponent<Camera> ().orthographicSize += Time.deltaTime * 2.0f;
				else
				canZoom = true;
		}
			
	}
}
