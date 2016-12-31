// Finds out whether target is on the left or right side of the screen
using UnityEngine;
using System.Collections;

public class Mirror : MonoBehaviour {
	public Transform target;
	public Camera _camera;
	
	void Start() {
//		_camera = GetComponent<Camera>();
	}
	
	void Update() {
		Vector3 viewPos = GetComponent<Camera>().WorldToViewportPoint(target.position);
//		if (viewPos.x > 0.5F)
//			print("target is on the right side!");
//		else
//			print("target is on the left side!");

		_camera.rect = new Rect(.6f,viewPos.y,0.06f,.6f);
	}
}