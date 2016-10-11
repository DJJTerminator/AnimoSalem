using UnityEngine;
using System.Collections;

public class PreventDriftingY : MonoBehaviour {
	
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
		Vector3 pos = transform.position;
		pos.y = 0;
		transform.position = pos;
		
	}
}
