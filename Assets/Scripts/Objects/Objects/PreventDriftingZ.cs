using UnityEngine;
using System.Collections;

public class PreventDriftingZ : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}

	// Update is called once per frame
	void Update () {

		Vector3 pos = transform.localPosition;
		pos.z = Mathf.Clamp(transform.localPosition.z, 0, 0);
		transform.localPosition = pos;


	
	}
}
